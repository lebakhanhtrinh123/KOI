using System;
using System.Collections.Generic;

namespace BusinessLayer.Entity;

public partial class KoiFish
{
    public int KoiId { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public int? Age { get; set; }

    public decimal? Size { get; set; }

    public decimal? Weight { get; set; }

    public string? Gender { get; set; }

    public string? Breed { get; set; }

    public string? Origin { get; set; }

    public decimal? Price { get; set; }

    public int? PondId { get; set; }

    public virtual Pond? Pond { get; set; }
}
