using AutoMapper;
using GeoService.Core.Model;
using GeoService.Core.Service;
using GeoServiceRest.Layer.DTOs;
using GeoServiceRest.Layer.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GeoServiceRest.Layer.Controllers {
    [Route("apicontinent/{continentId}/country/{countryId}")]
    [ApiController]
    public class CityController : ControllerBase {
        #region props
        private readonly IMapper _mapper;
        private readonly IContinentService _continentService;
        private readonly IService<Country> _countryService;
        private readonly IService<City> _cityService;
        private readonly ILogger logger;
        DateTime currentDateTime = DateTime.Now;
        CityMapper cityMapper = new CityMapper();
        ContinentMapper continentMapper = new ContinentMapper();
        CountryMapper countryMapper = new CountryMapper();
        #endregion

        #region Ctor
        public CityController(IMapper mapper, IContinentService continentService, IService<Country> countryService, IService<City> cityService, ILogger<City> logger) {
            _mapper = mapper;
            _continentService = continentService;
            _countryService = countryService;
            _cityService = cityService;
            this.logger = logger;
        }
        #endregion
        //http://localhost:4374/apicontinent/1/country/1/cities
        [HttpGet("cities")]
        public async Task<IActionResult> GetContinentWithCountryAndCities(int continentId, int countryId) {
            try {
                logger.LogInformation("GET {time}", currentDateTime);
                var continent = await _continentService.GetWithCC(continentId, countryId);
                if(continent == null || continent.countries.Where(x=> x.Id == countryId).FirstOrDefault() == null) { return NotFound(); }//404
                return Ok(_mapper.Map<ContinentWithCCDto>(continent)); //200
            } catch (Exception ex) {

                return BadRequest($"iets fout {ex.Message}");
            }
        }

        //http://localhost:4374/apicontinent/1/country/1/city
        [HttpPost("city")]
        public async Task<IActionResult> SaveCity(CityDto cityDto, int continentId, int countryId) {
            try {
                logger.LogInformation("POST {time}", currentDateTime);
                var continent = await _continentService.GetWithCC(continentId, countryId);
                if (cityDto == null) { return BadRequest(); } // 400
                var naam = continent.countries.SelectMany(x => x.cities).Select(x => x.Name).ToList();
                for (int i = 0; i < continent.countries.Where(x => x.Id == countryId).First().cities.Count; i++) {
                    if (cityDto.Name == naam[i]) { return Conflict(); }
                }
                cityDto.CountryId = countryId;
                var city = await _cityService.AddAsync(_mapper.Map<City>(cityDto));
                return Created(string.Empty, _mapper.Map<CityDto>(city)); // 201

            } catch (Exception ex) {

                return BadRequest($"iets fout {ex.Message}");
            }
        }
        [HttpPut("city/{cityId}")]
        public IActionResult UpdateCity(CityDto cityDto, int continentId, int countryId, int cityId) {

            try {
                logger.LogInformation("PUT {time}", currentDateTime);
                if (cityDto == null) { return BadRequest(); }  // 400
                if (_continentService.GetWithCC(continentId, countryId).Result == null) { return NotFound(); }
                cityDto.Id = cityId;
                cityDto.CountryId = countryId;
                var city = _cityService.Update(_mapper.Map<City>(cityDto));
                return Ok("The City is updated!"); 
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }

        [HttpDelete("city/{cityId}")]
        public IActionResult RemoveCity(int continentId, int countryId, int cityId) {
            try {
                logger.LogInformation("DEL {time}", currentDateTime);
                var continent = _continentService.GetWithCC(continentId, countryId).Result;
                if (continent.countries.Where(x => x.Id == countryId).FirstOrDefault().cities.Where(x => x.Id == cityId).FirstOrDefault() == null) { return NotFound(); }
                var city = continent.countries.Where(x => x.Id == countryId).FirstOrDefault().cities.Where(x => x.Id == cityId).FirstOrDefault();
                _cityService.Remove(city);
                return Ok("The city has been deleted!");
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
    }
}
