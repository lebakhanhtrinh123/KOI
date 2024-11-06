using BusinessLayer.Entity;
using BusinessLayer.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RepoitoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace RepoitoryLayer.Implement
{
    public class PondsRepository : IPondsRepository
    {
        private readonly KoiContext _context;
        private readonly ISaltCalculationsRepository _saltCalculationsRepository;
        private readonly IWaterParametersRepository _waterParametersRepository;

        public PondsRepository(KoiContext context, ISaltCalculationsRepository saltCalculationsRepository, IWaterParametersRepository waterParametersRepository)
        {
            _context = context;
            _saltCalculationsRepository = saltCalculationsRepository;
            _waterParametersRepository = waterParametersRepository;
        }

        public async Task<List<PondsResponse>>? GetAllPonds()
        {
            var ponds = await _context.Ponds
                    .Include(o => o.SaltCalculations)
                     .Include(n => n.WaterParameters)
                        .ToListAsync();
            var pondTasks = ponds.Select(async pond =>
            {
                var salt = await _saltCalculationsRepository.GetSaltCalculationsByPondID(pond.PondId);
                var waterParameter = await _waterParametersRepository.GetWaterParametersByPondID(pond.PondId);

                return new PondsResponse
                {
                    PondId = pond.PondId,
                    PondName = pond.PondName,
                    Size = pond.Size,
                    Depth = pond.Depth,
                    Volume = pond.Volume,
                    WaterDischargeRate = pond.WaterDischargeRate,
                    PumpCapacity = pond.PumpCapacity,
                    UserId = pond.UserId,
                    MeasurementDate = waterParameter.MeasurementDate,
                    Temperature = waterParameter.Temperature,
                    Salinity = waterParameter.Salinity,
                    PH = waterParameter.PH,
                    Oxygen = waterParameter.Oxygen,
                    No2 = waterParameter.No2,
                    No3 = waterParameter.No3,
                    Po4 = waterParameter.Po4,
                    CalculationDate = salt.CalculationDate,
                    SaltAmount = salt.SaltAmount,
                    Notes = salt.Notes,
                };
            });

            var pondsResponse = await Task.WhenAll(pondTasks);

            return pondsResponse.ToList();


        }

        public async Task<List<PondsResponse>> GetAllPondsByUserId(int userId)
        {
            var ponds = await _context.Ponds
                    .Include(o => o.SaltCalculations)
                     .Include(n => n.WaterParameters)
                     .Where(n => n.UserId == userId)
                        .ToListAsync();
            var pondTasks = ponds.Select(async pond =>
            {
                var salt = await _saltCalculationsRepository.GetSaltCalculationsByPondID(pond.PondId);
                var waterParameter = await _waterParametersRepository.GetWaterParametersByPondID(pond.PondId);

                return new PondsResponse
                {
                    PondId = pond.PondId,
                    PondName = pond.PondName,
                    Size = pond.Size,
                    Depth = pond.Depth,
                    Volume = pond.Volume,
                    WaterDischargeRate = pond.WaterDischargeRate,
                    PumpCapacity = pond.PumpCapacity,
                    UserId = pond.UserId,
                    MeasurementDate = waterParameter.MeasurementDate,
                    Temperature = waterParameter.Temperature,
                    Salinity = waterParameter.Salinity,
                    PH = waterParameter.PH,
                    Oxygen = waterParameter.Oxygen,
                    No2 = waterParameter.No2,
                    No3 = waterParameter.No3,
                    Po4 = waterParameter.Po4,
                    CalculationDate = salt.CalculationDate,
                    SaltAmount = salt.SaltAmount,
                    Notes = salt.Notes,
                };
            });

            var pondsResponse = await Task.WhenAll(pondTasks);

            return pondsResponse.ToList();
        }

        public async Task<PondsResponse> GetPondsById(int id)
        {
            var ponds = await _context.Ponds
                    .Include(o => o.SaltCalculations)
                     .Include(n => n.WaterParameters)
                   .FirstOrDefaultAsync(n => n.PondId == id);
           
                var salt = await _saltCalculationsRepository.GetSaltCalculationsByPondID(ponds.PondId);
                var waterParameter = await _waterParametersRepository.GetWaterParametersByPondID(ponds.PondId);
                
                return new PondsResponse
                {
                    PondId = ponds.PondId,
                    PondName = ponds.PondName,
                    Size = ponds.Size,
                    Depth = ponds.Depth,
                    Volume = ponds.Volume,
                    WaterDischargeRate = ponds.WaterDischargeRate,
                    PumpCapacity = ponds.PumpCapacity,
                    UserId = ponds.UserId,
                    MeasurementDate = waterParameter.MeasurementDate,
                    Temperature = waterParameter.Temperature,
                    Salinity = waterParameter.Salinity,
                    PH = waterParameter.PH,
                    Oxygen = waterParameter.Oxygen,
                    No2 = waterParameter.No2,
                    No3 = waterParameter.No3,
                    Po4 = waterParameter.Po4,
                    CalculationDate = salt.CalculationDate,
                    SaltAmount = salt.SaltAmount,
                    Notes = salt.Notes,
                };      

        }
    }
}
