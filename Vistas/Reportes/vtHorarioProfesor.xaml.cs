using LenguaVivaCliente.ServicioLenguaViva;
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

namespace LenguaVivaCliente.Vistas.Reportes
{
    /// <summary>
    /// Lógica de interacción para vtHorarioProfesor.xaml
    /// </summary>
    public partial class vtHorarioProfesor : Page
    {
        private SesionUsuario sesionUsuario;
        public vtHorarioProfesor(SesionUsuario sesionUsuario)
        {
            InitializeComponent();
            this.sesionUsuario = sesionUsuario;
        }

        private void CargarHorario()
        {
            GestionReportesClient proxy = new GestionReportesClient();
            var horarios = proxy.ObtenerHorariosCursos(sesionUsuario.correo);

            foreach (var curso in horarios)
            {
                int columna = ObtenerColumnaPorDia(curso.dia);
                int fila = ObtenerFilaPorHorario(curso.horaInicio, curso.horaSalida);

                if (columna == -1 || fila == -1) continue;

                TextBlock bloque = new TextBlock
                {
                    Text = curso.nombreCurso,
                    Background = new SolidColorBrush(Color.FromRgb(173, 216, 230)),
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5),
                    FontWeight = FontWeights.SemiBold
                };
                Grid.SetRow(bloque, fila);
                Grid.SetColumn(bloque, columna);
                HorarioGrid.Children.Add(bloque);
            }
        }

        private int ObtenerColumnaPorDia(string dia)
        {
            switch (dia.ToLower())
            {
                case "lunes": return 1;
                case "martes": return 2;
                case "miércoles":
                case "miercoles": return 3;
                case "jueves": return 4;
                case "viernes": return 5;
                case "sábado":
                case "sabado": return 6;
                default: return -1;
            }
        }

        private int ObtenerFilaPorHorario(string inicio, string fin)
        {
            string tramo = $"{inicio}-{fin}";
            switch (tramo)
            {
                case "07:00-09:00": return 1;
                case "09:00-11:00": return 2;
                case "11:00-13:00": return 3;
                case "13:00-15:00": return 4;
                case "15:00-17:00": return 5;
                case "17:00-19:00": return 6;
                case "19:00-21:00": return 7;
                default: return -1;
            }
        }

        private void BtnDescargar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
