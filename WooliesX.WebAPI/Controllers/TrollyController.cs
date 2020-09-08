using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WooliesX.Models.Resource;
using WooliesX.Resources;

namespace WooliesX.WebAPI.Controllers
{
    public class TrollyController : ApiController
    {

        public TrollyService TrollyService = new TrollyService();

        private TelemetryClient telemetryClient = new TelemetryClient();


        [HttpPost]
        public decimal Total([FromBody]Trolly trolly)
        {
            var total = TrollyService.CalculateTrollyTotal(trolly);

            string json = JsonConvert.SerializeObject(total);
            telemetryClient.TrackTrace($"JSON Sent from trolly/total API :{json})", SeverityLevel.Error);
            return total;

        }
       

      
    }
}