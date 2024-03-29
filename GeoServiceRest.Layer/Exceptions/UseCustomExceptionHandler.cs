﻿using GeoServiceRest.Layer.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.Exceptions {
    public static class UseCustomExceptionHandler {
        public static void UseCustomException(this IApplicationBuilder app) {
            app.UseExceptionHandler(config => {
                config.Run(async context => {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if(error != null) {
                        var er = error.Error;
                        ErrorDto errorDto = new ErrorDto();
                        errorDto.Status = 500;
                        errorDto.Errors.Add(er.Message);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));
                    }
                });
            });
        }
    }
}
