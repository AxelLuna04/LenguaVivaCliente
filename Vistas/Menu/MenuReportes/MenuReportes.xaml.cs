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

namespace LenguaVivaCliente.Vistas.Menu.MenuReportes
{
    /// <summary>
    /// Lógica de interacción para MenuReportes.xaml
    /// </summary>
    public partial class MenuReportes : Page
    {
        private SesionUsuario sesionUsuario;
        public MenuReportes(SesionUsuario sesionUsuario)
        {
            InitializeComponent();
            this.sesionUsuario = sesionUsuario;
            if(sesionUsuario.tipoUsuario == 2)
            {
                BtnReporteAlumnos.Visibility = Visibility.Collapsed;
                BtnReporteAsistencia.Visibility = Visibility.Collapsed;
                BtnReporteIngresos.Visibility = Visibility.Collapsed;
                BtnReporteProfesores.Visibility = Visibility.Collapsed;
                BorderReportesAlumnos.Visibility = Visibility.Collapsed;
                BorderReporteAsistencias.Visibility = Visibility.Collapsed;
                BorderReporteIngresos.Visibility = Visibility.Collapsed;
                BorderReporteProfesores.Visibility= Visibility.Collapsed;
                BorderVerHorario.Visibility = Visibility.Visible;
                BtnVerHorario.Visibility = Visibility.Visible;
            }
        }

        private void Click_VerHorario(object sender, RoutedEventArgs e)
        {
            Vistas.Reportes.vtHorarioProfesor vtHorarioProfesor = new Vistas.Reportes.vtHorarioProfesor(sesionUsuario);
            NavigationService.Navigate(vtHorarioProfesor);
        }

        private void Click_ReporteProfesores(object sender, RoutedEventArgs e)
        {
            Vistas.Reportes.vtReporteDeProfesores vtReporteDeProfesor = new Vistas.Reportes.vtReporteDeProfesores();
            NavigationService.Navigate(vtReporteDeProfesor);
        }

        private void Click_ReporteAsistencia(object sender, RoutedEventArgs e)
        {

        }

        private void Click_ReporteIngresos(object sender, RoutedEventArgs e)
        {

        }

        private void Click_ReporteAlumnos(object sender, RoutedEventArgs e)
        {

        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
