using LenguaVivaCliente.ServicioLenguaViva;
using LenguaVivaCliente.Vistas.Ediciones;
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

namespace LenguaVivaCliente.Vistas.Menu.MenuUsuarios
{
    /// <summary>
    /// Lógica de interacción para vtDetallesUsuario.xaml
    /// </summary>
    public partial class vtDetallesUsuario : Page
    {
        private UsuarioDTO usuarioSeleccionado;

        public vtDetallesUsuario(UsuarioDTO usuarioSeleccionado, int tipoUsuario)
        {
            InitializeComponent();
            this.usuarioSeleccionado = usuarioSeleccionado;
            TxtTitulo.Text = $"{usuarioSeleccionado.nombre} {usuarioSeleccionado.apellidos}";
            TxtCorreo.Text = usuarioSeleccionado.correo;
            TxtTelefono.Text = usuarioSeleccionado.telefono;
            TxtDireccion.Text = usuarioSeleccionado.direccion;
            if (usuarioSeleccionado.nivelIdioma != null)
            {
                TxtNivelIdioma.Text = usuarioSeleccionado.nivelIdioma;
                BorderNivelIdioma.Visibility = Visibility.Visible;
            }

            if(tipoUsuario == 2)
            {
                BtnEditar.Visibility = Visibility.Collapsed;
                BtnEliminar.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var proxy = new GestionUsuariosClient())
                {
                    if (usuarioSeleccionado.tipoUsuario == 1)
                    {
                        var adminReal = proxy.ObtenerAdministradorPorCorreo(usuarioSeleccionado.correo);

                        if (adminReal == null)
                        {
                            MessageBox.Show("No se encontró el administrador", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        AdministradorDTO administradorSeleccionado = new AdministradorDTO
                        {
                            idAdministrador = adminReal.idAdministrador, 
                            nombreUsuario = adminReal.nombreUsuario,     
                            nombre = adminReal.nombre,
                            apellidos = adminReal.apellidos,
                            direccion = adminReal.direccion,
                            email = adminReal.email,
                            telefono = adminReal.telefono,
                            contrasenia = "" 
                        };

                        NavigationService?.Navigate(new vtEditarAdministrador(administradorSeleccionado));

                    }else if (usuarioSeleccionado.tipoUsuario == 2)
                    {
                        var profesorReal = proxy.ObtenerProfesorPorCorreo(usuarioSeleccionado.correo);

                        if (profesorReal == null)
                        {
                            MessageBox.Show("No se encontró el profesor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        ProfesorDTO profesorSeleccionado = new ProfesorDTO
                        {
                            idProfesor = profesorReal.idProfesor,
                            nombreUsuario = profesorReal.nombreUsuario,
                            nombre = profesorReal.nombre,
                            apellidos = profesorReal.apellidos,
                            direccion = profesorReal.direccion,
                            email = profesorReal.email,
                            telefono = profesorReal.telefono,
                            contrasenia = "",
                            idiomasSeleccionados = profesorReal.idiomasSeleccionados
                        };

                        NavigationService?.Navigate(new vtEditarProfesor(profesorSeleccionado));
                    }
                    else if (usuarioSeleccionado.tipoUsuario == 3)
                    {
                        var alumnoReal = proxy.ObtenerAlumnoPorCorreo(usuarioSeleccionado.correo);

                        if (alumnoReal == null)
                        {
                            MessageBox.Show("No se encontró el alumno", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }


                        if (alumnoReal.idiomasNivel == null || alumnoReal.idiomasNivel.Length == 0)
                        {
                            MessageBox.Show("Este alumno no tiene idiomas asignados.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        AlumnoDTO alumnoSeleccionado = new AlumnoDTO
                        {
                            idAlumno = alumnoReal.idAlumno,
                            nombre = alumnoReal.nombre,
                            apellidos = alumnoReal.apellidos,
                            direccion = alumnoReal.direccion,
                            email = alumnoReal.email,
                            telefono = alumnoReal.telefono,
                            idiomasNivel = alumnoReal.idiomasNivel
                        };

                        NavigationService?.Navigate(new vtEditarAlumno(alumnoSeleccionado));
                    }
                    else
                    {
                        MessageBox.Show("Tipo de usuario no reconocido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                       
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmacion = MessageBox.Show("¿Estás seguro de eliminar este usuario?",
                                                             "Confirmación",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Warning);
            if (confirmacion == MessageBoxResult.Yes)
            {
                try
                {
                    using (var proxy = new GestionUsuariosClient())
                    {
                        UsuarioDTO usuarioEliminado = proxy.EliminarUsuario(usuarioSeleccionado.correo, usuarioSeleccionado.tipoUsuario);
                        if (usuarioEliminado != null)
                        {
                            MessageBox.Show("Usuario eliminado exitosamente.", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService?.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
