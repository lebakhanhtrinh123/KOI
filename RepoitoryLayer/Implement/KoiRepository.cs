using Azure;
using BusinessLayer.Entity;
using BusinessLayer.Response;
using Microsoft.EntityFrameworkCore;
using RepoitoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
    }
}
