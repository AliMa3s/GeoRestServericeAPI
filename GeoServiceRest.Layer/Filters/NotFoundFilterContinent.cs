using GeoService.Core.Service;
using GeoServiceRest.Layer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Filters {
    public class NotFoundFilterContinent : ActionFilterAttribute{
        private readonly IContinentService _continentService;
        public NotFoundFilterContinent(IContinentService continentService) {
            _continentService = continentService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var continent = await _continentService.GetByIdAsync(id);
            if (continent != null) {
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
