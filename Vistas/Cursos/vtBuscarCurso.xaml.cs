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

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtBuscarCurso.xaml
    /// </summary>
    public partial class vtBuscarCurso : Page
    {

        private class ResumenCurso
        {
            public int idCurso { get; set; }
            public string nombre { get; set; }
            public string idioma { get; set; }
            public string nivel { get; set; }
            public string estado { get; set; }
            public string profesor { get; set; }

        }

        public vtBuscarCurso()
        {
            InitializeComponent();
            actualizarTabla();
            
        }


        private void actualizarTabla ()
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
            
            CursoDTO[] cursos = servicio.ObtenerCursosPorNombre(tbNombre.Text);

            List<ResumenCurso> listaCursos = new List<ResumenCurso>();

            for (int i=0; i < cursos.Length; i++)
            {
                
                ResumenCurso resumenCurso = new ResumenCurso { 
                    idCurso = cursos[i].idCurso,
                    nombre = cursos[i].nombreCurso,
                    estado = cursos[i].estado
                };
                resumenCurso.idioma = servicio.ObtenerIdiomaPorID(cursos[i].idIdioma).nombreIdioma;
                resumenCurso.profesor = servicio.ObtenerProfesorPorID(cursos[i].idProfesor).nombre;

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
                    vtInformacionCurso vtInformacionCurso = new vtInformacionCurso(curso);
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

        private void tbNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualizarTabla();
        }
    }
}
