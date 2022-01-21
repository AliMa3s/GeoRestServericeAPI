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
    public class ContinentControllerTest {
        private readonly Mapper _mapper;
        private readonly Mock<IContinentService> _mockRepo;
        private readonly Mock<IService<Country>> _countryService;
        private readonly Mock<IService<City>> _cityService;
        private readonly Mock<ILogger<Continent>> _logger;
        DateTime currentDateTime = DateTime.Now;
        private readonly ContinentController _controller;
        private List<Continent> continents;
        private List<ContinentDto> continentDtos;
        ContinentMapper _continentMapper = new ContinentMapper();

        public ContinentControllerTest() {
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
            _logger = new Mock<ILogger<Continent>>();
            _controller = new ContinentController(_mapper, _mockRepo.Object, _countryService.Object, _cityService.Object, _logger.Object);

            continentDtos = new List<ContinentDto>() {
                new ContinentDto { Id = 1, Name = "Azië", Population = 30000 },
                new ContinentDto { Id = 2, Name = "Europa", Population = 20000 }};

            continents = new List<Continent>() {
                new Continent { Id = 1, Name = "Azië", Population = 30000 },
                new Continent { Id = 2, Name = "Europa", Population = 20000  }};
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetContinenten_IdValid_ReturnOk(int continentId) {
            var continent = continents.First(x => x.Id == continentId);
            //var resultaat = _continentMapper.Map(continent, this.Url);
            _mockRepo.Setup(x => x.GetByIdAsync(continentId)).ReturnsAsync(continent);
            var result = await _controller.GetById(continentId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnContinent = Assert.IsType<ContinentDto>(okResult.Value);
            Assert.Equal(continentId, returnContinent.Id);
            Assert.Equal(continent.Name, returnContinent.Name);
            Assert.Equal(continent.Population, returnContinent.Population);
        }

        [Theory]
        [InlineData(0)]
        public async void GetContinent_IdInValid_ReturnNotFound(int continentId) {
            Continent continent = null;
            _mockRepo.Setup(x => x.GetByIdAsync(continentId)).ReturnsAsync(continent);
            var result = await _controller.GetById(continentId);
            Assert.IsType<NotFoundResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public async void PostContinent_ReturnCreatedContinent() {
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(continents);
            var continent = continentDtos.First();
            continent.Id = 3;
            var result = await _controller.Save(continent);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void PostContinent_ReturnBadRequest() {
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
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public void PutContinent_IdIsEqualContinent_ReturnNoContent() {
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(continents);
            var continent = continentDtos.First();
            int continentId = continent.Id;
            var result = _controller.UpdateContinent(continent, continentId);
            var returnContinent = Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public void PutContinent_IdIsNotEqualContinent_ReturnBadRequest() {
            var continent = continents.First();
            int continentId = continent.Id;
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(continents);
            var continent1 = continentDtos.First();
            continent1 = null;
            var result = _controller.UpdateContinent(continent1, continentId);
            Assert.IsType<BadRequestResult>(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(1)]
        public void DeleteContinent_IdValid_ReturnNoContent(int continentId) {
            var continent = continents.First(x => x.Id == continentId);
            _mockRepo.Setup(x => x.GetByIdAsync(continentId)).ReturnsAsync(continent);
            var result = _controller.RemoveContinent(continentId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public void DeleteContinent_IdInValid_ReturnNotFound(int continentId) {
            Continent continent = null;
            _mockRepo.Setup(x => x.GetByIdAsync(continentId)).ReturnsAsync(continent);
            var result = _controller.RemoveContinent(continentId);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
