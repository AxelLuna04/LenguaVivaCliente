using LenguaVivaCliente.ServicioLenguaViva;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace LenguaVivaCliente.Vistas.Alumnos
{
    /// <summary>
    /// Lógica de interacción para vtReciboDePago.xaml
    /// </summary>
    public partial class vtReciboDePago : Window
    {
        private string tempImagePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "recibo_temp.png");
        private GestionAlumnosClient proxy = new GestionAlumnosClient();
        private int idInscripcion;

        public vtReciboDePago(int idInscripcion)
        {
            InitializeComponent();
            CargarRecibo(idInscripcion);
            this.idInscripcion = idInscripcion;
        }

        private void CargarRecibo(int idInscripcion)
        {
         
            try
            {
                ReciboPago recibo = proxy.ObtenerReciboPago(idInscripcion);

                if (recibo != null)
                {
                    txtCurso.Text = recibo.curso;
                    txtAlumno.Text = recibo.alumno;
                    txtFechaPago.Text = recibo.fechaPago;
                    txtCantidadPagada.Text = recibo.cantidadPagada;
                }
                else
                {
                    Utilidades.VentanasEmergentes.CrearVentanaEmergente("Recibo no encontrado", "No se encontró información del recibo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al recuperar el recibo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDescargar_Click(object sender, RoutedEventArgs e)
        {
            btnDescargar.Visibility = Visibility.Collapsed;
            btnEnviar.Visibility = Visibility.Collapsed;
            try
            {
                // Capturar el recibo y guardarlo como imagen
                BitmapSource imagenRecibo = CapturarReciboComoImagen();
                GuardarImagenTemporal(imagenRecibo);

                // Convertir la imagen a PDF
                string pdfPath = ConvertirImagenAPDF();

                // Mostrar mensaje de éxito
                if (!string.IsNullOrEmpty(pdfPath))
                {
                    MessageBox.Show("Recibo guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Limpiar imagen temporal
                EliminarImagenTemporal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Captura el recibo como imagen sin incluir otros elementos de la interfaz.
        /// </summary>
        private BitmapSource CapturarReciboComoImagen()
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)gridRecibo.ActualWidth, (int)gridRecibo.ActualHeight,
                96, 96, PixelFormats.Pbgra32);
            rtb.Render(gridRecibo);
            return rtb;
        }

        /// <summary>
        /// Guarda la imagen capturada en un archivo temporal.
        /// </summary>
        private void GuardarImagenTemporal(BitmapSource imagen)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imagen));

            using (FileStream fs = new FileStream(tempImagePath, FileMode.Create))
            {
                encoder.Save(fs);
            }
        }

        /// <summary>
        /// Convierte la imagen guardada en un archivo PDF.
        /// </summary>
        /*private string ConvertirImagenAPDF()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = "Guardar Recibo de Pago",
                FileName = "Recibo_Pago.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                PdfDocument pdf = new PdfDocument();
                PdfPage page = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XImage image = XImage.FromFile(tempImagePath);
                gfx.DrawImage(image, 0, 0, page.Width, page.Height);

                pdf.Save(saveFileDialog.FileName);
                return saveFileDialog.FileName;
            }

            return string.Empty;
        }*/

        private string ConvertirImagenAPDF()
        {
            string tempPdfPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "recibo_pago.pdf");

            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();

            // Configurar tamaño de página estándar (A4 vertical)
            page.Width = XUnit.FromMillimeter(210);  // Ancho A4: 210 mm
            page.Height = XUnit.FromMillimeter(297); // Alto A4: 297 mm

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XImage image = XImage.FromFile(tempImagePath);

            // Escalar la imagen para que quepa en la página manteniendo proporciones
            double scale = Math.Min(page.Width / image.PixelWidth, page.Height / image.PixelHeight);
            double finalWidth = image.PixelWidth * scale;
            double finalHeight = image.PixelHeight * scale;

            // Centrar la imagen
            double x = (page.Width - finalWidth) / 2;
            double y = (page.Height - finalHeight) / 2;

            gfx.DrawImage(image, x, y, finalWidth, finalHeight);

            pdf.Save(tempPdfPath);
            return tempPdfPath;
        }

        /// <summary>
        /// Elimina la imagen temporal generada para el PDF.
        /// </summary>
        private void EliminarImagenTemporal()
        {
            if (File.Exists(tempImagePath))
            {
                File.Delete(tempImagePath);
            }
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            btnDescargar.Visibility = Visibility.Collapsed;
            btnEnviar.Visibility = Visibility.Collapsed;

            try
            {
                BitmapSource imagenRecibo = CapturarReciboComoImagen();
                GuardarImagenTemporal(imagenRecibo);
                string pdfPath = ConvertirImagenAPDF();

                if (string.IsNullOrEmpty(pdfPath))
                {
                    MessageBox.Show("No se pudo generar el PDF.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                proxy.EnviarCorreoReciboPago(idInscripcion, pdfPath);

                // Mostrar mensaje de éxito
                MessageBox.Show("Recibo enviado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el recibo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnDescargar.Visibility = Visibility.Visible;
                btnEnviar.Visibility = Visibility.Visible;
                EliminarImagenTemporal();
            }

        }
    }
}
