using AutoMapper;
using GeoService.Core.Model;
using GeoService.Core.Service;
using GeoServiceRest.Layer.DTOs;
using GeoServiceRest.Layer.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Controllers {
    [Route("api/continent/{continentId}")]
    [ApiController]
    public class CountryController : ControllerBase {
        #region prop
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
        public CountryController(IMapper mapper, IContinentService continentService, IService<Country> countryService, IService<City> cityService, ILogger<Country> logger) {
            _mapper = mapper;
            _continentService = continentService;
            _countryService = countryService;
            _cityService = cityService;
            this.logger = logger;
        }
        #endregion
        //http://localhost:4374/api/continent/1/countries
        [HttpGet("countries")]
        public async Task<IActionResult> GetContinentWithCountries(int continentId) {
            try {
                logger.LogInformation("GET {time}", currentDateTime);
                var continent = await _continentService.GetWithC(continentId);
                if (continent == null) { return NotFound(); } // 404
                return Ok(_mapper.Map<ContinentWithCDto>(continent)); // 200
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
        //http://localhost:4374/api/continent/1/country
        [HttpPost("country")]
        public async Task<IActionResult> SaveCountry(CountryDto countryDto, int continentId) {
            try {
                logger.LogInformation("POST {time}", currentDateTime);
                var continent = await _continentService.GetWithC(continentId);
                if (countryDto == null) { return BadRequest(); } // 400
                var naam = continent.countries.Select(x => x.Name).ToList();
                for (int i = 0; i < continent.countries.Count; i++) {
                    if (countryDto.Name == naam[i]) { return Conflict(); }
                }
                countryDto.ContinentId = continent.Id;
                var country = await _countryService.AddAsync(_mapper.Map<Country>(countryDto));
                return Created(string.Empty, _mapper.Map<CountryDto>(country)); // 201
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }

        }
        //http://localhost:4374/api/continent/1/country/17
        [HttpPut("country/{countryId}")]
        public IActionResult UpdateCountry(CountryDto countryDto, int continentId, int countryId) {
            try {
                logger.LogInformation("PUT {time}", currentDateTime);
                if (countryDto == null) { return BadRequest(); }  // 400
                countryDto.Id = countryId;
                countryDto.ContinentId = continentId;
                var country = _countryService.Update(_mapper.Map<Country>(countryDto));
                return Ok("The country has been updated"); 
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
        //http://localhost:4374/api/continent/1/country/17
        [HttpDelete("country/{countryId}")]
        public IActionResult RemoveCountry(int continentId, int countryId) {
            try {
                logger.LogInformation("DEL {time}", currentDateTime);
                var continent = _continentService.GetWithCC(continentId, countryId).Result;
                if (continent == null) { return NotFound(); }
                var country = continent.countries.Where(x => x.Id == countryId).FirstOrDefault();
                if (country.cities.FirstOrDefault() != null) throw new Exception($"Delete first all Cities of {country.Name}");
                _countryService.Remove(country);
                return Ok("The country has been delete"); 
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
    }
}
