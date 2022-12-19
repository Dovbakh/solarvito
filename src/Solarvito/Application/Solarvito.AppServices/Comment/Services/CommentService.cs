using FluentValidation;
using Microsoft.Extensions.Logging;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.AppServices.Category.Repositories;
using Solarvito.AppServices.User.Repositories;
using Solarvito.AppServices.User.Services;
using Solarvito.Contracts;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.Category;
using Solarvito.Contracts.Comment;
using Solarvito.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Category.Services
{
    /// <inheritdoc/>
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ILogger<CommentService> _logger;
        private readonly IValidator<CommentRequestDto> _validator;
        private readonly IValidator<CommentUpdateRequestDto> _validatorUpdate;

        private const int numByPage = 20; // количество комментариев на одной странице

        /// <summary>
        /// Инициализировать экземпляр <see cref="CategoryService"/>
        /// </summary>
        /// <param name="repository">Репозиторий для работы с <see cref="CommentDto"/></param>
        public CommentService(
            ICommentRepository repository, 
            IUserService userService, 
            IUserRepository userRepository,
            IAdvertisementRepository advertisementRepository,
            ILogger<CommentService> logger,
            IValidator<CommentRequestDto> validator,
            IValidator<CommentUpdateRequestDto> validatorUpdate)
        {
            _commentRepository = repository;
            _userService = userService;
            _userRepository = userRepository;
            _advertisementRepository = advertisementRepository;
            _logger = logger;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(CommentRequestDto commentRequestDto, CancellationToken cancellation)
        {
            // Валидация полученных данных
            _logger.LogInformation("Валидация полученных данных из фильтра {FilterType}.", commentRequestDto.ToString());

            var validationResult = _validator.Validate(commentRequestDto);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Полученные данные не прошли валидацию со следующими ошибками: {ErrorMessage}", string.Join("~", validationResult.Errors.ToList()));
                throw new ArgumentException(validationResult.ToString("~"));
            }

            var advertisement = await _advertisementRepository.GetByIdAsync(commentRequestDto.AdvertisementId, cancellation);
            if(advertisement.UserId != commentRequestDto.UserId)
            {
                throw new ArgumentException($"Пользователь с идентификатором '{commentRequestDto.UserId}' не является владельцем обьявления с идентификатором '{commentRequestDto.AdvertisementId}'");
            }
            var user = await _userRepository.GetById(commentRequestDto.UserId, cancellation);       
            
            var currentUser = await _userService.GetCurrent(cancellation);
            if(currentUser.Id == commentRequestDto.UserId)
            {
                throw new InvalidOperationException("Пользователь не может оставлять отзыв себе.");
            }

            commentRequestDto.AuthorId = currentUser.Id;
            


            var commentDto = commentRequestDto.MapToDto();
            commentDto.CreatedAt = DateTime.UtcNow;

            var commentId = await _commentRepository.AddAsync(commentDto, cancellation);


            return commentId;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            var comment = await _commentRepository.GetByIdAsync(id, cancellation);
            var filter = new CommentFilterRequest() { UserId = comment.UserId };
            var user = await _userRepository.GetById(comment.UserId, cancellation);

            await _commentRepository.DeleteAsync(id, cancellation);                     
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CommentDto>> GetAllAsync(int page, CancellationToken cancellation)
        {
            int take = numByPage;
            int skip = take * page - take;

            var comments = await _commentRepository.GetAllAsync(take, skip, cancellation);
            foreach(var comment in comments)
            {
                comment.AuthorName = "Пользователь #" + comment.AuthorId;
            }

            return comments;
        }

        public async Task<IReadOnlyCollection<CommentDto>> GetAllFilteredAsync(CommentFilterRequest filterRequest, int page, CancellationToken cancellation)
        {
            int take = numByPage;
            int skip = take * page - take;

            var comments = await _commentRepository.GetAllFilteredAsync(filterRequest, take, skip, cancellation);
            foreach (var comment in comments)
            {
                comment.AuthorName = "Пользователь #" + comment.AuthorId;
            }

            return comments;
        }

        /// <inheritdoc/>
        public async Task<CommentDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var comment = await _commentRepository.GetByIdAsync(id, cancellation);
            if(comment.AuthorName == null)
            {
                comment.AuthorName = "Пользователь #" + comment.AuthorId;
            }

            return comment;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, CommentUpdateRequestDto сommentUpdateRequestDto, CancellationToken cancellation)
        {
            // Валидация полученных данных
            _logger.LogInformation("Валидация полученных данных из фильтра {FilterType}.", сommentUpdateRequestDto.ToString());

            var validationResult = _validatorUpdate.Validate(сommentUpdateRequestDto);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Полученные данные не прошли валидацию со следующими ошибками: {ErrorMessage}", string.Join("~", validationResult.Errors.ToList()));
                throw new ArgumentException(validationResult.ToString("~"));
            }

            var commentDto = сommentUpdateRequestDto.MapToDto();
            await _commentRepository.UpdateAsync(id, commentDto, cancellation);
        }
    }
}
