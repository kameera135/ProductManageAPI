using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<ProductsDTO>> searchProducts(string searchedProduct);

        //Task postProducts(PostProductDTO products, int createdBy);
    }
}
