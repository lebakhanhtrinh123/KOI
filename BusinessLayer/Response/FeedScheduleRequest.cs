using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Response
{
    public class FeedScheduleRequest
    {
        public DateTime? FeedDate { get; set; }

        public decimal? FeedAmount { get; set; }

        public string? Notes { get; set; }
    }
}
