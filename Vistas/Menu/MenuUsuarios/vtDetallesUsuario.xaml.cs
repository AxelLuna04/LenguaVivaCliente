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
        private UsuarioDTO usuarioSeleccionado;
        private int tipoUsuario;

        public vtDetallesUsuario(UsuarioDTO usuarioSeleccionado, int tipoUsuario)
        {
            InitializeComponent();
            this.usuarioSeleccionado = usuarioSeleccionado;
            this.tipoUsuario = tipoUsuario;

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
            MessageBoxResult confirmacion = MessageBox.Show("¿Estás seguro de eliminar este usuario?",
                                                             "Confirmación",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Warning);
            if (confirmacion == MessageBoxResult.Yes)
            {
                try
                {
                    using (var proxy = new GestionUsuariosClient())
                    {
                        UsuarioDTO usuarioEliminado = proxy.EliminarUsuario(usuarioSeleccionado.correo, usuarioSeleccionado.tipoUsuario);
                        if (usuarioEliminado != null)
                        {
                            MessageBox.Show("Usuario eliminado exitosamente.", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService?.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
