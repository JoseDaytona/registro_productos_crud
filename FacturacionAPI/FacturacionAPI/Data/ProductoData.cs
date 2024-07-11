using FacturacionAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace FacturacionAPI.Data
{
    public class ProductoData
    {

        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Producto>> Lista()
        {
            List<Producto> lista = new List<Producto>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_lista_producto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) {
                        lista.Add(new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["id_producto"]),
                            Nombre = reader["nombre_producto"].ToString(),
                            Cantidad = Convert.ToInt32(reader["stock"]),
                            Precio = Convert.ToDecimal(reader["precio_unitario"]),
                            Estado = reader["estado_str"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Producto> Obtener(int Id)
        {
            Producto objeto = new Producto();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtener_producto", con);
                cmd.Parameters.AddWithValue("@id_producto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["id_producto"]),
                            Nombre = reader["nombre_producto"].ToString(),
                            Cantidad = Convert.ToInt32(reader["stock"]),
                            Precio = Convert.ToDecimal(reader["precio_unitario"]),
                            IdEstado = Convert.ToInt16(reader["estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Producto objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_registrar_producto", con);
                cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true: false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Producto objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_modificar_producto", con);
                cmd.Parameters.AddWithValue("@id_producto", objeto.IdProducto);
                cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_eliminar_producto", con);
                cmd.Parameters.AddWithValue("@id_producto", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
