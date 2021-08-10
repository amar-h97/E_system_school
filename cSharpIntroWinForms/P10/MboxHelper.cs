using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.P10
{
    public class MboxHelper
    {
        const string error = "Error";
        public static void ShowError(Exception ex)
        {
            MessageBox.Show($"{ex.Message}\n{ex.InnerException?.Message}", 
                error,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
    
}
