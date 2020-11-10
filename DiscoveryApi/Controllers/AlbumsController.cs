using DnsClient.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DiscoveryApi.Controllers
{
    [Route("api/albums")]
    public class AlbumsController: Controller
    {
        private const string dbName = "discovery-db"; 
        private const string collectionName = "albums"; 
        private MongoRepository mongoRepo;
        private ILogger<AlbumsController> _logger;
        public AlbumsController(IConfiguration configuration, ILogger<AlbumsController> logger)
        {          
            mongoRepo = new MongoRepository(configuration.GetSection("MongoConnection").Value, dbName);
            _logger = logger;
        }

        /// <summary>
        /// Get list of albums from repository.
        /// </summary>
        /// <returns>Payload with count and list of results</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAlbums()
        {
            try
            {
                _logger?.LogInformation("Fetching albums from repo."); 
                var results = mongoRepo.FindAll<Album>(collectionName);
                return Ok(new { total = results?.Count, results = results });
            }
            catch(Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
          
        }

        /// <summary>
        /// Add album to repository. 
        /// </summary>
        /// <param name="album"></param>
        /// <returns>Ok 200</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAlbum([FromBody] Album album)
        {
            if(album == null)
            {
                return BadRequest();
            }

            try
            {
                _logger?.LogInformation("Adding album to repo.");
                await mongoRepo.InsertAsync<Album>(collectionName, album);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
