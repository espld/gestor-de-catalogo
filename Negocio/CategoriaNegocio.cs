using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            List<Categoria> listaCategorias = new List<Categoria>();

            try
            {
                accesoDatos.SetearConsulta("select Id,Descripcion from CATEGORIAS");
                accesoDatos.EjecutarConsulta();

                while(accesoDatos.Lector.Read())
                {
                    Categoria categoriaAux = new Categoria();

                    categoriaAux.Id = (int)accesoDatos.Lector["Id"];
                    categoriaAux.Descripcion = (string)accesoDatos.Lector["Descripcion"];

                    listaCategorias.Add(categoriaAux);
                }
                return listaCategorias;
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
