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

namespace SoftSentre.Shoppingendly.Services.Products.BasicTypes.Domain.Entities
{
    public abstract class EntityBase : IEntity, IAuditAbleEntity
    {
        public DateTime? UpdatedDate { get; private set; }
        public DateTime CreatedAt { get; }

        protected EntityBase()
        {
            CreatedAt = DateTime.UtcNow;
        }
        
        protected void SetUpdatedDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}