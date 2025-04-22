using mamisum_api.DTOs;
using mamisum_api.Models.Users;
using mamisum_api.Services;

namespace mamisum_api.Mappers
{
    public class CustomerProfileMapper
    {
        public static async Task<CustomerProfile> ToCustomerProfileAsync(CreateCustomerProfileDto dto, string userId, ImageService imageService)
        {
            return new CustomerProfile
            {
                Name = dto.Name,
                MobileNo = dto.MobileNo,
                EmailId = dto.EmailId,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                ProfileImage = dto.ImageFile != null ? await imageService.UploadImageAsync(dto.ImageFile, "profile") : string.Empty,
                UserId = userId
            };
        }
    }
}
