﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaPojos;
using System.Data;
using MySql.Data.MySqlClient;

namespace capaDatos
{
    public class clsDatosVenta
    {
        clsConexion cone = new clsConexion();
        public void AgregarProducto(clsVenta objProducto)
        {
            string sql;
            MySqlCommand cm;
            cone.conectar();
            cm = new MySqlCommand();
            cm.Parameters.AddWithValue("@folio", objProducto.Folio);
            cm.Parameters.AddWithValue("@idUsuario", objProducto.IdUusario);
            cm.Parameters.AddWithValue("@fecha", objProducto.Fecha);
            cm.Parameters.AddWithValue("@recibo", objProducto.Recibo);
            cm.Parameters.AddWithValue("@cambio", objProducto.Cambio);

            sql = "insert into ventas value(@folio, @idUsuario, @fecha, @recibo, @cambio);";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            cm.ExecuteNonQuery();
            cone.cerrar();
        }

        public void AgregarDVenta(clsDVenta objProducto)
        {
            string sql;
            MySqlCommand cm;
            cone.conectar();
            cm = new MySqlCommand();
            cm.Parameters.AddWithValue("@folio", objProducto.Folio);
            cm.Parameters.AddWithValue("@nombreProduct", objProducto.Nombre);
            cm.Parameters.AddWithValue("@precioProduct", objProducto.Precio);
            cm.Parameters.AddWithValue("@cantidadProduct", objProducto.Cantidad);
            cm.Parameters.AddWithValue("@total", objProducto.Total);

            sql = "insert  into dventas value(@folio,@nombreProduct,@precioProduct,@cantidadProduct, @total);";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            cm.ExecuteNonQuery();
            cone.cerrar();
        }

        public List<clsInventario> getProducto()
        {
            cone.conectar();
            List<clsInventario> lstUsuarios = new List<clsInventario>();
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select nombre from inventario;";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsInventario objAl = new clsInventario();
                objAl.Nombre = dr.GetString("nombre");
                lstUsuarios.Add(objAl);
            }
            cone.cerrar();
            return lstUsuarios;
        }
        public List<clsInventario> getProductos(string producto)
        {
            cone.conectar();
            List<clsInventario> lstUsuarios = new List<clsInventario>();
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select nombre, precio, existencia, descripcion from inventario where nombre = '" + producto + "';";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsInventario objAl = new clsInventario();
                objAl.Nombre = dr.GetString("nombre");
                objAl.Precio = dr.GetDouble("precio");
                objAl.Existencia = dr.GetInt32("existencia");
                objAl.Descripcion = dr.GetString("descripcion");
                lstUsuarios.Add(objAl);
            }
            cone.cerrar();
            return lstUsuarios;
        }
        public int cantidad(string producto)
        {
            cone.conectar();
            int numero = 0;
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select existencia from inventario where nombre = '" + producto + "';";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsInventario objUs = new clsInventario();
                numero = objUs.Existencia = dr.GetInt32("existencia");
            }
            cone.cerrar();
            return numero;
        }
        public int idUsuario(string usuario)
        {
            cone.conectar();
            int numero = 0;
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select idUsuario from usuarios where nombre = '" + usuario + "';";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsUsuarios objUs = new clsUsuarios();
                numero = objUs.IdUsuario = dr.GetInt32("idUsuario");
            }
            cone.cerrar();
            return numero;
        }
        public int folio()
        {
            cone.conectar();
            int numero = 0;
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select folio from ventas;";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsVenta objUs = new clsVenta();
                numero = dr.GetInt32("folio");
            }
            cone.cerrar();
            numero = numero + 1;
            return numero;
        }

        public int getPrecio(string producto)
        {
            cone.conectar();
            int numero = 0;
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select precio from inventario where nombre = '" + producto + "';";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsInventario objUs = new clsInventario();
                numero = objUs.Existencia = dr.GetInt32("precio");
            }
            cone.cerrar();
            return numero;
        }

        public void RestarExistencia(string nombre, int cantidad)
        {
            string sql;
            MySqlCommand cm;
            cone.conectar();
            cm = new MySqlCommand();
            cm.Parameters.AddWithValue("@nombre", nombre);
            cm.Parameters.AddWithValue("@cantidad", cantidad);

            sql = "UPDATE inventario SET nombre = @nombre, existencia = existencia - @cantidad   WHERE nombre = @nombre";

            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            cm.ExecuteNonQuery();
            cone.cerrar();
        }

        public int getyesorno(string producto)
        {
            cone.conectar();
            int numero = 0;
            string sql;
            MySqlCommand cm = new MySqlCommand();
            MySqlDataReader dr;
            sql = "select precio from inventario where nombre = '" + producto + "';";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Connection = cone.cn;
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                clsInventario objUs = new clsInventario();
                numero = objUs.Existencia = dr.GetInt32("precio");
            }
            cone.cerrar();
            return numero;
        }
    }
}