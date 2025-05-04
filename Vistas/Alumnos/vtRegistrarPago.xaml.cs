using LenguaVivaCliente.ServicioLenguaViva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LenguaVivaCliente.Vistas.Alumnos
{
    /// <summary>
    /// Lógica de interacción para vtRegistraPago.xaml
    /// </summary>
    public partial class vtRegistrarPago : Window
    {
        private int idInscripcion;
        private bool pagoRegistrado = false;
        public vtRegistrarPago(int idInscripcion)
        {
            InitializeComponent();
            this.idInscripcion = idInscripcion;
        }

        private bool ValidarCantidadIngresada()
        {
            string cantidad = tbCantidadPagada.Text;
            if (int.TryParse(cantidad, out int numero) && numero >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BtnRegistrarPago_Click(object sender, RoutedEventArgs e)
        {
            if(tbCantidadPagada.Text == "")
            {
                Utilidades.VentanasEmergentes.CrearVentanaEmergente("Campo vacío", "Favor de ingresar la cantidad pagada ");
                return;
            }

            if (!ValidarCantidadIngresada())
            {
                Utilidades.VentanasEmergentes.CrearVentanaEmergente("Cantidad no valida ", "Favor de ingresar una cantidad entera positiva.");
                return;
            }


            int cantidadPagada = int.Parse(tbCantidadPagada.Text);
            GestionAlumnosClient proxy= new GestionAlumnosClient();

            if(proxy.RegistrarPago(cantidadPagada, idInscripcion))
            {
                Utilidades.VentanasEmergentes.CrearVentanaEmergente("Pago registrado", "El pago se ha registrado correctamente");
                BtnRegistrarPago.Visibility = Visibility.Collapsed;
                BtnReciboDePago.Visibility = Visibility.Visible;
                pagoRegistrado = true;
    }
            else
            {
                Utilidades.VentanasEmergentes.CrearVentanaEmergente("Pago no registrado","Ocurrio un error al intentar registrar el pago, intente de nuevo mas tarde.");
            }
        }

        private void BtnReciboDePago_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            vtReciboDePago reciboPago = new vtReciboDePago(idInscripcion);
            reciboPago.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!pagoRegistrado)
            {
                Utilidades.VentanasEmergentes.CrearVentanaEmergente("", "Debe ingresar la cantidad y registrar el pago antes de cerrar esta ventana.");
                e.Cancel = true;
            }
        }
    }
}
