using cSharpIntroWinForms.P10;
using cSharpIntroWinForms.P8;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpIntroWinForms
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public byte[] Image { get; set; }

        public virtual Genders Gender { get; set; }
      
        public bool Admin { get; set; }
        public List<PassedSubjects> Passed { get; set; } = new List<PassedSubjects>();
        public virtual List<UsersSubjects> Success { get; set; } = new List<UsersSubjects>();


        public override string ToString()
        {
            return $"{Name} {Surname} ({Username})";
        }
    }
}
