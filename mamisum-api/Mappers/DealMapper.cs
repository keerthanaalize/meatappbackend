using mamisum_api.DTOs;
using mamisum_api.Models;

namespace mamisum_api.Mappers
{
    public class DealMapper
    {
        public static Deal ToDeal(CreateDealDto dto, string userId)
        {
            var deal = new Deal
            {
                Title = dto.Title,
                UserId = userId
            };

            if (dto.ImageFile != null)
            {
                deal.ImageUrl = ""; 
            }

            return deal;
        }
    }
}
