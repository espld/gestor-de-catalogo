using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class fmrDetalle : Form
    {
        private Articulo articulo;

        public fmrDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void fmrDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                if (articulo != null)
                {
                    Helper.CargarImagen(articulo.ImagenUrl, pbxDetalleImagen);
                    this.txtCodigo.Text = articulo.CodigoArticulo;
                    this.txtNombre.Text = articulo.Nombre;
                    this.txtPrecio.Text = articulo.Precio.ToString();
                    this.txtMarca.Text = articulo.Marca.Descripcion;
                    this.txtCategoria.Text = articulo.Categoria.Descripcion;
                    this.txtDescripcion.Text = articulo.Descripcion;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
