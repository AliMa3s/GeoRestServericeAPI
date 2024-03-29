﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Exceptions {
    public class RiverException : Exception {
        public RiverException() {
        }

        public RiverException(string message) : base(message) {
        }

        public RiverException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
