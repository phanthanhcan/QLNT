using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNTWS.Models
{
    public class BadRequestModel
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> ModelState { get; set; }
    }
}