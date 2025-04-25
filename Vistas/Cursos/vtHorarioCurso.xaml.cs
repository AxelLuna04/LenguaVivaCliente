using LenguaVivaCliente.Utilidades;
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

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtHorarioCurso.xaml
    /// </summary>
    public partial class vtHorarioCurso : Page
    {
        public vtHorarioCurso(int idCurso)
        {
            InitializeComponent();

            cargarHorarios(idCurso);
        }

        private void cargarHorarios(int idCurso)
        {
            ServicioLenguaViva.IGestionCursos proxy = new ServicioLenguaViva.GestionCursosClient();
            HorarioDTO[] horarios = proxy.ObtenerHorariosPorCurso(idCurso);

            if (horarios == null || horarios.Length == 0)
            {
                VentanasEmergentes.CrearVentanaEmergente("Advertencia", "No hay horarios registrados para este curso.");
                NavigationService?.GoBack();
                return;
            }

            dgHorario.ItemsSource = horarios;
            //dgAlumnos.ItemsSource = alumnos;
            //dgAlumnos.Items.Refresh();
        }

        private void Click_Regresar(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
