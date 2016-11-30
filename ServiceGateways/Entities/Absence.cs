using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public enum Statuses
    {
        S, HS, F, HF, FF, HFF, K, B, BS, AF, A, HA, SN
    }

    public class Absence : AbstractEntity
    {
        public DateTime Date { get; set; }
        public Statuses Status { get; set; }
        
    }
}
