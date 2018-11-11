namespace Shop.Models
{
    public class OrderItem
    {
        private OrderItem()
        { }

        public OrderItem(Order order, Product product, int count)
        {
            Product = product;
            ProductId = product.Id;
            OrderId = order.Id;
            Count = count;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}