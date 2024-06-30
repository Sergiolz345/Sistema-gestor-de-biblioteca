using Conexiones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminBiblioteca
{
    public partial class Login : Form
    {
        dbSqlServer conexion = null;
        public String sData = "";
        DataTable sUser = new DataTable();

        public Login()
        {
            InitializeComponent();
        }

        #region Mover Arrastrar Formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void lblform_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Silver;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Silver;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Silver;
            }
        }
        #endregion


        #region Botones de Ventana
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion


        #region Marca de Agua
        private void tbUsuario_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Silver;
            }

            if (tbUsuario.Text == "Usuario")
            {
                tbUsuario.Text = "";
                tbUsuario.ForeColor = Color.Black;
            }
        }

        private void tbPassword_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "Password")
            {
                cbContraseña.Enabled = true;
                tbPassword.UseSystemPasswordChar = true;
                tbPassword.Text = "";
                tbPassword.ForeColor = Color.Black;
            }
        }

        private void cbContraseña_CheckedChanged(object sender, EventArgs e)
        {

            if (cbContraseña.Checked)
            {
                tbPassword.UseSystemPasswordChar = false;
                cbContraseña.Text = "Ocultar Contraseña";
            }

            else
            {
                tbPassword.UseSystemPasswordChar = true;

                cbContraseña.Text = "Mostrar Contraseña";
            }
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbUsuario.Text == "Usuario")
            {
                tbUsuario.Text = "";
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Silver;
            }
        }
        #endregion


        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "Usuario";
                tbUsuario.ForeColor = Color.Silver;
            }

            if (tbPassword.Text == "")
            {
                cbContraseña.Checked = false;
                cbContraseña.Enabled = false;
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Silver;
            }

            if (tbUsuario.Text == "Usuario" || tbPassword.Text == "Password")
            {
                MessageBox.Show("Termine de rellenar los apartados");
                return;
            }

            conexion = new dbSqlServer(tbUsuario.Text, tbPassword.Text);

            if (conexion.AbrirConexion())
            {
                this.Hide();
                Menu c = new Menu(tbUsuario.Text, tbPassword.Text);
                c.ShowDialog();

                conexion.CerrarConexion();
            }

            else
            {
                MessageBox.Show($"Hay un error {conexion.sLastError}");
                return;
            }
        }
    }
}
