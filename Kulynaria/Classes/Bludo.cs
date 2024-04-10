using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulynaria.Classes
{
    public class Bludo
    {
        public int Identityb { get; set; }
        public string bludo { get; set; }
        public string Numbo { get; set; }
        public string Osnova { get; set; }
        public int Vyhod { get; set; }
        string image;

        public string ImageWithPath
        {
            get
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + image;
                if (!String.IsNullOrEmpty(image) && !String.IsNullOrWhiteSpace(image) && File.Exists(path)) { return path; }
                else { return @"..\..\Images\picture.jpg"; }
            }
        }

        public Bludo(int id, string bludoname, string categoriya, string osnova, int vyhod, string image)
        {
            Identityb = id;
            bludo = bludoname;
            Numbo = categoriya;
            Osnova = osnova;
            Vyhod = vyhod;
            this.image = image;
            
        }
    }
}

