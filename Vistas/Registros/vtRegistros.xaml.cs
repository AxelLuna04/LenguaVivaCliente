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

namespace LenguaVivaCliente.Vistas.Registros
{
    /// <summary>
    /// Lógica de interacción para vtRegistros.xaml
    /// </summary>
    public partial class vtRegistros : Page
    {
        public vtRegistros()
        {
            InitializeComponent();
        }

        private void BtnAlumno_Click(object sender, RoutedEventArgs e)
        {
            Vistas.Registros.vtRegistroAlumno registro = new Vistas.Registros.vtRegistroAlumno();
            NavigationService.Navigate(registro);
        }

        private void BtnAdministrador_Click(object sender, RoutedEventArgs e)
        {
            Vistas.Registros.vtRegistroAdministrador registro = new Vistas.Registros.vtRegistroAdministrador();
            NavigationService.Navigate(registro);
        }

        private void BtnProfesor_Click(object sender, RoutedEventArgs e)
        {
            Vistas.Registros.vtRegistroProfesor registro = new Vistas.Registros.vtRegistroProfesor();
            NavigationService.Navigate(registro);
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
