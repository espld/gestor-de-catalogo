using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        private string codigoArticulo;
        private string nombre;
        private string descripcion;
        private Marca marca;
        private Categoria categoria;
        private string imagenUrl;
        private float precio;
        private int id;

        public int Id
        {
            get { return this.id; }
            set {  this.id = value; }
        }

        [DisplayName("Código")]
        public string CodigoArticulo
        {
            get { return this.codigoArticulo; }
            set { this.codigoArticulo = value; }
        }
        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        [DisplayName("Descripción")]
        public string Descripcion
        {
            get { return this.descripcion;}
            set { this.descripcion = value;}
        }
        public string ImagenUrl
        {
            get { return this.imagenUrl; }
            set { this.imagenUrl = value;}
        }
        public float Precio
        {
            get { return this.precio; }
            set { this.precio = value; }
        }
        public Marca Marca
        {
            get { return this.marca; }
            set { this.marca = value; }
        }
        [DisplayName("Categoría")]
        public Categoria Categoria
        {
            get { return this.categoria;}
            set { this.categoria = value;}
        }
	}
}
