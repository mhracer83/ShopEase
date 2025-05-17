using System;

namespace ShopEase.Api.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }

        // No-args constructor
        public Product() { }

        // Optional: convenience constructor without ProductID
        public Product(string name, decimal price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
        }

        // Returns details in the desired format.
        public string GetProductDetails()
        {
            return $"Product: {Name} | Price: ${Price:0.00} | Category: {Category}";
        }
    }
}
