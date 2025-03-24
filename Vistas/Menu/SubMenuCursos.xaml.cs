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
using LenguaVivaCliente.Vistas.Cursos;

namespace LenguaVivaCliente.Vistas.Menu
{
    /// <summary>
    /// Lógica de interacción para SubMenuCursos.xaml
    /// </summary>
    public partial class SubMenuCursos : Page
    {
        private int tipoUsuario;
        public SubMenuCursos (SesionUsuario sesionUsuario)
        {
            InitializeComponent();
            tipoUsuario = sesionUsuario.tipoUsuario;
            if (sesionUsuario.tipoUsuario == 2)
            {
                BtnRegistrarCurso.Visibility = Visibility.Collapsed;
                BorderRegistrarCurso.Visibility = Visibility.Collapsed;
            }
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de regreso al menú principal
            NavigationService?.GoBack();
        }

        private void Click_RegistrarCurso(object sender, RoutedEventArgs e)
        {
            vtRegistrarCurso vtRegistrarCurso = new vtRegistrarCurso();
            NavigationService.Navigate(vtRegistrarCurso);
        }

        private void Click_BuscarCurso(object sender, RoutedEventArgs e)
        {
            vtBuscarCurso vtBuscarCurso = new vtBuscarCurso(tipoUsuario);
            NavigationService.Navigate(vtBuscarCurso);
        }
    }
}
