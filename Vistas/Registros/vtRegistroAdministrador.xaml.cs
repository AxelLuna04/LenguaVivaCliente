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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServicioContrato;
using LVServicio = LenguaVivaCliente.ServicioLenguaViva.IGestionUsuarios;


namespace LenguaVivaCliente.Vistas.Registros
{
    /// <summary>
    /// Lógica de interacción para vtRegistroAdministrador.xaml
    /// </summary>
    public partial class vtRegistroAdministrador : Page
    {
        private ServicioContrato.IGestionUsuarios _servicio;

        public vtRegistroAdministrador()
        {
            InitializeComponent();
            _servicio = new ImplementacionServicio();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
           


        }

        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtContrasenia.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtCorreo.Clear();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
