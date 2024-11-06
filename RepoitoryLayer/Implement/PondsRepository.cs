using BusinessLayer.Entity;
using BusinessLayer.Request;
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

       

        public async Task<bool> DeletePond(int id)
        {
            var pond = await _context.Ponds
                        .Include(o => o.SaltCalculations)
                        .Include(n => n.WaterParameters)
                        .FirstOrDefaultAsync(n => n.PondId == id);
            if (pond == null)
                return false;
            _context.SaltCalculations.RemoveRange(pond.SaltCalculations);
            _context.WaterParameters.RemoveRange(pond.WaterParameters);
            _context.Ponds.Remove(pond);

            await _context.SaveChangesAsync();
            return true;

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
        public async Task<bool> CreatePonds(int userId, PondsRequest pondsRequest)
        {
            Pond pond = new Pond
            {
                PondName = pondsRequest.PondName,
                Size = pondsRequest.Size,
                Depth = pondsRequest.Depth,
                Volume = pondsRequest.Volume,
                WaterDischargeRate = pondsRequest.WaterDischargeRate,
                PumpCapacity = pondsRequest.PumpCapacity,
                UserId = userId,
            };
            _context.Ponds.Add(pond);
            _context.SaveChanges();
            SaltCalculation saltCalculation = new SaltCalculation
            {
                PondId = pond.PondId,
                CalculationDate = pondsRequest.CalculationDate,
                SaltAmount = pondsRequest.SaltAmount,
                Notes = pondsRequest.Notes,
            };
            WaterParameter waterParameter = new WaterParameter
            {
                PondId = pond.PondId,
                MeasurementDate = pondsRequest.MeasurementDate,
                Temperature = pondsRequest.Temperature,
                Salinity = pondsRequest.Salinity,
                PH = pondsRequest.PH,
                Oxygen = pondsRequest.Oxygen,
                No2 = pondsRequest.No2,
                No3 = pondsRequest.No3,
                Po4 = pondsRequest.Po4,
            };
            await _context.SaltCalculations.AddAsync(saltCalculation);
            await _context.WaterParameters.AddAsync(waterParameter);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePond(int  pondId , PondsRequest pondsRequest)
        {
            var pond = await _context.Ponds
                .Include(o => o.SaltCalculations)
                 .Include(n => n.WaterParameters)
               .FirstOrDefaultAsync(n => n.PondId == pondId);

            pond.PondName = pondsRequest.PondName;
            pond.Size = pondsRequest.Size;
            pond.Depth = pondsRequest.Depth;
            pond.Volume = pondsRequest.Volume;
            pond.WaterDischargeRate = pondsRequest.WaterDischargeRate;
            pond.PumpCapacity = pondsRequest.PumpCapacity;
            var salt = await _saltCalculationsRepository.GetSaltCalculationsByPondID(pondId);
            var waterParameter = await _waterParametersRepository.GetWaterParametersByPondID(pondId);
            if (salt != null)
            {
                salt.CalculationDate = pondsRequest.CalculationDate;
                salt.SaltAmount = pondsRequest.SaltAmount;
                salt.Notes = pondsRequest.Notes;
            }
            else
            {
                salt = new SaltCalculation
                {
                    PondId = pond.PondId,
                    CalculationDate = pondsRequest.CalculationDate,
                    SaltAmount = pondsRequest.SaltAmount,
                    Notes = pondsRequest.Notes
                };
                await _context.SaltCalculations.AddAsync(salt);
            }
            if (waterParameter != null)
            {
                waterParameter.MeasurementDate = pondsRequest.MeasurementDate;
                waterParameter.Temperature = pondsRequest.Temperature;
                waterParameter.Salinity = pondsRequest.Salinity;
                waterParameter.PH = pondsRequest.PH;
                waterParameter.Oxygen = pondsRequest.Oxygen;
                waterParameter.No2 = pondsRequest.No2;
                waterParameter.No3 = pondsRequest.No3;
                waterParameter.Po4 = pondsRequest.Po4;
            }
            else
            {
                waterParameter = new WaterParameter
                {
                    PondId = pond.PondId,
                    MeasurementDate = pondsRequest.MeasurementDate,
                    Temperature = pondsRequest.Temperature,
                    Salinity = pondsRequest.Salinity,
                    PH = pondsRequest.PH,
                    Oxygen = pondsRequest.Oxygen,
                    No2 = pondsRequest.No2,
                    No3 = pondsRequest.No3,
                    Po4 = pondsRequest.Po4
                };
                await _context.WaterParameters.AddAsync(waterParameter);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
