﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMarket.Models
{
    public class Client
    {
       public int Id { get; set; }
        public string Name { get; set; }
     
        public string Family { get; set; }
 
        public string Patronymic { get; set; }

   
        public string Email { get; set; }

        
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }    
      
    }
}