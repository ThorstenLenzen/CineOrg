using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toto.CineOrg.Queries;
using Toto.CineOrg.ServiceModel;
using Toto.Utilities.Cqrs.AspNetCore.Queries;

namespace Toto.CineOrg.WebApi.Controllers
{
    /// <summary>
    /// Exposes endpoints to manage movies played in a theatre.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("movie")]
    public class MovieQueryController : ControllerBase
    {
        private readonly IControllerQueryProcessor _processor;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="processor">The command processor to handle commands execution.</param>
        public MovieQueryController(IControllerQueryProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <param name="filter">All filter parameters possible with this action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>All movies available.</returns>
        /// <response code="200">Returns all movies available.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMoviesAsync([FromQuery] MoviesQueryFilter filter, CancellationToken cancellationToken)
            => await _processor.ProcessAsync(new MoviesQuery { Filter = filter }, cancellationToken);

        /// <summary>
        /// Gets the movie specified by the given identifier.
        /// </summary>
        /// <param name="id">The identifier of the movie to be retrieved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The movie specified by the given id.</returns>
        /// <response code="200">Returns the movie specified by the given id.</response>
        /// <response code="404">If the movie, specified by the given id, could not be found.</response>
        [HttpGet("{id}", Name = "GetMovieByIdAsync")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _processor.ProcessAsync(new MovieQuery {Id = id}, cancellationToken);
    }
}