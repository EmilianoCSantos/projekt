using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

using Services;
using Configuration;
using Configuration.Options;
using Microsoft.Extensions.Options;
using DbModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]   
    public class AdminController : Controller
    {
        readonly DatabaseConnections _dbConnections;
        readonly IAdminService _service;
        readonly ILogger<AdminController> _logger;
        readonly VersionOptions _versionOptions;

        //GET: api/admin/environment
        [HttpGet()]
        [ActionName("Environment")]
        [ProducesResponseType(200, Type = typeof(DatabaseConnections.SetupInformation))]
        public IActionResult Environment()
        {
            try
            {
                var info = _dbConnections.SetupInfo;

                _logger.LogInformation($"{nameof(Environment)}:\n{JsonConvert.SerializeObject(info)}");
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Environment)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
         }

        [HttpGet()]
        [ActionName("Version")]
        [ProducesResponseType(typeof(VersionOptions), 200)]
        public IActionResult Version()
        {
            try
            {
                _logger.LogInformation($"{nameof(Version)}:\n{JsonConvert.SerializeObject(_versionOptions)}");
                return Ok(_versionOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving version information");
                return BadRequest(ex.Message);
            }
        }
        //GET: api/admin/seed?count={count} Seedar 10 items
        [HttpGet()]
        [ActionName("Seed")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
       public async Task<IActionResult> Seed(int nrItems = 10)
        {
            try
            {
                _logger.LogInformation($"{nameof(Seed)}");
                await _service.SeedAsync(nrItems);

                return Ok($"Seeded {nrItems} items successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Seed)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //GET: api/admin/seed?count={count} Seedar 50 användare
        [HttpGet()]
        [ActionName("SeedUsers")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SeedUsers(int nrItems = 50)
        {
            try
            {
                _logger.LogInformation($"{nameof(SeedUsers)}");
                await _service.SeedUsersAsync(nrItems);

                return Ok($"Seeded {nrItems} user items successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SeedUsers)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //GET: api/admin/seed?count={count} Seedar 100 Locations
        [HttpGet()]
        [ActionName("SeedLocations")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SeedLocations(int nrItems = 100)
        {
            try
            {
                _logger.LogInformation($"{nameof(SeedLocations)}");
                await _service.SeedLocationsAsync(nrItems);

                return Ok($"Seeded {nrItems} location items successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SeedLocations)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //GET: api/admin/seed?count={count} Seedar 1000 Attractions med FK till Locations
        [HttpGet()]
        [ActionName("SeedAttractions")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SeedAttractions(int nrItems = 1000)
        {
            try
            {
                _logger.LogInformation($"{nameof(SeedAttractions)}");
                await _service.SeedAttractionsAsync(nrItems);

                return Ok($"Seeded {nrItems} attraction items successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SeedAttractions)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        //GET: api/admin/seed?count={count} Seedar Reviews med FK till Attractions och Users
        [HttpGet()]
        [ActionName("SeedReviews")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SeedReviewsAsync(int nrItems = 1000)
        {
            try
            {
                _logger.LogInformation($"{nameof(SeedReviewsAsync)}");
                await _service.SeedReviewsAsync(nrItems);

                return Ok($"Seeded reviews successfully (0-20 reviews per attraction)");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SeedReviewsAsync)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        //GET: api/admin/attractions?category=&title=&description=&country=&city= Visar sevärdheter filtrerade på kategori, rubrik, beskrivning, land, och ort
        [HttpGet()]
        [ActionName("GetFilteredAttractions")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AttractionsDbM>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> GetFilteredAttractions(
            string category = null,
            string title = null,
            string description = null,
            string country = null,
            string city = null)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetFilteredAttractions)}");
                var result = await _service.GetFilteredAttractionsAsync(category, title, description, country, city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetFilteredAttractions)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //GET: api/admin/log
        [HttpGet()]
        [ActionName("Log")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogMessage>))]
        public async Task<IActionResult> Log([FromServices] ILoggerProvider _loggerProvider)
        {
            //Note the way to get the LoggerProvider, not the logger from Services via DI
            if (_loggerProvider is InMemoryLoggerProvider cl)
            {
                return Ok(await cl.MessagesAsync);
            }
            return Ok("No messages in log");
        }

        public AdminController(IAdminService service, ILogger<AdminController> logger,
                DatabaseConnections dbConnections, IOptions<VersionOptions> versionOptions)
        {
            _service = service;
            _logger = logger;
            _dbConnections = dbConnections;
            _versionOptions = versionOptions.Value;
        }
    }
}

