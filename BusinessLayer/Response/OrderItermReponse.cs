using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Response
{
    public class OrderItermReponse
    {
        public string productName {  get; set; }
        public string? image {  get; set; }
        public decimal? price {  get; set; }
        public string? quantity {  get; set; }
    }
}
