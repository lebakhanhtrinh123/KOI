using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using Microsoft.EntityFrameworkCore;
using RepoitoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Implement
{
    public class KoiGrowthRepository : IKoiGrowthRepository
    {
        private readonly KoiContext _context;

        public KoiGrowthRepository(KoiContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateKoiGrowth(KoiGrowth koiGrowth)
        {
            await _context.KoiGrowths.AddAsync(koiGrowth);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteKoiGrowth(int koiGrowthId)
        {
            var koiGrowth = await _context.KoiGrowths
                  .Include(m => m.FeedSchedules)  
                  .FirstOrDefaultAsync(n => n.GrowthId == koiGrowthId); 
            _context.FeedSchedules.RemoveRange(koiGrowth.FeedSchedules);

            _context.KoiGrowths.Remove(koiGrowth);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<KoiGrowth> GetKoiGrowthById(int koiId)
        {
            var koiGrowth = await _context.KoiGrowths
                  .FirstOrDefaultAsync(n => n.GrowthId == koiId);
            return koiGrowth;
        }

        public async Task<List<KoiGrowthResponse>> GetKoiGrowths(int koiId)
        {
            var koiGrowths = await _context.KoiGrowths
                .Where(n => n.KoiId == koiId)  
                .ToListAsync();

            var koi = koiGrowths.Select(koiGrowth => new KoiGrowthResponse
            {
                GrowthId = koiGrowth.GrowthId,
                GrowthDate = koiGrowth.GrowthDate,
                Size = koiGrowth.Size,
                Weight = koiGrowth.Weight,
                Notes = koiGrowth.Notes
            })
            .OrderBy(koiGrowth =>
            {
                DateTime growthDateTime = koiGrowth.GrowthDate?.ToDateTime(new TimeOnly()) ?? DateTime.MaxValue;
                return Math.Abs((growthDateTime - DateTime.Now).TotalDays);  
            })
            .ToList();

            return koi;
        }

    }
}
