using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtBuscarCurso.xaml
    /// </summary>
    public partial class vtBuscarCurso : Page
    {
        private class NivelComboBoxItem
        {
            public int nivel { get; set; }
            public string nombre { get; set; }
        }

        private class ResumenCurso
        {
            public int idCurso { get; set; }
            public string nombre { get; set; }
            public string idioma { get; set; }
            public string nivel { get; set; }
            public string estado { get; set; }
            public string profesor { get; set; }

        }

        private int tipoUsuario;

        public vtBuscarCurso(int tipoUsuario)
        {
            InitializeComponent();
            this.Loaded += Page_Loaded;
            this.tipoUsuario = tipoUsuario;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cargarComboBox();
            actualizarTabla();
        }

        private void cargarComboBox()
        {
            cbBusqueda.Items.Clear();
            cbBusqueda.Items.Add("Nombre del Curso");
            cbBusqueda.Items.Add("Nombre del Profesor");
            cbBusqueda.Items.Add("Idioma");
            cbBusqueda.Items.Add("Nivel");
            cbBusqueda.SelectedIndex = 0;

            List<NivelComboBoxItem> niveles = new List<NivelComboBoxItem>
            {
                new NivelComboBoxItem { nivel = 1, nombre = "Básico"},
                new NivelComboBoxItem { nivel = 2, nombre = "Intermedio"},
                new NivelComboBoxItem { nivel = 3, nombre = "Avanzado"}
            };
            cbNiveles.ItemsSource = null;
            cbNiveles.ItemsSource = niveles;
            cbNiveles.DisplayMemberPath = "nombre";
            cbNiveles.SelectedValuePath = "nivel";
            cbNiveles.SelectedIndex = 0;
        }

        private void actualizarTabla ()
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();

            CursoDTO[] cursos = obtenerCursos();


            List<ResumenCurso> listaCursos = new List<ResumenCurso>();

            for (int i=0; i < cursos.Length; i++)
            {
                
                ResumenCurso resumenCurso = new ResumenCurso { 
                    idCurso = cursos[i].idCurso,
                    nombre = cursos[i].nombreCurso,
                    estado = cursos[i].estado
                };
                resumenCurso.idioma = servicio.ObtenerIdiomaPorID(cursos[i].idIdioma).nombreIdioma;

                resumenCurso.profesor = servicio.ObtenerProfesorPorID(cursos[i].idProfesor).nombre + " " + servicio.ObtenerProfesorPorID(cursos[i].idProfesor).apellidos;

                switch (cursos[i].nivel)
                {
                    case 1:
                        resumenCurso.nivel = "Básico";
                        break;
                    case 2:
                        resumenCurso.nivel = "Intermedio";
                        break;
                    case 3:
                        resumenCurso.nivel = "Avanzado";
                        break;
                    default:
                        resumenCurso.nivel = "Desconocido";
                        break;
                }
                listaCursos.Add(resumenCurso);
            }

            dgCursos.ItemsSource = listaCursos;
            dgCursos.Items.Refresh();
        }

        private CursoDTO[] obtenerCursos()
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
            CursoDTO[] cursos;
            switch (cbBusqueda.SelectedItem)
            {
                case "Nombre del Curso":
                    cursos = servicio.ObtenerCursosPorNombre(tbBusqueda.Text);
                    break;
                case "Nombre del Profesor":
                    cursos = servicio.ObtenerCursosPorNombreProfesor(tbBusqueda.Text);
                    break;
                case "Idioma":
                    cursos = servicio.ObtenerCursosPorNombreIdioma(tbBusqueda.Text);
                    break;
                case "Nivel":
                    cursos = servicio.ObtenerCursosPorNivel((int)cbNiveles.SelectedValue);
                    break;
                default:
                    cursos = servicio.ObtenerCursosPorNombre(tbBusqueda.Text);
                    break;
            }
            return cursos;
        }

        private void Click_VerInformacion(object sender, RoutedEventArgs e)
        {
            if (dgCursos.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Seleccione un curso");
                return;
            } 
            else
            {
                ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
                CursoDTO curso = servicio.ObtenerCursoPorID(((ResumenCurso)dgCursos.SelectedItem).idCurso);

                if (curso != null)
                {
                    vtInformacionCurso vtInformacionCurso = new vtInformacionCurso(curso, tipoUsuario);
                    NavigationService.Navigate(vtInformacionCurso);
                }
                else
                {
                    System.Windows.MessageBox.Show("Error al obtener la información del curso");
                }


            }
        }

        private void Click_Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tbBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualizarTabla();
        }

        private void cbBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbBusqueda.SelectedItem)
            {
                case "Nombre del Curso":
                    lbInstrucciones.Content = "Ingrese el nombre del curso";
                    break;
                case "Nombre del Profesor":
                    lbInstrucciones.Content = "Ingrese el nombre del profesor";
                    break;
                case "Idioma":
                    lbInstrucciones.Content = "Ingrese el idioma";
                    break;
                case "Nivel":
                    lbInstrucciones.Content = "Seleccione un nivel";
                    break;
            }

            if ((String)cbBusqueda.SelectedItem == "Nivel")
            {
                cbNiveles.Visibility = Visibility.Visible;
                tbBusqueda.IsEnabled = false;
                tbBusqueda.Visibility = Visibility.Hidden;
                tbBusqueda.Text = "";
                
            }
            else
            {
                cbNiveles.Visibility = Visibility.Hidden;
                tbBusqueda.IsEnabled = true;
                tbBusqueda.Visibility = Visibility.Visible;
            }
            actualizarTabla();
        }

        private void cbNiveles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualizarTabla();
        }
    }
}
