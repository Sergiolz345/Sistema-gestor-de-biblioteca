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
    public partial class NPrestamo : Form
    {
        public dbSqlServer conexion = null;

        public String sData = "";
        string sUsuario, sPassword;


        public NPrestamo(string Usuario, string Pasword)
        {
            InitializeComponent();

            sUsuario = Usuario;
            sPassword = Pasword;
            //lblUser.Text = "Usuario: " + Usuario;

            conexion = new dbSqlServer(sUsuario, sPassword);
        }

        public NPrestamo(string Usuario, string Pasword,string Titulo)
        {
            InitializeComponent();

            sUsuario = Usuario;
            sPassword = Pasword;

            tbTitulo.Text = Titulo;

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

        private void Inventarios_MouseDown(object sender, MouseEventArgs e)
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (tbTitulo.Text == "" || tbAlumno.Text == "" || tbMatricula.Text == "" || tbGrupo.Text == "")
            {
                MessageBox.Show("Termine de rellenar los apartados");
                return;
            }

            string titulo = tbTitulo.Text; // asumiendo que tienes un TextBox llamado txtTitulo para ingresar el título del libro
            string nombreAlumno = tbAlumno.Text; // asumiendo que tienes un TextBox llamado txtAutor para ingresar el autor del libro
            string Matricula = tbMatricula.Text;
            string grupo = tbGrupo.Text; // asumiendo que tienes un TextBox llamado txtTitulo para ingresar el título del libro


            if (!conexion.AgregarPrestamo(titulo, nombreAlumno, int.Parse(Matricula), int.Parse(grupo)))
            {
                MessageBox.Show(conexion.sLastError);

                tbTitulo.Text = ""; // asumiendo que tienes un TextBox llamado txtTitulo para ingresar el título del libro
                tbAlumno.Text = ""; // asumiendo que tienes un TextBox llamado txtAutor para ingresar el autor del libro
                tbMatricula.Text = "";
                tbGrupo.Text = "";
            }

            else
            {
               tbTitulo.Text = ""; // asumiendo que tienes un TextBox llamado txtTitulo para ingresar el título del libro
               tbAlumno.Text = ""; // asumiendo que tienes un TextBox llamado txtAutor para ingresar el autor del libro
               tbMatricula.Text = "";
               tbGrupo.Text = "";

               MessageBox.Show("El Prestamo se Realizo correctamente :)");
            }
        }
    }
}
