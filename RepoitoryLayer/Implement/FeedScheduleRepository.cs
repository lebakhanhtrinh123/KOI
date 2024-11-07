using BusinessLayer.Entity;
using BusinessLayer.Request;
using Microsoft.EntityFrameworkCore;
using RepoitoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace RepoitoryLayer.Implement
{
    public class FeedScheduleRepository : IFeedScheduleRepository
    {
        private readonly KoiContext _context;

        public FeedScheduleRepository(KoiContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateFeedSchedule(FeedSchedule feedSchedule)
        {
            await _context.FeedSchedules.AddAsync(feedSchedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeedSchedule(int feedScheduleId)
        {
            var delete = await _context.FeedSchedules.FirstOrDefaultAsync(n => n.FeedId == feedScheduleId);
            _context.FeedSchedules.Remove(delete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FeedScheduleResponse>> GetFeedScheduleByKoiGrowthID(int koiGrowthID)
        {
            var feedSchedules = await _context.FeedSchedules.Where(n=>n.KoiGrowth == koiGrowthID).ToListAsync();
            var feedSchedulesAsync = feedSchedules.Select(feedSchedule => new FeedScheduleResponse
            {
                FeedDate = feedSchedule.FeedDate,
                FeedAmount = feedSchedule.FeedAmount,
                Notes = feedSchedule.Notes,
            }).ToList();
            return feedSchedulesAsync;
        }
    }
}
