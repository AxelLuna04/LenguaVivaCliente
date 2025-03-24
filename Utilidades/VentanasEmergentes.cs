using LenguaVivaCliente.VentanasReutilizables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LenguaVivaCliente.Utilidades
{
    public class VentanasEmergentes
    {
        public static void CrearVentanaEmergente(string tituloVentanaEmergente, string descripcionVentanaEmergente)
        {
            vtVentanaEmergente ventanaEmergente = new vtVentanaEmergente(
                tituloVentanaEmergente,
                descripcionVentanaEmergente
            );

            ventanaEmergente.Show();
        }
        

        public static void CrearConexionFallidaMensajeVentana()
        {
            string tituloVentanaEmergente = "Conexion fallida con el servidor";
            string descripcionVentanaEmergente = "Hubo un error al intentar conectarse con el servidor. Intente de nuevo más tarde";

            Application.Current.Dispatcher.Invoke(() =>
            {
                vtVentanaEmergente ventanaEmergente = new vtVentanaEmergente(
                    tituloVentanaEmergente,
                    descripcionVentanaEmergente
                );

                ventanaEmergente.Show();
            });
        }

        public static void CrearVentanaMensajeTimeOut()
        {
            string tituloVentanaEmergente = "Conexion fallida";
            string descriptionEmergentWindow = "Servidor inaccesible. Intente de nuevo mas tarde.";

            vtVentanaEmergente ventanaEmergente = new vtVentanaEmergente(
                tituloVentanaEmergente,
                descriptionEmergentWindow
            );

            ventanaEmergente.Show();
        }

        public static void CrearErrorMensajeVentanaBaseDatos()
        {
            string tituloVentanaEmergente = "Conexion fallida";
            string descriptionEmergentWindow = "Hubo un error al intentar acceder a la base de datos. Intente de nuevo más tarde";

            vtVentanaEmergente ventanaEmergente = new vtVentanaEmergente(
                tituloVentanaEmergente,
                descriptionEmergentWindow
            );

            ventanaEmergente.Show();
        }

    }
}
