using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _repo;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerProfileRepository _customerProfileRepository;
        public ReviewService(IReviewRepository repo, IProductRepository productRepository, ICustomerProfileRepository customerProfileRepository )
        {
            _repo = repo;
            _productRepository = productRepository;
            _customerProfileRepository = customerProfileRepository;
        }

        public async Task<List<Review>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Review?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task<List<object>> GetReviewsByShopperIdWithDetailsAsync(string shopperId)
        {
            var products = await _productRepository.GetProductsAsync(shopperId);
            var productIds = products.Select(p => p.Id).ToList();
            var productDict = products.ToDictionary(p => p.Id, p => p.ProductName);

            var allReviews = await _repo.GetAllAsync();
            var relevantReviews = allReviews.Where(r => productIds.Contains(r.ProductId)).ToList();

            var result = new List<object>();

            foreach (var review in relevantReviews)
            {
                var userProfile = await _customerProfileRepository.GetByUserIdAsync(review.UserId);
                var userName = userProfile?.Name ?? "Unknown User";
                var productName = productDict.ContainsKey(review.ProductId) ? productDict[review.ProductId] : "Unknown Product";

                result.Add(new
                {
                    review.ReviewTitle,
                    review.ReviewDescription,
                    review.Rating,
                    review.ReviewImage,
                    ProductName = productName,
                    UserName = userName
                });
            }

            return result;
        }


        public async Task CreateAsync(Review review) => await _repo.CreateAsync(review);
        public async Task<bool> UpdateAsync(Review review) => await _repo.UpdateAsync(review);
        public async Task<bool> DeleteAsync(string id) => await _repo.DeleteAsync(id);
        public async Task<List<Review>> GetByProductIdAsync(string productId) => await _repo.GetByProductIdAsync(productId);
        public async Task<List<Review>> GetByUserIdAsync(string userId) => await _repo.GetByUserIdAsync(userId);
    }
}
