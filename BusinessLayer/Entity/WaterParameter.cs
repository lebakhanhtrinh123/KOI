using System;
using System.Collections.Generic;

namespace BusinessLayer.Entity;

public partial class WaterParameter
{
    public int ParameterId { get; set; }

    public int? PondId { get; set; }

    public DateTime? MeasurementDate { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? Salinity { get; set; }

    public decimal? PH { get; set; }

    public decimal? Oxygen { get; set; }

    public decimal? No2 { get; set; }

    public decimal? No3 { get; set; }

    public decimal? Po4 { get; set; }
}
