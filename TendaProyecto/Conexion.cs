using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TendaProyecto
{
    internal class Conexion
    {
        public static MySqlConnection conexion()
        {
            //Se obtienen los datos del servidor 
            string servidor = "localhost";
            string bd = "Abarrotes";
            string usuario = "root";
            string password = "si55.";

            String cadenaConexion = "Database=" + bd + ";Data Source=" + servidor + ";User Id=" + usuario + ";Password=" + password + "";
            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error :" + ex.Message);
                return null;
            }
        }
    }
}
