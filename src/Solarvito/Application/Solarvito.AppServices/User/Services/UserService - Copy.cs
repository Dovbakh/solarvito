﻿//using Solarvito.AppServices.User.Repositories;
//using Solarvito.Contracts.User;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.Extensions.Configuration;
//using System.Text;
//using System.Threading.Tasks;
//using FluentValidation;
//using Solarvito.AppServices.User.Validators;
//using FluentValidation.Results;
//using Microsoft.AspNetCore.Identity;
//using Solarvito.Domain;
//using Solarvito.AppServices.User.Additional;
//using Microsoft.Extensions.Logging;
//using Solarvito.AppServices.Advertisement.Repositories;
//using Solarvito.Contracts.User.Interfaces;
//using FluentValidation.Validators;

//namespace Solarvito.AppServices.User.Services
//{
//    /// <inheritdoc />
//    public class UserService : IUserService
//    {
//        private readonly IUserRepository _userRepository;
//        private readonly IClaimsAccessor _claimsAccessor;
//        private readonly IConfiguration _configuration;
//        private readonly IValidator<UserRegisterDto> _validatorRegister;
//        private readonly IValidator<UserLoginDto> _validatorLogin;

//        private readonly IPasswordHasher<UserRegisterDto> _hasherRegister;
//        private readonly IPasswordHasher<UserLoginDto> _hasherLogin;
//        private readonly IPasswordHasher<UserDto> _hasher;
//        private readonly ILogger<UserService> _logger;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;

//        /// <summary>
//        /// Инициализировать экземпляр <see cref="UserService"/>
//        /// </summary>
//        /// <param name="userRepository">Репозиторий для работы с <see cref="UserDto"/>.</param>
//        /// <param name="claimsAccessor">Аксессор для работы с клеймами.</param>
//        /// <param name="configuration">Конфигурация проекта.</param>
//        /// <param name="validator">Валидатор данных для работы с <see cref="UserCredentialsDto"/>.</param>
//        /// <param name="hasher">Хэшер пароля для работы с <see cref="UserCredentialsDto"/>.</param>
//        public UserService(
//            IUserRepository userRepository,
//            IClaimsAccessor claimsAccessor,
//            IConfiguration configuration,
//            IValidator<UserRegisterDto> validatorRegister,
//            IValidator<UserLoginDto> validatorLogin,
//            IPasswordHasher<UserRegisterDto> hasherRegister,
//            IPasswordHasher<UserLoginDto> hasherLogin,
//            ILogger<UserService> logger,
//            IPasswordHasher<UserDto> hasher,
//            UserManager<ApplicationUser> userManager,
//            RoleManager<IdentityRole> roleManager)
//        {
//            _userRepository = userRepository;
//            _claimsAccessor = claimsAccessor;
//            _configuration = configuration;
//            _validatorRegister = validatorRegister;
//            _validatorLogin = validatorLogin;
//            _hasherRegister = hasherRegister;
//            _hasherLogin = hasherLogin;
//            _logger = logger;
//            _hasher = hasher;
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }
//        /// <inheritdoc/>
//        public async Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken)
//        {
//            var result = await _userManager.CreateAsync(new ApplicationUser() { Email = "admin@admin.com", UserName = "admin@admin.com" }, "Password1234$");

//            return await _userRepository.GetAll(take, skip, cancellationToken);
//        }

//        public Task<UserDto> GetById(int id, CancellationToken cancellationToken)
//        {
//            return _userRepository.GetById(id, cancellationToken);
//        }

//        /// <inheritdoc/>
//        public async Task<UserDto> GetCurrent(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Получение данных о текущем пользователе и их обновление.");

//            var claims = await _claimsAccessor.GetClaims(cancellationToken);
//            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

//            if (string.IsNullOrWhiteSpace(claimId))
//            {
//                return null;
//            }

//            var id = int.Parse(claimId);
//            var userDto = await _userRepository.GetById(id, cancellationToken);

//            return userDto;
//        }

//        /// <inheritdoc/>
//        public async Task<int> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
//        {

//            var validationResult = _validatorRegister.Validate(userRegisterDto);
//            if (!validationResult.IsValid) {
//                throw new Exception(validationResult.ToString("~"));
//            }


//            var existingUser = await _userRepository.GetByEmail(userRegisterDto.Email, cancellationToken);
//            if (existingUser != null) {

//                throw new ArgumentException($"Пользователь с почтой '{userRegisterDto.Email}' уже зарегистрирован!");
//            }


//            var passwordHash = _hasherRegister.HashPassword(userRegisterDto, userRegisterDto.Password);

