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

        public async Task<int> postProducts(PostProductDTO products, long createdBy)
        {
            try
            {
                using var _dbContext = m_dbContext.CreateDbContext();

                Product tempProduct = new Product
                {
                    ProductName = products.ProductName,
                    Price = products.Price,
                    IsActive = products.IsActive,
                    CreatedAt = DateTime.Now,
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

        public async Task<int> updateProduct(PutProductDTO product, long updatedBy)
        {
            try
            {
                using var _dbContext = m_dbContext.CreateDbContext();

                //Product? existingResult = m_pmsContext.Products.Where(p=>p.ProductId == product.ProductId).FirstOrDefault();
                Product? existingResult = _dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);


                if (existingResult == null)
                {
                    throw new Exception("product not found");
                }

                existingResult.ProductName = product.ProductName;
                existingResult.Price = product.Price;
                existingResult.IsActive = product.IsActive;
                existingResult.UpdatedAt = DateTime.Now;
                existingResult.UpdatedBy = updatedBy;

                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
