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
using System.Windows.Shapes;

namespace LenguaVivaCliente.VentanasReutilizables
{
    /// <summary>
    /// Lógica de interacción para vtSolicitarCorreo.xaml
    /// </summary>
    public partial class vtSolicitarCorreo : Window
    {
        public string CorreoIngresado { get; private set; }

        public vtSolicitarCorreo()
        {
            InitializeComponent();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtCorreo.Text))
            {
                CorreoIngresado = TxtCorreo.Text.Trim();
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Por favor ingrese un correo válido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
