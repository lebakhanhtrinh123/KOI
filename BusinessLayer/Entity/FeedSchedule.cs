using System;
using System.Collections.Generic;

namespace BusinessLayer.Entity;

public partial class FeedSchedule
{
    public int FeedId { get; set; }

    public int? KoiId { get; set; }

    public DateTime? FeedDate { get; set; }

    public decimal? FeedAmount { get; set; }

    public string? Notes { get; set; }

    public int? KoiGrowth { get; set; }

    public virtual KoiFish? Koi { get; set; }

    public virtual KoiGrowth? KoiGrowthNavigation { get; set; }
}
