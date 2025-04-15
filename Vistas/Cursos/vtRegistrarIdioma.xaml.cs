using LenguaVivaCliente.ServicioLenguaViva;
using ServicioContrato;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LenguaVivaCliente.Vistas.Cursos
{
    /// <summary>
    /// Lógica de interacción para vtRegistrarIdioma.xaml
    /// </summary>
    public partial class vtRegistrarIdioma : Page
    {
        public vtRegistrarIdioma()
        {
            InitializeComponent();
        }

        private void Click_Volver(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void Click_RegistrarIdioma(object sender, RoutedEventArgs e)
        {
            // 1) Validación local
            if (string.IsNullOrWhiteSpace(tbNombreIdioma.Text))
            {
                lbNombreVacio.Visibility = Visibility.Visible;
                return;
            }
            lbNombreVacio.Visibility = Visibility.Hidden;

            // 2) Crear DTO
            var idiomaDTO = new IdiomaDTO
            {
                nombreIdioma = tbNombreIdioma.Text.Trim()
            };

            // 3) Llamada al servicio
            try
            {
                bool registrado = false;
                string mensaje = string.Empty;

                await Task.Run(() =>
                {
                    // Usamos el mismo cliente de IGestionUsuarios
                    var cliente = new GestionUsuariosClient();
                    registrado = cliente.RegistrarIdioma(idiomaDTO, out mensaje);
                    cliente.Close();
                });

                // 4) Mostrar feedback
                MessageBox.Show(
                    mensaje,
                    registrado ? "Éxito" : "Error",
                    MessageBoxButton.OK,
                    registrado ? MessageBoxImage.Information : MessageBoxImage.Warning
                );

                // 5) Si fue exitoso, limpiar y volver
                if (registrado)
                {
                    tbNombreIdioma.Clear();
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error de comunicación con el servicio: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
