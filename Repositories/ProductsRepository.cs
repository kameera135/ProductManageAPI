using Microsoft.EntityFrameworkCore;
using ProductManageAPI.DTO;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;

namespace ProductManageAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly PmsContext m_pmsContext;
        private readonly IDbContextFactory<PmsContext> m_dbContext;

        public ProductsRepository(PmsContext pmsContext,IDbContextFactory<PmsContext> dbContext)
        {
            m_pmsContext = pmsContext;
            m_dbContext = dbContext;
        }

        public Task<List<ProductsDTO>> searchProducts(string? searchedProduct)
        {
            try
            {
                List<Product> list = new List<Product>();

                if (string.IsNullOrEmpty(searchedProduct))
                {
                    list = m_pmsContext.Products.Where(q=>q.DeletedAt == null).ToList();
                }
                else
                {
                    list = m_pmsContext.Products.Where(q=>q.ProductName.ToLower().Contains(searchedProduct.ToLower()) && q.DeletedAt == null).ToList();
                }

                List<ProductsDTO> products = new List<ProductsDTO>();

                foreach (Product product in list)
                {
                    ProductsDTO tempProduct = new()
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        IsActive = product.IsActive,
                    };

                    products.Add(tempProduct);
                }

                return Task.FromResult(products);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> postProducts(PostProductDTO products, int createdBy)
        {
            try
            {
                using var _dbContext = m_dbContext.CreateDbContext();

                Product tempProduct = new Product
                {
                    ProductName = products.ProductName,
                    Price = products.Price,
                    IsActive = products.IsActive,
                    CreatedBy = createdBy
                };

               _dbContext.Add(tempProduct);
               return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
