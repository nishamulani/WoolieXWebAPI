using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Connector;
using WooliesX.Interface;

namespace WooliesX.Users
{
    public class UserService
    {

        private static string Exercice1_URL = "/api/Exercise/exercise1";

       // private string WooliesX_Exercie1_URL = "";
        private IJsonConnector _wooliesXConnector;
        public IJsonConnector WooliesXConnector
        {
            get
            {
                if (_wooliesXConnector == null)
                {
                    return _wooliesXConnector = new JsonConnector (string.Format("{0}{1}",ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.URL"], Exercice1_URL),
                        HttpVerb.Get, "", "application/json");

                }
                return _wooliesXConnector;
            }
        }

        public object GetUser(string url)
        {

            var data = string.Format("?token:{0}&url:{1}", ConfigurationManager.AppSettings["WooliesX.Dev.TestAPIs.Token"], url);
            var json = WooliesXConnector.MakeRequest(data);
            var userDetail = JsonConvert.DeserializeObject(json);
           
             return userDetail; 
        }
    }
}
