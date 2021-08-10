using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpIntroWinForms.P10
{
    public class UsersSubjects
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Subjects Subject { get; set; }
        public int Grade { get; set; }
        public string Date { get; set; }

    }
}
