using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using LenguaVivaCliente.Vistas.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace LenguaVivaCliente.Vistas.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para vtInicioSesion.xaml
    /// </summary>
    public partial class vtInicioSesion : Page
    {
        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            using (GestionUsuariosClient proxy = new GestionUsuariosClient())
            {
                try
                {
                    SesionUsuario sesionUsuario = proxy.IniciarSesion(tbUsuario.Text, tpContraseña.Password);
                    if (sesionUsuario.esValido)
                    {
                        NavigationService?.Navigate(new Vistas.Menu.MenuPrincipal(sesionUsuario));
                    }
                    else
                    {
                        lbCredencialesIncorrectas.Visibility = Visibility.Visible;
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error de conexión: {ex.Message}");
                }
            }
        }

        private void tbUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbUsuarioPlaceholder.Visibility = string.IsNullOrEmpty(tbUsuario.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void tpContraseña_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tpContraseñaPlaceholder.Visibility = string.IsNullOrEmpty(tpContraseña.Password) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
