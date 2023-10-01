using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category (string name)
        {
            ValidateDomain(name);
        }

        public Category(int id,string name)
        {
            DomainExceptionValidation.When(id < 0, "This Id is not valid.");
            Id = id;
            ValidateDomain(name);
        }

        public void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),"This name is not Valid!");

            DomainExceptionValidation.When(name.Length < 3, "Invalid name,too short,minimum 3 caracters");

            Name = name;
        }

        public void UpdateCategory(string categoryName)
        {
            ValidateDomain(categoryName);
        }
    }
}
