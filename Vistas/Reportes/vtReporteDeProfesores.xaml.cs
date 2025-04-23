    using LenguaVivaCliente.ServicioLenguaViva; 
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic;
    using System;
    using ServicioContrato;

namespace LenguaVivaCliente.Vistas.Reportes
    {
        public partial class vtReporteDeProfesores : Page
        {
            private ServicioLenguaViva.IGestionReportes _servicio;

            public vtReporteDeProfesores()
            {
                InitializeComponent();

                _servicio = new GestionReportesClient();

                CargarReporte();
            }

            private void CargarReporte()
            {
                try
                {
                    var reporte = _servicio.ObtenerReporteProfesoresActivos();

                    var listaProfesores = new List<ProfesorReporteDTO>();

                    foreach (var prof in reporte)
                    {
                        listaProfesores.Add(new ProfesorReporteDTO
                        {
                            NombreCompleto = prof.NombreCompleto,
                            CursosActivos = prof.CursosActivos
                        });
                    }

                    dgProfesores.ItemsSource = listaProfesores;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el reporte de profesores:\n" + ex.Message,
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Click_Volver(object sender, RoutedEventArgs e)
            {
                NavigationService.GoBack();
            }
        }
    }
