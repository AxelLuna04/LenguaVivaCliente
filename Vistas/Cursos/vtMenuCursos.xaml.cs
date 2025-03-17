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
    /// Lógica de interacción para vtMenuCursos.xaml
    /// </summary>
    public partial class vtMenuCursos : Page
    {
        public vtMenuCursos()
        {
            InitializeComponent();
        }

        private void Click_Registrar(object sender, RoutedEventArgs e)
        {
            vtRegistrarCurso vtRegistrarCurso = new vtRegistrarCurso();
            NavigationService.Navigate(vtRegistrarCurso);
        }

        private void Click_Buscar(object sender, RoutedEventArgs e)
        {
            vtBuscarCurso vtBuscarCurso = new vtBuscarCurso();
            NavigationService.Navigate(vtBuscarCurso);
        }

        private void Click_Editar(object sender, RoutedEventArgs e)
        {
            vtEditarCurso vtEditarCurso = new vtEditarCurso();
            NavigationService.Navigate(vtEditarCurso);
        }

    }
}
