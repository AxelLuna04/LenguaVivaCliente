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
                contrasenia = txtContrasenia.Password,
                telefono = txtTelefono.Text
            };

            ServicioLenguaViva.IGestionUsuarios servicio = new ServicioLenguaViva.GestionUsuariosClient();
            bool registrado = servicio.RegistrarAdministrador(administrador);

            if (registrado)
            {
                MessageBox.Show("Administrador registrado con éxito");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al registrar el administrador");
            }
        }

        private bool validarCampos()
        {
            int contadorCamposVacios = 0;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lbNombreVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbNombreVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                lbApellidosVacios.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbApellidosVacios.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                lbCorreoVacio.Content = "Campo obligatorio";
                lbCorreoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else if (!EsCorreoValido(txtCorreo.Text))
            {
                lbCorreoVacio.Content = "Formato de correo inválido";
                lbCorreoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbCorreoVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lbDireccionVacia.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbDireccionVacia.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lbTelefonoVacio.Content = "Campo obligatorio";
                lbTelefonoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else if (!EsTelefonoValido(txtTelefono.Text))
            {
                lbTelefonoVacio.Content = "Solo números permitidos";
                lbTelefonoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbTelefonoVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lbUsuarioVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbUsuarioVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtContrasenia.Password))
            {
                lbContraseniaVacia.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbContraseniaVacia.Visibility = Visibility.Hidden;
            }

            return contadorCamposVacios == 0;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtUsuario.Clear();
            txtContrasenia.Clear();
            txtTelefono.Clear();
        }

        private bool EsCorreoValido(string correo)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool EsTelefonoValido(string telefono)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d+$");
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}