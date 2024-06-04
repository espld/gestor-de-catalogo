using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            List<Marca> listaMarca = new List<Marca>();

            try
            {
                accesoDatos.SetearConsulta("select Id,Descripcion from MARCAS");
                accesoDatos.EjecutarConsulta();

                while (accesoDatos.Lector.Read())
                {
                    Marca marcaAux = new Marca();

                    marcaAux.Id = (int)accesoDatos.Lector["Id"];
                    marcaAux.Descripcion = (string)accesoDatos.Lector["Descripcion"];

                    listaMarca.Add(marcaAux);
                }
                return listaMarca;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }
    }
}
