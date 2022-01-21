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
    public class NotFoundFilterCity : ActionFilterAttribute{
        private readonly IService<City> _cityService;
        public NotFoundFilterCity(IService<City> cityService) {
            _cityService = cityService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var city = await _cityService.GetByIdAsync(id);
            if(city != null) {
                await next();
            } else {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;
                errorDto.Errors.Add($"id {id} niet gevonden in de database");
                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
