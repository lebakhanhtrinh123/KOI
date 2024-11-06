using BusinessLayer.Request;
using BusinessLayer.ViewModel;
using ServiceLayer.Interface;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Implement
{
    public class SaltCalculationService : ISaltCalculationService
    {
        public async Task<AmountSaltModel> CalculateSaltAmount(CaculatorSaltRequest request)
        {
            decimal saltAmount = request.Volume * request.SaltConcentration;

            if (request.WaterChangeRate.HasValue)
            {
                saltAmount *= request.WaterChangeRate.Value;
            }

            if (!string.IsNullOrEmpty(request.PondCondition) && request.PondCondition.ToLower() == "disease")
            {
                saltAmount *= 1.5m;
            }

            if (request.Temperature.HasValue)
            {
                if (request.Temperature > 28)
                {
                    saltAmount *= 1.1m;
                }
                else if (request.Temperature < 20)
                {
                    saltAmount *= 0.9m; 
                }
            }

            if (request.PhLevel.HasValue)
            {
                if (request.PhLevel < 6.5m)
                {
                    saltAmount *= 0.95m; 
                }
                else if (request.PhLevel > 8)
                {
                    saltAmount *= 1.05m; 
                }
            }
            decimal saltAmountInKg = saltAmount; 
            decimal saltAmountInMg = saltAmount * 1000000; 

            // Đặt kết quả với đơn vị vào trong model
            string saltAmountWithUnit = $"{saltAmountInKg:F2} kg"; 

            return new AmountSaltModel
            {
                SaltAmount = saltAmountInKg,
                SaltAmountWithUnit = saltAmountWithUnit 
            };
        }
    }
}
