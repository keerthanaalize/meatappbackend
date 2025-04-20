namespace mamisum_api.DTOs
{
    public class CreateDealDto
    {
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}
