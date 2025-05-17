namespace ShopEase.Client.Data
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}
