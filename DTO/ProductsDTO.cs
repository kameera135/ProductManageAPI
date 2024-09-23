namespace ProductManageAPI.DTO
{
    public class ProductsDTO
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }

    public class PostProductDTO
    { 
        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}