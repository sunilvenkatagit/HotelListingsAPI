using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/countries")] // versioning via query parameter
    //[Route("api/{v:apiversion}/countries")] // versioning via route URL
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountriesController> _logger;
        private readonly IMapper _mapper;

        public CountryV2Controller(IUnitOfWork unitOfWork, ILogger<CountriesController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            _logger.LogInformation("This is a test endpoint for versioning!");

            var countries = await _unitOfWork.Countries.GetAll(requestParams);
            var results = _mapper.Map<IList<CountryDto>>(countries);
            return Ok(results);
        }
    }
}
