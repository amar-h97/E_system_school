using cSharpIntroWinForms.P10;
using cSharpIntroWinForms.P11;
using cSharpIntroWinForms.P9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.P8
{
    public partial class UsersPassedSubjects : Form
    {
        private User user;

        ConnectionOnBase connectionToDatebase = DLWMS.DB;

        public UsersPassedSubjects()
        {
            InitializeComponent();
            dgvPassedSubjects.AutoGenerateColumns = false;
        }

        public UsersPassedSubjects(User user) : this()
        {
            this.user = user;
        }

        private void UsersPassedSubjects_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadGrades();
            LoadPassedSubjects();

        }

        private void LoadSubjects(List<Subjects> result = null)
        {
            try
            {
                if (result != null)
                {
                    cmbSubjects.DataSource = result;

                }
                else
                {
                    cmbSubjects.DataSource = connectionToDatebase.Subjects.ToList();

                }
                cmbSubjects.DisplayMember = "Name";
                cmbSubjects.ValueMember = "Id";

            }
            catch (Exception ex)
            {
                MboxHelper.ShowError(ex);
            }

        }
        private void LoadGrades()
        {
            List<int> grade = new List<int>()
                {
                    6,
                    7,
                    8,
                    9,
                    10
                };

            cmbGrades.DataSource = grade;
        }
        private void LoadPassedSubjects()
        {
            dgvPassedSubjects.DataSource = null;
            dgvPassedSubjects.DataSource = user.Success;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
     
            try
            {
                int grade = int.Parse(cmbGrades.SelectedItem.ToString());
                if (grade < 6 || grade > 10)
                    throw new Exception("The grade is not correct!");

                Subjects selectedSubject = cmbSubjects.SelectedItem as Subjects;
                CheckingExistSubject(selectedSubject);

                UsersSubjects passedSubjects = new UsersSubjects();
                passedSubjects.Subject = selectedSubject;
                passedSubjects.Grade = grade;
                passedSubjects.Date = dtpDate.Value.ToString("dd.MM.yyyy");
                user.Success.Add(passedSubjects);
                connectionToDatebase.SaveChanges();

                LoadPassedSubjects();

            }
            catch (Exception ex)
            {
                MboxHelper.ShowError(ex);
            }
        }

        private void CheckingExistSubject(Subjects selectedSubject)
        {
            if (user.Success.Where(x => x.Subject.Id == selectedSubject.Id).Count() > 0)
                throw new Exception($"Subject {selectedSubject} is already evident to user {user}");
        }

        private void cbLoadUnpassed_CheckedChanged(object sender, EventArgs e)
        {

            if (cbLoadUnpassed.Checked)
            {

                
                var query = from c in connectionToDatebase.Subjects.AsEnumerable()
                            where !(from o in user.Success
                                    select o.Subject.Id)
                                   .Contains(c.Id)
                            select c;

                LoadSubjects(query.ToList());

            }
            else
                LoadSubjects();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var forma = new Reports(user);
            forma.ShowDialog();
        }

  
        private void btnASYNC_Click(object sender, EventArgs e)
        {

            
            Subjects selectedSubject = cmbSubjects.SelectedItem as Subjects;

            var AddSubjectTask = Task.Run(() =>
            {
                try
                {
                    for (int i = 0; i < 500; i++)
                    {

                        UsersSubjects kp = new UsersSubjects();

                        kp.Subject = selectedSubject;
                        kp.Grade = 6;
                        kp.Date = DateTime.Now.ToString("dd.MM.yyyy");

                       
                        user.Success.Add(kp);

                      
                        connectionToDatebase.SaveChanges();
                    }
                } catch (Exception ex)
                {
                    MboxHelper.ShowError(ex);
                }
            });

            var cekanje = AddSubjectTask.GetAwaiter();
            cekanje.OnCompleted(() => {
                MessageBox.Show("500 subjects were successfully added");
             
                LoadPassedSubjects();
            });
        }

      
    }
}
