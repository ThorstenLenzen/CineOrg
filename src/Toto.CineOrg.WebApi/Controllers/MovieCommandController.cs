using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toto.CineOrg.Commands;
using Toto.CineOrg.ServiceModel;
using Toto.CineOrg.WebApi.ModelConverters;
using Toto.Utilities.Cqrs.AspNetCore.Commands;

namespace Toto.CineOrg.WebApi.Controllers
{
    /// <summary>
    /// Exposes endpoints to manage movies played in a theatre.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("movie")]
    public class MovieCommandController : ControllerBase
    {
        private readonly IControllerCommandProcessor _processor;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="processor">The command processor to handle commands execution.</param>
        public MovieCommandController(IControllerCommandProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <param name="id">The identifier of the movie to be updated.</param>
        /// <param name="movie">The information to be updated on the movie.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Nothing.</returns>
        /// <response code="204">If the update was successful.</response>
        /// <response code="400">If the provided information of the movie was not valid.</response>
        /// <response code="404">If the identifier of the movie be updated was not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovieAsync(Guid id, [FromBody] MovieForUpdate movie, CancellationToken cancellationToken)
            => await _processor.ProcessAsync(movie.ConvertToCommand(id), cancellationToken);

        /// <summary>
        /// Creates a movie.
        /// </summary>
        /// <param name="movie">The information for the movie to be created.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created movie.</returns>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the provided information of the movie was not valid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMovieAsync([FromBody] MovieForCreate movie, CancellationToken cancellationToken)
            => await _processor.ProcessAsync(movie.ConvertToCommand(), cancellationToken);
        
        /// <summary>
        /// Deletes a movie.
        /// </summary>
        /// <param name="id">The identifier of the movie to be deleted.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Nothing.</returns>
        /// <response code="204">If the deletion was successful.</response>
        /// <response code="404">If the identifier of the movie be deleted was not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovieAsync(Guid id, CancellationToken cancellationToken)
            => await _processor.ProcessAsync(new DeleteMovieCommand {Id = id}, cancellationToken);
    }
}