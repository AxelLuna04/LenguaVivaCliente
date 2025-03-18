using LenguaVivaCliente.ServicioLenguaViva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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


namespace LenguaVivaCliente.Vistas.Registros
{
    /// <summary>
    /// Lógica de interacción para vtRegistroAdministrador.xaml
    /// </summary>
    public partial class vtRegistroAdministrador : Page
    {
        private IGestionUsuarios _servicio;

        public vtRegistroAdministrador()
        {
            InitializeComponent();
            ChannelFactory<IGestionUsuarios> factory = new ChannelFactory<IGestionUsuarios>("NetTcpBinding_IGestionUsuarios");
            _servicio = factory.CreateChannel();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

            string nombreUsuario = txtUsuario.Text;
            string contrasenia = txtContrasenia.Password;
            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string direccion = txtDireccion.Text;
            string email = txtCorreo.Text;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasenia) ||
                string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool registrado = _servicio.RegistrarAdministrador(nombreUsuario, contrasenia, nombre, apellidos, direccion, email);

            if (registrado == true)
            {
                MessageBox.Show("Administrador registrado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al registrar el administrador", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtContrasenia.Clear();
            txtTelefono.Clear();
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
