using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Registros
{
    public partial class vtRegistroProfesor : Page
    {
        public vtRegistroProfesor()
        {
            InitializeComponent();
            CargarIdiomas();
        }

        private void CargarIdiomas()
        {
            try
            {
                var servicio = new GestionUsuariosClient();
                var idiomas = servicio.CargarIdiomas();

                if (idiomas != null)
                {
                    dataGridIdiomas.ItemsSource = idiomas.Select(i => new IdiomaDTO
                    {
                        idIdioma = i.idIdioma,
                        nombreIdioma = i.nombreIdioma,
                        IsSelected = false
                    }).ToList();
                }
                else
                {
                    MessageBox.Show("No se encontraron idiomas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar idiomas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarCampos()
        {
            int errores = 0;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lbNombreVacio.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbNombreVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                lbApellidosVacios.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbApellidosVacios.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                lbCorreoVacio.Content = "Campo obligatorio";
                lbCorreoVacio.Visibility = Visibility.Visible;
                errores++;
            }
            else if (!EsCorreoValido(txtCorreo.Text))
            {
                lbCorreoVacio.Content = "Formato de correo inválido";
                lbCorreoVacio.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbCorreoVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lbDireccionVacia.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbDireccionVacia.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lbTelefonoVacio.Content = "Campo obligatorio";
                lbTelefonoVacio.Visibility = Visibility.Visible;
                errores++;
            }
            else if (!EsTelefonoValido(txtTelefono.Text))
            {
                lbTelefonoVacio.Content = "Solo números permitidos";
                lbTelefonoVacio.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbTelefonoVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lbUsuarioVacio.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbUsuarioVacio.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(txtContraseña.Password))
            {
                lbContraseniaVacia.Visibility = Visibility.Visible;
                errores++;
            }
            else
            {
                lbContraseniaVacia.Visibility = Visibility.Hidden;
            }

            var idiomasSeleccionados = (dataGridIdiomas.ItemsSource as List<IdiomaDTO>)?
                .Where(i => i.IsSelected)
                .ToList();

            if (idiomasSeleccionados == null || idiomasSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un idioma.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                errores++;
            }

            return errores == 0;
        }

        private bool EsCorreoValido(string correo)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool EsTelefonoValido(string telefono)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d+$");
        }


        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            var idiomasSeleccionados = (dataGridIdiomas.ItemsSource as List<IdiomaDTO>)?
                .Where(i => i.IsSelected)
                .Select(i => i.idIdioma)
                .ToArray();

            if (idiomasSeleccionados == null || idiomasSeleccionados.Length == 0)
            {
                MessageBox.Show("Seleccione al menos un idioma.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var servicio = new GestionUsuariosClient();
                var profesorDTO = new ProfesorDTO
                {
                    nombre = txtNombre.Text,
                    apellidos = txtApellidos.Text,
                    email = txtCorreo.Text,
                    direccion = txtDireccion.Text,
                    nombreUsuario = txtUsuario.Text,
                    contrasenia = txtContraseña.Password,
                    telefono = txtTelefono.Text
                };

                bool resultado = servicio.RegistrarProfesor(profesorDTO, idiomasSeleccionados);

                if (resultado)
                {
                    MessageBox.Show("Profesor registrado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al registrar el profesor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtTelefono.Clear();

            foreach (var item in dataGridIdiomas.ItemsSource)
            {
                if (item is IdiomaDTO idioma)
                    idioma.IsSelected = false;
            }
            dataGridIdiomas.Items.Refresh();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}