using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcAsyncDemo.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public decimal Price { get; set; }
    }

    public class FlightsService
    {
        public readonly static string Url = "http://localhost:8089/flightdata";
    }
}