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

using SoftSentre.Shoppingendly.Services.Products.BasicTypes.Domain.DomainEvents;
using SoftSentre.Shoppingendly.Services.Products.Domain.ValueObjects;

namespace SoftSentre.Shoppingendly.Services.Products.Domain.Events.Categories
{
    public class NewCategoryCreatedDomainEvent : DomainEventBase
    {
        internal NewCategoryCreatedDomainEvent(CategoryId categoryId, string categoryName, Picture categoryIcon)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryIcon = categoryIcon;
        }
        
        internal NewCategoryCreatedDomainEvent(CategoryId categoryId, CategoryId parentCategoryId, string categoryName, Picture categoryIcon)
        {
            CategoryId = categoryId;
            ParentCategoryId = parentCategoryId;
            CategoryName = categoryName;
            CategoryIcon = categoryIcon;
        }

        internal NewCategoryCreatedDomainEvent(CategoryId categoryId, string categoryName, string categoryDescription,
            Picture categoryIcon)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
            CategoryIcon = categoryIcon;
        }
        
        internal NewCategoryCreatedDomainEvent(CategoryId categoryId, CategoryId parentCategoryId, string categoryName, string categoryDescription,
            Picture categoryIcon)
        {
            CategoryId = categoryId;
            ParentCategoryId = parentCategoryId;
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
            CategoryIcon = categoryIcon;
        }

        public CategoryId CategoryId { get; }
        public CategoryId ParentCategoryId { get; set; }
        public string CategoryName { get; }
        public string CategoryDescription { get; }
        public Picture CategoryIcon { get; }
    }
}