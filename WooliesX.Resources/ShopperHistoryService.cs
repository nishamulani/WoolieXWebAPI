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

namespace WooliesX.Resources
{
    public class ShopperHistoryService
    {

        private static string Resource_ShopperHistory_API_URL = "/api/resource/shopperHistory";

  

         
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

            var data = string.Format("?token={0}", ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.Token"]);
            var json = WooliesXProductConnector.MakeRequest(data);
           

            var shopperHistoryDetails = JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(json);
           
            return shopperHistoryDetails;
        }

    }
}
