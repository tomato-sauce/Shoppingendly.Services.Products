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
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SoftSentre.Shoppingendly.Services.Products.Infrastructure.Configuration.DomainEvents;
using SoftSentre.Shoppingendly.Services.Products.Infrastructure.DomainEvents.Base;
using SoftSentre.Shoppingendly.Services.Products.Tests.Unit.Infrastructure.Configuration.Extensions;
using Xunit;

namespace SoftSentre.Shoppingendly.Services.Products.Tests.Unit.Infrastructure.Configuration
{
    public class DomainEventsModuleTests
    {
        [Fact]
        public void CheckIfAllTypesFromDomainEventsModuleAreRegistered()
        {
            //Arrange
            var typesToCheck = new List<Type>
            {
                typeof(IDomainEventAccessor),
                typeof(IDomainEventPublisher),
                typeof(IDomainEventsDispatcher)
            };

            var cqrsModule = new DomainEventsModule();

            //Act
            var typesRegistered = cqrsModule.GetTypesRegisteredInModule().ToList();

            //Arrange
            typesRegistered.Should().Contain(typesToCheck);
        }
    }
}