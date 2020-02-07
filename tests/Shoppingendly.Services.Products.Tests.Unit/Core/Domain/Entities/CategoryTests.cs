﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Shoppingendly.Services.Products.Core.Domain.Products.Entities;
using Shoppingendly.Services.Products.Core.Domain.Products.Events.Categories;
using Shoppingendly.Services.Products.Core.Domain.Products.ValueObjects;
using Shoppingendly.Services.Products.Core.Exceptions.Categories;
using Xunit;

namespace Shoppingendly.Services.Products.Tests.Unit.Core.Domain.Entities
{
    public class CategoryTests
    {
        #region domain logic

        [Fact]
        public void CheckIfSetNameMethodReturnFalseWhenParameterIsTheSameAsExistingValue()
        {
            // Arrange
            const string categoryName = "ExampleCategory";
            var category = new Category(new CategoryId(), categoryName);

            // Act
            var testResult = category.SetName(categoryName);

            // Assert
            testResult.Should().BeFalse();
        }

        [Fact]
        public void CheckIfSetNameMethodReturnTrueWhenParameterIsDifferentAsExistingValue()
        {
            // Arrange
            const string categoryName = "OtherCategory";
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            var testResult = category.SetName(categoryName);

            // Assert
            testResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("Home")]
        [InlineData("IProvideMaximalNumberOfLetters")]
        public void CheckIfSetNameMethodReturnTrueWhenCorrectNameHasBeenProvidedAndDoNotThrowAnyException(string name)
        {
            // Arrange
            var categoryName = name;
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            Func<bool> func = () => category.SetName(categoryName);
            var testResult = func.Invoke();

            // Assert
            func.Should().NotThrow();
            testResult.Should().BeTrue();
        }

        [Fact]
        public void CheckIfSetNameMethodSetValuesWhenCorrectNameHasBeenProvided()
        {
            // Arrange
            const string categoryName = "OtherCategory";
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            category.SetName(categoryName);
            var isAssigned = category.Name == categoryName;
            var updatedDateAreChanged = category.UpdatedDate != default;

            // Assert
            isAssigned.Should().BeTrue();
            updatedDateAreChanged.Should().BeTrue();
        }

        [Fact]
        public void CheckIfSetNameMethodThrowProperExceptionAndMessageWhenEmptyNameHasBeenProvided()
        {
            // Arrange
            var categoryName = string.Empty;
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            Func<bool> func = () => category.SetName(categoryName);

            // Assert
            func.Should().Throw<InvalidCategoryNameException>()
                .WithMessage("Category name can not be empty.");
        }

        [Fact]
        public void CheckIfSetNameMethodThrowProperExceptionAndMessageWhenTooShortNameHasBeenProvided()
        {
            // Arrange
            const string categoryName = "Hom";
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            Func<bool> func = () => category.SetName(categoryName);

            // Assert
            func.Should().Throw<InvalidCategoryNameException>()
                .WithMessage("Category name can not be shorter than 4 characters.");
        }

        [Fact]
        public void CheckIfSetNameMethodThrowProperExceptionAndMessageWhenTooLongNameHasBeenProvided()
        {
            // Arrange
            const string categoryName = "IProvideMaximalNumberOfLettersAndFewMore";
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            Func<bool> func = () => category.SetName(categoryName);

            // Assert
            func.Should().Throw<InvalidCategoryNameException>()
                .WithMessage("Category name can not be longer than 30 characters.");
        }

        [Fact]
        public void CheckIfSetDescriptionMethodReturnFalseWhenParameterIsTheSameAsExistingValue()
        {
            // Arrange
            const string description = "Description is correct.";
            var category = new Category(new CategoryId(), "ExampleCategory", description);

            // Act
            var testResult = category.SetDescription(description);

            // Assert
            testResult.Should().BeFalse();
        }

        [Fact]
        public void CheckIfSetDescriptionMethodReturnTrueWhenParameterIsDifferentAsExistingValue()
        {
            // Arrange
            const string description = "Description is correct.";
            var category = new Category(new CategoryId(), "ExampleCategory", "Other description is correct.");

            // Act
            var testResult = category.SetDescription(description);

            // Assert
            testResult.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(CategoryDataGenerator.CorrectCategoryDescriptions),
            MemberType = typeof(CategoryDataGenerator))]
        public void CheckIfSetDescriptionMethodReturnTrueWhenCorrectDescriptionHasBeenProvidedAndDoNotThrowAnyException(
            string description)
        {
            // Arrange
            var categoryDescription = description;
            var category = new Category(new CategoryId(), "ExampleCategory", "Other correct description");

            // Act
            Func<bool> func = () => category.SetDescription(categoryDescription);
            var testResult = func.Invoke();

            // Assert
            func.Should().NotThrow();
            testResult.Should().BeTrue();
        }

        [Fact]
        public void CheckIfSetDescriptionMethodSetValuesWhenCorrectDescriptionHasBeenProvided()
        {
            // Arrange
            const string categoryDescription = "Description is correct.";
            var category = new Category(new CategoryId(), "ExampleCategory", "Other correct description");

            // Act
            category.SetDescription(categoryDescription);
            var isAssigned = category.Description == categoryDescription;
            var updatedDateAreChanged = category.UpdatedDate != default;

            // Assert
            isAssigned.Should().BeTrue();
            updatedDateAreChanged.Should().BeTrue();
        }

