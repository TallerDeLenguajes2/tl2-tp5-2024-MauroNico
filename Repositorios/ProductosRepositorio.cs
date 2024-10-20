using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Tienda.Models;

namespace Tienda.Repositorios{
   
   public class ProductosRepositorio : IProductosRepositorio{

    private string cadenaConexion = "Data Source=DB/Tienda.db;Cache=Shared";

    public void crearProducto(Producto producto){
        var query = $"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion)){

            connection.Open();
            var command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void modificarProducto(int id, Producto producto){
        var query = $"UPDATE Productos SET Descripcion = @Descripcion SET Precio = @Precio WHERE idProducto = @idProducto";
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion)){

            connection.Open();
            var command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@idProducto", id));
            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));

            command.ExecuteNonQuery();

            connection.Close();
        }

    }

    public List<Producto> listarProductos(){
        var queryString = @"SELECT * FROM Productos;";
        List<Producto> productos = new List<Producto>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion)){
            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();

            using(SqliteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var Producto = new Producto();
                    
                }
            }
        }
    }



   }



}

