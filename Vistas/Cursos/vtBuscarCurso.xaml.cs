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
            
            Curso[] cursos = servicio.ObtenerCursos(tbNombre.Text);


            List<ResumenCurso> listaCursos = new List<ResumenCurso>();
            for (int i=0; i < cursos.Length; i++)
            {
                ResumenCurso resumenCurso = new ResumenCurso { 
                    nombre = cursos[i].nombreCurso,
                    idioma = cursos[i].Idioma.nombreIdioma,
                    estado = cursos[i].estado, 
                    profesor = cursos[i].Profesor.nombre };

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

        }

        private void Click_Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Click_Confirmar(object sender, RoutedEventArgs e)
        {

        }

        private void tbNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualizarTabla();
        }
    }
}
