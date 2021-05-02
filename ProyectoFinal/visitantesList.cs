using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class visitantesList : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler SalidaButtonClick;
        public visitantesList()
        {
            InitializeComponent();
        }
        private int _id;
        private string _nombre;
        private string _destino;
        private string _motivo;
        private string _hora_entrada;
        private string _hora_salida;
        private string _codigo;
        private string _foto;

        [Category("Custom Props")]
        public int IdVisitante
        {
            get { return _id; }
            set { _id = value; }
        }

        [Category("Custom Props")]
        public string NombreVisitante
        {
            get { return _nombre; }
            set { _nombre = value; lblNombre.Text = value; }
        }

        [Category("Custom Props")]
        public string DestinoVisitante
        {
            get { return _destino; }
            set { _destino = value; lblDestino.Text = value; }
        }

        [Category("Custom Props")]
        public string Motivo
        {
            get { return _motivo; }
            set { _motivo = value; lblMotivo.Text = value; }
        }

        [Category("Custom Props")]
        public string Hora_Entrada
        {
            get { return _hora_entrada; }
            set { _hora_entrada = value; lblEntrada.Text = value; }
        }

        [Category("Custom Props")]
        public string Hora_Salida
        {
            get { return _hora_salida; }
            set { _hora_salida = value; lblSalida.Text = value; }
        }

        [Category("Custom Props")]
        public string CodigoVisitante
        {
            get { return _codigo; }
            set { _codigo = value; lblCodigo.Text = value; }
        }

        [Category("Custom Props")]
        public string FotoVisitante
        {
            get { return _foto; }
            set { _foto = value; pbFoto.ImageLocation = value; }
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (this.SalidaButtonClick != null)
                this.SalidaButtonClick(this, e);
        }
    }
}
