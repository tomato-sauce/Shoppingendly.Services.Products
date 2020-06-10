// Copyright 2020 SoftSentre Contributors
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

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.Types;
using SoftSentre.Shoppingendly.Services.Products.Domain.Entities;
using SoftSentre.Shoppingendly.Services.Products.Domain.Repositories;
using SoftSentre.Shoppingendly.Services.Products.Domain.ValueObjects;
using SoftSentre.Shoppingendly.Services.Products.Extensions;

namespace SoftSentre.Shoppingendly.Services.Products.Infrastructure.EntityFramework.Repositories
{
    public class CategoryEfRepository : ICategoryRepository
    {
        private readonly ProductServiceDbContext _productServiceDbContext;

        public CategoryEfRepository(ProductServiceDbContext productServiceDbContext)
        {
            _productServiceDbContext = productServiceDbContext
                .IfEmptyThenThrowAndReturnValue();
        }

        public async Task<Maybe<Category>> GetByIdAsync(CategoryId id)
        {
            return await _productServiceDbContext.Categories.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<Maybe<Category>> GetByNameAsync(string name)
        {
            return await _productServiceDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
        }

        public async Task<Maybe<Category>> GetByNameWithIncludesAsync(string name)
        {
            return await _productServiceDbContext.Categories.Include(c => c.ProductCategories)
                .FirstOrDefaultAsync(c => c.CategoryName == name);
        }

        public async Task<Maybe<IEnumerable<Category>>> GetAllAsync()
        {
            return await _productServiceDbContext.Categories.ToListAsync();
        }

        public async Task<Maybe<IEnumerable<Category>>> GetAllWithIncludesAsync()
        {
            return await _productServiceDbContext.Categories.Include(c => c.ProductCategories).ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _productServiceDbContext.AddAsync(category);
        }

        public void Update(Category category)
        {
            _productServiceDbContext.Update(category);
        }

        public void Delete(Category category)
        {
            _productServiceDbContext.Remove(category);
        }
    }
}