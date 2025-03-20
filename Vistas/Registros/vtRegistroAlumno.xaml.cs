using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Registros
{
    public partial class vtRegistroAlumno : Page
    {
        public ObservableCollection<IdiomaDTO> IdiomasDisponibles { get; set; }
        public ObservableCollection<string> Niveles { get; set; } = new ObservableCollection<string> { "Básico", "Intermedio", "Avanzado" };
        public ObservableCollection<IdiomaNivelDTO> IdiomasAgregados { get; set; } = new ObservableCollection<IdiomaNivelDTO>();

        public vtRegistroAlumno()
        {
            InitializeComponent();
            this.DataContext = this;
            CargarIdiomas();
        }

        private void CargarIdiomas()
        {
            try
            {
                var servicio = new GestionUsuariosClient();
                IdiomasDisponibles = new ObservableCollection<IdiomaDTO>(servicio.CargarIdiomas());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando idiomas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (cboIdiomas.SelectedItem == null || cboNiveles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un idioma y un nivel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var idiomaSeleccionado = (IdiomaDTO)cboIdiomas.SelectedItem;
            var nivelSeleccionado = cboNiveles.SelectedItem.ToString();

            int nivelInt;
            switch (nivelSeleccionado)
            {
                case "Básico":
                    nivelInt = 1;
                    break;
                case "Intermedio":
                    nivelInt = 2;
                    break;
                case "Avanzado":
                    nivelInt = 3;
                    break;
                default:
                    nivelInt = 0;
                    break;
            }

            if (IdiomasAgregados.Any(i => i.idIdioma == idiomaSeleccionado.idIdioma))
            {
                MessageBox.Show("Este idioma ya ha sido agregado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nuevoIdiomaNivel = new IdiomaNivelDTO
            {
                idIdioma = idiomaSeleccionado.idIdioma,
                nivel = nivelInt
            };
            nuevoIdiomaNivel.idiomaNombre = idiomaSeleccionado.nombreIdioma;

            IdiomasAgregados.Add(nuevoIdiomaNivel);
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Complete todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IdiomasAgregados.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un idioma.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var servicio = new GestionUsuariosClient();
                var alumnoDTO = new AlumnoDTO
                {
                    nombre = txtNombre.Text,
                    apellidos = txtApellidos.Text,
                    direccion = txtDireccion.Text,
                    email = txtCorreo.Text,
                    telefono = txtTelefono.Text,
                };

                bool resultado = servicio.RegistrarAlumno(alumnoDTO, IdiomasAgregados.ToArray());

                if (resultado)
                {
                    MessageBox.Show("Alumno registrado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al registrar el alumno.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            IdiomasAgregados.Clear();
            txtTelefono.Clear();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
