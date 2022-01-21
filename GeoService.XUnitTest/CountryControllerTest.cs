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
    public class CountryControllerTest {
        private readonly Mapper _mapper;
        private readonly Mock<IContinentService> _mockRepo;
        private readonly Mock<IService<Country>> _countryService;
        private readonly Mock<IService<City>> _cityService;
        private readonly Mock<ILogger<Country>> _logger;
        DateTime currentDateTime = DateTime.Now;
        private readonly CountryController _controller;
        private List<Continent> continents;
        private List<ContinentDto> continentDtos;
        private List<Country> countries;
        private List<CountryDto> countryDtos;
        CountryMapper _countryMapper = new CountryMapper();

        public CountryControllerTest() {
            if (_mapper == null) {
                var mappingConfig = new MapperConfiguration(mc => {
                    mc.AddProfile(new MapProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = (Mapper)mapper;
            }
            _mockRepo = new Mock<IContinentService>();
            _countryService = new Mock<IService<Country>>();
            _cityService = new Mock<IService<City>>();
            _logger = new Mock<ILogger<Country>>();
            _controller = new CountryController(_mapper, _mockRepo.Object, _countryService.Object, _cityService.Object, _logger.Object);

            continentDtos = new List<ContinentDto>() {
                new ContinentDto { Id = 1, Name = "Azië", Population = 30000 },
                new ContinentDto { Id = 2, Name = "Europa", Population = 20000 }};

            continents = new List<Continent>() {
                new Continent { Id = 1, Name = "Azië", Population = 30000 },
                new Continent { Id = 2, Name = "Europa", Population = 20000  }};

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
        public async void GetCountry_IdValid_ReturnOk(int continentId) {
            var continent = continents.First(x => x.Id == continentId);
            //var resultaat = _continentMapper.Map(continent, this.Url);
            _mockRepo.Setup(x => x.GetWithC(continentId)).ReturnsAsync(continent);
            continent.countries.Add(countries.Where(x => x.Id == continentId).First());
            var result = await _controller.GetContinentWithCountries(continentId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnContinent = Assert.IsType<ContinentWithCDto>(okResult.Value);
            Assert.Equal(continent.countries.Select(x => x.Id == continentId).First(), returnContinent.Countries.Select(x => x.Id == continentId).First());
            Assert.Equal(continent.countries.Select(x => x.Name == continent.countries.Select(x => x.Name).First()).First(), returnContinent.Countries.Select(x => x.Name == continent.countries.Select(x => x.Name).First()).First());
            Assert.Equal(continent.countries.Select(x => x.Population == continent.countries.Select(x => x.Population).First()).First(), returnContinent.Countries.Select(x => x.Population == continent.countries.Select(x => x.Population).First()).First());
        }

        [Theory]
        [InlineData(0)]
        public async void GetCountry_IdInValid_ReturnNotFound(int continentId) {
            Continent continent = null;
            _mockRepo.Setup(x => x.GetByIdAsync(continentId)).ReturnsAsync(continent);
            var result = await _controller.GetContinentWithCountries(continentId);
            Assert.IsType<NotFoundResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public async void PostCountry_ReturnCreatedContinent() {
            var continent = continents.First();
            _mockRepo.Setup(x => x.GetByIdAsync(continent.Id)).ReturnsAsync(continent);
            _mockRepo.Setup(x => x.GetWithC(continent.Id)).ReturnsAsync(continent);
            var country = countryDtos.First();
            var result = await _controller.SaveCountry(country, continent.Id);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void PostCountry_ReturnBadRequest() {
            var continent = continents.First();
            _mockRepo.Setup(x => x.GetWithC(1)).ReturnsAsync(continent);
            var country = countryDtos.First();
            country = null;
            var result = await _controller.SaveCountry(country, continent.Id);
            Assert.IsType<BadRequestResult>(result);

            var country2 = countryDtos.First();
            var country3 = countries.First();
            continent.countries.Add(country3);
            result = await _controller.SaveCountry(country2, continent.Id);
            Assert.IsType<ConflictResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public async void PutCountry_IdIsEqualContinent_ReturnNoContent() {
            var continent = continents.First();
            _mockRepo.Setup(x => x.GetByIdAsync(continent.Id)).ReturnsAsync(continent);
            _mockRepo.Setup(x => x.GetWithC(continent.Id)).ReturnsAsync(continent);
            var country = countryDtos.First();
            var result = await _controller.SaveCountry(country, continent.Id);
            continent.countries.Add(countries.Where(x => x.Id == continent.Id).First());
            var result1 = _controller.UpdateCountry(country, continent.Id, continent.countries.Select(x => x.Id).First());
            var returnContinent = Assert.IsAssignableFrom<OkObjectResult>(result1);
        }

        [Fact]
        public async void PutContinent_IdIsNotEqualContinent_ReturnBadRequest() {
            var continent = continents.First();
            _mockRepo.Setup(x => x.GetByIdAsync(continent.Id)).ReturnsAsync(continent);
            _mockRepo.Setup(x => x.GetWithC(continent.Id)).ReturnsAsync(continent);
            var country = countryDtos.First();
            var result = await _controller.SaveCountry(country, continent.Id);
            continent.countries.Add(countries.Where(x => x.Id == continent.Id).First());
            country = countryDtos.First();
            country = null;
            var result2 = _controller.UpdateCountry(country, continent.Id, continent.countries.Select(x => x.Id).First());
            Assert.IsType<BadRequestResult>(result2);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(1, 1)]
        public void DeleteCountry_IdValid_ReturnNoContent(int continentId, int countryId) {
            var continent = continents.First(x => x.Id == continentId);
            var country = countries.First();
            _mockRepo.Setup(x => x.GetWithCC(continentId, countryId)).ReturnsAsync(continent);
            continent.countries.Add(country);
            var result = _controller.RemoveCountry(continentId, 1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public void DeleteCountry_IdInValid_ReturnNotFound(int continentId, int countryId) {
            var continent = continents.First(x => x.Id == continentId);
            var country = countries.First();
            _mockRepo.Setup(x => x.GetWithCC(continentId, countryId)).ReturnsAsync(continent);
            continent.countries.Add(country);
            var result = _controller.RemoveCountry(continentId, 0);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
