namespace ShopEase.Client.Data
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public string GetProductDetails()
        {
            return $"Product: {Name} | Price: ${Price:0.00} | Category: {Category}";
        }
    }
}
