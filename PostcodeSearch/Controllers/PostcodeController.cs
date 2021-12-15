using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostcodeSearch.Db.Models;
using PostcodeSearch.Services;

namespace PostcodeSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostcodeController : ControllerBase
    {
        private readonly IPostcodeRepo postcodeRepo;

        public PostcodeController(IPostcodeRepo postcodeRepo)
        {
            this.postcodeRepo = postcodeRepo;
        }

        /// <summary>
        /// Controller action to return postcodes with partial string matches
        /// </summary>
        /// <param name="partialPostcode">Partial postcode string</param>
        /// <returns>List of postcodes</returns>
        [HttpGet]
        [Route("search")]
        public List<string> Search([FromQuery]string partialPostcode)
        {
            return this.postcodeRepo.Search(partialPostcode);
        }

        /// <summary>
        /// Controller action to return postcodes near a specified location (lat/long)
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="distanceKM">Distance from location in kilometres</param>
        /// <returns></returns>
        [HttpGet]
        [Route("searchradius")]
        public List<string> SearchRadius([FromQuery] double latitude, double longitude, double distanceKM)
        {
            return this.postcodeRepo.Search(latitude, longitude, distanceKM);
        }

        /// <summary>
        /// Controller action to import postcodes into database
        /// </summary>
        [HttpGet]
        [Route("import")]
        public void Import()
        {
            this.postcodeRepo.Import();
        }
    }
}
