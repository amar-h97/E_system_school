using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpIntroWinForms.P10
{
    public class Genders
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name; 
        }
    }
}
