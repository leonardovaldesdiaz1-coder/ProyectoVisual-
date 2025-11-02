using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TendaProyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            {
                String codigo = txtCodigo.Text; // Se obtiene el código ingresado por el usuario
                MySqlDataReader reader = null;  // Variable para leer los resultados de la base de datos

                // Consulta SQL para buscar el artículo con ese código
                String sql = "Select id, codigo, nombre, marca, precio_publico FROM articulos WHERE codigo LIKE '" + codigo + "' LIMIT 1";
                MySqlConnection conexionBD = Conexion.conexion(); // Se obtiene la conexión a la base de datos
                conexionBD.Open(); // Se abre la conexión
                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD); // Se prepara el comando con la consulta y la conexión
                    reader = comando.ExecuteReader(); // Se ejecuta la consulta
                    // Si se encontraron resultados
                    if (reader.HasRows)
                    {
                        while (reader.Read()) // Se leen los datos del registro
                        {
                            // Se muestran los datos en las cajas de texto
                            txtId.Text = reader.GetInt32(0).ToString();
                            txtCodigo.Text = reader.GetString(1);
                            txtNombre.Text = reader.GetString(2);
                            txtMarca.Text = reader.GetString(3);
                            txtPrecioPublico.Text = reader.GetDouble(4).ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros");
                    }
                }
                catch (MySqlException ex)
                {
                    // Si ocurre un error con la base de datos, se muestra el mensaje
                    MessageBox.Show("Error al buscar " + ex.Message);
                }
                finally
                {
                    conexionBD.Close(); // Siempre se cierra la conexión
                }
            }
        }
        private void btnConectar_Click_1(object sender, EventArgs e)
        {
            // Prueba de conexión a la base de datos
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            MessageBox.Show("Se conecto");
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Se obtienen los datos de las cajas de texto
                String codigo = txtCodigo.Text;
                String nombre = txtNombre.Text;
                String marca = txtMarca.Text;
                Double precio_publico = double.Parse(txtPrecioPublico.Text);

                // Verifica que los campos no estén vacíos
                if (codigo != "" && nombre != "" && marca != "" && precio_publico > 0)
                {
                    // Consulta para insertar un nuevo registro
                    String sql = "INSERT INTO articulos (codigo, nombre, marca, precio_publico) VALUES ('" + codigo + "', '" + nombre + "', '" + marca + "','" + precio_publico + "')";
                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();
                    try
                    {
                        // Ejecuta la inserción
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro guardado");
                        // Limpia los campos
                        txtId.Text = "";
                        txtCodigo.Text = "";
                        txtNombre.Text = "";
                        txtMarca.Text = "";
                        txtPrecioPublico.Text = "";
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Se obtienen los datos
                String codigo = txtCodigo.Text;
                String nombre = txtNombre.Text;
                String marca = txtMarca.Text;
                Double precio_publico = double.Parse(txtPrecioPublico.Text);

                if (codigo != "" && nombre != "" && marca != "" && precio_publico > 0)
                {
                    // Consulta para eliminar el registro con ese código
                    String sql = "DELETE FROM articulos WHERE codigo = @codigo";

                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();
                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);

                        comando.Parameters.AddWithValue("@codigo", codigo); // Se usa parámetro para evitar inyección SQL

                        int filasAfectadas = comando.ExecuteNonQuery(); // Ejecuta el borrado

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Se borraron los datos correctamente");
                            txtId.Text = "";
                            txtCodigo.Text = "";
                            txtNombre.Text = "";
                            txtMarca.Text = "";
                            txtPrecioPublico.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron registros con ese código.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al borrar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Se obtienen los datos
                String codigo = txtCodigo.Text;
                String nombre = txtNombre.Text;
                String marca = txtMarca.Text;
                Double precio_publico = double.Parse(txtPrecioPublico.Text);

                if (codigo != "" && nombre != "" && marca != "" && precio_publico > 0)
                {
                    // Consulta SQL para actualizar el registro
                    String sql = "UPDATE articulos SET nombre = @nombre, marca = @marca, precio_publico = @precio_publico WHERE codigo = @codigo";

                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        // Se pasan los parámetros
                        comando.Parameters.AddWithValue("@codigo", codigo);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@marca", marca);
                        comando.Parameters.AddWithValue("@precio_publico", precio_publico);

                        int filasAfectadas = comando.ExecuteNonQuery(); // Ejecuta la actualización

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Registro actualizado correctamente");
                            txtId.Text = "";
                            txtCodigo.Text = "";
                            txtNombre.Text = "";
                            txtMarca.Text = "";
                            txtPrecioPublico.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un registro con ese código para actualizar.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al actualizar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            // Limpia todas las cajas de texto
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtMarca.Text = "";
            txtPrecioPublico.Text = "";
        }
    }

}

