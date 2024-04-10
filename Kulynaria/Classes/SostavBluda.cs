using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulynaria.Classes
{
    public class SostavBluda
    {
        public int Id_Bluda { get; set; }
        public string ProductName { get; set; }
        public int Weight { get; set; }

        public SostavBluda(int id_Bluda, string productName, int weight)
        {
            Id_Bluda = id_Bluda;
            ProductName = productName;
            Weight = weight;
        }
    }
}
