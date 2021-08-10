using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cSharpIntroWinForms.P10;
using cSharpIntroWinForms.P9;
using Microsoft.Reporting.WinForms;
namespace cSharpIntroWinForms.P11
{
    public partial class Reports : Form
    {
        private User user;
        ConnectionOnBase connectionToDatebase = DLWMS.DB;
        public Reports()
        {
            InitializeComponent();
        }

        public Reports(User user) : this()
        {
            this.user = user;
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            try
            {
                if (user == null) 
                    return;


               
                    ReportParameterCollection rpc = new ReportParameterCollection();
                    rpc.Add(new ReportParameter("FullName", $"{user.Name} {user.Surname}"));
                    rpc.Add(new ReportParameter("Index", user.Username));
             
                    reportViewer1.LocalReport.SetParameters(rpc);

              
                List<object> list = new List<object>();
                    int i = 1; 
                   
                    foreach (var passed in connectionToDatebase.Subjects)
                    {

                        
                        var data = connectionToDatebase.UsersSubjects.ToList().Where(x => x.Subject.Id == passed.Id && x.User.Id == user.Id).Select(y => new { grade = y.Grade, date = y.Date }).FirstOrDefault();
                        
                        list.Add(new
                        {
                            On = i++,
                            Name = passed.Name,
                            
                           
                            Grade = data?.grade.ToString() ?? "Not passed", 
                            Date = data?.date.ToString() ?? "Not passed"
                        });

                    }


                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "DataSet1";
                    rds.Value = list;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MboxHelper.ShowError(ex);
            }
        }
    

    }
}
