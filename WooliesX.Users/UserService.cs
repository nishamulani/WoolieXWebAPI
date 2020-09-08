using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Connector;
using WooliesX.Interface;
using WooliesX.Models;

namespace WooliesX.Users
{
    public class UserService
    {

       
         public User GetUser(string name, string token)
        {
           var user = new User();           
           user.Name = name;
           user.Token = token;
            
            
           return user; 
        }
    }
}
