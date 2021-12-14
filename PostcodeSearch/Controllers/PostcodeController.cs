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

        [HttpGet]
        [Route("search")]
        public List<PostcodeEntry> Search(string partialPostcode)
        {
            return this.postcodeRepo.Search(partialPostcode);
        }
    }
}