        [Fact]
        public void CheckIfSetDescriptionMethodThrowProperExceptionAndMessageWhenTooShortDescriptionHasBeenProvided()
        {
            // Arrange
            const string description = "Description is too";
            var category = new Category(new CategoryId(), "ExampleCategory", "Description is correct.");

            // Act
            Func<bool> func = () => category.SetDescription(description);

            // Assert
            func.Should().Throw<InvalidCategoryDescriptionException>()
                .WithMessage("Category description can not be shorter than 20 characters.");
        }

        [Fact]
        public void CheckIfSetDescriptionMethodThrowProperExceptionAndMessageWhenTooLongDescriptionHasBeenProvided()
        {
            // Arrange
            var description = new string('*', 4001);
            var category = new Category(new CategoryId(), "ExampleCategory", "Description is correct.");

            // Act
            Func<bool> func = () => category.SetDescription(description);

            // Assert
            func.Should().Throw<InvalidCategoryDescriptionException>()
                .WithMessage("Category description can not be longer than 4000 characters.");
        }

        #endregion

        #region domain events

        [Fact]
        public void CheckIfCreateNewCategoryByConstructorWithoutDescriptionProduceDomainEventWithAppropriateTypeAndValues()
        {
            // Arrange

            // Act
            var category = new Category(new CategoryId(), "ExampleCategory");
            var newCategoryCreatedDomainEvent = category.GetUncommitted().LastOrDefault() as NewCategoryCreatedDomainEvent;
            
            // Assert
            category.DomainEvents.Should().NotBeEmpty();
            category.DomainEvents.LastOrDefault().Should().BeOfType<NewCategoryCreatedDomainEvent>();
            newCategoryCreatedDomainEvent.Should().NotBeNull();
            newCategoryCreatedDomainEvent.CategoryId.Should().Be(category.Id);
            newCategoryCreatedDomainEvent.CategoryName.Should().Be(category.Name);
            newCategoryCreatedDomainEvent.CategoryDescription.Should().Be(category.Description);
        }

        [Fact]
        public void CheckIfCreateNewCategoryByConstructorWithDescriptionProduceDomainEventWithAppropriateTypeAndValues()
        {
            // Arrange

            // Act
            var category = new Category(new CategoryId(), "ExampleCategory", "Description is correct.");
            var newCategoryCreatedDomainEvent = category.GetUncommitted().LastOrDefault() as NewCategoryCreatedDomainEvent;
            
            // Assert
            category.DomainEvents.Should().NotBeEmpty();
            category.DomainEvents.LastOrDefault().Should().BeOfType<NewCategoryCreatedDomainEvent>();
            newCategoryCreatedDomainEvent.Should().NotBeNull();
            newCategoryCreatedDomainEvent.CategoryId.Should().Be(category.Id);
            newCategoryCreatedDomainEvent.CategoryName.Should().Be(category.Name);
            newCategoryCreatedDomainEvent.CategoryDescription.Should().Be(category.Description);
        }

        [Fact]
        public void CheckIfSetCategoryNameMethodProduceDomainEventWithAppropriateTypeAndValues()
        {
            // Arrange
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            category.SetName("NewCategoryName");
            var categoryNameChangedDomainEvent = category.GetUncommitted().LastOrDefault() as CategoryNameChangedDomainEvent;
            
            // Assert
            category.DomainEvents.Should().NotBeEmpty();
            category.DomainEvents.LastOrDefault().Should().BeOfType<CategoryNameChangedDomainEvent>();
            categoryNameChangedDomainEvent.Should().NotBeNull();
            categoryNameChangedDomainEvent.CategoryId.Should().Be(category.Id);
            categoryNameChangedDomainEvent.CategoryName.Should().Be(category.Name);
        }

        [Fact]
        public void CheckIfSetCategoryDescriptionMethodProduceDomainEventWithAppropriateTypeAndValues()
        {
            // Arrange
            var category = new Category(new CategoryId(), "ExampleCategory", "Description is correct.");

            // Act
            category.SetDescription("Other correct description");
            var categoryDescriptionChanged = category.GetUncommitted().LastOrDefault() as CategoryDescriptionChangedDomainEvent;

            // Assert
            category.DomainEvents.Should().NotBeEmpty();
            category.DomainEvents.LastOrDefault().Should().BeOfType<CategoryDescriptionChangedDomainEvent>();
            categoryDescriptionChanged.Should().NotBeNull();
            categoryDescriptionChanged.CategoryId.Should().Be(category.Id);
            categoryDescriptionChanged.CategoryDescription.Should().Be(category.Description);
        }

        [Fact]
        public void CheckIfClearDomainEventsMethodWorkingProperly()
        {
            // Arrange
            var category = new Category(new CategoryId(), "ExampleCategory");

            // Act
            category.ClearDomainEvents();

            // Assert
            category.DomainEvents.Should().BeEmpty();
        }

        [Fact]
        public void CheckIfGetUncommittedDomainEventsMethodWorkingProperly()
        {
            // Arrange
            var category = new Category(new CategoryId(), "ExampleCategory", "Description is correct.");

            // Act
            var domainEvents = category.GetUncommitted();
            
            // Assert
            domainEvents.Should().NotBeNull();
            domainEvents.LastOrDefault().Should().BeOfType<NewCategoryCreatedDomainEvent>();
        }
        
        #endregion
    }

    public class CategoryDataGenerator
    {
        public static IEnumerable<object[]> CorrectCategoryDescriptions =>
            new List<object[]>
            {
                new object[] {"Description is correct"},
                new object[] {new string('*', 3999)}
            };
    }
}