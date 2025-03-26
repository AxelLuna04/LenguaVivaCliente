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
using System.Windows.Media.Animation;
using LenguaVivaCliente.Utilidades;

namespace LenguaVivaCliente.Vistas.InicioSesion
{
    public partial class vtInicioSesion : Page
    {
        public vtInicioSesion()
        {
            InitializeComponent();
        }

        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string correo = tbUsuario.Text;
            string contrasenia = tpContraseña.Password;

            if(correo == "" || contrasenia == "")
            {
                VentanasEmergentes.CrearVentanaEmergente("Campos vacíos", "Favor de llenar todos los campos.");
            }
            else
            {
                using (GestionUsuariosClient proxy = new GestionUsuariosClient())
                {
                    try
                    {
                        SesionUsuario sesionUsuario = proxy.IniciarSesion(correo, contrasenia);
                        if (sesionUsuario.esValido)
                        {
                            NavigationService?.Navigate(new Vistas.Menu.MenuPrincipal(sesionUsuario));
                        }
                        else
                        {
                            lbCredencialesIncorrectas.Visibility = Visibility.Visible;
                            lbCredencialesIncorrectas.Opacity = 1;

                            Storyboard fadeOutStoryboard = (Storyboard)FindResource("FadeOutStoryboard");
                            fadeOutStoryboard.Begin();

                            Storyboard shakeAnimation = (Storyboard)FindResource("ShakeAnimation");
                            shakeAnimation.Begin(LoginPanel);

                            tpContraseña.Password = "";
                            tpContraseña.Focus();
                        }
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("No se pudo conectar con el servidor. Por favor, intente más tarde.",
                                      "Error de conexión",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show("Error de comunicación con el servidor.",
                                      "Error",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error inesperado: {ex.Message}",
                                      "Error",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
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

            if (tbContraseñaVisible.Visibility == Visibility.Visible)
            {
                tbContraseñaVisible.Text = tpContraseña.Password;
            }
        }

        private void btnTogglePassword_Click(object sender, RoutedEventArgs e)
        {
            var eyeIcon = (Image)btnTogglePassword.Content;

            if (tpContraseña.Visibility == Visibility.Visible)
            {
                tbContraseñaVisible.Text = tpContraseña.Password;
                tbContraseñaVisible.Visibility = Visibility.Visible;
                tpContraseña.Visibility = Visibility.Collapsed;
                eyeIcon.Source = new BitmapImage(new Uri("/ImagenesProyecto/eye_hide.png", UriKind.Relative));
            }
            else
            {
                tpContraseña.Password = tbContraseñaVisible.Text;
                tpContraseña.Visibility = Visibility.Visible;
                tbContraseñaVisible.Visibility = Visibility.Collapsed;
                eyeIcon.Source = new BitmapImage(new Uri("/ImagenesProyecto/eye_show.png", UriKind.Relative));
            }

            tpContraseñaPlaceholder.Visibility = string.IsNullOrEmpty(tpContraseña.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void tbContraseñaVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbContraseñaVisible.Visibility == Visibility.Visible)
            {
                tpContraseña.Password = tbContraseñaVisible.Text;
            }
            tpContraseñaPlaceholder.Visibility = string.IsNullOrEmpty(tbContraseñaVisible.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
