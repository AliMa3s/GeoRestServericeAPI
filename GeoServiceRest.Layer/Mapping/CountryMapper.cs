using GeoService.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Mapping {
    public class CountryMapper {
        public object Map(Continent continent, IUrlHelper urlHelper) {
            return new {
                ContinentId = urlHelper.ActionLink("GetContinentWithCountries", "Country", new {
                    continentId = continent.Id,
                    countries = continent.countries.Select(x => x.Id + x.Name)
                }),
                Naam = continent.Name,
                Population = continent.Population,
            };
        }
    }
}
