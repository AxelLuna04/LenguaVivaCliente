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

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtBuscarCurso.xaml
    /// </summary>
    public partial class vtBuscarCurso : Page
    {
        public class Persona
        {
            public string Nombre { get; set; }
            public int Edad { get; set; }
        }

        public vtBuscarCurso()
        {
            InitializeComponent();
            List<Persona> personas = new List<Persona>
                {
                    new Persona { Nombre = "Juan", Edad = 25 },
                    new Persona { Nombre = "Ana", Edad = 30 }
                };
            dgCursos.ItemsSource = personas;
        }



        private void Click_VerInformacion(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Click_Confirmar(object sender, RoutedEventArgs e)
        {

        }
    }
}
