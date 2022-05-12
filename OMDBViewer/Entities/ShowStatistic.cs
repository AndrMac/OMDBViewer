// <copyright file="ShowStatistic.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using System;

namespace OMDBViewer.Entities
{
    public class ShowStatistic : EntityBase
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