//            var user = new UserDto
//            {
//                Email = userRegisterDto.Email,
//                PasswordHash = passwordHash,
//                CreatedAt = DateTime.UtcNow,
//                RoleId = 1
//            };           
            
//            return await _userRepository.AddAsync(user, cancellationToken);

//        }

//        /// <inheritdoc/>
//        public async Task<string> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken)
//        {
//            var validationResult = _validatorLogin.Validate(userLoginDto);
//            if (!validationResult.IsValid)
//            {
//                throw new Exception(validationResult.ToString("~"));
//            }

//            var existingUser = await _userRepository.GetByEmail(userLoginDto.Email, cancellationToken);           
//            if (existingUser == null)
//            {
//                throw new ArgumentException("Введен неверный email или пароль.");
//            }

//            var isPasswordVerified = _hasherLogin.VerifyHashedPassword(userLoginDto, existingUser.PasswordHash, userLoginDto.Password) != PasswordVerificationResult.Failed;
//            if (!isPasswordVerified)
//            {
//                throw new ArgumentException("Введен неверный email или пароль.");
//            }

//            var token = GenerateToken(existingUser);



//            return token;
//        }

//        /// <inheritdoc/>
//        public Task UpdateAsync(int id, UserUpdateRequestDto request, CancellationToken cancellationToken)
//        {
//            return _userRepository.UpdateAsync(id, request, cancellationToken);
//        }

//        ///// <inheritdoc/>
//        //public async Task ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto, CancellationToken cancellationToken)
//        //{
//        //    var currentUser = await GetCurrent(cancellationToken);

//        //    var isPasswordVerified = _hasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, userChangePasswordDto.Password) != PasswordVerificationResult.Failed;
//        //    if (!isPasswordVerified)
//        //    {
//        //        throw new ArgumentException("Введен неверный пароль.");
//        //    }

//        //    var validationResult = _validatorPassword.Validate(userChangePasswordDto);
//        //    if (!validationResult.IsValid)
//        //    {
//        //        throw new Exception(validationResult.ToString("~"));
//        //    }

//        //    var newPasswordHash = _hasher.HashPassword(currentUser, userChangePasswordDto.NewPassword);
//        //    currentUser.PasswordHash = newPasswordHash;

//        //    await _userRepository.UpdateAsync(currentUser, cancellationToken);
//        //}

//        ///// <inheritdoc/>
//        //public async Task ChangeEmailRequestAsync(UserEmailDto userEmailDto, CancellationToken cancellationToken)
//        //{
//        //    var currentUser = await GetCurrent(cancellationToken);

//        //    if(currentUser.Email == userEmailDto.Email)
//        //    {
//        //        throw new ArgumentException("Электронные адреса не должны совпадать.");
//        //    }

//        //    var validationResult = _validatorEmail.Validate(userEmailDto);
//        //    if (!validationResult.IsValid)
//        //    {
//        //        throw new Exception(validationResult.ToString("~"));
//        //    }


//        //    // TODO generate token and Email notify
//        //}

//        /// <inheritdoc/>
//        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
//        {
//            var currentUser = await GetCurrent(cancellationToken);

//            if (currentUser.Id == id || currentUser.RoleName == "admin")
//            {
//                await _userRepository.DeleteAsync(id, cancellationToken);
//            }
//        }

//        /// <summary>
//        /// Генератор JWT-токена.
//        /// </summary>
//        /// <param name="user">Элемент <see cref="UserHashDto"/>.</param>
//        /// <returns>Токен.</returns>
//        private string GenerateToken(UserDto user)
//        {
//            _logger.LogInformation("Генерация JWT-токена для пользователя с идентификатором {UserId}", user.Id);

//            try
//            {
//                var claims = new List<Claim> {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Role, user.RoleName)
//            };

//                var secretKey = _configuration["AuthToken:SecretKey"];

//                var token = new JwtSecurityToken
//                    (
//                    claims: claims,
//                    expires: DateTime.UtcNow.AddDays(1),
//                    notBefore: DateTime.UtcNow,
//                    signingCredentials: new SigningCredentials(
//                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
//                        SecurityAlgorithms.HmacSha256
//                        )
//                    );

//                var result = new JwtSecurityTokenHandler().WriteToken(token);

//                return result;
//            }
//            catch(Exception e)
//            {
//                _logger.LogError("Ошибка при генерации токена для пользователя с идентификатором {UserId}: {ErrorMessage}", user.Id, e.Message);
//                throw;
//            }
//        }


//    }
//}
