using GeoService.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Mapping {
    public class CityMapper {
        public object Map(Continent continent, IUrlHelper urlHelper) {
            return new {
                ContinentId = urlHelper.ActionLink("GetContinentWithCountryAndCities", "City", new {
                    continentId = continent.Id,
                    countryId = continent.countries.Select(x => x.Id).FirstOrDefault(),
                    cities = continent.countries.Select(x => x.cities.Select(x => x.Id + x.Name)).FirstOrDefault()
            }),
        };
        }
    }
}
