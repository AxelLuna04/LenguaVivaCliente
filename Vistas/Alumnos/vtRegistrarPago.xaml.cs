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
        public vtRegistrarPago(int idInscripcion)
        {
            InitializeComponent();
            this.idInscripcion = idInscripcion;
        }

        private void BtnRegistrarPago_Click(object sender, RoutedEventArgs e)
        {
            int cantidadPagada = int.Parse(tbCantidadPagada.Text);
            GestionAlumnosClient proxy= new GestionAlumnosClient();

            if(proxy.RegistrarPago(cantidadPagada, idInscripcion))
            {
                Utilidades.VentanasEmergentes.CrearVentanaEmergente("Pago registrado", "El pago se ha registrado correctamente");
                BtnRegistrarPago.Visibility = Visibility.Collapsed;
                BtnReciboDePago.Visibility = Visibility.Visible;
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
    }
}
