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
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vistas.Registros.vtRegistros registros = new Vistas.Registros.vtRegistros();
            NavigationService.Navigate(registros);

        }

        private void Click_MenuCursos(object sender, RoutedEventArgs e)
        {
            Vistas.Cursos.vtMenuCursos menuCursos = new Vistas.Cursos.vtMenuCursos();
            NavigationService.Navigate(menuCursos);
        }
    }
}
