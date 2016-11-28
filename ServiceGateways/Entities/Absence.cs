using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public class Absence
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public enum Status
        {
            S, HS, F, HF, FF, HFF, K, B, BS, AF, A, HA, SN
        }

    }
}
