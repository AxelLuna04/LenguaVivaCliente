using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Ediciones
{
    public partial class vtEditarProfesor : Page
    {
        private ProfesorDTO profesorSeleccionado;

        public vtEditarProfesor(ProfesorDTO profesorSeleccionado)
        {
            InitializeComponent();
            this.profesorSeleccionado = profesorSeleccionado;
            CargarIdiomas();
            CargarDatos();
        }

        private void CargarIdiomas()
        {
            try
            {
                var servicio = new GestionUsuariosClient();
                var idiomas = servicio.CargarIdiomas();

                if (idiomas != null)
                {
                    var lista = idiomas.Select(i => new IdiomaDTO
                    {
                        idIdioma = i.idIdioma,
                        nombreIdioma = i.nombreIdioma,
                        IsSelected = profesorSeleccionado.idiomasDominados != null && profesorSeleccionado.idiomasDominados.Contains(i.idIdioma)
                    }).ToList();

                    dataGridIdiomas.ItemsSource = lista;
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

        private void CargarDatos()
        {
            txtNombre.Text = profesorSeleccionado.nombre;
            txtApellidos.Text = profesorSeleccionado.apellidos;
            txtCorreo.Text = profesorSeleccionado.email;
            txtDireccion.Text = profesorSeleccionado.direccion;
            txtTelefono.Text = profesorSeleccionado.telefono;
            txtUsuario.Text = profesorSeleccionado.nombreUsuario;
            txtContraseña.Password = profesorSeleccionado.contrasenia; 
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

            if (!string.IsNullOrWhiteSpace(txtContraseña.Password))
            {
                if (txtContraseña.Password.Length < 8)
                {
                    lbContraseniaVacia.Content = "Mínimo 8 caracteres si desea cambiar";
                    lbContraseniaVacia.Visibility = Visibility.Visible;
                    errores++;
                }
                else
                {
                    lbContraseniaVacia.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                lbContraseniaVacia.Visibility = Visibility.Hidden;
            }

            var idiomasSeleccionados = (dataGridIdiomas.ItemsSource as System.Collections.Generic.List<IdiomaDTO>)?
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

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            profesorSeleccionado.nombre = txtNombre.Text;
            profesorSeleccionado.apellidos = txtApellidos.Text;
            profesorSeleccionado.direccion = txtDireccion.Text;
            profesorSeleccionado.telefono = txtTelefono.Text;

            
            profesorSeleccionado.contrasenia = string.Empty;

            var idiomasSeleccionados = (dataGridIdiomas.ItemsSource as System.Collections.Generic.List<IdiomaDTO>)?
                .Where(i => i.IsSelected)
                .Select(i => i.idIdioma)
                .ToList();

            profesorSeleccionado.idiomasDominados = idiomasSeleccionados ?? new List<int>();

            try
            {
                var servicio = new GestionUsuariosClient();
                bool resultado = servicio.ModificarProfesor(profesorSeleccionado);

                if (resultado)
                {
                    MessageBox.Show("Profesor actualizado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.GoBack();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el profesor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                txtContraseña.Password = string.Empty;
            }
        }


        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
