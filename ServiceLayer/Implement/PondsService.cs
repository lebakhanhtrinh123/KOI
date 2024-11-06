using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using RepoitoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implement
{
    public class PondsService : IPondsService
    {
        private readonly IPondsRepository pondsRepository;

        public PondsService(IPondsRepository pondsRepository)
        {
            this.pondsRepository = pondsRepository;
        }

        public async Task<string> CreatePonds(int userId, PondsRequest pondsRequest)
        {
            bool createpond = await  pondsRepository.CreatePonds(userId,pondsRequest);
            if (createpond == true)
            {
                return "Create Ponds Succesfully";
            }
            return "Create Ponds Fail";
           
        }

     

        public async Task<List<PondsResponse>> GetAllPonds()
        {
            return await pondsRepository.GetAllPonds();
        }

        public async Task<List<PondsResponse>> GetAllPondsByUserId(int userId)
        {
            return await pondsRepository.GetAllPondsByUserId(userId);
        }

        public async Task<PondsResponse> GetPondsById(int id)
        {
            return await pondsRepository.GetPondsById(id);
        }

        public async Task<string> UpdatePonds(int ponds, PondsRequest pondsRequest)
        {
           bool updatePond = await pondsRepository.UpdatePond(ponds, pondsRequest);
            if(!updatePond == true)
            {
                return "update pond succesfully";
            }
            return "fail";
        }
        public async Task<bool> DeletePonds(int id)
        {
            bool deletePond = await pondsRepository.DeletePond(id);
            if (deletePond == true)
            {
                return true;
            }
            return false;
        }
    }
}
