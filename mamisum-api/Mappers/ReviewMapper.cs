using mamisum_api.DTOs;
using mamisum_api.Models;

namespace mamisum_api.Mappers
{
    public class ReviewMapper
    {
        public static ReviewDTO ToDTO(Review review) => new ReviewDTO
        {
            Id = review.Id,
            ReviewTitle = review.ReviewTitle,
            ReviewDescription = review.ReviewDescription,
            Rating = review.Rating,
            ReviewImage = review.ReviewImage,
            UserId = review.UserId,
            ProductId = review.ProductId
        };

        public static Review ToModel(ReviewDTO dto) => new Review
        {
            Id = dto.Id ?? string.Empty,
            ReviewTitle = dto.ReviewTitle,
            ReviewDescription = dto.ReviewDescription,
            Rating = dto.Rating,
            ReviewImage = dto.ReviewImage,
            UserId = dto.UserId,
            ProductId = dto.ProductId
        };
    }
}
