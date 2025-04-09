using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Ediciones
{
    public partial class vtEditarAlumno : Page
    {
        private AlumnoDTO alumnoOriginal;

        // Colecciones para binding
        public ObservableCollection<IdiomaDTO> IdiomasDisponibles { get; set; }
        public ObservableCollection<string> Niveles { get; set; } = new ObservableCollection<string> { "Básico", "Intermedio", "Avanzado" };
        public ObservableCollection<IdiomaNivelDTO> IdiomasAgregados { get; set; } = new ObservableCollection<IdiomaNivelDTO>();

        public vtEditarAlumno(AlumnoDTO alumno)
        {
            InitializeComponent();
            this.DataContext = this;

            alumnoOriginal = alumno;

            CargarIdiomas();
            CargarDatos();
        }

        private void CargarIdiomas()
        {
            try
            {
                var servicio = new GestionUsuariosClient();
                var lista = servicio.CargarIdiomas();
                IdiomasDisponibles = new ObservableCollection<IdiomaDTO>(lista);
                // refrescar binding
                cboIdiomas.ItemsSource = IdiomasDisponibles;
                cboIdiomas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando idiomas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarDatos()
        {
            // Precargar campos de texto
            txtNombre.Text = alumnoOriginal.nombre;
            txtApellidos.Text = alumnoOriginal.apellidos;
            txtCorreo.Text = alumnoOriginal.email;
            txtDireccion.Text = alumnoOriginal.direccion;
            txtTelefono.Text = alumnoOriginal.telefono;

            // Precargar idiomas y niveles existentes
            if (alumnoOriginal.idiomasNivel != null)
            {
                foreach (var inDto in alumnoOriginal.idiomasNivel)
                {
                    IdiomasAgregados.Add(new IdiomaNivelDTO
                    {
                        idIdioma = inDto.idIdioma,
                        nivel = inDto.nivel,
                        idiomaNombre = inDto.idiomaNombre
                    });
                }
            }else
            {
                MessageBox.Show("El alumno no tiene idiomas asignados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            dgIdiomas.ItemsSource = IdiomasAgregados;
        }

        private bool ValidarCampos()
        {
            int errores = 0;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lbNombreVacio.Visibility = Visibility.Visible; errores++;
            }
            else lbNombreVacio.Visibility = Visibility.Hidden;

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                lbApellidosVacios.Visibility = Visibility.Visible; errores++;
            }
            else lbApellidosVacios.Visibility = Visibility.Hidden;

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                lbCorreoVacio.Content = "Campo obligatorio"; lbCorreoVacio.Visibility = Visibility.Visible; errores++;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lbCorreoVacio.Content = "Formato inválido"; lbCorreoVacio.Visibility = Visibility.Visible; errores++;
            }
            else lbCorreoVacio.Visibility = Visibility.Hidden;

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lbDireccionVacia.Visibility = Visibility.Visible; errores++;
            }
            else lbDireccionVacia.Visibility = Visibility.Hidden;

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lbTelefonoVacio.Content = "Campo obligatorio"; lbTelefonoVacio.Visibility = Visibility.Visible; errores++;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, @"^\d+$"))
            {
                lbTelefonoVacio.Content = "Solo números"; lbTelefonoVacio.Visibility = Visibility.Visible; errores++;
            }
            else lbTelefonoVacio.Visibility = Visibility.Hidden;

            if (IdiomasAgregados.Count == 0)
            {
                lbIdiomasError.Visibility = Visibility.Visible; errores++;
            }
            else lbIdiomasError.Visibility = Visibility.Hidden;

            return errores == 0;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (cboIdiomas.SelectedItem is IdiomaDTO idioma && cboNiveles.SelectedItem is string nivelStr)
            {
                var idiomaExistente = IdiomasAgregados.FirstOrDefault(i => i.idIdioma == idioma.idIdioma);

                if (idiomaExistente != null)
                {
                    int nivel = 0;
                    if (nivelStr == "Básico") nivel = 1;
                    else if (nivelStr == "Intermedio") nivel = 2;
                    else if (nivelStr == "Avanzado") nivel = 3;

                    idiomaExistente.nivel = nivel;

                    dgIdiomas.Items.Refresh();
                }
                else
                {
                    int nivel = 0;
                    if (nivelStr == "Básico") nivel = 1;
                    else if (nivelStr == "Intermedio") nivel = 2;
                    else if (nivelStr == "Avanzado") nivel = 3;

                    IdiomasAgregados.Add(new IdiomaNivelDTO
                    {
                        idIdioma = idioma.idIdioma,
                        nivel = nivel,
                        idiomaNombre = idioma.nombreIdioma
                    });
                }
            }
        }


        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos()) return;

            alumnoOriginal.nombre = txtNombre.Text;
            alumnoOriginal.apellidos = txtApellidos.Text;
            alumnoOriginal.email = txtCorreo.Text;
            alumnoOriginal.direccion = txtDireccion.Text;
            alumnoOriginal.telefono = txtTelefono.Text;
            alumnoOriginal.idiomasNivel = IdiomasAgregados.ToArray();

            try
            {
                var servicio = new GestionUsuariosClient();
                bool exito = servicio.ModificarAlumno(alumnoOriginal, IdiomasAgregados.ToArray());
                if (exito)
                {
                    MessageBox.Show("Alumno actualizado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.GoBack();
                }
                else
                {
                    MessageBox.Show("Error al actualizar alumno.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
