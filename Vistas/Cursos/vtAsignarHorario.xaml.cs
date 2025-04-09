using LenguaVivaCliente.ServicioLenguaViva;
using LenguaVivaCliente.Utilidades;
using ServicioContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Lógica de interacción para vtAsignarHorario.xaml
    /// </summary>
    public partial class vtAsignarHorario : Page
    {
        private int idCurso;
        public vtAsignarHorario(int idCurso)
        {
            InitializeComponent();
            this.idCurso = idCurso;
            CargarHoras();
            CargarDias(); 
        }

        private void CargarDias()
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
            HorarioDTO[] horarios = servicio.ObtenerHorariosPorCurso(idCurso);
            string[] diasOcupados = new string[horarios.Length];
            for (int i = 0; i < horarios.Length; i++)
            {
                diasOcupados[i] = horarios[i].diaSemana;
            }


            foreach (var child in gRadioButtons.Children)
            {
                if (child is RadioButton radioButton)
                {
                    string dia = radioButton.Content.ToString();
                    
                    if (diasOcupados.Contains(dia))
                    {
                        radioButton.IsEnabled = false;
                        radioButton.Foreground = Brushes.LawnGreen;
                    }
                    else
                    {
                        radioButton.IsEnabled = true; // opcional, por si quieres reactivar los demás
                    }
                }
            }
        }

        private void BtnAsignar_Click(object sender, RoutedEventArgs e)
        {
            if (cbHoraInicio.SelectedItem == null || cbHoraTermino.SelectedItem == null)
            {
                VentanasEmergentes.CrearVentanaEmergente("Error", "Por favor, seleccione una hora de inicio y una hora de término.");
                return;
            }
            HorarioDTO horario = new HorarioDTO
            {
                idCurso = idCurso, // Cambia esto por el ID del curso real
                diaSemana = ObtenerDiaSeleccionado(),
                horaInicio = ((HoraDelDia)cbHoraInicio.SelectedItem).Hora,
                horaTermino = ((HoraDelDia)cbHoraTermino.SelectedItem).Hora
            };
            if (horario.diaSemana != null)
            {
                ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
                if (servicio.AsignarHorario(horario))
                {
                    VentanasEmergentes.CrearVentanaEmergente("Éxito", "Horario asignado correctamente.");
                    NavigationService.GoBack();
                }
                else
                {
                    VentanasEmergentes.CrearVentanaEmergente("Error", "No se pudo asignar el horario. Por favor, intente nuevamente.");
                }
            }
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de regreso al menú principal
            NavigationService?.GoBack();
        }




        public class HoraDelDia
        {
            public TimeSpan Hora { get; set; }

            // Propiedad que formatea la hora como "7 AM", "1 PM", etc.
            public string HoraFormateada
            {
                get
                {
                    DateTime horaDateTime = DateTime.Today.Add(Hora);
                    return horaDateTime.ToString("h tt");
                }
            }

            // Propiedad para comparar horas fácilmente
            public int HoraNumerica
            {
                get { return (int)Hora.TotalHours; }
            }
        }

        private List<HoraDelDia> GenerarHorasDelDia()
        {
            var horas = new List<HoraDelDia>();

            for (int i = 7; i <= 21; i++) // Desde 7 AM hasta 9 PM
            {
                horas.Add(new HoraDelDia
                {
                    Hora = new TimeSpan(i, 0, 0) // Horas, minutos, segundos
                });
            }
            return horas;
        }

        // En el constructor o método de carga
        private void CargarHoras()
        {
            var horas = GenerarHorasDelDia();
            cbHoraInicio.ItemsSource = horas;
            cbHoraTermino.ItemsSource = horas.ToList(); // Copia para no compartir la misma referencia
        }

        // Manejar el cambio de selección
        private void cbHoraInicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbHoraInicio.SelectedItem is HoraDelDia horaSeleccionada)
            {
                // Filtrar las horas posteriores
                var horasFiltradas = GenerarHorasDelDia()
                    .Where(h => h.Hora > horaSeleccionada.Hora)
                    .ToList();

                cbHoraTermino.ItemsSource = horasFiltradas;
                cbHoraTermino.IsEnabled = horasFiltradas.Any();

                if (horasFiltradas.Any())
                {
                    cbHoraTermino.SelectedIndex = 0;
                }
            }
            else
            {
                cbHoraTermino.ItemsSource = null;
                cbHoraTermino.IsEnabled = false;
            }
        }

        private string ObtenerDiaSeleccionado()
        {
            foreach (var child in gRadioButtons.Children)
            {
                if (child is RadioButton radioButton && radioButton.IsChecked == true)
                {
                    string diaSeleccionado = radioButton.Content.ToString();
                    return diaSeleccionado;
                }
            }
            
            MessageBox.Show("No hay ningún día seleccionado.");
            return null;
        }
    }
}
