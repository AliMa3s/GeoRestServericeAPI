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
   public class CityControllerTest {

        private readonly Mapper _mapper;
        private readonly Mock<IContinentService> _mockRepo;
        private readonly Mock<IService<Country>> _countryService;
        private readonly Mock<IService<City>> _cityService;
        private readonly Mock<ILogger<City>> _logger;
        DateTime currentDateTime = DateTime.Now;
        private readonly CityController _controller;
        private List<Continent> continents;
        private List<ContinentDto> continentDtos;
        private List<Country> countries;
        private List<CountryDto> countryDtos;
        private List<City> cities;
        private List<CityDto> cityDtos;
        CountryMapper _countryMapper = new CountryMapper();

        public CityControllerTest() {
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
            _logger = new Mock<ILogger<City>>();
            _controller = new CityController(_mapper, _mockRepo.Object, _countryService.Object, _cityService.Object, _logger.Object);

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

            cities = new List<City>(){
                new City { Id = 1, CountryId = 1, Name = "Brussel", Population = 5000},
                new City { Id = 2, CountryId = 2, Name = "Peking", Population = 10000}};

            cityDtos = new List<CityDto>() {
                new CityDto { Id = 1, CountryId = 1, Name = "Brussel", Population = 5000 },
                new CityDto { Id = 2, CountryId = 2, Name = "Peking", Population = 10000 }};
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async void GetCity_IdValid_ReturnOk(int continentId, int countryId) {
            var continent = continents.First(x => x.Id == continentId);
            var country = countries.First(x => x.Id == countryId);
            var city = cities.First(x => x.Id == countryId);
            _mockRepo.Setup(x => x.GetWithCC(continentId, countryId)).ReturnsAsync(continent);
            continent.countries.Add(countries.Where(x => x.Id == countryId).First());
            continent.countries.Select(x => x.cities).First().Add(city);
            var result = await _controller.GetContinentWithCountryAndCities(continentId, countryId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnContinent = Assert.IsType<ContinentWithCCDto>(okResult.Value);
            Assert.Equal(continent.countries.Select(x => x.cities).First().Select(x => x.Id), returnContinent.Countries.Select(x => x.Cities).First().Select(x => x.Id));
            Assert.Equal(continent.countries.Select(x => x.cities).First().Select(x => x.Name), returnContinent.Countries.Select(x => x.Cities).First().Select(x => x.Name));
            Assert.Equal(continent.countries.Select(x => x.cities).First().Select(x => x.Population), returnContinent.Countries.Select(x => x.Cities).First().Select(x => x.Population));
        }

        [Theory]
        [InlineData(0, 0)]
        public async void GetCity_IdInValid_ReturnNotFound(int continentId, int countryId) {
            Continent continent = null;
            _mockRepo.Setup(x => x.GetByIdAsync(continentId)).ReturnsAsync(continent);
            var result = await _controller.GetContinentWithCountryAndCities(continentId, countryId);
            Assert.IsType<NotFoundResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public async void PostCity_ReturnCreatedContinent() {
            var continent = continents.First();
            var country = countries.First();
            _mockRepo.Setup(x => x.GetWithCC(continent.Id, country.Id)).ReturnsAsync(continent);
            continent.countries.Add(country);
            var city = cityDtos.First();
            var result = await _controller.SaveCity(city, continent.Id, country.Id);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void PostCity_ReturnBadRequest() {
            var continent = continents.First();
            var country = countries.First();
            _mockRepo.Setup(x => x.GetWithCC(1, 1)).ReturnsAsync(continent);
            var city = cityDtos.First();
            city = null;
            var result = await _controller.SaveCity(city, continent.Id, country.Id);
            Assert.IsType<BadRequestResult>(result);

            var city2 = cityDtos.First();
            var city3 = cities.First();
            country.cities.Add(city3);
            continent.countries.Add(country);
            result = await _controller.SaveCity(city2, continent.Id, country.Id);
            Assert.IsType<ConflictResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public async void PutCity_IdIsEqualContinent_ReturnNoContent() {
            var continent = continents.First();
            var country = countries.First();
            _mockRepo.Setup(x => x.GetWithCC(continent.Id, country.Id)).ReturnsAsync(continent);
            var city = cityDtos.First();
            var city2 = cities.First();
            country.cities.Add(city2);
            continent.countries.Add(country);
            var result = await _controller.SaveCity(city, continent.Id, country.Id);
            var result1 = _controller.UpdateCity(city, continent.Id, country.Id, continent.countries.SelectMany(x => x.cities).Select(x => x.Id).First());
            var returnContinent = Assert.IsAssignableFrom<OkObjectResult>(result1);
        }

        [Fact]
        public async void PutCity_IdIsNotEqualContinent_ReturnBadRequest() {
            var continent = continents.First();
            var country = countries.First();
            _mockRepo.Setup(x => x.GetWithCC(continent.Id, country.Id)).ReturnsAsync(continent);
            var city = cityDtos.First();
            var city2 = cities.First();
            country.cities.Add(city2);
            continent.countries.Add(country);
            var result = await _controller.SaveCity(city, continent.Id, country.Id);
            city = cityDtos.First();
            city = null;
            var result1 = _controller.UpdateCity(city, continent.Id, country.Id, continent.countries.SelectMany(x => x.cities).Select(x => x.Id).First());
            var returnContinent = Assert.IsAssignableFrom<BadRequestResult>(result1);
            city = cityDtos.First();
            var result2 = _controller.UpdateCity(city, continent.Id, 0, continent.countries.SelectMany(x => x.cities).Select(x => x.Id).First());
            var returnContinent2 = Assert.IsAssignableFrom<NotFoundResult>(result2);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(1, 1)]
        public void DeleteCountry_IdValid_ReturnNoContent(int continentId, int countryId) {
            var continent = continents.First(x => x.Id == continentId);
            var country = countries.First(x => x.Id == countryId);
            var city = cities.First();
            _mockRepo.Setup(x => x.GetWithCC(continentId, countryId)).ReturnsAsync(continent);
            country.cities.Add(city);
            continent.countries.Add(country);
            var result = _controller.RemoveCity(continentId, countryId, city.Id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public void DeleteCountry_IdInValid_ReturnNotFound(int continentId, int countryId) {
            var continent = continents.First(x => x.Id == continentId);
            var country = countries.First(x => x.Id == countryId);
            var city = cities.First();
            _mockRepo.Setup(x => x.GetWithCC(continentId, countryId)).ReturnsAsync(continent);
            country.cities.Add(city);
            continent.countries.Add(country);
            var result = _controller.RemoveCity(continentId, countryId, 0);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
