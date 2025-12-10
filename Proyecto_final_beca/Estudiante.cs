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
    public partial class Estudiante : Form
    {
        public Estudiante()
        {
            InitializeComponent();
        }

        SqlConnection conexion = new SqlConnection(
          @"Server=DESKTOP-0S5QCQQ; Database=GestionBecas; Integrated Security=True");
        private void Estudiante_Load(object sender, EventArgs e)
        {
            CargarEstudiante();
        }

        private void Limpiardatos()
        {
            txtIdEstudiante.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            dtFechaNacimiento.Value = DateTime.Today;
            txtCedula.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtCarrera.Clear();
            txtSemestre.Clear();
  
        }
        private void CargarEstudiante()
        {
            string sql = "SELECT id_estudiante, nombre, apellido, fecha_nacimiento, cedula, telefono, correo, direccion, carrera, semestre FROM estudiante";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvEstudiante.DataSource = dt;
                        dgvEstudiante.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los Estudiantes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO estudiante
            (id_estudiante, nombre, apellido, fecha_nacimiento, cedula, telefono, correo, direccion, carrera, semestre)
            VALUES
            (@id_estudiante, @nombre, @apellido, @fecha_nacimiento, @cedula, @telefono, @correo, @direccion, @carrera, @semestre)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_estudiante", txtIdEstudiante.Text.Trim());
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", dtFechaNacimiento.Value.Date);;
                    cmd.Parameters.AddWithValue("cedula", txtCedula.Text.Trim());
                    cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                    cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text.Trim());
                    cmd.Parameters.AddWithValue("@carrera", txtCarrera.Text.Trim());
                    cmd.Parameters.AddWithValue("@semestre", txtSemestre.Text.Trim());

                    conexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Estudiante guardado Correctamente.",
                            "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiardatos();
                        CargarEstudiante();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el Estudiante",
                         "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar al estudiante: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdEstudiante.Text))
            {
                MessageBox.Show("Debe ingresar el ID del Estudiante a eliminar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro que desea eliminar este registro?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes)
                return;

            string sql = @"DELETE FROM estudiante WHERE id_estudiante = @id_estudiante";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_estudiante", txtIdEstudiante.Text.Trim());

                    conexion.Open();
                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Estudiante eliminado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiardatos();
                        CargarEstudiante();   // Actualiza el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un estudiante con ese ID.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar al estudiante: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdEstudiante.Text))
            {
                MessageBox.Show("Debe Ingresar el ID del cliente que desea actualizar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"UPDATE estudiante 
                      SET nombre = @nombre,
                          apellido = @apellido,
                          fecha_nacimiento = @fecha_nacimiento,
                          cedula = @cedula,
                          telefono = @telefono,
                          correo = @correo,
                          direccion = @direccion,
                          carrera = @carrera,
                          semestre = @semestre
                    WHERE id_estudiante = @id_estudiante";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_estudiante", txtIdEstudiante.Text.Trim());
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", dtFechaNacimiento.Value.Date); ;
                    cmd.Parameters.AddWithValue("cedula", txtCedula.Text.Trim());
                    cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                    cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text.Trim());
                    cmd.Parameters.AddWithValue("@carrera", txtCarrera.Text.Trim());
                    cmd.Parameters.AddWithValue("@semestre", txtSemestre.Text.Trim());


                    conexion.Open();
                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Estudiante actualizado correctamente",
                   "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiardatos();
                        CargarEstudiante();

                    }
                    else
                    {
                        MessageBox.Show("No se encontro un estudiante con ese ID ",
                   "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Actualizar cliente " + ex.Message,
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SeleccionTablas freturn = new SeleccionTablas();
            freturn.Show();
            this.Hide();
        }
    }
}
