using GeoService.Core.Model;
using GeoService.Core.Service;
using GeoServiceRest.Layer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Filters {
    public class NotFoundFilterCountry : ActionFilterAttribute{
        private readonly IService<Continent> _countryService;
        public NotFoundFilterCountry(IService<Continent> countryService) {
            _countryService = countryService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var country = await _countryService.GetByIdAsync(id);
            if (country != null) {
                await next();
            } else {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;
                errorDto.Errors.Add($"id {id} niet gevonden in database");
                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
