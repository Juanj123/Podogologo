﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capaDatos;
using capaPojos;
using System.IO;
using Microsoft.VisualBasic.Devices;

namespace ProyectoMovistar
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
        }
        
        Computer mycomputer = new Computer();
        clsDatosInventario consulta = new clsDatosInventario();
        List<clsInventario> tabla = new List<clsInventario>();
        clsValidaciones va = new clsValidaciones();
        String Direccion;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog BuscarImagen = new OpenFileDialog();
            BuscarImagen.Filter = "Archivos de Imagen|*.jpg";
            //Aquí incluiremos los filtros que queramos.
            BuscarImagen.FileName = "";
            BuscarImagen.Title = "Titulo del Dialogo";
            BuscarImagen.InitialDirectory = "C:\\Users\\Juanjo\\Documents\\Visual Studio 2015\\Projects\\Personal\\Imagenes";

            if (BuscarImagen.ShowDialog() == DialogResult.OK)
            {
                /// Si esto se cumple, capturamos la propiedad File Name y la guardamos en el control
                //this.textBox1.Text = BuscarImagen.FileName;
                Direccion = BuscarImagen.FileName;

                pbProducto.ImageLocation = Direccion;
                //Pueden usar tambien esta forma para cargar la Imagen solo activenla y comenten la linea donde se cargaba anteriormente 
                //this.pictureBox1.ImageLocation = textBox1.Text;
                pbProducto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        string rutaFinal;
        principal p = new principal();
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                rutaFinal = @"C:\ImagenesProductos\" + Direccion.Substring(Direccion.LastIndexOf(@"\"));
                clsInventario objProducto = new clsInventario();
                clsDatosInventario objDatosInventario = new clsDatosInventario();
                //Se leen los datos de los txt
                objProducto.Clave = txtClave.Text;
                objProducto.Nombre = txtNombre.Text;
                objProducto.Precio = Convert.ToInt32(txtPrecio.Text);
                objProducto.Categoria = objDatosInventario.getIdCategoria(cmbCategoria.Text);
                objProducto.Existencia = Convert.ToInt32(txtExistencia.Text);
                objProducto.Descripcion = txtDescripcion.Text;
                objProducto.Idusuario = objDatosInventario.getIdEmpleado(Program.nombre);
                objProducto.RutaImg = rutaFinal;
                // INSERTA AL PRODUCTO MEDIANTE EL MÉTODO
                objDatosInventario.AgregarProducto(objProducto);
                // MUESTRA MENSAJE DE CONFIRMACION
                MessageBox.Show("Agregado", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //creas carpeta que contendra las imagenes de los productos




                if (Directory.Exists(@"C:\ImagenesProductos"))
                {
                    //MessageBox.Show("Capeta ya existe");
                    mycomputer.FileSystem.MoveFile(Direccion, rutaFinal);
                }
                else
                {
                    // MessageBox.Show("No existe Carpeta Creando..............");
                    Directory.CreateDirectory(@"C:\ImagenesProductos\");
                    mycomputer.FileSystem.MoveFile(Direccion, rutaFinal);

                }


                txtClave.Text = "";
                txtNombre.Text = "";
                cmbCategoria.Text = "";
                txtPrecio.Text = "";
                txtExistencia.Text = "";
                txtDescripcion.Text = "";
                pbProducto.Image = null;
                verProductos();
            }
            catch (Exception ex) {

                MessageBox.Show("Campos Vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void verProductos()
        {
            dataGridView1.Rows.Clear();
            tabla = consulta.getDatosProducto();
            foreach (clsInventario elemento in tabla)
            {
                dataGridView1.Rows.Add(elemento.Clave, elemento.Nombre, elemento.Precio, elemento.Existencia);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // CREA LOS OBJETOS
            clsInventario objProducto = new clsInventario();
            clsDatosInventario objDatosInventario = new clsDatosInventario();

            // LEE LOS DATOS DE LAS CAJAS Y LOS GUARDA EN EL OBJETO
            objProducto.Clave = txtClave.Text;
            objProducto.Nombre = txtNombre.Text;
            objProducto.Precio = Convert.ToInt32(txtPrecio.Text);
            objProducto.Categoria = objDatosInventario.getIdCategoria(cmbCategoria.Text);
            objProducto.Existencia = Convert.ToInt32(txtExistencia.Text);
            objProducto.Descripcion = txtDescripcion.Text;
            objProducto.Idusuario = objDatosInventario.getIdEmpleado(Program.nombre);
            objProducto.RutaImg = rutaFinal;
            // MUESTRA MENSAJE DE CONFIRMACION
            objDatosInventario.ModificarProducto(objProducto);
            MessageBox.Show("Producto Modificado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
            txtClave.Text = "";
            txtNombre.Text = "";
            cmbCategoria.Text = "";
            txtPrecio.Text = "";
            txtExistencia.Text = "";
            txtDescripcion.Text = "";
            pbProducto.Image = null;
            verProductos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Visible == false) {
                btnGuardar.Visible = true;
                btnModificar.Visible = false;
                txtClave.Text = "";
                txtNombre.Text = "";
                cmbCategoria.Text = "";
                txtPrecio.Text = "";
                txtExistencia.Text = "";
                txtDescripcion.Text = "";
                pbProducto.Image = null;
            }
            txtClave.Text = "";
            txtNombre.Text = "";
            cmbCategoria.Text = "";
            txtPrecio.Text = "";
            txtExistencia.Text = "";
            txtDescripcion.Text = "";
            pbProducto.Image = null;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == dataGridView1.Columns["Modificar"].Index && e.RowIndex >= 0)
            {
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                clsInventario objinv = new clsInventario();
                clsDatosInventario objdatos = new clsDatosInventario();
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                objinv.Clave = Convert.ToString(fila.Cells["Clave"].Value);
                objdatos.buscarProducto(ref objinv);


                txtClave.Text = objinv.Clave;
                txtNombre.Text = objinv.Nombre;
                txtPrecio.Text = Convert.ToString(objinv.Precio);
                cmbCategoria.Text = objdatos.getnombreCategoria(objinv.Categoria);
                txtExistencia.Text = Convert.ToString(objinv.Existencia);
                txtDescripcion.Text = objinv.Descripcion;
                pbProducto.ImageLocation = objinv.RutaImg;
            }
            else
            {
                // CREA LOS OBJETOS
                clsDatosInventario datos = new clsDatosInventario();
                clsInventario producto = new clsInventario();
                DialogResult result = MessageBox.Show("Seguro que deseas eliminar?", "Movistar.....", MessageBoxButtons.YesNoCancel);
                // REFRESCA LOS DATOS Y MUESTRA EL MENSAJE "ELIMINADO"
                if (result == DialogResult.Yes)
                {
                    DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                    producto.Clave = Convert.ToString(fila.Cells["Clave"].Value);
                    datos.Eliminar(producto);

                    //verProductos();
                    MessageBox.Show("Producto Eliminado");
                    verProductos();
                }
            }
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            lblNuevaCategoria.Visible = false;
            txtNuevaCategoria.Visible = false;
            clsDatosInventario o = new clsDatosInventario();
            var lista = o.listaCategorias();
            btnModificar.Visible = false;
            verProductos();
            dataGridView1.AllowUserToAddRows = false;

            rbnNombre.Checked = true;

            for (int i = 0; i < lista.Count; i++)
            {
                cmbCategoria.Items.Insert(i, lista[i].Nombre);
            }
        }

       
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (rbnClave.Checked == true)
            {
                dataGridView1.Rows.Clear();
                tabla = consulta.getDatosProductobyclave(txtBuscar.Text);
                foreach (clsInventario elemento in tabla)
                {
                    dataGridView1.Rows.Add(elemento.Clave, elemento.Nombre, elemento.Precio, elemento.Existencia);
                }
            }
            else
            {
                if (rbnNombre.Checked == true)
                {
                    dataGridView1.Rows.Clear();
                    tabla = consulta.getDatosProductobynombre(txtBuscar.Text);
                    foreach (clsInventario elemento in tabla)
                    {
                        dataGridView1.Rows.Add(elemento.Clave, elemento.Nombre, elemento.Precio, elemento.Existencia);
                    }
                }
                else
                {
                    if (rbnTipo.Checked == true)
                    {
                        dataGridView1.Rows.Clear();
                        int id = consulta.getIdCategoria(txtBuscar.Text);
                        tabla = consulta.getDatosProductobytipo(id);
                        foreach (clsInventario elemento in tabla)
                        {
                            dataGridView1.Rows.Add(elemento.Clave, elemento.Nombre, elemento.Precio, elemento.Existencia);
                        }
                    }
                }

            }
        }

        private void txtClave_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.Letras(e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.NumerosDoubles(e);
        }

        private void cmbCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.Letras(e);
        }

        private void txtExistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.Numeros(e);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.Numeros_Letras(e);
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.Numeros(e);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.Numeros_Letras(e);
        }

        private void lblEmpleado_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Existencia")
            {
                if (Convert.ToInt32(e.Value) <= 10)
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Orange;

                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbCategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNuevaCategoria.Visible = true;
            lblNuevaCategoria.Visible = true;
        }

        private void txtNuevaCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtNuevaCategoria.Text.Equals("") | txtNuevaCategoria.Text.Equals(" "))
                {
                    MessageBox.Show("Categoria Vacia", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else {
                    clsCategorias obj = new clsCategorias();
                    clsDatosInventario obj2 = new clsDatosInventario();
                    obj.Nombre = txtNuevaCategoria.Text;
                    obj2.AgregarCategoria(obj);
                    MessageBox.Show("Se agrego categoria");
                    txtNuevaCategoria.Text = "";
                    txtNuevaCategoria.Visible = false;
                    lblNuevaCategoria.Visible = false;
                    cmbCategoria.Items.Clear();
                    clsDatosInventario o = new clsDatosInventario();
                    var lista = o.listaCategorias();
                    for (int i = 0; i < lista.Count; i++)
                    {
                        cmbCategoria.Items.Insert(i, lista[i].Nombre);
                    }

                }
               
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (rbnTipo.Checked == true)
            {
                dataGridView1.Rows.Clear();
                int id = consulta.getIdCategoria(txtBuscar.Text);
                tabla = consulta.getDatosProductobytipo(id);
                foreach (clsInventario elemento in tabla)
                {
                    dataGridView1.Rows.Add(elemento.Clave, elemento.Nombre, elemento.Precio, elemento.Existencia);
                }
            }
        }

        private void txtNuevaCategoria_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
