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

namespace LenguaVivaCliente.Vistas.Menu
{
    /// <summary>
    /// Lógica de interacción para SubMenuAlumnos.xaml
    /// </summary>
    public partial class SubMenuAlumnos : Page
    {
        private int tipoUsuario;
        public SubMenuAlumnos(SesionUsuario sesionUsuario)
        {
            InitializeComponent();
            tipoUsuario = sesionUsuario.tipoUsuario;
            if (sesionUsuario.tipoUsuario == 2)
            {
                btnInscribirAlumno.Visibility = Visibility.Collapsed;
                borderBotones.Visibility = Visibility.Collapsed;
            }
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de regreso al menú principal
            NavigationService?.GoBack();
        }

        private void Click_InscribirAlumno(object sender, RoutedEventArgs e)
        {
            Vistas.Alumnos.vtInscribirAlumno vtInscribirAlumno = new Vistas.Alumnos.vtInscribirAlumno();
            NavigationService.Navigate(vtInscribirAlumno);
        }
    }
}

