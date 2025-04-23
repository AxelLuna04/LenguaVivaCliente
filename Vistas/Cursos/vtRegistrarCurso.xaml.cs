//using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtRegistrarCurso.xaml
    /// </summary>
    public partial class vtRegistrarCurso : Page
    {
        public vtRegistrarCurso()
        {

            InitializeComponent();

            cargarIdiomas();
            actualizarProfesores();

        }


        private void Click_Registrar(object sender, RoutedEventArgs e)
        {
            if (validarCampos() != true || ValidarFechas(dpFechaInicio, dpFechaTermino) != true)
            {
                return;
            }
            ProfesorDTO profesorSeleccionado = (ProfesorDTO)cbProfesores.SelectedItem;
            IdiomaDTO idiomaSeleccionado = (IdiomaDTO)cbIdiomas.SelectedItem;

            CursoDTO curso = new CursoDTO
            {
                nombreCurso = tbNombre.Text,
                cupoMaximo = int.Parse(tbCupo.Text),
                descripcion = tbDescripcion.Text,
                fechaInicio = dpFechaInicio.SelectedDate.Value,
                fechaTermino = dpFechaTermino.SelectedDate.Value,
                idProfesor = profesorSeleccionado.idProfesor,
                idIdioma = idiomaSeleccionado.idIdioma
            };

            if (rbBasico.IsChecked == true)
            {
                curso.nivel = 1;
            }
            else if (rbIntermedio.IsChecked == true)
            {
                curso.nivel = 2;
            }
            else if (rbAvanzado.IsChecked == true)
            {
                curso.nivel = 3;
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione un nivel");
                return;
            }
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();
            if (servicio.RegistrarCurso(curso) == true)
            {
                System.Windows.MessageBox.Show("Curso registrado con éxito");
                NavigationService.GoBack();
            }
            else
            {
                System.Windows.MessageBox.Show("Error al registrar el curso");
            }
        }

        private void Click_Cancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void actualizarProfesores()
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();

            ProfesorDTO[] profesores = servicio.ObtenerProfesoresPorNombre(tbProfesor.Text);

            if (profesores != null)
            {
                cbProfesores.ItemsSource = null;
                cbProfesores.ItemsSource = profesores;
            }
        }

        private void cargarIdiomas()
        {
            ServicioLenguaViva.IGestionCursos servicio = new ServicioLenguaViva.GestionCursosClient();

            IdiomaDTO[] idiomas = servicio.ObtenerIdiomas();
            if (idiomas != null)
            {
                cbIdiomas.ItemsSource = idiomas;
                cbIdiomas.DisplayMemberPath = "nombreIdioma";
                cbIdiomas.Items.Refresh();
            }
            else
            {
                MessageBox.Show("No se han encontrado idiomas para registrar un curso");
                NavigationService.GoBack();
            }
        }

        private void tbProfesor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbProfesores != null)
            {
                actualizarProfesores();
            }

        }

        //Para que el textbox de cupo solo acepte números
        private void tbCupo_SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        //Para que el textbox no acepte pegar texto
        private void tbCupo_SoloPegarNumeros(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!text.All(char.IsDigit))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool validarCampos()
        {
            int contadorCamposVacios = 0;
            if (tbNombre.Text == "")
            {
                lbNombreVacío.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbNombreVacío.Visibility = Visibility.Hidden;
            }

            if (tbCupo.Text == "" || int.Parse(tbCupo.Text) < 1)
            {
                lbCupoVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbCupoVacio.Visibility = Visibility.Hidden;
            }

            if (tbDescripcion.Text == "")
            {
                lbDescripcionVacia.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbDescripcionVacia.Visibility = Visibility.Hidden;
            }

            if (dpFechaInicio.SelectedDate == null)
            {
                lbFechaInicioVacia.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbFechaInicioVacia.Visibility = Visibility.Hidden;
            }

            if (dpFechaTermino.SelectedDate == null)
            {
                lbFechaTerminoVacia.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbFechaTerminoVacia.Visibility = Visibility.Hidden;
            }

            if (cbProfesores.SelectedItem == null)
            {
                lbProfesorVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbProfesorVacio.Visibility = Visibility.Hidden;
            }

            if (cbIdiomas.SelectedItem == null)
            {
                lbIdiomaVacio.Visibility = Visibility.Visible;
                contadorCamposVacios++;
            }
            else
            {
                lbIdiomaVacio.Visibility = Visibility.Hidden;
            }

            if (contadorCamposVacios > 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }


        //TODO: Convertirla en una función de utilidad
        private bool ValidarFechas(DatePicker fechaInicio, DatePicker fechaTermino)
        {
            DateTime inicio = fechaInicio.SelectedDate.Value;
            DateTime termino = fechaTermino.SelectedDate.Value;

            if (termino <= inicio)
            {
                MessageBox.Show("La fecha de término debe ser posterior a la fecha de inicio.");
                return false;
            }

            return true;
        }

    }
}
