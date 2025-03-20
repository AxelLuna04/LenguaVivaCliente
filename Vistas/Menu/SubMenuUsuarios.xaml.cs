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
    /// Lógica de interacción para SubMenuUsuarios.xaml
    /// </summary>
    public partial class SubMenuUsuarios : Page
    {
        private int tipoUsuario;
        public SubMenuUsuarios(SesionUsuario sesionUsuario)
        {
            InitializeComponent();
            tipoUsuario = sesionUsuario.tipoUsuario;
            if (sesionUsuario.tipoUsuario == 2)
            {
                BtnRegistrarUsuarios.Visibility = Visibility.Collapsed;
                BorderRegistrarUsuarios.Visibility = Visibility.Collapsed;
            }
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de regreso al menú principal
            NavigationService?.GoBack();
        }

        private void Click_RegistrarUsuarios(object sender, RoutedEventArgs e)
        {
            Vistas.Registros.vtRegistros vtRegistros= new Vistas.Registros.vtRegistros();
            NavigationService.Navigate(vtRegistros);
        }

        private void Click_BuscarUsuarios(object sender, RoutedEventArgs e)
        {
            Vistas.Menu.MenuUsuarios.vtBuscarUsuarios vtBuscarUsuarios = new Vistas.Menu.MenuUsuarios.vtBuscarUsuarios(tipoUsuario);
            NavigationService.Navigate(vtBuscarUsuarios);
        }
    }
}
