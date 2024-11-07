using Azure;
using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using RepoitoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceLayer.Implement
{
    public class KoiService : IKoiService
    {
        private readonly IKoiRepository koiRepository;

        public KoiService(IKoiRepository koiRepository)
        {
            this.koiRepository = koiRepository;
        }

        public async Task<string> CreateKoiFish(int pondId, KoiFishRequest koiFishRequest)
        {
            var Koi = new KoiFish
            {
                Name = koiFishRequest.Name,
                Image = koiFishRequest?.Image,
                Age = koiFishRequest?.Age,
                Size = koiFishRequest?.Size,
                Weight = koiFishRequest?.Weight,
                Gender = koiFishRequest?.Gender,
                Breed = koiFishRequest?.Breed,
                Origin = koiFishRequest?.Origin,
                Price = koiFishRequest?.Price,
                PondId = pondId,
            };
            bool creatKoi = await koiRepository.CreateKoiFish(Koi);
            if (creatKoi == true) {
                return "Create Koi successfully";
            }
            return "Create Koi fail";

        }

        public async Task<bool> DeleteKoiFish(int id)
        {
            bool delete =  await koiRepository.DeleteKoiFish(id);
            if(delete == true) { return true; }
            return false;

        }

        public async Task<KoiFishResponse> GetKoiFishById(int id)
        {
            return await koiRepository.GetKoiFishById(id);
        }

        public async Task<List<KoiFishResponse>> GetKoiFishByPondId(int Pondid)
        {
            return await koiRepository.GetKoiFishByPondId(Pondid);
        }

        public async Task<string> UpdateKoiFish(int id, KoiFishRequest koiFishRequest)
        {
            bool updateKoi = await koiRepository.UpdateKoi(id, koiFishRequest);
            if (updateKoi == true)
            {
                return "update koi successfully";
            }
            return "update koi fail";
        }
    }
}
