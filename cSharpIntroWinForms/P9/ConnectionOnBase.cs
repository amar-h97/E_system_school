using cSharpIntroWinForms.P10;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpIntroWinForms.P9
{
    public class ConnectionOnBase : DbContext
    {
        public ConnectionOnBase(): base("PutanjaDoBaze")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genders> Genders { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<UsersSubjects> UsersSubjects { get; set; }




    }

    [Table("Students")]
    public class Students
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

}
