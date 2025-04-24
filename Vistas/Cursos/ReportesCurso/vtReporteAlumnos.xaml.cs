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

namespace LenguaVivaCliente.Vistas.Cursos.ReportesCurso
{
    /// <summary>
    /// Lógica de interacción para vtReporteAlumnos.xaml
    /// </summary>
    public partial class vtReporteAlumnos : Page
    {
        public vtReporteAlumnos(int idCurso)
        {
            InitializeComponent();
            cargarDatos(idCurso);
        }

        private void Click_Regresar(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void cargarDatos(int idCurso)
        {
            ServicioLenguaViva.IGestionReportes proxy = new ServicioLenguaViva.GestionReportesClient();

            AlumnoReporteDTO[] alumnos = proxy.ObtenerReporteAlumnosPorCurso(idCurso);

            if (alumnos == null || alumnos.Length == 0)
            {
                VentanasEmergentes.CrearVentanaEmergente("Advertencia", "No hay alumnos inscritos en este curso.");
                NavigationService?.GoBack();
                return;
            }

            dgAlumnos.ItemsSource = alumnos;
            dgAlumnos.Items.Refresh();

            //dgCursos.ItemsSource = listaCursos;
            //dgCursos.Items.Refresh();
        }
    }
}
