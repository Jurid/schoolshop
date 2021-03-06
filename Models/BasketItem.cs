﻿namespace Shop.Models
{
    public class BasketItem
    {
        private BasketItem()
        { }

        public BasketItem(Product product, int count)
        {
            Product = product;
            ProductId = Product.Id;
            Count = count;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}