// <copyright file="AppDataContext.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using Microsoft.EntityFrameworkCore;
using OMDBViewer.Entities;

namespace OMDBViewer.Data
{
    public partial class AppDataContext
    {
        public DbSet<ShowStatistic> ShowStatistic { get; set; }

        public DbSet<SearchStatistic> SearchStatistic { get; set; }
    }
}
