using mamisum_api.DTOs;
using mamisum_api.Models;
using mamisum_api.Services;

namespace mamisum_api.Mappers
{
    public class ProductMapper
    {
        public static async Task<Product> ToProductAsync(CreateProductDto dto, string userId, ImageService imageService)
        {
            return new Product
            {
                ProductName = dto.ProductName,
                ProductCategoryId = dto.ProductCategoryId,
                ProductCategoryName = dto.ProductCategoryName,
                ProductDescription = dto.ProductDescription,
                ProductQuantity = dto.ProductQuantity,
                ProductPrice = dto.ProductPrice,
                Discount = dto.Discount,
                KeyFeatures = dto.KeyFeatures,
                ProductAvailability = dto.ProductAvailability,
                Unit = dto.Unit,
                Tags = dto.Tags,
                Rating = dto.Rating,
                Favourite = dto.Favourite,
                UserId = userId,
                ShopId = dto.ShopId,
                ImageUrl = dto.ImageFile != null
                    ? await imageService.UploadImageAsync(dto.ImageFile, "products")
                    : string.Empty
            };
        }
    }
}
