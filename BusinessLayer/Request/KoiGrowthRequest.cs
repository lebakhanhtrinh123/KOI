using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Request
{
    public class KoiGrowthRequest
    {
        public DateOnly? GrowthDate { get; set; }

        public decimal? Size { get; set; }

        public decimal? Weight { get; set; }

        public string? Notes { get; set; }
    }
}
