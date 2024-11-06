using System;
using System.Collections.Generic;

namespace BusinessLayer.Entity;

public partial class Pond
{
    public int PondId { get; set; }

    public string? PondName { get; set; }

    public decimal? Size { get; set; }

    public decimal? Depth { get; set; }

    public decimal? Volume { get; set; }

    public decimal? WaterDischargeRate { get; set; }

    public decimal? PumpCapacity { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();

    public virtual ICollection<SaltCalculation> SaltCalculations { get; set; } = new List<SaltCalculation>();

    public virtual User? User { get; set; }

    public virtual ICollection<WaterParameter> WaterParameters { get; set; } = new List<WaterParameter>();
}
