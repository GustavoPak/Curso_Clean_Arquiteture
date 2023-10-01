using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description,price,stock,image);
        }
        public Product(int id,string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid value for ID.");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        private void ValidateDomain(string name,string description,decimal price,int stock,string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                 "This name is not Valid!");

            DomainExceptionValidation.When(name.Length < 3,
                 "Invalid name,too short,minimum 3 characters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                 "This Description is not Valid!");

            DomainExceptionValidation.When(description.Length < 10,
                 "Invalid Description,too short,minimum 10 characters");

            DomainExceptionValidation.When(price < 0, "The price can't be 0,00.");

            DomainExceptionValidation.When(stock < 0, "The stock can't be 0.");

            DomainExceptionValidation.When(image?.Length > 250,
                 "Too long your Baka! >:(");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }

        public void Update(Product product)
        {
            ValidateDomain(product.Name, product.Description, product.Price, product.Stock, product.Image);
            CategoryId = product.CategoryId;
        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
