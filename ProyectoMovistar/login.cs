﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using capaDatos;
using capaPojos;
using MySql.Data.MySqlClient;


namespace ProyectoMovistar
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
 

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals("USUARIO"))
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;

            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals(""))
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;

            }
        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtContraseña.Text.Equals("CONTRASEÑA"))
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.Black;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (txtContraseña.Text.Equals(""))
            {
                txtContraseña.Text = "CONTRASEÑA";
                txtContraseña.ForeColor = Color.DimGray;
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            clsLogin login = new clsLogin();
            clsDatosLogin datosLogin = new clsDatosLogin();
            MySqlDataReader dr;
            
            if (!cbTipo.Text.Equals(""))
            {
                login.Nombre = txtUsuario.Text;
                login.Contrasenia = txtContraseña.Text;
                login.Tipo = cbTipo.SelectedItem.ToString();
                if (login.Nombre == txtUsuario.Text)
                {
                    if (login.Contrasenia == txtContraseña.Text)
                    {
                        dr = datosLogin.iniciarSesionAd(login);
                        //&cbTipo.SelectedItem.Equals("Administrador")
                        if (dr.Read() == true )
                        {
                            this.Hide();
                            //Llamar al formulario Principal
                            Program.tipo = dr["tipo"].ToString();
                            Program.nombre = dr["nombre"].ToString();
                            principal frmPrincipal = new principal();
                            frmPrincipal.Show();
                            frmPrincipal.lbTipo.Text = cbTipo.SelectedItem.ToString() + ":";
                            frmPrincipal.lbUsuario.Text = Program.nombre;
                        }
                        //else if (dr.Read() == true & cbTipo.SelectedItem.Equals("Empleado"))
                        //{
                        //    this.Hide();
                        //    //Llamar al formulario Ventas
                        //    Program.tipo = dr["tipo"].ToString();
                        //    MessageBox.Show(Program.tipo = dr["tipo"].ToString());
                        //    principal frmPrincipal = new principal();
                        //    frmPrincipal.Show();
                        //    frmPrincipal.lbTipo.Text = cbTipo.SelectedItem.ToString() + ":";
                        //    frmPrincipal.lbUsuario.Text = txtUsuario.Text;
                        //}
                        else
                        {
                            MessageBox.Show("Usuario o Contraseña incorrecta", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show(login.Contrasenia, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(login.Nombre, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campo Tipo de Usuario Vacio o incorrecto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTipo.BackColor = Color.Red;
                cbTipo.ForeColor = Color.White;
            }
        }

        private void linkPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recuperarContrasenia frmRecupContra = new recuperarContrasenia();
            frmRecupContra.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

//            Color myRgbColor = new Color();
  //          myRgbColor = Color.FromArgb(214,231,247);
    //        panel2.BackColor = myRgbColor;

        }
    }
}
