using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toto.CineOrg.Queries;
using Toto.Utilities.Cqrs.AspNetCore.Queries;

namespace Toto.CineOrg.WebApi.Controllers
{
    /// <summary>
    /// Exposes endpoints to query genres.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("genre")]
    public class GenreQueryController : ControllerBase
    {
        private readonly IControllerQueryProcessor _processor;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="processor">The command processor to handle commands execution.</param>
        public GenreQueryController(IControllerQueryProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }
        
        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>All genres available.</returns>
        /// <response code="200">Returns all genres available.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGenresAsync(CancellationToken cancellationToken)
            => await _processor.ProcessAsync(new GenresQuery(), cancellationToken);
    }
}