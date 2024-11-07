using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using BusinessLayer.ViewModel;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<bool> UpdatePond(int pondId, PondsRequest pondsRequest)
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

        public async Task<TestWaterParameterModel> TestWaterParameter(int ponid)
        {
            var pond = await _context.Ponds
                    .Include(o => o.SaltCalculations)
                     .Include(n => n.WaterParameters)
                   .FirstOrDefaultAsync(n => n.PondId == ponid);
            var waterParameter = await _waterParametersRepository.GetWaterParametersByPondID(ponid);
            if (waterParameter.Temperature != CalculateTemperature(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity)
                 || waterParameter.PH != CalculatePH(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity)
                    || waterParameter.Oxygen != CalculateOxygen(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity)
                     || waterParameter.No2 != CalculateNO2(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity)
                      || waterParameter.No3 != CalculateNO3(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity)
                       || waterParameter.Po4 != CalculatePO4(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity))
            {
                return new TestWaterParameterModel()
                {
                    message = "thông số không phù hợp",
                    model = new CaculatorWaterParameterModel()
                    {
                        Temperature = CalculateTemperature(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity),
                        TemperatureUnit = "°C",  // Thêm đơn vị Nhiệt độ

                        PH = CalculatePH(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity),
                        PHUnit = "pH",  // Thêm đơn vị pH

                        Oxygen = CalculateOxygen(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity),
                        OxygenUnit = "mg/L",  // Đơn vị Oxy

                        No2 = CalculateNO2(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity),
                        No2Unit = "mg/L",  // Đơn vị NO2

                        No3 = CalculateNO3(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity),
                        No3Unit = "mg/L",  // Đơn vị NO3

                        Po4 = CalculatePO4(pond.Size, pond.Depth, pond.Volume, pond.WaterDischargeRate, pond.PumpCapacity),
                        Po4Unit = "mg/L"
                    }

                };
            }
            return new TestWaterParameterModel()
            {
                message = "Thông số phù hợp",
            };
        }
        public decimal? CalculateTemperature(decimal? size, decimal? depth, decimal? volume, decimal? waterDischargeRate, decimal? pumpCapacity)
        {
            decimal baseTemperature = 25m; 

            decimal? depthFactor = 1 - (depth * 0.01m);
            decimal? sizeFactor = 1 + (size * 0.005m);
            decimal? pumpFactor = 1 + (pumpCapacity * 0.01m);
            decimal? dischargeFactor = 1 - (waterDischargeRate * 0.02m);

            decimal? calculatedTemperature = baseTemperature * depthFactor * sizeFactor * pumpFactor * dischargeFactor;

            calculatedTemperature -= volume * 0.0005m;

            return calculatedTemperature;
        }
        
        public decimal? CalculatePH(decimal? size, decimal? depth, decimal? volume, decimal? waterDischargeRate, decimal? pumpCapacity)
        {
            decimal? basePH = 7.5m; 

            decimal? sizeFactor = 1 + (size * 0.001m); 
            decimal? depthFactor = 1 - (depth * 0.002m); 
            decimal? pumpFactor = 1 + (pumpCapacity * 0.003m);
            decimal? dischargeFactor = 1 - (waterDischargeRate * 0.005m); 

            decimal? calculatedPH = basePH * sizeFactor * depthFactor * pumpFactor * dischargeFactor;

            calculatedPH -= volume * 0.0001m; 

            if (calculatedPH < 6.5m) calculatedPH = 6.5m;
            if (calculatedPH > 8.5m) calculatedPH = 8.5m;

            return calculatedPH;
        }
     
        public decimal? CalculateOxygen(decimal? size, decimal? depth, decimal? volume, decimal? waterDischargeRate, decimal? pumpCapacity)
        {
                decimal? baseOxygen = 6.0m;

                decimal? sizeFactor = 1 + (size * 0.002m);
                decimal? depthFactor = 1 - (depth * 0.001m);
                decimal? pumpFactor = 1 + (pumpCapacity * 0.004m);
                decimal? dischargeFactor = 1 - (waterDischargeRate * 0.003m);

                decimal? calculatedOxygen = baseOxygen * sizeFactor * depthFactor * pumpFactor * dischargeFactor;

                calculatedOxygen -= volume * 0.0002m;

                if (calculatedOxygen < 4.0m) calculatedOxygen = 4.0m;
                if (calculatedOxygen > 12.0m) calculatedOxygen = 12.0m;

                return calculatedOxygen;
        }
        public decimal? CalculateNO2(decimal? size, decimal? depth, decimal? volume, decimal? waterDischargeRate, decimal? pumpCapacity)
        {
            decimal baseNO2 = 0.02m; // Giá trị cơ bản cho NO2 (mg/L)

            decimal? sizeFactor = 1 + (size * 0.0005m);
            decimal? depthFactor = 1 - (depth * 0.001m);
            decimal? pumpFactor = 1 + (pumpCapacity * 0.002m);
            decimal? dischargeFactor = 1 - (waterDischargeRate * 0.003m);

            decimal? calculatedNO2 = baseNO2 * sizeFactor * depthFactor * pumpFactor * dischargeFactor;

            calculatedNO2 += volume * 0.00001m;

            if (calculatedNO2 < 0.01m) calculatedNO2 = 0.01m; // Giới hạn tối thiểu
            if (calculatedNO2 > 0.05m) calculatedNO2 = 0.05m; // Giới hạn tối đa

            return calculatedNO2;
        }

        public decimal? CalculateNO3(decimal? size, decimal? depth, decimal? volume, decimal? waterDischargeRate, decimal? pumpCapacity)
        {
            decimal baseNO3 = 20.0m; // Giá trị cơ bản cho NO3 (mg/L)

            decimal? sizeFactor = 1 + (size * 0.001m);
            decimal? depthFactor = 1 - (depth * 0.0008m);
            decimal? pumpFactor = 1 + (pumpCapacity * 0.0025m);
            decimal? dischargeFactor = 1 - (waterDischargeRate * 0.004m);

            decimal? calculatedNO3 = baseNO3 * sizeFactor * depthFactor * pumpFactor * dischargeFactor;

            calculatedNO3 -= volume * 0.0005m;

            if (calculatedNO3 < 10.0m) calculatedNO3 = 10.0m; // Giới hạn tối thiểu
            if (calculatedNO3 > 40.0m) calculatedNO3 = 40.0m; // Giới hạn tối đa

            return calculatedNO3;
        }
        public decimal? CalculatePO4(decimal? size, decimal? depth, decimal? volume, decimal? waterDischargeRate, decimal? pumpCapacity)
        {
            decimal basePO4 = 0.5m; // Giá trị cơ bản cho PO4 (mg/L)

            decimal? sizeFactor = 1 + (size * 0.0003m);
            decimal? depthFactor = 1 - (depth * 0.0005m);
            decimal? pumpFactor = 1 + (pumpCapacity * 0.0015m);
            decimal? dischargeFactor = 1 - (waterDischargeRate * 0.002m);

            decimal? calculatedPO4 = basePO4 * sizeFactor * depthFactor * pumpFactor * dischargeFactor;

            calculatedPO4 -= volume * 0.00001m;

            if (calculatedPO4 < 0.1m) calculatedPO4 = 0.1m; // Giới hạn tối thiểu
            if (calculatedPO4 > 1.0m) calculatedPO4 = 1.0m; // Giới hạn tối đa

            return calculatedPO4;
        }

    }
}

