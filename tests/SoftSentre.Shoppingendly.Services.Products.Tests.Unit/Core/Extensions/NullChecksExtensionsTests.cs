﻿// Copyright 2020 SoftSentre Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using FluentAssertions;
using SoftSentre.Shoppingendly.Services.Products.Extensions;
using Xunit;

namespace SoftSentre.Shoppingendly.Services.Products.Tests.Unit.Core.Extensions
{
    public class NullChecksExtensionsTests
    {
        [Fact]
        public void CheckIfEmptyAndThrowAndReturnBoolMethodDoNotThrowExceptionWhenInputIsNotEmpty()
        {
            // Arrange
            const string testValue = "Not empty value";

            // Act
            Func<bool> func = () => testValue.IfEmptyThenThrowAndReturnBool();
            var testResult = func.Invoke();

            // Assert
            func.Should().NotThrow();
            testResult.Should().BeTrue();
        }

        [Fact]
        public void CheckIfEmptyAndThrowAndReturnBoolMethodThrowExceptionWhenInputIsEmpty()
        {
            // Arrange
            string testValue = null;

            // Act
            Func<bool> func = () => testValue.IfEmptyThenThrowAndReturnBool();

            // Assert
            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CheckIfEmptyAndThrowAndReturnBoolMethodWithMessageReturnCorrectMessageWhenExceptionWasThrown()
        {
            // Arrange
            string testValue = null;

            // Act
            Func<bool> func = () => testValue.IfEmptyThenThrowAndReturnBool("Test value can not be null.");

            // Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Test value can not be null. (Parameter 'String')");
        }

        [Fact]
        public void CheckIfEmptyAndThrowMethodDoNotThrowExceptionWhenInputIsNotEmpty()
        {
            // Arrange
            const string testValue = "Not empty value";

            // Act
            Action action = () => testValue.IfEmptyThenThrow();

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void CheckIfEmptyAndThrowMethodThrowExceptionWhenInputIsEmpty()
        {
            // Arrange
            string testValue = null;

            // Act
            Action action = () => testValue.IfEmptyThenThrow();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CheckIfEmptyAndThrowMethodWithMessageReturnCorrectMessageWhenExceptionWasThrown()
        {
            // Arrange
            string testValue = null;

            // Act
            Action action = () => testValue.IfEmptyThenThrow("Test value can not be null.");

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("Test value can not be null. (Parameter 'String')");
        }

        [Fact]
        public void CheckIfEmptyThenThrowAndReturnValueMethodDoNotThrowExceptionWhenInputIsNotEmpty()
        {
            // Arrange
            const string testValue = "Not empty value";

            // Act
            Func<string> func = () => testValue.IfEmptyThenThrowAndReturnValue();
            var testResult = func.Invoke();

            // Assert
            func.Should().NotThrow();
            testResult.Should().Be(testValue);
        }

        [Fact]
        public void CheckIfEmptyThenThrowAndReturnValueMethodThrowExceptionWhenInputIsEmpty()
        {
            // Arrange
            string testValue = null;

            // Act
            Func<string> func = () => testValue.IfEmptyThenThrowAndReturnValue();

            // Assert
            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CheckIfEmptyThenThrowAndReturnValueMethodWithMessageReturnCorrectMessageWhenExceptionWasThrown()
        {
            // Arrange
            string testValue = null;

            // Act
            Func<string> func = () => testValue.IfEmptyThenThrowAndReturnValue("Test value can not be null.");

            // Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Test value can not be null. (Parameter 'String')");
        }
    }
}