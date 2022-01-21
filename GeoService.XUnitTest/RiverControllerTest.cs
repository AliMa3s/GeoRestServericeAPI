using AutoMapper;
using GeoService.Core.Model;
using GeoService.Core.Service;
using GeoServiceRest.Layer.Controllers;
using GeoServiceRest.Layer.DTOs;
using GeoServiceRest.Layer.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoService.XUnitTest {
    public class RiverControllerTest {
        private readonly Mapper _mapper;
        private readonly Mock<IService<River>> _mockRepo;
        private readonly Mock<IService<Country>> _countryService;
        private readonly Mock<IService<CountryRiver>> _countryRiverService;
        private readonly Mock<ILogger<River>> _logger;
        private readonly RiverController _controller;
        private List<River> rivers;
        private List<RiverDto> riverDtos;
        private List<CountryRiver> countryRivers;
        private List<CountryRiverDto> countryRiverDtos;
        private List<Country> countries;
        private List<CountryDto> countryDtos;

        public RiverControllerTest() {
            if (_mapper == null) {
                var mappingConfig = new MapperConfiguration(mc => {
                    mc.AddProfile(new MapProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = (Mapper)mapper;
            }
            _mockRepo = new Mock<IService<River>>();
            _countryService = new Mock<IService<Country>>();
            _countryRiverService = new Mock<IService<CountryRiver>>();
            _logger = new Mock<ILogger<River>>();
            _controller = new RiverController(_mapper, _mockRepo.Object, _countryService.Object, _countryRiverService.Object, _logger.Object);

            countryRivers = new List<CountryRiver>() {
            new CountryRiver{Id = 1, CountryId = 1, RiverId = 1 },
            new CountryRiver{Id = 2, CountryId = 2, RiverId = 2 }
            };

            countryRiverDtos = new List<CountryRiverDto>() {
            new CountryRiverDto{Id = 1, CountryId = 1, RiverId = 1 },
            new CountryRiverDto{Id = 2, CountryId = 2, RiverId = 2 }
            };

            riverDtos = new List<RiverDto>() {
                new RiverDto { Id = 1, Name = "Yellow River", Length = 5464, countryRiver = new List<CountryRiverDto>() },
                new RiverDto { Id = 2, Name = "Loire", Length = 1012, countryRiver = new List<CountryRiverDto>() }};

            rivers = new List<River>() {
                new River { Id = 1, Name = "Yellow River", Length = 5464 },
                new River { Id = 2, Name = "Loire", Length = 1012}};

            countryDtos = new List<CountryDto>() {
                new CountryDto { Id = 1, ContinentId = 1, Name = "Belgie", Population = 5000 },
                new CountryDto { Id = 2, ContinentId = 2, Name = "China", Population = 10000 }};

            countries = new List<Country>() {
                new Country { Id = 1, ContinentId = 1, Name = "Belgie", Population = 5000 },
                new Country { Id = 2, ContinentId = 2, Name = "China", Population = 10000 }};
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetRiver_IdValid_ReturnOk(int riverId) {
            var river = rivers.First(x => x.Id == riverId);
            _mockRepo.Setup(x => x.GetByIdAsync(riverId)).ReturnsAsync(river);
            var result = await _controller.GetById(riverId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnRiver = Assert.IsType<RiverDto>(okResult.Value);
            Assert.Equal(riverId, returnRiver.Id);
            Assert.Equal(river.Name, returnRiver.Name);
            Assert.Equal(river.Length, returnRiver.Length);
        }

        [Theory]
        [InlineData(0)]
        public async void GetRiver_IdInValid_ReturnNotFound(int riverId) {
            River river = null;
            _mockRepo.Setup(x => x.GetByIdAsync(riverId)).ReturnsAsync(river);
            var result = await _controller.GetById(riverId);
            var okResult = Assert.IsType<NotFoundResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public async void PostRiver_ReturnCreatedContinent() {
            var river1 = rivers.First();
            _mockRepo.Setup(x => x.GetByIdAsync(river1.Id)).ReturnsAsync(river1);
            var river = riverDtos.First();
            var countryRiver = countryRiverDtos.First();
            //river1.countryRiver.Add(countryRiver);
            river.countryRiver.Add(countryRiver);
            var result = await _controller.Save(river);
            Assert.IsType<CreatedResult>(result);
        }

        /*[Fact]
        public async void PostRiver_ReturnBadRequest()
        {
            var continent = continents.First();
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(continents);
            _mockRepo.Setup(x => x.AddAsync(_mapper.Map<Continent>(continent)));
            var continent1 = continentDtos.First();
            continent1 = null;
            var result = await _controller.Save(continent1);
            Assert.IsType<BadRequestResult>(result);
            continent1 = continentDtos.First();
            result = await _controller.Save(continent1);
            Assert.IsType<ConflictResult>(result);
        }*/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
