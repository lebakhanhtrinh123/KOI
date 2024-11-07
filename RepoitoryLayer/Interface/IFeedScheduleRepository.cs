using BusinessLayer.Entity;
using BusinessLayer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Interface
{
    public interface IFeedScheduleRepository
    {
        Task<bool> CreateFeedSchedule(FeedSchedule feedSchedule);
        Task<bool> DeleteFeedSchedule(int feedScheduleId);
        Task<List<FeedScheduleResponse>> GetFeedScheduleByKoiGrowthID(int koiGrowthID);
    }
}
