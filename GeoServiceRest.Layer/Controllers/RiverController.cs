using AutoMapper;
using GeoService.Core.Model;
using GeoService.Core.Service;
using GeoServiceRest.Layer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RiverController : ControllerBase {
        #region prop
        private readonly IMapper _mapper;
        private readonly IService<River> _riverService;
        private readonly IService<Country> _countryService;
        private readonly IService<CountryRiver> _countryRiverService;
        private readonly ILogger logger;
        DateTime currentDateTime = DateTime.Now;
        #endregion
        #region Ctor
        public RiverController(IMapper mapper, IService<River> riverService, IService<Country> countryService, IService<CountryRiver> countryRiverService, ILogger<River> logger) {
            _mapper = mapper;
            _riverService = riverService;
            _countryService = countryService;
            _countryRiverService = countryRiverService;
            this.logger = logger;
        }
        #endregion

        //http://localhost:4374/api/River/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            try {
                logger.LogInformation("GET {time}", currentDateTime);
                var river = await _riverService.GetByIdAsync(id);
                if (river == null) { return NotFound(); } // 404
                return Ok(_mapper.Map<RiverDto>(river)); // 200
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
        //http://localhost:4374/api/River/11
        [HttpPost]
        public async Task<IActionResult> Save(RiverDto riverDto) {
            try {
                var riverr = _riverService.GetByIdAsync(riverDto.Id).Result;
                var country = _countryService.GetByIdAsync(riverDto.countryRiver.Select(x => x.CountryId).FirstOrDefault()).Result;
                if (riverDto.countryRiver.Select(x => x.CountryId).FirstOrDefault() <= 0) return BadRequest("CountryId bestaat niet"); // 400
                if (riverDto == null) { return BadRequest(); } // 400
                var river = await _riverService.AddAsync(_mapper.Map<River>(riverDto));
                return Created(string.Empty, _mapper.Map<RiverDto>(river)); // 201
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
        //http://localhost:4374/api/River/11
        [HttpPut("{riverId}")]
        public IActionResult UpdateContinent(RiverDto riverDto, int riverId) {
            try {
                var countryRiver = _countryRiverService.GetAllAsync().Result;
                var riverr = _riverService.GetByIdAsync(riverDto.Id).Result;
                if (riverr != null) { return Conflict(); } // 409
                var country = _countryService.GetByIdAsync(riverDto.countryRiver.Select(x => x.CountryId).FirstOrDefault()).Result;
                if (riverDto.countryRiver.Select(x => x.CountryId).FirstOrDefault() <= 0 || country == null) return BadRequest("CountryId bestaat niet"); // 400
                if (riverDto == null) { return BadRequest(); } // 400
                riverDto.Id = riverId;
                foreach (CountryRiver items in countryRiver) {
                    if (items.RiverId == riverDto.Id) {
                        var countryRiverr = _countryRiverService.GetByIdAsync(items.Id).Result;
                        _countryRiverService.Remove(countryRiverr);
                    }
                }
                var river = _riverService.Update(_mapper.Map<River>(riverDto));
                var k = riverDto.countryRiver.Select(x => x.CountryId).ToList();
                for (int i = 0; i < k.Count; i++) {
                    CountryRiver countryRiver1 = new CountryRiver();
                    countryRiver1.CountryId = k[i];
                    countryRiver1.RiverId = riverId;
                    _countryRiverService.AddAsync(countryRiver1);
                }
                return Ok("The river has been updated!");
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }

        //http://localhost:4374/api/River/11
        [HttpDelete("{riverId}")]
        public IActionResult RemoveContinent(int riverId) {
            try {
                var river = _riverService.GetByIdAsync(riverId).Result;
                if (river == null) { return NotFound(); } // 404
                _riverService.Remove(river);
                return Ok("The river has been deleted");
            } catch (Exception ex) {
                return BadRequest($"iets fout {ex.Message}");
            }
        }
    }
}
