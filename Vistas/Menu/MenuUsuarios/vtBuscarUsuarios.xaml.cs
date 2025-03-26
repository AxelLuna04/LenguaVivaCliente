using LenguaVivaCliente.Utilidades;
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

namespace LenguaVivaCliente.Vistas.Menu.MenuUsuarios
{
    /// <summary>
    /// Lógica de interacción para vtBuscarUsuarios.xaml
    /// </summary>
    public partial class vtBuscarUsuarios : Page
    {
        private int tipoUsuario;
        public vtBuscarUsuarios(int tipoUsuario)
        {
            InitializeComponent();
            this.tipoUsuario = tipoUsuario;
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de regreso al menú principal
            NavigationService?.GoBack();
        }

        private void Click_Buscar(object sender, RoutedEventArgs e)
        {
            string criterioBusqueda = TxtBuscar.Text;
            if (criterioBusqueda != "")
            {
                Vistas.Menu.MenuUsuarios.vtUsuariosEncontrados vtUsuariosEncontrados = new Vistas.Menu.MenuUsuarios.vtUsuariosEncontrados(criterioBusqueda, tipoUsuario);
                NavigationService.Navigate(vtUsuariosEncontrados);
            }
            else
                VentanasEmergentes.CrearVentanaEmergente("Campos vacíos", "Favor de ingresar la información en el campo de búsqueda.");
        }
    }
}
