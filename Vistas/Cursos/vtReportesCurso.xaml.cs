using LenguaVivaCliente.Utilidades;
using ServicioContrato;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para vtReportesCurso.xaml
    /// </summary>
    public partial class vtReportesCurso : Page
    {
        private CursoDTO curso;
        public vtReportesCurso(CursoDTO curso)
        {
            InitializeComponent();
            this.curso = curso;
        }

        private void Click_ReporteAlumnos(object sender, RoutedEventArgs e)
        {
            
        }

        private void Click_ReporteIngresos(object sender, RoutedEventArgs e)
        {
            Vistas.Cursos.ReportesCurso.vtReporteDeIngresos vtReporteDeIngresos = new ReportesCurso.vtReporteDeIngresos(curso.idCurso);
            NavigationService.Navigate(vtReporteDeIngresos);
        }

        private void Click_ReporteAsistencia(object sender, RoutedEventArgs e)
        {
            if (curso.listaAsistencia == null || curso.listaAsistencia.Length == 0)
            {
                VentanasEmergentes.CrearVentanaEmergente("Advertencia", "No hay una lista de asistencia guardada para este curso.");
                return;
            }
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Title = "Guardar lista de asistencia";
            saveFileDialog.Filter = "Archivo Excel (*.xlsx)|*.xlsx|Archivo Excel 97-2003 (*.xls)|*.xls";
            saveFileDialog.FileName = "ListaAsistencia_" + curso.nombreCurso;

            if (saveFileDialog.ShowDialog() == true)
            {
                string rutaDestino = saveFileDialog.FileName;

                try
                {
                    File.WriteAllBytes(rutaDestino, curso.listaAsistencia);
                    VentanasEmergentes.CrearVentanaEmergente("Éxito", "Lista guardada correctamente.");
                }
                catch (Exception ex)
                {
                    VentanasEmergentes.CrearVentanaEmergente("Error", "Hubo un problema al guardar el archivo: " + ex.Message);
                }
            }
        }


        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
