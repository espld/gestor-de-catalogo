using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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
        private void fmrPrincipal_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Helper.CargarImagen(articuloSeleccionado.ImagenUrl, pbxImagenArticulo);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            fmrAltaArticulo alta = new fmrAltaArticulo();

            alta.ShowDialog();
            this.Cargar();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado;

            try
            {
                articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                fmrAltaArticulo modificar = new fmrAltaArticulo(articuloSeleccionado);
                modificar.ShowDialog();
                this.Cargar();
            }
            catch (Exception)
            {
                MessageBox.Show("Debe seleccionar un ítem para modificar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado;

            try
            {
                articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                ArticuloNegocio articuloNegocio = new ArticuloNegocio();

                DialogResult resultado = MessageBox.Show("Desea eliminar el item seleccionado?", "Eliminar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    articuloNegocio.Eliminar(articuloSeleccionado.Id);
                    this.Cargar();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Debe seleccionar un ítem para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if (this.ValidarComboBoxVacio(this.cboCampo))
                {
                    MessageBox.Show("Debe seleccionar un campo de búsqueda","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                if (this.ValidarComboBoxVacio(this.cboCriterio))
                {
                    MessageBox.Show("Debe seleccionar un criterio de búsqueda","Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ValidarTextoDeBusqueda(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debe ingresar un filtro para buscar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ValidarCampoNumerico(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debe ingresar solo números.","Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string campo = this.cboCampo.SelectedItem.ToString();
                string criterio = this.cboCriterio.SelectedItem.ToString();
                string filtro = this.txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = articuloNegocio.Filtrar(campo, criterio, filtro);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al buscar");
            }
        }
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = this.txtFiltro.Text;

            if (filtro.Length >= 3)
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            this.dgvArticulos.DataSource = null;

            this.dgvArticulos.DataSource = listaFiltrada;

            if (listaFiltrada.Count < 1)
            {
                this.btnModificar.Enabled = false;
                this.btnEliminar.Enabled = false;
                this.btnDetalle.Enabled = false;
            }
            else
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                this.btnDetalle.Enabled = true;
            }

            this.OcultarColumnas();
            this.dgvArticulos.ClearSelection();
        }
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = this.cboCampo.SelectedItem.ToString();

            switch (opcion)
            {
                case "Precio":
                    this.cboCriterio.Items.Clear();
                    this.cboCriterio.Items.Add("Mayor a");
                    this.cboCriterio.Items.Add("Menor a");
                    this.cboCriterio.Items.Add("Igual a");
                    break;
                default:
                    this.cboCriterio.Items.Clear();
                    this.cboCriterio.Items.Add("Empieza con");
                    this.cboCriterio.Items.Add("Termina con");
                    this.cboCriterio.Items.Add("Contiene");
                    break;
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.cboCriterio.Items.Clear();
            this.cboCampo.Items.Clear();
            this.txtFiltroAvanzado.Text = string.Empty;
            this.Cargar();
        }
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado;

            try
            {
                articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                fmrDetalle detalle = new fmrDetalle(articuloSeleccionado);
                detalle.ShowDialog();
                this.Cargar();
            }
            catch (Exception)
            {
                MessageBox.Show("Debe seleccionar un ítem para ver el detalle.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool ValidarComboBoxVacio(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex < 0)
            {
                return true;
            }
            return false;
        }
        private bool ValidarTextoDeBusqueda(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
            {
                return true;
            }
            return false;
        }
        private bool ValidarCampoNumerico(string cadena)
        {
            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (!Helper.SoloNumeros(cadena))
                {
                    return true;
                }
            }
            return false;
        }
        private void Cargar()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                this.listaArticulos = articuloNegocio.Listar();
                this.dgvArticulos.DataSource = listaArticulos;
                this.dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C";
                this.OcultarColumnas();
                Helper.CargarImagen(listaArticulos[0].ImagenUrl, pbxImagenArticulo);
                this.dgvArticulos.ClearSelection();
                this.dgvArticulos.CurrentCell = null;
                this.txtFiltro.Clear();
                if (this.cboCampo.Items.Count < 1)
                {
                    this.SetearComboBox();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OcultarColumnas()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }
        private void SetearComboBox()
        {
            this.cboCampo.Items.Add("Nombre");
            this.cboCampo.Items.Add("Precio");
            this.cboCampo.Items.Add("Código");
            this.cboCampo.Items.Add("Marca");
            this.cboCampo.Items.Add("Categoría");
        }
    }
}
