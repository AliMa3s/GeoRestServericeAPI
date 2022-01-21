using GeoService.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Mapping {
    public class ContinentMapper {
        public object Map(Continent continent, IUrlHelper urlHelper) {
            return new {
                ContinentId = urlHelper.ActionLink("GetById", "Continent", new {
                    continentId = continent.Id
                }),
                Naam = continent.Name,
                Population = continent.Population,
            };
        }
    }
}
