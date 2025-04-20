namespace mamisum_api.DTOs
{
    public class UpdateCustomerProfileDto
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
