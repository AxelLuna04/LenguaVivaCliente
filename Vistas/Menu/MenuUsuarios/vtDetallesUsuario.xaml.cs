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

namespace LenguaVivaCliente.Vistas.Menu.MenuUsuarios
{
    /// <summary>
    /// Lógica de interacción para vtDetallesUsuario.xaml
    /// </summary>
    public partial class vtDetallesUsuario : Page
    {
        public vtDetallesUsuario(UsuarioDTO usuarioSeleccionado)
        {
            InitializeComponent();
            TxtTitulo.Text = $"{usuarioSeleccionado.nombre} {usuarioSeleccionado.apellidos}";
            TxtCorreo.Text = usuarioSeleccionado.correo;
            TxtTelefono.Text = usuarioSeleccionado.telefono;
            TxtDireccion.Text = usuarioSeleccionado.direccion;
            if (usuarioSeleccionado.nivelIdioma != null)
            {
                TxtNivelIdioma.Text = usuarioSeleccionado.nivelIdioma;
                BorderNivelIdioma.Visibility = Visibility.Visible;
            }
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
