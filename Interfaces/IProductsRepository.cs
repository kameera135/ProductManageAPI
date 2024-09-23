using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<ProductsDTO>> searchProducts(string searchedProduct);

        Task<int> postProducts(PostProductDTO products, long createdBy);

        Task<int> updateProduct(PutProductDTO product, long updatedBy);

        //Task<int> deleteProduct(long id, long deletedBy);
    }
}
