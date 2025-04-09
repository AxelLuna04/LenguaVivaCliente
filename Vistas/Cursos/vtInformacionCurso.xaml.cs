using LenguaVivaCliente.ServicioLenguaViva;
using LenguaVivaCliente.Utilidades;
using ServicioContrato;
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

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtInformacionCurso.xaml
    /// </summary>
    public partial class vtInformacionCurso : Page
    {
        private CursoDTO curso;
        private int tipoUsuario;
        public vtInformacionCurso(CursoDTO curso, int tipoUsuario)
        {
            InitializeComponent();
            Application.Current.MainWindow.Width = this.MinWidth;
            Application.Current.MainWindow.Height = this.MinHeight; 
            this.curso = curso;
            this.Loaded += Page_Loaded;
            this.tipoUsuario = tipoUsuario;
            if (tipoUsuario == 2)
            {
                btnEditar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Collapsed;
                btnSubirLista.Visibility = Visibility.Collapsed;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
            this.curso = servicio.ObtenerCursoPorID(curso.idCurso);
            cargarDatos();
        }

        private void cargarDatos()
        {
            tbNombre.Text = curso.nombreCurso;
            tbDescripcion.Text = curso.descripcion;
            tbCupo.Text = curso.cupoMaximo.ToString();

            cbEstado.Items.Clear();
            cbEstado.Items.Add(curso.estado);
            cbEstado.SelectedIndex = 0;

            dpFechaInicio.SelectedDate = curso.fechaInicio;
            dpFechaTermino.SelectedDate = curso.fechaTermino;

            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
            IdiomaDTO idioma = servicio.ObtenerIdiomaPorID(curso.idIdioma);

            cbIdiomas.Items.Clear();
            cbIdiomas.Items.Add(idioma);
            cbIdiomas.DisplayMemberPath = "nombreIdioma";
            cbIdiomas.SelectedIndex = 0;

            ProfesorDTO profesor = servicio.ObtenerProfesorPorID(curso.idProfesor);
            cbProfesores.Items.Clear();
            cbProfesores.Items.Add(profesor);
            cbProfesores.SelectedIndex = 0;

            switch (curso.nivel)
            {
                case 1:
                    rbBasico.IsChecked = true;
                    break;
                case 2:
                    rbIntermedio.IsChecked = true;
                    break;
                case 3:
                    rbAvanzado.IsChecked = true;
                    break;
            }

        }

        private void Click_Volver(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Click_Eliminar(object sender, RoutedEventArgs e)
        {
            //TODO: Eliminar curso
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();


            if (servicio.TieneAlumnosInscritos(curso.idCurso) == false)
            {
                bool confirmado = VentanasEmergentes.CrearVentanaConfirmacion("¿Eliminar archivo?",
                    "¿Estás seguro de que quieres eliminar el archivo? Esta acción no se puede deshacer.");

                if (confirmado)
                {
                    if (servicio.EliminarCurso(curso.idCurso) == true)
                    {
                        VentanasEmergentes.CrearVentanaEmergente("Eliminado", "Curso eliminado correctamente.");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        VentanasEmergentes.CrearVentanaEmergente("Error", "No se pudo eliminar el curso.");
                    }

                    //servicio.EliminarCurso(curso.idCurso);
                }
            }
            else
            {
                VentanasEmergentes.CrearVentanaEmergente("Error", "No se puede eliminar el curso porque tiene alumnos inscritos.");

            }
                
        }

        private void Click_Editar(object sender, RoutedEventArgs e)
        {
            vtEditarCurso vtEditarCurso = new vtEditarCurso(curso);
            NavigationService.Navigate(vtEditarCurso);
        }

        private void Click_SubirLista(object sender, RoutedEventArgs e)
        {
            //TODO_ Subir lista de asistencia
        }

        private void Click_VerHorarios(object sender, RoutedEventArgs e)
        {

        }

        private void Click_AsignarHorario(object sender, RoutedEventArgs e)
        {

        }
    }
}
