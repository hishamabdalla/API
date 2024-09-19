namespace API.DTO
{
    public class CategoryWithProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductDTO> Products { get; set; }=new List<ProductDTO>();
    }

    public class ProductDTO
    {

        public string Name { get; set; }
        public double Price { get; set; }
    }
}
