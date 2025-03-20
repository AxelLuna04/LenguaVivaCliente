using LenguaVivaCliente.ServicioLenguaViva;
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

namespace LenguaVivaCliente.Vistas.Menu
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Page
    {
        SesionUsuario sesionUsuario;
        public MenuPrincipal(SesionUsuario sesionUsuario)
        {
            InitializeComponent();
            this.sesionUsuario = sesionUsuario;
            if (sesionUsuario.tipoUsuario == 2)
            {
                BtnMenuAlumnos.Visibility = Visibility.Collapsed;
                BorderMenuAlumnos.Visibility = Visibility.Collapsed;
            }
        }

        private void Click_MenuUsuarios(object sender, RoutedEventArgs e)
        {
            Vistas.Menu.SubMenuUsuarios subMenuUsuarios = new Vistas.Menu.SubMenuUsuarios(sesionUsuario);
            NavigationService.Navigate(subMenuUsuarios);
        }

        private void Click_MenuCursos(object sender, RoutedEventArgs e)
        {
            Vistas.Cursos.vtMenuCursos menuCursos = new Vistas.Cursos.vtMenuCursos();
            NavigationService.Navigate(menuCursos);
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            // Cierra la aplicación o la ventana actual
            Application.Current.Shutdown();
            // Si estás en una ventana secundaria, puedes usar this.Close();
        }

        private void Click_MenuReportes(object sender, RoutedEventArgs e)
        {

        }

        private void Click_MenuAlumnos(object sender, RoutedEventArgs e)
        {

        }
    }
}
