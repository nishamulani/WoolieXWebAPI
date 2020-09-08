using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Connector;
using WooliesX.Interface;
using WooliesX.Models.Resource;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace WooliesX.Resources
{
    public class ShopperHistoryService
    {

        private static string Resource_ShopperHistory_API_URL = "/api/resource/shopperHistory";

        private TelemetryClient telemetryClient = new TelemetryClient();


        private IJsonConnector _wooliesXProductConnector;
        public IJsonConnector WooliesXProductConnector
        {
            get
            {
                if (_wooliesXProductConnector == null)
                {
                    return _wooliesXProductConnector = new JsonConnector(string.Format("{0}{1}",
                        ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.URL"], Resource_ShopperHistory_API_URL),
                        HttpVerb.Get, "", "application/json");

                }
                return _wooliesXProductConnector;
            }
        }
        
        public IEnumerable<ShopperHistory> GetShopperHistoryDetails()
        {
            try
            {
                var data = string.Format("?token={0}", ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.Token"]);
                var json = WooliesXProductConnector.MakeRequest(data);
                telemetryClient.TrackTrace($"JSON from ShopperHistory API: {json}", SeverityLevel.Information);

                var shopperHistoryDetails = JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(json);

                return shopperHistoryDetails;

            }
            catch (Exception ex)
            {

                telemetryClient.TrackTrace($"Error in retrieving shoppers history:{ex.Message})", SeverityLevel.Error);
                return null;
            }

         
        }

    }
}
