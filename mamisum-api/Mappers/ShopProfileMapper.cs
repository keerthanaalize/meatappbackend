using mamisum_api.DTOs;
using mamisum_api.Models;
using mamisum_api.Services;

namespace mamisum_api.Mappers
{
    public class ShopProfileMapper
    {
        public static async Task<ShopProfile> ToShopProfileAsync(CreateShopProfileDto dto, ImageService imageService)
        {
            return new ShopProfile
            {
                ShopName = dto.ShopName,
                ShopImage = dto.ShopImage != null ? await imageService.UploadImageAsync(dto.ShopImage, "shop_images") : string.Empty,
                DocumentImage = dto.DocumentImage != null ? await imageService.UploadImageAsync(dto.DocumentImage, "documents") : string.Empty,
                MobileNo = dto.MobileNo,
                EmailId = dto.EmailId,
                ShopAddress = dto.ShopAddress,
                ShopCity = dto.ShopCity,
                OwnerName = dto.OwnerName,
                ShopOpenTime = dto.ShopOpenTime,
                ShopCloseTime = dto.ShopCloseTime,
                ShopDate = dto.ShopDate,
                ShopCategory = dto.ShopCategory,
                ShopState = dto.ShopState,
            };
        }
    }
}
