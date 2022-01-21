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
    [Route("api/continent")]
    [ApiController]
    public class ContinentController : ControllerBase {
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
        public ContinentController(IMapper mapper, IContinentService continentService, IService<Country> countryService, IService<City> cityService, ILogger<Continent> logger) {
            _mapper = mapper;
            _continentService = continentService;
            _countryService = countryService;
            _cityService = cityService;
            this.logger = logger;
        }
        #endregion

        //http://localhost:4374/api/continent/12
        [HttpGet("{continentId}")]
        public async Task<IActionResult> GetById(int continentId) {
            try {
                logger.LogInformation("GET {time}", currentDateTime);
                var continent = await _continentService.GetByIdAsync(continentId);
                if (continent == null) { return NotFound(); } // 404
                return Ok(_mapper.Map<ContinentDto>(continent)); // 200
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }

        //http://localhost:4374/api/continent
        [HttpPost]
        public async Task<IActionResult> Save(ContinentDto continentDto) {
            try {
                logger.LogInformation("POST {time}", currentDateTime);
                var continentt = _continentService.GetAllAsync().Result.ToList();
                if (continentDto == null) { return BadRequest(); } // 400
                for (int i = 0; i < continentt.Count; i++) {
                    if (continentt[i].Id == continentDto.Id) { return Conflict(); } // 409
                }
                var continent = await _continentService.AddAsync(_mapper.Map<Continent>(continentDto));
                return Created(string.Empty, _mapper.Map<ContinentDto>(continent)); // 201
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
        //http://localhost:4374/api/continent/2
        [HttpPut("{continentId}")]
        public IActionResult UpdateContinent(ContinentDto continentDto, int continentId) {
            try {
                logger.LogInformation("PUT {time}", currentDateTime);
                if (continentDto == null) { return BadRequest(); }
                continentDto.Id = continentId;
                var continent = _continentService.Update(_mapper.Map<Continent>(continentDto));
                return Ok("The continent has been updated");
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
        //http://localhost:4374/api/continent/12
        [HttpDelete("{continentId}")]
        public IActionResult RemoveContinent(int continentId) {
            try {
                logger.LogInformation("DEL {time}", currentDateTime);
                var continent = _continentService.GetByIdAsync(continentId).Result;
                if (continent == null) { return NotFound(); } // 404
                if (continent.countries.FirstOrDefault() != null) throw new Exception($"Delete first all Countries of {continent.Name}");
                _continentService.Remove(continent);
                return Ok("The continent has been deleted!");
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
    }
}
