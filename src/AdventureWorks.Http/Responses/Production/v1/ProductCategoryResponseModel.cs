namespace AdventureWorks.Http.Responses.Products.v1
{
    public class ProductCategoryResponseModel
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
