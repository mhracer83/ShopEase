namespace ShopEase.Api.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }

        // For convenience: include product details
        public Product? Product { get; set; }
    }
}
