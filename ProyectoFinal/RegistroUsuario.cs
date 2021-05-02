using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class RegistroUsuario : Form
    {
        private int _id;
        public string user;
        public RegistroUsuario()
        {
            _id = 0;
            InitializeComponent();
        }

        void mostrarUsuarios()
        {
            dgUsuario.DataSource = new N_Usuarios().ListarUsuarios("");
            this.dgUsuario.Columns["IDUSUARIO"].Visible = false;
        }

        private void RegistroUsuario_Load(object sender, EventArgs e)
        {
            mostrarUsuarios();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ControlVisitas form = new ControlVisitas();
            form.user = user;
            this.Hide();
            form.Show();
        }

        void registrarUsuario()
        {
         

            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text))
            {
                bool tipo = false;
                switch (cboTipo.SelectedItem.ToString())
                {
                    case "General":
                        tipo = false;
                        break;
                    case "Administrador":
                        tipo = true;
                        break;
                }
                E_Usuarios e_usuario = new E_Usuarios(txtNombre.Text,txtApellido.Text,dtFecha.Value,tipo);
                N_Usuarios n_usuario = new N_Usuarios();
                n_usuario.InsertarUsuario(e_usuario);
                MessageBox.Show("Usuarip agregada correctamente");
            }
            else
            {
                MessageBox.Show("Complete los campos requeridos");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            registrarUsuario();
            mostrarUsuarios();
        }

        private void dgUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            N_Carreras n_Carrera = new N_Carreras();
            N_Lugar_Destino n_Lugar = new N_Lugar_Destino();
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                _id = Convert.ToInt32(dgUsuario.Rows[index].Cells[0].Value.ToString());
                txtNombre.Text = dgUsuario.Rows[index].Cells[2].Value.ToString();
                txtApellido.Text = dgUsuario.Rows[index].Cells[3].Value.ToString();
                dtFecha.Value = Convert.ToDateTime(dgUsuario.Rows[index].Cells[4].Value);
            }
        }
    }
}
