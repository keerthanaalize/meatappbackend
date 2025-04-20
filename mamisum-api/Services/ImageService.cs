namespace mamisum_api.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile, string folderName)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var uploadPath = Path.Combine(_env.WebRootPath, "images", folderName);

            Directory.CreateDirectory(uploadPath); 

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Path.Combine("images", folderName, fileName).Replace("\\", "/");
        }

    }
}
