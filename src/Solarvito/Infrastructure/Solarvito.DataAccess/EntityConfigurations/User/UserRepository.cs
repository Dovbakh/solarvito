using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts;
using Solarvito.Contracts.User;
using Solarvito.Contracts.User.Interfaces;
using Solarvito.Domain;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<Domain.User> _userManager;

        public UserRepository(
            UserManager<Domain.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.CommentsBy)
                .Include(u => u.CommentsFor)
                .Include(u => u.Advertisements)
                .Select(u => u.MapToDto())
                .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken)
        {           
            return await _userManager.Users
                .Where(u => u.Email == email)
                .Include(u => u.Role)
                .Include(u => u.CommentsBy)
                .Include(u => u.CommentsFor)
                .Include(u => u.Advertisements)
                .Select(u => u.MapToDto()).FirstOrDefaultAsync();

        }

        public async Task<UserDto> GetById(string id, CancellationToken cancellationToken)
        {                  
            return await _userManager.Users
                .Where(u => u.Id.Equals(id))
                .Include(u => u.Role)
                .Include(u => u.CommentsBy)
                .Include(u => u.CommentsFor)
                .Include(u => u.Advertisements)
                .Select(u => u.MapToDto()).FirstOrDefaultAsync();
        }

        public async Task<string> AddAsync(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
        {
            var user = userRegisterDto.MapToEntity();

            await _userManager.CreateAsync(user, userRegisterDto.Password);
            return user.Id;
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);

            await _userManager.DeleteAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task ChangePasswordAsync(string email, string currentPassword, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if(!result.Succeeded)
            {
                throw new Exception(string.Join("~", result.Errors));
            }
        }

        public async Task ChangeEmailAsync(string currentEmail, string newEmail, string token, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(currentEmail);

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join("~", result.Errors));
            }

            await _userManager.SetUserNameAsync(user, newEmail);
        }

        public async Task<string> ChangeEmailRequestAsync(string currentEmail, string newEmail, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(currentEmail);

            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> ResetPasswordRequestAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            token = token.Replace(" ", "+");
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join("~", result.Errors));
            }
        }

        public async Task UpdateAsync(string id, UserUpdateRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(!request.Name.IsNullOrEmpty())
            {
                user.Name = request.Name;
            }
            if (!request.Phone.IsNullOrEmpty())
            {
                user.PhoneNumber = request.Phone;
            }
            if (!request.Address.IsNullOrEmpty())
            {
                user.Address = request.Address;
            }

            await _userManager.UpdateAsync(user);
        }

    }
}
