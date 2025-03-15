using LenguaVivaCliente.ServicioLenguaViva;
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
            if (proxy.IniciarSesion(tbUsuario.Text, tpContraseña.Password))
                MessageBox.Show("Inicio correcto");
            else
                MessageBox.Show("Inicio incorrecto");
        }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error de conexión: {ex.Message}");
                }
            }
        }
    }
}
