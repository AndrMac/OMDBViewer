// <copyright file="SearchStatistic.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using System;

namespace OMDBViewer.Entities
{
    public class SearchStatistic : EntityBase
    {
        public string SearchValue { get; set; }

        public int ResultsCount { get; set; }
    }
}
