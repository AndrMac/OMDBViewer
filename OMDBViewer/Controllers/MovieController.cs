// <copyright file="WeatherForecastController.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OMDB.API.Client;
using OMDBViewer.Data;
using OMDBViewer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMDBViewer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        public MovieController(ILogger<MovieController> logger, AppDataContext cx, IMapper mapper, Client omdbClient)
            : base(logger, cx, mapper, omdbClient)
        {
            this.Logger = logger;
        }

        [HttpGet]
        [Route("/list")]
        public CombinedResult GetMovieList([FromQuery] string search)
        {
            var result = this.OmdbClient
                            .Get_OMDb_SearchAsync(R.Json, s: search)
                            .GetAwaiter()
                            .GetResult();

            this.Context.Add(new Entities.SearchStatistic() { SearchValue = search, ResultsCount = result.Search.Count });
            this.Context.SaveChanges();

            return result;
        }

        [HttpGet]
        [Route("/details")]
        public CombinedResult GetMovie([FromQuery] string imdbId)
        {
            var result = this.OmdbClient
                            .Get_OMDb_SearchAsync(R.Json, i: imdbId)
                            .GetAwaiter()
                            .GetResult();

            return result;
        }

        [HttpGet]
        [Route("/search/statistic")]
        public List<SearchStatistic> GetSearchStatistic()
        {
            var result = this.Context.SearchStatistic.OrderByDescending(x => x.Id).Take(5).ToList();

            return result;
        }
    }
}
