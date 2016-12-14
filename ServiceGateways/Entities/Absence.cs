using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public enum Statuses
    {
        S, HS, F, HF, FF, HFF, K, B, BS, AF, A, HA, SN, GRAY
    }

    public class Absence : AbstractEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Statuses Status { get; set; }
        public User User { get; set; }
    }
}
