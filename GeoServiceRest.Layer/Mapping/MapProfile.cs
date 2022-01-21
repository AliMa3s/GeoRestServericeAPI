using AutoMapper;
using GeoService.Core.Model;
using GeoServiceRest.Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Mapping {
    public class MapProfile : Profile{
        public MapProfile() {
            CreateMap<Continent, ContinentDto>();
            CreateMap<ContinentDto, Continent>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();

            CreateMap<Continent, ContinentWithCDto>();
            CreateMap<ContinentWithCDto, Continent>();

            CreateMap<Continent, ContinentWithCCDto>();
            CreateMap<ContinentWithCCDto, Continent>();

            CreateMap<Country, CountryWithCDto>();
            CreateMap<CountryWithCDto, Country>();

            CreateMap<River, RiverDto>();
            CreateMap<RiverDto, River>();

            CreateMap<CountryRiver, CountryRiverDto>();
            CreateMap<CountryRiverDto, CountryRiver>();

            CreateMap<River, CountryRiverDto>();
            CreateMap<CountryRiverDto, River>();

            CreateMap<ContinentMapper, ContinentWithCCDto>();
            CreateMap<ContinentWithCCDto, ContinentMapper>();
        }
    }
}
