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
        S = 0, HS = 1, F = 2, HF = 3, FF = 4, HFF = 5, K = 6, B = 7, BS = 8, AF = 9, A = 10, HA = 11, SN = 12
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
