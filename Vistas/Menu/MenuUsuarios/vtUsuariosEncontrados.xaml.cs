using LenguaVivaCliente.ServicioLenguaViva;
using LenguaVivaCliente.Utilidades;
using ServicioContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
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
    /// Lógica de interacción para vtUsuariosEncontrados.xaml
    /// </summary>
    public partial class vtUsuariosEncontrados : Page
    {
        private UsuarioDTO usuarioSeleccionado;
        private UsuarioDTO[] usuariosEncontrados;
        private int tipoUsuario;
        public vtUsuariosEncontrados(string criterioBusqueda, int tipoUsuario)
        {
            InitializeComponent();
            this.tipoUsuario = tipoUsuario;
            CargarUsuariosEncontrados(criterioBusqueda, tipoUsuario);
        }

        private void CargarUsuariosEncontrados(string criterioBusqueda, int tipoUsuario)
        {
            GestionUsuariosClient proxy = new GestionUsuariosClient();
            usuariosEncontrados = proxy.BuscarUsuarios(criterioBusqueda, tipoUsuario);
            if (usuariosEncontrados.Length != 0)
                LvUsuarios.ItemsSource = usuariosEncontrados;
            else
                VentanasEmergentes.CrearVentanaEmergente("Sin resultados de búsqueda", "No se ha encontrado ningún usuario que coincida con la información proporcionada.");

        }

        private void ObtenerUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            usuarioSeleccionado = LvUsuarios.SelectedItem as UsuarioDTO;

            if (usuarioSeleccionado != null)
            {
                NavigationService?.Navigate(new vtDetallesUsuario(usuarioSeleccionado, tipoUsuario));
            }
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

    }
}
