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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public decimal Total([FromBody]Trolly trolly)
        {
            return TrollyService.CalculateTrollyTotal(trolly);
        }
       

      
    }
}