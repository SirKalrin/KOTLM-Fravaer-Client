using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using DateTimeExtensions.Export;
using ServiceGateways.Entities;

namespace Fravaer_WebApp_Client.Models
{

    public class UserDetailsViewModel
    {
        public User User { get; set; }
        public DateTime DateTime {get; set;}
        public int InitIndex { get; set; }
        public ArrayList AbsenceTypes { get; set; }
        public string ChosenAbsence { get; set; }
    }




    
}