using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class ControlVisitas : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private string _filename;
        private string _path;
        private int _id;
        private int _idAula;
        private int _idEficio;
        public string user;
        public ControlVisitas()
        {
            _id = 0;
            _idAula = 0;
            _idEficio = 0;
            _filename = "";
            _path = "";
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void ControlVisitas_Load(object sender, EventArgs e)
        {
            cargarVisitantes();
            cargarCarreras();
            cargarLugares();
            mostrarAulas();
            mostrarEdificio();
            N_Usuarios usuarios = new N_Usuarios();
            var usuario = usuarios.Login(user);
            if (usuario.TipoUsuario)
            {
                bunifuThinButton21.Visible = true;
                bunifuThinButton22.Visible = true;
                bunifuThinButton23.Visible = true;
            }
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

                cboEdicios.DataSource = n_Lugar.ListarLugarDestino("");
                cboEdicios.DisplayMember = "Nombre";
                cboEdicios.ValueMember = "IdLugar_Destino";

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
        void mostrarAulas()
        {
         
            dataGridView3.DataSource = new N_Aulas().GetAulas("");
            this.dataGridView3.Columns["IDAULA"].Visible = false;
            this.dataGridView3.Columns["ID_EDIFICIO"].Visible = false;
        }

        void mostrarEdificio()
        {

            dgEdificios.DataSource = new N_Lugar_Destino().ListarLugarDestino("");
            this.dgEdificios.Columns["IDLUGAR_DESTINO"].Visible = false;
        
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
                N_Visitantes n_Visitantes = new N_Visitantes();
                int id = _id == 0 ? n_Visitantes.GetLastId() : _id;

                string directory = @"Images\ControlVisitas\" + id + "\\";


                string[] fileNameSplit = _filename.Split('\\');
                string filename = fileNameSplit[(fileNameSplit.Length - 1)];

                CreateDirectory(directory);

                string destination = directory + filename;

                File.Copy(_filename, destination, true);
                n_Visitantes.SavePhoto(id, destination);
            }

        }

        void registrarVisitante()
        {
            try
            {
                var lugar = (E_Lugar_Destino)cboLugares.SelectedItem;

                var aula = (E_Aula)cboAula.SelectedItem;

                var carrera = (E_Carreras)cboCarreras.SelectedItem;
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && lugar.Nombre != "Seleccione una opcion" && aula.Nombre != "Seleccione una opcion")
                {
                    N_Visitantes n_Visitantes = new N_Visitantes();

                    E_Visitantes e_Visitantes = new E_Visitantes(txtNombre.Text, txtApellido.Text, carrera.IdCarrera, txtCorreo.Text, txtMotivo.Text, _filename, lugar.IdLugar_Destino, aula.IdAula, dtEntrada.Value.ToString(), dtSalida.Value.ToString(), true);

                    n_Visitantes.InsertarVisitante(e_Visitantes);
                    _path = n_Visitantes.GetPhotoPath(n_Visitantes.GetLastId());
                    savePhoto();
                    MessageBox.Show("Visitante Agregado Correctamente");
                    limpiar();



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

        void registrarAula()
        {
            var lugar = (E_Lugar_Destino)cboEdicios.SelectedItem;

            if (lugar.Nombre != "Seleccione una opcion" && !string.IsNullOrEmpty(txtAula.Text))
            {
                E_Aula e_aula = new E_Aula(txtAula.Text, lugar.IdLugar_Destino);
                N_Aulas n_Aula = new N_Aulas();
                n_Aula.InsertarAula(e_aula);
                MessageBox.Show("Aula agregada correctamente");
            }
            else
            {
                MessageBox.Show("Complete los campos requeridos");
            }
        }
        void registrarEdificio()
        {
           

            if (!string.IsNullOrEmpty(txtNombreEdificio.Text))
            {
                E_Lugar_Destino edificio = new E_Lugar_Destino(txtNombreEdificio.Text);
                N_Lugar_Destino n_edificio = new N_Lugar_Destino();
                n_edificio.InsertarLugarDestino(edificio);
                MessageBox.Show("Edificio agregada correctamente");
            }
            else
            {
                MessageBox.Show("Complete los campos requeridos");
            }
        }
        void cargarVisitantes()
        {
            flowLayoutPanel3.Controls.Clear();
            int cantidad = new N_Visitantes().GetAll().Count();
            visitantesList[] visitantesList = new visitantesList[cantidad];
            N_Visitantes _visitante = new N_Visitantes();
            for (int i = 0; i < visitantesList.Length; i++)
            {
                visitantesList[i] = new visitantesList();
                visitantesList[i].IdVisitante = _visitante.GetAll()[i].IdVisitante;
                visitantesList[i].NombreVisitante = _visitante.GetAll()[i].Nombre + " " + _visitante.GetAll()[i].Apellido;
                visitantesList[i].Motivo = _visitante.GetAll()[i].Motivo_Visita;
                visitantesList[i].Hora_Entrada = _visitante.GetAll()[i].Hora_Entrada;
                visitantesList[i].Hora_Salida = _visitante.GetAll()[i].Hora_Salida;
                visitantesList[i].FotoVisitante = _visitante.GetAll()[i].Foto;
                visitantesList[i].DestinoVisitante = _visitante.GetAll()[i].Edificio + "-" + _visitante.GetAll()[i].AulaNombre;
                visitantesList[i].CodigoVisitante = _visitante.GetAll()[i].Codigo;
                visitantesList[i].SalidaButtonClick += new EventHandler(darSalida);
                flowLayoutPanel3.Controls.Add(visitantesList[i]);
            }
        }
        public void darSalida(object sender, EventArgs e)
        {
            visitantesList item = sender as visitantesList;

            N_Visitantes _visitante = new N_Visitantes();
            _visitante.darSalida(item.IdVisitante, false);
            cargarVisitantes();
        }
        void limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreo.Clear();
            txtMotivo.Clear();

            pbProfile.ImageLocation = "";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }
        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            addFoto();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrarVisitante();
            cargarVisitantes();
        }

        private void cboLugares_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarAulas();
        }

        private void sbRegistrar_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panelEdificio.Visible = false;
            flowLayoutPanel3.Visible = false;
        }

        private void sbVisitantes_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            flowLayoutPanel3.Visible = true;
            panelAulas.Visible = false;
            panelEdificio.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditarVisitantes form = new EditarVisitantes();
            form.user = user;
            this.Hide();
            form.Show();
        }



        private void ControlVisitas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            N_Usuarios usuarios = new N_Usuarios();
            var usuario = usuarios.Login(user);
            if (usuario.TipoUsuario)
            {
            
            panelAulas.Visible = true;
            panel2.Visible = false;
            panel3.Visible = true;
            flowLayoutPanel3.Visible = false;
            panelEdificio.Visible = false;
        }
        }

        private void btnGuardarAula_Click(object sender, EventArgs e)
        {
            registrarAula();
            mostrarAulas();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            N_Lugar_Destino n_Lugar = new N_Lugar_Destino();
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                _idAula = Convert.ToInt32(dataGridView3.Rows[index].Cells[0].Value.ToString());
                cboEdicios.DataSource = n_Lugar.ListarLugarDestino(dataGridView3.Rows[index].Cells[3].Value.ToString());
                cboEdicios.DisplayMember = "Nombre";
                cboEdicios.ValueMember = "IdLugar_Destino";
                txtAula.Text = dataGridView3.Rows[index].Cells[1].Value.ToString();
            }
        }

        void editarAula()
        {
            if (_idAula <= 0)
            {
                MessageBox.Show("Debe Seleccionar un aula", "Notificacion");
            }
            else
            {
                try
                {
                    var lugar = (E_Lugar_Destino)cboEdicios.SelectedItem;

                    if (!string.IsNullOrEmpty(txtAula.Text) && lugar.Nombre != "Seleccione una opcion")
                    {
                        N_Aulas n_Aulas = new N_Aulas();

                        E_Aula e_Aula = new E_Aula(txtAula.Text, lugar.IdLugar_Destino);
                        e_Aula.IdAula = _idAula;
                        n_Aulas.EditarAula(e_Aula);
                      
                        savePhoto();
                        MessageBox.Show("Aula Editado Correctamente");
                        limpiar();
                        mostrarAulas();



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
        void editarEdificio()
        {
            if (_idEficio <= 0)
            {
                MessageBox.Show("Debe Seleccionar un edificio", "Notificacion");
            }
            else
            {
                try
                {

                    if (!string.IsNullOrEmpty(txtNombreEdificio.Text))
                    {
                        N_Lugar_Destino n_Lugar_Destino = new N_Lugar_Destino();

                        E_Lugar_Destino edificio = new E_Lugar_Destino(txtNombreEdificio.Text);
                        edificio.IdLugar_Destino = _idEficio;
                        n_Lugar_Destino.EditarLugarDestino(edificio);

                        savePhoto();
                        MessageBox.Show("Edificio Editado Correctamente");
                        limpiar();
                        mostrarAulas();
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
        void eliminarAula()
        {
            if (_idAula <= 0)
            {
                MessageBox.Show("Debe Seleccionar un aula", "Notificacion");
            }
            else
            {
                var result = MessageBox.Show("¿Seguro que desea eliminar a esta aula?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    N_Aulas n_Aulas = new N_Aulas();
                    n_Aulas.EliminarAula(_idAula);
                    _idAula = 0;
                    mostrarAulas();
                    limpiar();
                }
            }
        }

        void eliminarEdificio()
        {
            if (_idEficio <= 0)
            {
                MessageBox.Show("Debe Seleccionar un edificio", "Notificacion");
            }
            else
            {
                var result = MessageBox.Show("¿Seguro que desea eliminar a este edificio?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    N_Lugar_Destino n_edificio = new N_Lugar_Destino();
                    n_edificio.EliminarLugarDestino(_idEficio);
                    _idEficio = 0;
                    mostrarEdificio();

                }
            }
        }

        private void btnEditarAula_Click(object sender, EventArgs e)
        {
            editarAula();
        }

        private void btnEliminarAula_Click(object sender, EventArgs e)
        {
            eliminarAula();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            N_Usuarios usuarios = new N_Usuarios();
            var usuario = usuarios.Login(user);
            if (usuario.TipoUsuario)
            {
                panelEdificio.Visible = true;
                panelAulas.Visible = false;
                panel2.Visible = false;
                panel3.Visible = true;
                flowLayoutPanel3.Visible = false;
            }
        }

        private void btnGuardarEdificios_Click(object sender, EventArgs e)
        {
            registrarEdificio();
            mostrarEdificio();
        }

        private void dgEdificios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            N_Lugar_Destino n_Lugar = new N_Lugar_Destino();
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                _idEficio = Convert.ToInt32(dgEdificios.Rows[index].Cells[0].Value.ToString());
                txtNombreEdificio.Text = dgEdificios.Rows[index].Cells[1].Value.ToString();
            }
        }

        private void btnEditarEdificios_Click(object sender, EventArgs e)
        {
            editarEdificio();
            mostrarEdificio();
        }

        private void btnEliminarEdificios_Click(object sender, EventArgs e)
        {
            eliminarEdificio();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            RegistroUsuario form = new RegistroUsuario();
            this.Hide();
            form.user = user;
            form.Show();

        }
    }
}
