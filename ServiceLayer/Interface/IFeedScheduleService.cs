using BusinessLayer.Request;
using BusinessLayer.Response;
using BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IFeedScheduleService
    {
        Task<FeedScheduleModel> CaculatorFeed(int KoiGrowthId);
        Task<List<FeedScheduleResponse>> GetFeedScheduleByKoiGrowthID(int KoiGrowthID);
        Task<string> CreateFeedSchedule(int  KoiGrowthID, FeedScheduleRequest feedScheduleRequest);
        Task<bool> DeleteFeedSchedule(int FeedScheduleId);



    }
}
