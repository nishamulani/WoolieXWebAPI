
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WooliesX.Models;
using WooliesX.Models.Resource;
using WooliesX.Resources;
using WooliesX.Users;

namespace WooliesX.WebAPI.Controllers
{
    public class AnswersController : ApiController
    {         

        public UserService UserService = new UserService();
        private TelemetryClient telemetryClient = new TelemetryClient();

        [HttpGet]
        [ActionName("user")]
        public User Get(string name, string token)
        {
            
            if(ModelState.IsValid)
            {
                var user =  UserService.GetUser(name, token);
                string json = JsonConvert.SerializeObject(user);
                telemetryClient.TrackTrace($"JSON Sent from answers/users API :{json})", SeverityLevel.Error);
                return user;
            }

            return null;
        }
       
    }
}