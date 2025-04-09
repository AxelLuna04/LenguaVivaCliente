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

namespace LenguaVivaCliente.Vistas.Alumnos
{
    /// <summary>
    /// Lógica de interacción para vtInscribirAlumno.xaml
    /// </summary>
    public partial class vtInscribirAlumno : Page
    {
        public vtInscribirAlumno()
        {
            InitializeComponent();

            actualizarAlumnos();
            actualizarCursos();
        }

        private void BtnInscribir_Click(object sender, RoutedEventArgs e)
        {
            ServicioLenguaViva.IGestionAlumnos servicio = new ServicioLenguaViva.GestionAlumnosClient();

            

            InscripcionDTO inscripcion = new InscripcionDTO
            {
                idAlumno = ((AlumnoDTO)cbAlumnos.SelectedItem).idAlumno,
                idCurso = ((CursoDTO)cbCursos.SelectedItem).idCurso,
                fechaInscripcion = DateTime.Now
            };

            if (servicio.InscripcionExistente(inscripcion.idCurso, inscripcion.idAlumno))
            {
                VentanasEmergentes.CrearVentanaEmergente("Error", "El alumno ya está inscrito en este curso.");
                return;
            }

            if (servicio.RegistrarInscripcion(inscripcion))
            {
                VentanasEmergentes.CrearVentanaEmergente("Inscripción exitosa", "El alumno se ha inscrito correctamente al curso.");
                NavigationService?.GoBack();
            }
            else
            {
                VentanasEmergentes.CrearVentanaEmergente("Error", "No se pudo inscribir al alumno en el curso.");
            }
        }
        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de regreso al menú principal
            NavigationService?.GoBack();
        }

        private void tbAlumno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbAlumnos != null)
            {
                actualizarAlumnos();
            }
        }

        private void tbCurso_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbCursos != null)
            {
                actualizarCursos();
            }
        } 

        private void actualizarAlumnos()
        {
            ServicioLenguaViva.IGestionAlumnos servicio = new ServicioLenguaViva.GestionAlumnosClient();
            AlumnoDTO[] alumnos = servicio.BuscarAlumnos(tbAlumno.Text);
            if (alumnos != null)
            {
                cbAlumnos.ItemsSource = null;
                cbAlumnos.ItemsSource = alumnos;
            }
        }

        private void actualizarCursos()
        {
            ServicioLenguaViva.IGestionCursos servicioCursos = new ServicioLenguaViva.GestionCursosClient();
            ServicioLenguaViva.IGestionAlumnos servicioAlumnos = new ServicioLenguaViva.GestionAlumnosClient();
            CursoDTO[] cursos = servicioCursos.ObtenerCursosPorNombre(tbCurso.Text);
            
            if (cursos != null)
            {
                List<CursoDTO> cursosDisponibles = new List<CursoDTO>();
                for (int i = 0; i<cursos.Length; i++)
                {
                    int numInscritos = servicioAlumnos.ObtenerNumInscripciones(cursos[i].idCurso);
                    if ((cursos[i].estado != "Inactivo") && (numInscritos < cursos[i].cupoMaximo && numInscritos >= 0))
                    {
                        cursosDisponibles.Add(cursos[i]);
                    }
                }
                cbCursos.ItemsSource = null;
                cbCursos.ItemsSource = cursosDisponibles;
            }
        }
    }
}
