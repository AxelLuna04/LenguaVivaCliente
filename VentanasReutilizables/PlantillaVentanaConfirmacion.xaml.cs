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

namespace LenguaVivaCliente.VentanasReutilizables
{
    /// <summary>
    /// Lógica de interacción para PlantillaVentanaConfirmacion.xaml
    /// </summary>
    public partial class PlantillaVentanaConfirmacion : UserControl
    {
        public PlantillaVentanaConfirmacion()
        {
            InitializeComponent();
        }
        private readonly Window ventanaPrincipal;
        public PlantillaVentanaConfirmacion(string titulo, string descripcion)
        {
            InitializeComponent();
            ventanaPrincipal = Application.Current.MainWindow;
            lbTituloVentanaEmergente.Content = titulo;
            tbDescripcionVentanaEmergente.Text = descripcion;
             
        } 

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //return true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
