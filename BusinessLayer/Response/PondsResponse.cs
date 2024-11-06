using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Response
{
    public class PondsResponse
    {
        public int PondId { get; set; }

        public string? PondName { get; set; }

        public decimal? Size { get; set; }

        public decimal? Depth { get; set; }

        public decimal? Volume { get; set; }

        public decimal? WaterDischargeRate { get; set; }

        public decimal? PumpCapacity { get; set; }

        public int? UserId { get; set; }

        public DateTime? MeasurementDate { get; set; }

        public decimal? Temperature { get; set; }

        public decimal? Salinity { get; set; }

        public decimal? PH { get; set; }

        public decimal? Oxygen { get; set; }

        public decimal? No2 { get; set; }

        public decimal? No3 { get; set; }

        public decimal? Po4 { get; set; }

        public DateTime? CalculationDate { get; set; }

        public decimal? SaltAmount { get; set; }

        public string? Notes { get; set; }


    }
}
