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

namespace LenguaVivaCliente.Vistas.Cursos.ReportesCurso
{
    /// <summary>
    /// Lógica de interacción para vtReporteDeIngresos.xaml
    /// </summary>
    public partial class vtReporteDeIngresos : Page
    {
        private CursoDTO curso;
        ServicioLenguaViva.IGestionCursos proxy = new ServicioLenguaViva.GestionCursosClient();
        public vtReporteDeIngresos(int idCurso)
        {
            InitializeComponent();
            curso = proxy.ObtenerCursoPorID(idCurso);
            CargarDatos();
        }

        public void CargarDatos()
        {
            string fechaInicioFormateada = curso.fechaInicio.ToString("dd/MM/yyyy");
            string fechaFinFormateada = curso.fechaTermino.ToString("dd/MM/yyyy");
            string numeroAlumnosFormateado = proxy.ObtenerNumeroAlumnosInscritosACurso(curso.idCurso).ToString();
            string ingresosTotalesFormateado = proxy.ObtenerIngresosDeCurso(curso.idCurso).ToString();
            txtCurso.Text = curso.nombreCurso;
            txtFechaInicio.Text = fechaInicioFormateada;
            txtFechaFin.Text = fechaFinFormateada;
            txtAlumnosInscritos.Text = numeroAlumnosFormateado;
            txtTotalIngresos.Text = "$"+ingresosTotalesFormateado;
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
