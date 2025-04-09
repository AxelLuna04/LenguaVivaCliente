using LenguaVivaCliente.ServicioLenguaViva;
using LenguaVivaCliente.Utilidades;
using LenguaVivaCliente.Vistas.Menu.MenuUsuarios;
//using ServicioContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
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

namespace LenguaVivaCliente.Vistas.Alumnos
{
    /// <summary>
    /// Lógica de interacción para vtHistorialDeCursos.xaml
    /// </summary>
    public partial class vtHistorialDeCursos : Page
    {
        private int idAlumno;
        private CursoAlumno[] cursosEncontrados;
        private CursoAlumno cursoSeleccionado;
        GestionAlumnosClient proxy = new GestionAlumnosClient();
        public vtHistorialDeCursos(int idAlumno)
        {
            InitializeComponent();
            this.idAlumno = idAlumno;
            CargarHistorialCursos(idAlumno);
        }

        public void CargarHistorialCursos(int idAlumno)
        {
            cursosEncontrados = proxy.ObtenerCursosDeAlumno(idAlumno);

            if(cursosEncontrados.Length != 0)
            {
                LvCursos.ItemsSource = cursosEncontrados;
            }else
                VentanasEmergentes.CrearVentanaEmergente("Sin resultados de búsqueda", "No se ha encontrado ningún curso");
        }

        private void ObtenerCursoSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            cursoSeleccionado = LvCursos.SelectedItem as CursoAlumno;
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnReciboPago_Click(object sender, RoutedEventArgs e)
        {
            var idInscripcion = proxy.ObtenerIDInscripcionPorAlumnoYCurso(idAlumno, cursoSeleccionado.idCurso);
            vtReciboDePago reciboPago = new vtReciboDePago(idInscripcion);
            reciboPago.Show();
        }
    }
}
