using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using BusinessLayer.ViewModel;
using RepoitoryLayer.Implement;
using RepoitoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace ServiceLayer.Implement
{
    public class FeedScheduleService : IFeedScheduleService
    {
        private readonly IKoiGrowthRepository koiGrowthRepository;
        private readonly IFeedScheduleRepository feedScheduleRepository;

        public FeedScheduleService(IKoiGrowthRepository koiGrowthRepository, IFeedScheduleRepository feedScheduleRepository)
        {
            this.koiGrowthRepository = koiGrowthRepository;
            this.feedScheduleRepository = feedScheduleRepository;
        }

        public async Task<FeedScheduleModel> CaculatorFeed(int KoiGrowthId)
        {
            var koiGrowth = await koiGrowthRepository.GetKoiGrowthById(KoiGrowthId);
            if (koiGrowth == null)
            {
                return new FeedScheduleModel
                {
                    messsage = "Không tìm thấy thông tin tăng trưởng cá."
                };
            }
            decimal koiWeight = koiGrowth.Weight ?? 0;

            decimal? feedAmount = CalculateFeedAmount(koiGrowth.Weight);

            DateTime feedDate = DateTime.Now;

            string notes = $"Lượng thức ăn cho cá là {feedAmount} gram. Bạn có thể chia nhỏ làm 3 : buổi sáng {feedAmount/3} , trưa {feedAmount / 3} , tối {feedAmount / 3}";

            return new FeedScheduleModel
            {
                FeedDate = feedDate,
                FeedAmount = feedAmount,
                Notes = notes,
                messsage = "Tính toán lịch cho ăn thành công."
            };

        }
        public decimal? CalculateFeedAmount(decimal? weight)
        {
            decimal? feedRate = 0.015m;
            return weight * feedRate;
        }

        public async Task<string> CreateFeedSchedule(int KoiGrowthID, FeedScheduleRequest feedScheduleRequest)
        {
            FeedSchedule feedSchedule = new FeedSchedule
            {
                FeedDate = feedScheduleRequest.FeedDate,
                FeedAmount = feedScheduleRequest.FeedAmount,
                Notes =feedScheduleRequest.Notes,
                KoiGrowth = KoiGrowthID,
            };
            bool createFeedSchedule = await feedScheduleRepository.CreateFeedSchedule(feedSchedule);
            if (createFeedSchedule == true) {
                return "create FeedSchedule successfully";
            }
            return "create FeedSchedule fail";

        }

        public async Task<bool> DeleteFeedSchedule(int FeedScheduleId)
        {
            bool delete = await feedScheduleRepository.DeleteFeedSchedule(FeedScheduleId);
            if (delete == true) { return true; }
            return false;
        }

        public async Task<List<FeedScheduleResponse>> GetFeedScheduleByKoiGrowthID(int KoiGrowthID)
        {
            return await feedScheduleRepository.GetFeedScheduleByKoiGrowthID(KoiGrowthID);  
        }
    }
}
