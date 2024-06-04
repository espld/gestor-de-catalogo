using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.ComponentModel.Design;

namespace Negocio
{
    public class ArticuloNegocio
    {
       
        public List<Articulo> Listar()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Articulo> listaArticulos = new List<Articulo>();

            try
            {
                accesoDatos.SetearConsulta("select a.Codigo,a.Nombre,a.Precio,a.ImagenUrl,c.Descripcion Categoria,m.Descripcion Marca,a.Descripcion,a.IdMarca,a.IdCategoria,a.Id from ARTICULOS a,CATEGORIAS c,MARCAS m where a.IdCategoria = c.Id and a.IdMarca = m.Id");
                accesoDatos.EjecutarConsulta();

                while(accesoDatos.Lector.Read())
                {
                    Articulo articuloAux = new Articulo();

                    articuloAux.Id = (int)accesoDatos.Lector["Id"];
                    articuloAux.CodigoArticulo = (string)accesoDatos.Lector["Codigo"];
                    articuloAux.Nombre = (string)accesoDatos.Lector["Nombre"];
                    articuloAux.Precio = (float)(decimal)accesoDatos.Lector["Precio"];

                    if (!(accesoDatos.Lector["ImagenUrl"] is DBNull))
                    {
                        articuloAux.ImagenUrl = (string)accesoDatos.Lector["ImagenUrl"];
                    }

                    articuloAux.Categoria = new Categoria();
                    articuloAux.Categoria.Id = (int)accesoDatos.Lector["IdCategoria"];
                    articuloAux.Categoria.Descripcion = (string)accesoDatos.Lector["Categoria"];
                    articuloAux.Marca = new Marca();
                    articuloAux.Marca.Id = (int)accesoDatos.Lector["IdMarca"];
                    articuloAux.Marca.Descripcion = (string)accesoDatos.Lector["Marca"];

                    articuloAux.Descripcion = (string)accesoDatos.Lector["Descripcion"];

                    listaArticulos.Add(articuloAux);
                }

                return listaArticulos;
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

        public void Agregar(Articulo articuloNuevo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta("insert into ARTICULOS (Codigo,Nombre,Precio,ImagenUrl,IdCategoria,IdMarca,Descripcion) values(@Codigo,@Nombre,@Precio,@ImagenUrl,@IdCategoria,@IdMarca,@Descripcion)");
                accesoDatos.SetearParametros("@Codigo", articuloNuevo.CodigoArticulo);
                accesoDatos.SetearParametros("@Nombre", articuloNuevo.Nombre);
                accesoDatos.SetearParametros("@Precio", articuloNuevo.Precio);
                accesoDatos.SetearParametros("@ImagenUrl", articuloNuevo.ImagenUrl);
                accesoDatos.SetearParametros("@IdCategoria", articuloNuevo.Categoria.Id);
                accesoDatos.SetearParametros("@IdMarca", articuloNuevo.Marca.Id);
                accesoDatos.SetearParametros("@Descripcion", articuloNuevo.Descripcion);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex )
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public void Modificar(Articulo articuloAModificar)
        {

            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @img, precio = @precio where Id = @id");
                accesoDatos.SetearParametros("@codigo", articuloAModificar.CodigoArticulo);
                accesoDatos.SetearParametros("@nombre", articuloAModificar.Nombre);
                accesoDatos.SetearParametros("@desc", articuloAModificar.Descripcion);
                accesoDatos.SetearParametros("@idMarca", articuloAModificar.Marca.Id);
                accesoDatos.SetearParametros("@idCategoria", articuloAModificar.Categoria.Id);
                accesoDatos.SetearParametros("@img", articuloAModificar.ImagenUrl);
                accesoDatos.SetearParametros("@precio", articuloAModificar.Precio);
                accesoDatos.SetearParametros("@id", articuloAModificar.Id);

                accesoDatos.EjecutarAccion();
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
