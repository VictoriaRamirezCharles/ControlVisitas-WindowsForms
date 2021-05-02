using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class EditarVisitantes : Form
    {
        private int _id;
        private string _filename;
        private string _path;
        public string user;
        public EditarVisitantes()
        {
            _id = 0;
            _filename = "";
            _path = "";
            InitializeComponent();
        }
        void mostrarVisitantes()
        {
            dataGridView3.DataSource = new N_Visitantes().GetEditList();
            this.dataGridView3.Columns["IDVISITANTE"].Visible = false;
            this.dataGridView3.Columns["Status"].Visible = false;
            this.dataGridView3.Columns["CARRERA_ID"].Visible = false;
            this.dataGridView3.Columns["LUGAR_DESTINO_ID"].Visible = false;
            this.dataGridView3.Columns["AULA_ID"].Visible = false;
            this.dataGridView3.Columns["FOTO"].Visible = false;
            dataGridView3.ClearSelection();

        }
        void cargarCarreras()
        {
            N_Carreras n_Carrera = new N_Carreras();

            for (int i = 0; i < n_Carrera.ListarCarreras("").Count(); i++)
            {

                cboCarreras.DataSource = n_Carrera.ListarCarreras("");
                cboCarreras.DisplayMember = "Nombre";
                cboCarreras.ValueMember = "IdCarrera";

            }
        }
        void cargarLugares()
        {
            N_Lugar_Destino n_Lugar = new N_Lugar_Destino();

            for (int i = 0; i < n_Lugar.ListarLugarDestino("").Count(); i++)
            {

                cboLugares.DataSource = n_Lugar.ListarLugarDestino("");
                cboLugares.DisplayMember = "Nombre";
                cboLugares.ValueMember = "IdLugar_Destino";

            }
        }
        void cargarAulas()
        {
            var lugar = (E_Lugar_Destino)cboLugares.SelectedItem;
            N_Aulas n_Aulas = new N_Aulas();
            if (n_Aulas.ListarAulas(lugar.IdLugar_Destino).Count() != 0)
            {
                for (int i = 0; i < n_Aulas.ListarAulas(lugar.IdLugar_Destino).Count(); i++)
                {

                    cboAula.DataSource = n_Aulas.ListarAulas(lugar.IdLugar_Destino);
                    cboAula.DisplayMember = "Nombre";
                    cboAula.ValueMember = "IdAula";

                }
            }
            else
            {
                cboAula.DataSource = null;
                cboAula.Text = "Seleccione una opcion";
                cboAula.Items.Clear();

            }

        }
        void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        void addFoto()
        {

            DialogResult result = photoDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = photoDialog.FileName;

                _filename = file;

                pbProfile.ImageLocation = _filename;
            }
        }

        public void savePhoto()
        {
            if (_filename != "")
            {
                if (_id <= 0)
                {
                    MessageBox.Show("Debe Seleccionar un visitante", "Notificacion");
                }
                else
                {
                    N_Visitantes n_Visitantes = new N_Visitantes();

                    string directory = @"Images\ControlVisitas\" + _id + "\\";


                    string[] fileNameSplit = _filename.Split('\\');
                    string filename = fileNameSplit[(fileNameSplit.Length - 1)];

                    CreateDirectory(directory);

                    string destination = directory + filename;

                    File.Copy(_filename, destination, true);
                    n_Visitantes.SavePhoto(_id, destination);
                }
          
            }

        }
        void limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreo.Clear();
            txtMotivo.Clear();

            pbProfile.ImageLocation = "";
        }
        void eliminarVisitante()
        {
            if (_id <= 0)
            {
                MessageBox.Show("Debe Seleccionar un cliente", "Notificacion");
            }
            else
            {
                var result = MessageBox.Show("¿Seguro que desea eliminar a este visitante?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    N_Visitantes n_Visitante = new N_Visitantes();
                    n_Visitante.EliminarVisitante(_id);
                    _id = 0;
                    mostrarVisitantes();
                    limpiar();
                }
            }
        }

        void editarVisitante()
        {
            if (_id <= 0)
            {
                MessageBox.Show("Debe Seleccionar un visitante", "Notificacion");
            }
            else
            {
                try
                {
                    var lugar = (E_Lugar_Destino)cboLugares.SelectedItem;

                    var aula = (E_Aula)cboAula.SelectedItem;

                    var carrera = (E_Carreras)cboCarreras.SelectedItem;
                    if (!string.IsNullOrEmpty(txtNombre.Text) || !string.IsNullOrEmpty(txtApellido.Text) || lugar.Nombre != "Seleccione una opcion" || aula.Nombre != "Seleccione una opcion")
                    {
                        N_Visitantes n_Visitantes = new N_Visitantes();

                        E_Visitantes e_Visitantes = new E_Visitantes(txtNombre.Text, txtApellido.Text, carrera.IdCarrera, txtCorreo.Text, txtMotivo.Text, _filename, lugar.IdLugar_Destino, aula.IdAula, dtEntrada.Value.ToString(), dtSalida.Value.ToString(), true);
                        e_Visitantes.IdVisitante = _id;
                        n_Visitantes.EditarVisitante(e_Visitantes);
                        _path = n_Visitantes.GetPhotoPath(_id);
                        savePhoto();
                        MessageBox.Show("Visitante Editado Correctamente");
                        limpiar();
                        mostrarVisitantes();



                    }
                    else
                    {
                        MessageBox.Show("Complete los campos obligatorios");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Complete los campos obligatorios");
                }
            }
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            N_Carreras n_Carrera = new N_Carreras();
            N_Lugar_Destino n_Lugar = new N_Lugar_Destino();
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                _id = Convert.ToInt32(dataGridView3.Rows[index].Cells[0].Value.ToString());
                txtNombre.Text = dataGridView3.Rows[index].Cells[2].Value.ToString();
                txtApellido.Text = dataGridView3.Rows[index].Cells[3].Value.ToString();
                txtCorreo.Text = dataGridView3.Rows[index].Cells[6].Value.ToString();

                cboCarreras.DataSource = n_Carrera.ListarCarreras(dataGridView3.Rows[index].Cells[5].Value.ToString());
                cboCarreras.DisplayMember = "Nombre";
                cboCarreras.ValueMember = "IdCarrera";

                cboLugares.DataSource = n_Lugar.ListarLugarDestino(dataGridView3.Rows[index].Cells[10].Value.ToString());
                cboLugares.DisplayMember = "Nombre";
                cboLugares.ValueMember = "IdLugar_Destino";

                dtEntrada.Value = Convert.ToDateTime(dataGridView3.Rows[index].Cells[13].Value);
                dtSalida.Value = Convert.ToDateTime(dataGridView3.Rows[index].Cells[14].Value);
                txtMotivo.Text = dataGridView3.Rows[index].Cells[7].Value.ToString();
                pbProfile.ImageLocation = dataGridView3.Rows[index].Cells[8].Value.ToString();
            }
        }

        private void EditarVisitantes_Load(object sender, EventArgs e)
        {
            mostrarVisitantes();
            cargarCarreras();
            cargarLugares();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ControlVisitas form = new ControlVisitas();
            form.user = user;
            this.Hide();
            form.Show();
        }

        private void cboLugares_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarAulas();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editarVisitante();
        }

        private void btnEditPhoto_Click(object sender, EventArgs e)
        {
            addFoto();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarVisitante();
        }
    }
}
