using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Ediciones
{
    /// <summary>
    /// Lógica de interacción para vtEditarAdministrador.xaml
    /// </summary>
    public partial class vtEditarAdministrador : Page
    {
        private AdministradorDTO administradorSeleccionado;

        public vtEditarAdministrador(AdministradorDTO administradorSeleccionado)
        {
            InitializeComponent();
            this.administradorSeleccionado = administradorSeleccionado;

            txtNombre.Text = administradorSeleccionado.nombre;
            txtApellidos.Text = administradorSeleccionado.apellidos;
            txtCorreo.Text = administradorSeleccionado.email;
            txtDireccion.Text = administradorSeleccionado.direccion;
            txtUsuario.Text = administradorSeleccionado.nombreUsuario;
            txtTelefono.Text = administradorSeleccionado.telefono;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos() != true)
            {
                return;
            }

            AdministradorDTO administrador = new AdministradorDTO
            {
                idAdministrador = administradorSeleccionado.idAdministrador,
                nombre = txtNombre.Text,
                apellidos = txtApellidos.Text,
                email = txtCorreo.Text,
                direccion = txtDireccion.Text,
                nombreUsuario = txtUsuario.Text,
                contrasenia = txtContrasenia.Password,  
                telefono = txtTelefono.Text
            };

            try
            {
                using (var servicio = new GestionUsuariosClient())
                {
                    bool actualizado = servicio.ModificarAdministrador(administrador);

                    if (actualizado)
                    {
                        MessageBox.Show("Administrador actualizado con éxito");
                        NavigationService?.GoBack();  // Regresamos a la vista anterior
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el administrador");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar actualizar el administrador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool validarCampos()
        {
            int contadorCamposVacios = 0;

            // Validación de campos vacíos
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
                lbCorreoVacio.Text = "Campo obligatorio";
                lbCorreoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else if (!EsCorreoValido(txtCorreo.Text))
            {
                lbCorreoVacio.Text = "Formato de correo inválido";
                lbCorreoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbCorreoVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lbTelefonoVacio.Text = "Campo obligatorio";
                lbTelefonoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else if (!EsTelefonoValido(txtTelefono.Text))
            {
                lbTelefonoVacio.Text = "Solo números permitidos";
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
            NavigationService?.GoBack();
        }
    }
}
