using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AccesoADatos
{
    public class GestorDeDatos
    {
        private const string cadenaDeConexion =
            "Data Source=database.db; Version=3;New=True;Compress=True;";
        public string CadenaDeConexion
        {
            get { return cadenaDeConexion; }
        }

        public static SQLiteConnection EstablecerConexion()
        {
            SQLiteConnection connection =
                new SQLiteConnection(cadenaDeConexion);
            try
            {
                connection.Open();
                return connection;
            }
            catch
            {
                throw;
            }
        }

        public static void CrearTabla(SQLiteConnection cnx)
        {
            string definicion =
                "CREATE TABLE Articulos (" +
                "Id INT," +
                "Descripcion VARCHAR(30) )";
            var state = cnx.State;
            if (state==System.Data.ConnectionState.Open)
            {
                SQLiteCommand crear = cnx.CreateCommand();
                crear.CommandText = definicion;
                crear.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException("la conexion no está abierta");
            }
        }
        public static int InsertarArticulo(SQLiteConnection cnx, Articulo articulo)
        {
            if (cnx.State!=System.Data.ConnectionState.Open)
            {
                throw new ArgumentException("La conexión no está abierta");
            }
            if (articulo==null)
            {
                throw new ArgumentException("El artículo es nulo");
            }
            SQLiteCommand insertar = cnx.CreateCommand();
            insertar.CommandText =
                "INSERT INTO Articulos (Id, Descripcion) values "+
                "("+articulo.Id+", '" + articulo.Descripcion + "' )";
            try
            {
                int respuesta = insertar.ExecuteNonQuery();
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public static List<Articulo> CargarArticulos(SQLiteConnection cnx)
        {
            if (cnx.State!=System.Data.ConnectionState.Open)
            {
                throw new ArgumentException("La conexión no está abierta");
            }
            SQLiteDataReader reader;
            SQLiteCommand leer = cnx.CreateCommand();
            leer.CommandText = "SELECT * FROM Articulos";
            reader = leer.ExecuteReader();
            List<Articulo> resultado = new List<Articulo>();
            while (reader.Read())
            {
                Articulo a = new Articulo();
                a.Id = reader.GetInt32(0);
                a.Descripcion = reader.GetString(1);
                resultado.Add(a);
            }
            return resultado;
        }
        public static void CerrarConexion (SQLiteConnection cnx)
        {
            if (cnx.State == System.Data.ConnectionState.Open)
            {
                cnx.Close();
            }
        } 
        public static void Eliminar(SQLiteConnection cnx,Articulo articulo)
        {
            if (cnx.State != System.Data.ConnectionState.Open)
            {
                throw new ArgumentException("La coneccion no esta abierta");
            }
            if (articulo == null)
            {
                throw new ArgumentException("El articulo es nulo");
            }
            SQLiteCommand borrar = cnx.CreateCommand();
            borrar.CommandText = "DELETE FROM Articulos WHERE id = '" + articulo.Id + "'";
            try
            {
                borrar.ExecuteNonQuery();
            }
            catch
            {

                throw;
            }

        }
    }

}
