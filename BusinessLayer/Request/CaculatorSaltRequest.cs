using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Request
{
    public class CaculatorSaltRequest
    {
        public decimal Volume { get; set; }
        public decimal SaltConcentration { get; set; } 
        public decimal? WaterChangeRate { get; set; } 
        public string? PondCondition { get; set; } 
        public decimal? Temperature { get; set; } 
        public decimal? PhLevel { get; set; } 
    }
}
