using mamisum_api.DTOs;
using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class DealService
    {
        private readonly IDealRepository _dealRepository;
        private readonly ImageService _imageService;

        public DealService(IDealRepository dealRepository, ImageService imageService)
        {
            _dealRepository = dealRepository;
            _imageService = imageService;
        }

        public async Task<List<Deal>> GetDealsByUserIdAsync(string userId)
        {
            return await _dealRepository.GetDealsByUserIdAsync(userId);
        }

        public async Task<Deal?> GetDealByIdAsync(string id)
        {
            return await _dealRepository.GetDealByIdAsync(id);
        }

        public async Task CreateDealAsync(CreateDealDto dto, string userId)
        {
            var deal = new Deal
            {
                Title = dto.Title,
                UserId = userId
            };

            if (dto.ImageFile != null)
            {
                deal.ImageUrl = await _imageService.UploadImageAsync(dto.ImageFile, "deals");
            }

            await _dealRepository.CreateDealAsync(deal);
        }

        public async Task<bool> DeleteDealAsync(string id)
        {
            return await _dealRepository.DeleteDealAsync(id);
        }
    }
}
