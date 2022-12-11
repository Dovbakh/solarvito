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

namespace Solarvito.AppServices.User.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IConfiguration _configuration;
        private readonly IValidator<UserCredsDto> _validator;
        private readonly IPasswordHasher<UserCredsDto> _hasher;


        public UserService(
            IUserRepository userRepository,
            IClaimsAccessor claimsAccessor,
            IConfiguration configuration,
            IValidator<UserCredsDto> validator,
            IPasswordHasher<UserCredsDto> hasher)
        {
            _userRepository = userRepository;
            _claimsAccessor = claimsAccessor;
            _configuration = configuration;
            _validator = validator;
            _hasher = hasher;
        }

        public async Task<int> Register(UserCredsDto userCreds, CancellationToken cancellationToken)
        {

            var validationResult = _validator.Validate(userCreds);
            if (!validationResult.IsValid) {
                throw new Exception(validationResult.ToString("~"));
            }


            var existingUser = await _userRepository.GetWithHashByEmail(userCreds.Email, cancellationToken);
            if (existingUser != null) {
                throw new Exception($"Пользователь с почтой '{userCreds.Email}' уже зарегистрирован!");
            }


            var passwordHash = _hasher.HashPassword(userCreds, userCreds.Password); 
            
            var user = new UserHashDto { Email = userCreds.Email, PasswordHash = passwordHash };           
            
            return await _userRepository.AddAsync(user, cancellationToken);

        }


        public async Task<string> Login(UserCredsDto userCreds, CancellationToken cancellationToken)
        {

            var existingUser = await _userRepository.GetWithHashByEmail(userCreds.Email, cancellationToken);           
            if (existingUser == null)
            {
                throw new Exception("Введен неверный email или пароль.");
            }

            var isPasswordVerified = _hasher.VerifyHashedPassword(userCreds, existingUser.PasswordHash, userCreds.Password) != PasswordVerificationResult.Failed;
            if (!isPasswordVerified)
            {
                throw new Exception("Введен неверный email или пароль.");
            }

            var token = GenerateToken(existingUser);



            return token;
        }


        public Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll(take, skip, cancellationToken);
        }

        public Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            return _userRepository.GetById(id, cancellationToken);
        }


        public async Task<UserDto> GetCurrent(CancellationToken cancellationToken)
        {
            var claims = await _claimsAccessor.GetClaims(cancellationToken);
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = int.Parse(claimId);
            var userDto = await _userRepository.GetById(id, cancellationToken);

            if (userDto == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'");
            }

            return userDto;
        }

        public Task UpdateAsync(UserUpdateRequestDto request, CancellationToken cancellationToken)
        {
            return _userRepository.UpdateAsync(request, cancellationToken);
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return _userRepository.DeleteAsync(id, cancellationToken);
        }

        private string GenerateToken(UserHashDto user)
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


    }
}
