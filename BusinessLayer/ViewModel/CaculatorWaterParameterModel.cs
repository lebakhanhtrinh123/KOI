using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModel
{
    public class CaculatorWaterParameterModel
    {
        public decimal? Temperature { get; set; }
        public string TemperatureUnit { get; set; } = "°C";  // Đơn vị Nhiệt độ

        public decimal? Salinity { get; set; }
        public string SalinityUnit { get; set; } = "ppt";  // Đơn vị độ mặn (parts per thousand)

        public decimal? PH { get; set; }
        public string PHUnit { get; set; } = "pH";  // Đơn vị pH

        public decimal? Oxygen { get; set; }
        public string OxygenUnit { get; set; } = "mg/L";  // Đơn vị Oxy (mg/L)

        public decimal? No2 { get; set; }
        public string No2Unit { get; set; } = "mg/L";  // Đơn vị NO2 (mg/L)

        public decimal? No3 { get; set; }
        public string No3Unit { get; set; } = "mg/L";  // Đơn vị NO3 (mg/L)

        public decimal? Po4 { get; set; }
        public string Po4Unit { get; set; } = "mg/L";  // Đơn vị PO4 (mg/L)
    }
}
