using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MvcAsyncDemo.Models;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using SignalR.Web;

namespace MvcAsyncDemo.Controllers
{
    public class CheapFlightsController : Controller
    {
        public ActionResult Index()
        {
            string json = new WebClient().DownloadString(FlightsService.Url);
            Flight[] flights = new JavaScriptSerializer().Deserialize<Flight[]>(json);
            return View("FlightList", flights);
        }
    }

    public class CheapFlightsAsyncController : AsyncController
    {
        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            var webClient = new WebClient();

            webClient.DownloadStringCompleted += (sender, evt) => {
                // When the web service request completes, capture the result
                AsyncManager.Parameters["json"] = evt.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };

            // Commence the web service request asynchronously
            webClient.DownloadStringAsync(new Uri(FlightsService.Url));
        }

        public ActionResult IndexCompleted(string json)
        {
            Flight[] flights = new JavaScriptSerializer().Deserialize<Flight[]>(json);
            return View("FlightList", flights);
        }
    }

    public class CheapFlightsTaskAsyncController : TaskAsyncController
    {
        public async Task<ActionResult> Index()
        {
            string json = await new WebClient().DownloadStringTaskAsync(FlightsService.Url);
            Flight[] flights = new JavaScriptSerializer().Deserialize<Flight[]>(json);
            return View("FlightList", flights);
        }
    }
}