using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_final_beca
{
    public partial class Loggin : Form
    {
        public Loggin()
        {
            InitializeComponent();
        }
        SqlConnection conexion = new SqlConnection(
            @"Server=DESKTOP-0S5QCQQ; Database=GestionBecas; Integrated Security=True");

        private void Loggin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();

            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion: " + ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtPassword.Text.Trim();

            if (usuario == "" || clave == "")
            {
                MessageBox.Show("Debe Ingresar usuario y contrasena.", "Atencion",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("Select * from Loggin WHERE usuario='" + usuario + "'AND password='" + clave + "'", conexion);
                SqlDataReader Lector = cmd.ExecuteReader();

                if (Lector.Read())
                {
                    MessageBox.Show("Acceso concedido. Bienvenido " + usuario + "!");

                    SeleccionTablas frmSeleccion = new SeleccionTablas();
                    frmSeleccion.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrectos. ", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }

            finally
            {
                conexion.Close();
            }
        }




        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
