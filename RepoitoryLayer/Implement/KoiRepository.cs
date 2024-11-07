using Azure;
using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using Microsoft.EntityFrameworkCore;
using RepoitoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RepoitoryLayer.Implement
{
    public class KoiRepository : IKoiRepository
    {
        private readonly KoiContext _context;

        public KoiRepository(KoiContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateKoiFish(KoiFish koiFish)
        {
            await _context.AddAsync(koiFish);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteKoiFish(int id)
        {
            var koifish = await _context.KoiFishes
                .Include(m => m.KoiGrowths)
                .Include(p => p.FeedSchedules)
                .FirstOrDefaultAsync(n => n.KoiId == id);
            _context.KoiGrowths.RemoveRange(koifish.KoiGrowths);
            _context.FeedSchedules.RemoveRange(koifish.FeedSchedules);
            _context.KoiFishes.Remove(koifish);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<KoiFishResponse> GetKoiFishById(int id)
        {
            var koifish = await _context.KoiFishes.FirstOrDefaultAsync(n => n.KoiId == id);
            return new KoiFishResponse()
            {
                KoiId = koifish.KoiId,
                Name = koifish.Name,
                Image = koifish.Image,
                Age = koifish.Age,
                Size = koifish.Size,
                Weight = koifish.Weight,
                Gender = koifish.Gender,
                Breed = koifish.Breed,
                Origin = koifish.Origin,
                Price = koifish.Price,
                PondId = koifish.PondId,
            };
        }

        public async Task<List<KoiFishResponse>> GetKoiFishByPondId(int pondid)
        {
            var koifishs = await _context.KoiFishes
                    .Where(n => n.PondId == pondid)
                    .ToListAsync();
            var koiFishResponses = koifishs.Select(koifish => new KoiFishResponse
            {
                KoiId = koifish.KoiId,
                Name = koifish.Name,
                Image = koifish.Image,
                Age = koifish.Age,
                Size = koifish.Size,
                Weight = koifish.Weight,
                Gender = koifish.Gender,
                Breed = koifish.Breed,
                Origin = koifish.Origin,
                Price = koifish.Price,
                PondId = pondid,
            }).ToList();
            return koiFishResponses;
        }

        public async Task<bool> UpdateKoi(int id, KoiFishRequest koiFishRequest)
        {
            var koifish = await _context.KoiFishes.FirstOrDefaultAsync(n => n.KoiId == id);
            koifish.Name = koiFishRequest.Name ?? koifish.Name;
            koifish.Image = koiFishRequest.Image ?? koifish.Image;
            koifish.Age = koiFishRequest.Age ?? koifish.Age;
            koifish.Size = koiFishRequest.Size ?? koifish.Size;
            koifish.Weight = koiFishRequest.Weight ?? koifish.Weight;
            koifish.Gender = koiFishRequest.Gender ?? koifish.Gender;
            koifish.Breed = koiFishRequest.Breed ?? koifish.Breed;
            koifish.Origin = koiFishRequest.Origin ?? koifish.Origin;
            koifish.Price = koiFishRequest.Price ?? koifish.Price;
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}
