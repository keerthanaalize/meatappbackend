using mamisum_api.DTOs;
using mamisum_api.Mappers;
using mamisum_api.Models;
using mamisum_api.Models.Users;
using mamisum_api.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Services
{
    public class CustomerProfileService
    {
        private readonly ICustomerProfileRepository _repo;
        private readonly ImageService _imageService;

        public CustomerProfileService(ICustomerProfileRepository repo, ImageService imageService)
        {
            _repo = repo;
            _imageService = imageService;
        }

        public async Task<CustomerProfile?> GetProfileAsync(string userId) =>
            await _repo.GetByUserIdAsync(userId);

        public async Task CreateProfileAsync(CreateCustomerProfileDto dto, string userId)
        {
            var profile = await CustomerProfileMapper.ToCustomerProfileAsync(dto, userId, _imageService);
            await _repo.CreateAsync(profile);
        }

        public async Task<bool> UpdateProfileAsync(UpdateCustomerProfileDto dto, string userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            if (profile == null) return false;

            profile.Name = dto.Name;
            profile.MobileNo = dto.MobileNo;
            profile.EmailId = dto.EmailId;
            profile.Address = dto.Address;
            profile.City = dto.City;
            profile.Location = new double[] { dto.Longitude, dto.Latitude };
            profile.State = dto.State;

            if (dto.ImageFile != null)
                profile.ProfileImage = await _imageService.UploadImageAsync(dto.ImageFile, "profile");

            return await _repo.UpdateAsync(profile);
        }
    }
}
