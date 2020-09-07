
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

       

        [HttpGet]
        [ActionName("user")]
        public User Get(string name, string token)
        {

            //var user = UserService.GetUser();
            User user1 = new User();
            user1.Name = name;
            user1.Token = token;
           return user1;
        }

       

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}