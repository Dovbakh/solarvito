using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Solarvito.AppServices.User.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Solarvito.Domain;
using Solarvito.AppServices.User.Additional;
using Microsoft.Extensions.Logging;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.Contracts.User.Interfaces;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Solarvito.AppServices.Notifier.Services;
using Solarvito.Contracts.Notifier;
using System.Security.Policy;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Solarvito.AppServices.User.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IConfiguration _configuration;
        private readonly IValidator<UserRegisterDto> _validatorRegister;
        private readonly IValidator<UserLoginDto> _validatorLogin;
        private readonly IValidator<UserChangePasswordDto> _validatorPassword;

        private readonly IPasswordHasher<UserRegisterDto> _hasherRegister;
        private readonly IPasswordHasher<UserLoginDto> _hasherLogin;
        private readonly IPasswordHasher<UserDto> _hasher;
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<Domain.User> _userManager;
        private readonly RoleManager<Domain.Role> _roleManager;
        private readonly IDistributedCache _distributedCache;

        private readonly INotifierService _notifierService;

        /// <summary>
        /// Инициализировать экземпляр <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с <see cref="UserDto"/>.</param>
        /// <param name="claimsAccessor">Аксессор для работы с клеймами.</param>
        /// <param name="configuration">Конфигурация проекта.</param>
        /// <param name="validator">Валидатор данных для работы с <see cref="UserCredentialsDto"/>.</param>
        /// <param name="hasher">Хэшер пароля для работы с <see cref="UserCredentialsDto"/>.</param>
        public UserService(
            IUserRepository userRepository,
            IClaimsAccessor claimsAccessor,
            IConfiguration configuration,
            IValidator<UserRegisterDto> validatorRegister,
            IValidator<UserLoginDto> validatorLogin,
            IValidator<UserChangePasswordDto> validatorPassword,
            IPasswordHasher<UserRegisterDto> hasherRegister,
            IPasswordHasher<UserLoginDto> hasherLogin,
            ILogger<UserService> logger,
            IPasswordHasher<UserDto> hasher,
            UserManager<Domain.User> userManager,
            RoleManager<Domain.Role> roleManager,
            INotifierService notifierService,
            IDistributedCache distributedCache)
        {
            _userRepository = userRepository;
            _claimsAccessor = claimsAccessor;
            _configuration = configuration;
            _validatorRegister = validatorRegister;
            _validatorLogin = validatorLogin;
            _validatorPassword = validatorPassword;
            _hasherRegister = hasherRegister;
            _hasherLogin = hasherLogin;
            _logger = logger;
            _hasher = hasher;
            _userManager = userManager;
            _roleManager = roleManager;
            _notifierService = notifierService;
            _distributedCache = distributedCache;
        }
        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken)
        {        
            return await _userRepository.GetAll(take, skip, cancellationToken);
        }

        public Task<UserDto> GetById(string id, CancellationToken cancellationToken)
        {
            return _userRepository.GetById(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetCurrent(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Получение данных о текущем пользователе и их обновление.");

            var claims = await _claimsAccessor.GetClaims(cancellationToken);
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }
            var id = claimId;
            var user = await _userRepository.GetById(id, cancellationToken);

            return user;
        }

        /// <inheritdoc/>
        public async Task<string> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
        {
            var validationResult = _validatorRegister.Validate(userRegisterDto);
            if (!validationResult.IsValid) {
                throw new Exception(validationResult.ToString("~"));
            }

            var existingUser = await _userRepository.GetByEmail(userRegisterDto.Email, cancellationToken);
            if (existingUser != null) {

                throw new ArgumentException($"Пользователь с почтой '{userRegisterDto.Email}' уже зарегистрирован!");
            }
          
            
            return await _userRepository.AddAsync(userRegisterDto, cancellationToken);

        }

        /// <inheritdoc/>
        public async Task<string> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken)
        {
            var validationResult = _validatorLogin.Validate(userLoginDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString("~"));
            }
            

            var existingUser = await _userRepository.GetByEmail(userLoginDto.Email, cancellationToken);           
            if (existingUser == null)
            {
                throw new ArgumentException("Введен неверный email или пароль.");
            }

            var isPasswordVerified = await _userRepository.CheckPasswordAsync(userLoginDto.Email, userLoginDto.Password, cancellationToken);
            if (!isPasswordVerified)
            {
                throw new ArgumentException("Введен неверный email или пароль.");
            }

            var token = GenerateToken(existingUser);


            return token;
        }

        /// <inheritdoc/>
        public Task UpdateAsync(string id, UserUpdateRequestDto request, CancellationToken cancellationToken)
        {
            return _userRepository.UpdateAsync(id, request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto, CancellationToken cancellationToken)
        {           
            var validationResult = _validatorPassword.Validate(userChangePasswordDto);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString("~"));
            }

            var currentUser = await GetCurrent(cancellationToken);

            await _userRepository.ChangePasswordAsync(currentUser.Email, userChangePasswordDto.Password, userChangePasswordDto.NewPassword, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task ChangeEmailRequestAsync(string newEmail, string password, string changeLink, CancellationToken cancellationToken)
        {
            // Валидация введенной почты

            // Верификация пароля

            // Генерация токена смены почты
            var currentUser = await GetCurrent(cancellationToken);

            var token = await _userRepository.ChangeEmailRequestAsync(currentUser.Email, newEmail, cancellationToken);

            // Отправление сообщения на почту

            changeLink = changeLink.Replace("tokenValue", token);

            var notifyWarning = new NotifyDto()
            {
                To = currentUser,
                Subject = "Запрос на изменение почты",
                Body = "Поступил запрос на изменение электронной почты на <b>" + newEmail + "</b>. Если это были вы, то ничего делать не нужно. В противном случае напишите в поддержку."
            };
            _notifierService.Send(notifyWarning);

            currentUser.Email = newEmail;
            var notifyConfirm = new NotifyDto()
            {
                To = currentUser,
                Subject = "Запрос на изменение почты",
                Body = "Чтобы изменить почту, нажмите <a href='" + changeLink + "'>сюда</a>"
            };

            _notifierService.Send(notifyConfirm);
        }

        public async Task ChangeEmailAsync(string newEmail, string token, CancellationToken cancellationToken)
        {
            var currentUser = await GetCurrent(cancellationToken);

            await _userRepository.ChangeEmailAsync(currentUser.Email, newEmail, token, cancellationToken);

        }

            public async Task ResetPasswordRequestAsync(string email, string resetLink, CancellationToken cancellationToken)
        {
            // валидация данных (почты)

            var user = await _userRepository.GetByEmail(email, cancellationToken);
            if(user == null)
            {
                throw new KeyNotFoundException("Пользователя с такой почтой не существует.");
            }

            var token = await _userRepository.ResetPasswordRequestAsync(email, cancellationToken);
            resetLink = resetLink.Replace("tokenValue", token);


            var notify = new NotifyDto()
            {
                To = user,
                Subject = "Сброс пароля",
                Body = "Чтобы сбросить пароль, нажмите <a href='" + resetLink + "'>сюда</a>"
            };

            _notifierService.Send(notify);
        }

        public Task ResetPasswordAsync(string email, string newPassword, string token, CancellationToken cancellationToken)
        {
            return _userRepository.ResetPasswordAsync(email, token, newPassword, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var currentUser = await GetCurrent(cancellationToken);
;
            if (currentUser.Id != id && currentUser.RoleName != "admin")
            {
                throw new AccessViolationException("Нет прав.");
            }

            await _userRepository.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// Генератор JWT-токена.
        /// </summary>
        /// <param name="user">Элемент <see cref="UserHashDto"/>.</param>
        /// <returns>Токен.</returns>
        private string GenerateToken(UserDto user)
        {
            _logger.LogInformation("Генерация JWT-токена для пользователя с идентификатором {UserId}", user.Id);

            try
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

                var secretKey = _configuration["AuthToken:SecretKey"];

                var token = new JwtSecurityToken
                    (
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        SecurityAlgorithms.HmacSha256
                        )
                    );

                var result = new JwtSecurityTokenHandler().WriteToken(token);

                return result;
            }
            catch(Exception e)
            {
                _logger.LogError("Ошибка при генерации токена для пользователя с идентификатором {UserId}: {ErrorMessage}", user.Id, e.Message);
                throw;
            }
        }


    }
}
