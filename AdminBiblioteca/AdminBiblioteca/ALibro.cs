using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using Conexiones;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdminBiblioteca
{
    public partial class ALibro : Form
    {

        public dbSqlServer conexion = null;

        public String sData = "";
        string sUsuario, sPassword;


        public ALibro(string Usuario, string Pasword)
        {
            InitializeComponent();

            sUsuario = Usuario;
            sPassword = Pasword;
            //lblUser.Text = "Usuario: " + Usuario;

            conexion = new dbSqlServer(sUsuario, sPassword);
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
        }

        private void ALibro_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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


        private void btnRMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu c = new Menu(sUsuario, sPassword);
            c.ShowDialog();
        }
        #endregion


        ///////////Agregado por jorge
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text == "" || txtEditorial.Text == "" || txtCategoria.Text == "" || txtAutor.Text == "")
            {
                MessageBox.Show("Termine de rellenar los apartados");
                return;
            }

            string titulo = txtTitulo.Text; // asumiendo que tienes un TextBox llamado txtTitulo para ingresar el título del libro
            string editorial = txtEditorial.Text; // asumiendo que tienes un TextBox llamado txtAutor para ingresar el autor del libro
            string categoria = txtCategoria.Text;
            string autor = txtAutor.Text; // asumiendo que tienes un TextBox llamado txtTitulo para ingresar el título del libro


            if (conexion.AgregarLibro(titulo, editorial, categoria, autor, (int)numEjemplares.Value))
            {
                MessageBox.Show("El libro se a guardado correctamente :)");
            }
        }
        ////.////

    }
}
