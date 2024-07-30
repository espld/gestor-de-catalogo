using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public static class Helper
    {
        public static void CargarImagen(string imagen, PictureBox pictureBox)
        {
            try
            {
                pictureBox.Load(imagen);
            }
            catch (Exception ex)
            {
                pictureBox.Load("https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg");
            }
        }
        public static bool SoloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!char.IsNumber(caracter))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ValidarCamposVacios(ComboBox comboBox,string nombreDelCampo)
        {
            if (comboBox.SelectedIndex < 0)
            {
                MessageBox.Show($"Debe seleccionar {nombreDelCampo}.");
                return true;
            }
            //if (comboBox.SelectedIndex < 0)
            //{
            //    //MessageBox.Show("Debe seleccionar un criterio de búsqueda");
            //    return true;
            //}
            return false;
        }
    } 
}
