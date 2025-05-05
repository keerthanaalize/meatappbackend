using mamisum_api.Models;
using mamisum_api.Repositories;
using mamisum_api.Services;
using mamisum_api.Utilities;

namespace mamisum_api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMamisumDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB settings
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            // Services
            services.AddSingleton<EmailService>();
            services.AddSingleton<OTPService>();
            services.AddSingleton<AuthService>();
            services.AddSingleton<ImageService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<CustomerProfileService>();
            services.AddSingleton<ShopProfileService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<CartService>();
            services.AddScoped<CustomerProfileService>();
            services.AddScoped<ShopProfileService>();
            services.AddScoped<OrderService>();
            services.AddScoped<DealService>();
            services.AddScoped<ReviewService>();
            services.AddScoped<FavoriteService>();

            // Repositories
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ICartRepository, CartRepository>();
            services.AddSingleton<ICustomerProfileRepository, CustomerProfileRepository>();
            services.AddSingleton<IShopProfileRepository, ShopProfileRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IDealRepository, DealRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IFavoriteRepository, FavoriteRepository>();

            return services;
        }
    }
}
