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
using System.Threading.Tasks;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.Domain.Entities;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.Domain.SeedWork;
using SoftSentre.Shoppingendly.Services.Products.BasicTypes.Types;

namespace SoftSentre.Shoppingendly.Services.Products.Extensions
{
    public static class RepositoryExtensionMethods
    {
        public static async Task<TEntity> GetByIdAndThrowIfEntityNotFound<TEntity, TId>(this IRepository<TEntity, TId> repository,
            TId entityId, Exception noValueException) where TEntity : class, IEntity
        {
            return await repository
                .GetByIdAsync(entityId)
                .UnwrapAsync(noValueException);
        }
        
        public static async Task<Maybe<TEntity>> GetByIdAndThrowIfEntityAlreadyExists<TEntity, TId>(this IRepository<TEntity, TId> repository,
            TId entityId, Exception noValueException) where TEntity : class, IEntity
        {
             var entity = await repository.GetByIdAsync(entityId);
             if (entity.HasValue)
             {
                 throw noValueException;
             }

             return entity;
        }
    }
}