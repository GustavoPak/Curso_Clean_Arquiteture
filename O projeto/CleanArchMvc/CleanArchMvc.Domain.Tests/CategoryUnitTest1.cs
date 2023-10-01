using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category with valid state")]
        public void CreateCategory_WithValidParametrers_ReturnValidStateObject()
        {
            Action action = () => new Category(1, "Burritos");

            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with invalid State")]
        public void CreateCategory_WithInvalidParameters_ReturnException()
        {
            Action action = () => new Category(-1, "Category name");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("This Id is not valid.");
        }

        [Fact(DisplayName = "Short Name")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                   .WithMessage("Invalid name,too short,minimum 3 caracters");
        }

        [Fact(DisplayName = "Required Name")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("This name is not Valid!");
        }

        [Fact(DisplayName = "Null name")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
    }
}