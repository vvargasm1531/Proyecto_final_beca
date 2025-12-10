using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_final_beca
{
    public partial class SeleccionTablas : Form
    {
        public SeleccionTablas()
        {
            InitializeComponent();
        }

        private void btnEstudiante_Click(object sender, EventArgs e)
        {
            Estudiante f1 = new Estudiante();
            f1.Show();
            this.Hide();
        }
    }
}
