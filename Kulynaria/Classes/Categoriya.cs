using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulynaria.Classes
{
    public class Categoriya
    {
        public int Identityb { get; set; }
        public string vids_blud { get; set; }
        public Categoriya(int id, string categoryName)
        {
            Identityb = id;
            vids_blud = categoryName;
        }
    }
}
