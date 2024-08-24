using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Marca
    {
        private int id; 

        private string descripcion;

        //

        public int Id
        {
            get { return this.id; } 
            set { this.id = value; }
        }
        public string Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}
