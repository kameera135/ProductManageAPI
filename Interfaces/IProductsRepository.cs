using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<ProductsDTO>> searchProducts(string searchedProduct);

        Task<int> postProducts(PostProductDTO products, int createdBy);
    }
}
