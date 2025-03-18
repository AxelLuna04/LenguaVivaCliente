using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Registros
{
    public partial class vtRegistroAdministrador : Page
    {
        public vtRegistroAdministrador()
        {
            InitializeComponent();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos() != true)
            {
                return;
            }

            AdministradorDTO administrador = new AdministradorDTO
            {
                nombre = txtNombre.Text,
                apellidos = txtApellidos.Text,
                email = txtCorreo.Text,
                direccion = txtDireccion.Text,
                nombreUsuario = txtUsuario.Text,
                contrasenia = txtContrasenia.Password
            };

            ServicioLenguaViva.IGestionUsuarios servicio = new ServicioLenguaViva.GestionUsuariosClient();
            bool registrado = servicio.RegistrarAdministrador(administrador);

            if (registrado)
            {
                MessageBox.Show("Administrador registrado con éxito");
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Error al registrar el administrador");
            }
        }

        private bool validarCampos()
        {
            int contadorCamposVacios = 0;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lbNombreVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbNombreVacio.Visibility = Visibility.Hidden;
            }

            // Repetir para apellidos, correo, dirección, teléfono, usuario, contraseña...

            return contadorCamposVacios == 0;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}