using cSharpIntroWinForms.P10;
using cSharpIntroWinForms.P11;
using cSharpIntroWinForms.P8;
using cSharpIntroWinForms.P9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms
{
    public partial class UsersAdmin : Form
    {

        ConnectionOnBase ConnectionToDatebase = DLWMS.DB;

        public UsersAdmin()
        {
            InitializeComponent();
            dgvUsers.AutoGenerateColumns = false;
        }

        private void UsersAdmin_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void CalculateAverage(List<User> results = null)
        {
            try
            {
                
                int usersWithGrades = 0;
                double SumOfGrades = 0; 

                if (results?.Count() > 0)
                {

                    foreach (var rr in results)
                    {
                        double tempSum = 0;

                        tempSum += rr.Success.Sum(x => x.Grade); 
                        if (tempSum == 0) 
                            continue;

                        tempSum /= rr.Success.Count();  
                        SumOfGrades += tempSum; 
                        usersWithGrades++;
                    }
                }

                if (usersWithGrades > 0)
                    lblAverage.Text = (SumOfGrades / usersWithGrades).ToString("0.##"); 
                else
                    lblAverage.Text = "0";

            } catch(Exception ex)
            {
                MboxHelper.ShowError(ex);
            }
               

        }
        private void LoadData(List<User> users = null)
        {
            try
            {
                List<User> results = users ?? ConnectionToDatebase.Users.ToList();

                dgvUsers.DataSource = null;
                dgvUsers.DataSource = results;


                CalculateAverage(results);
            }
            catch (Exception ex)
            {
                MboxHelper.ShowError(ex);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.ToLower();

            List<User> result = ConnectionToDatebase.Users.ToList().Where(x => x.Name.ToLower().Contains(filter) || x.Surname.ToLower().Contains(filter)).ToList();

            LoadData(result);
            
        }


        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            User user = dgvUsers.SelectedRows[0].DataBoundItem as User;
            Form forma = null;
            if (user != null)
            {
                if (e.ColumnIndex == 5)
                {
                    forma = new UsersPassedSubjects(user);
                    forma.ShowDialog();
                    LoadData();
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            

            ConnectionToDatebase.UsersSubjects.RemoveRange(ConnectionToDatebase.UsersSubjects.ToList());
            LoadData();
            ConnectionToDatebase.SaveChanges();

            MessageBox.Show("Successfully deleted all subjects from the database!");
        }
    }
}
