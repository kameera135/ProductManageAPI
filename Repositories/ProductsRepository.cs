using ProductManageAPI.DTO;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;

namespace ProductManageAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly PmsContext m_pmsContext;
        public ProductsRepository(PmsContext pmsContext)
        {
            m_pmsContext = pmsContext;
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
    }
}
