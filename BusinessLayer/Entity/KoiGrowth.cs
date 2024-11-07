using System;
using System.Collections.Generic;

namespace BusinessLayer.Entity;

public partial class KoiGrowth
{
    public int GrowthId { get; set; }

    public int? KoiId { get; set; }

    public DateOnly? GrowthDate { get; set; }

    public decimal? Size { get; set; }

    public decimal? Weight { get; set; }

    public string? Notes { get; set; }


    public virtual ICollection<FeedSchedule> FeedSchedules { get; set; } = new List<FeedSchedule>();

    public virtual KoiFish? Koi { get; set; }
}
