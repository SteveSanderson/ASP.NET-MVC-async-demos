using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using FlightInfoWebService.Models;
using System.Threading.Tasks;
using SignalR.Web;

namespace FlightInfoWebService.Controllers
{
    public class FlightDataController : TaskAsyncController
    {
        public async Task<JsonResult> Index()
        {
            await TaskEx.Delay(1000);
            return Json(GetFlightsData(), JsonRequestBehavior.AllowGet);
        }

        private static IEnumerable<Flight> GetFlightsData() {
            return new[] {
                new Flight { FlightId = 38434, FromCity = "London", ToCity = "New York", Price = 755.95M },
                new Flight { FlightId = 38434, FromCity = "London", ToCity = "Singapore", Price = 1270.00M },
                new Flight { FlightId = 38434, FromCity = "Paris", ToCity = "Tokyo", Price = 820.50M },
                new Flight { FlightId = 38434, FromCity = "Stockholm", ToCity = "Chicago", Price = 475.20M },
                new Flight { FlightId = 38434, FromCity = "Glasgow", ToCity = "Barcelona", Price = 185.00M },
            };
        }
    }
}
