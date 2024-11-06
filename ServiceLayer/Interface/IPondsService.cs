﻿using BusinessLayer.Request;
using BusinessLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IPondsService
    {
        Task<List<PondsResponse>> GetAllPonds();
        Task<PondsResponse> GetPondsById(int id);
        Task<string> CreatePonds(PondsRequest pondsRequest);
        Task<bool> DeletePonds(int id);    
    }
}
