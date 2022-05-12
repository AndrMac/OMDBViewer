// <copyright file="EntityBase.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using System;

namespace OMDBViewer.Entities
{
    public class EntityBase : IEntity
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
