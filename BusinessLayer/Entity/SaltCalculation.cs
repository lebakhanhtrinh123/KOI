using System;
using System.Collections.Generic;

namespace BusinessLayer.Entity;

public partial class SaltCalculation
{
    public int SaltId { get; set; }

    public int? PondId { get; set; }

    public DateTime? CalculationDate { get; set; }

    public decimal? SaltAmount { get; set; }

    public string? Notes { get; set; }

    public virtual Pond? Pond { get; set; }
}
