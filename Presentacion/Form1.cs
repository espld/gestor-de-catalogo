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
    public partial class fmrPrincipal : Form
    {
        private List<Articulo> listaArticulos;

        public fmrPrincipal()
        {
            InitializeComponent();
        }

        private void Cargar()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            listaArticulos = articuloNegocio.Listar();
            dgvArticulos.DataSource = listaArticulos;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            CargarImagen(listaArticulos[0].ImagenUrl);
        }
        private void fmrPrincipal_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }
        private void CargarImagen(string imagen)
        {
            try
            {
                pbxImagenArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagenArticulo.Load("https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            this.CargarImagen(articuloSeleccionado.ImagenUrl);
            //pbxImagenArticulo.Load(articuloSeleccionado.ImagenUrl);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            fmrAltaArticulo alta = new fmrAltaArticulo();

            alta.ShowDialog();

        }
    }
}
