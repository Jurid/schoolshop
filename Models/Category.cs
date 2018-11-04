using System;

namespace Shop.Models
{
    public class Category
    {
        public Category()
        { }

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Категория не может существовать без имени");

            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}