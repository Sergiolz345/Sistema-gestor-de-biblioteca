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
    public partial class Menu : Form
    {

        public dbSqlServer conexion = null;

        public String sData = "";
        string  sUsuario, sPassword;

        public Menu(string Usuario, string Pasword)
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

        private void Menu_MouseDown(object sender, MouseEventArgs e)
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
        #endregion


        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ALibro c = new ALibro(sUsuario, sPassword);
            c.ShowDialog();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            BLibro c = new BLibro(sUsuario, sPassword);
            c.ShowDialog();
        }


        private void btnPrestamos_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Registros c = new Registros(sUsuario, sPassword);
            c.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            int day;
            int month;
            int year;
            DateTime fechaActual = DateTime.Now;
            day = fechaActual.Day;
            month = fechaActual.Month;
            year = fechaActual.Year;

            DataTable dt = new DataTable();
            int Row = 0;

            dt = conexion.Registros();
            int n = dt.Rows.Count;

            foreach (DataRow r in dt.Rows)
            {
                int o = 1;
                int D = 0;
                int M = 0;
                int Y = 0;

                string[] FechaVencimiento = r["Fecha_Vencimiento"].ToString().Split('/', ' ');

                foreach (string l in FechaVencimiento)
                {
                    if (o == 1)
                    {
                        D = int.Parse(l);
                    }

                    if (o == 2)
                    {
                        M = int.Parse(l);
                    }

                    if (o == 3)
                    {
                        Y = int.Parse(l);
                    }

                    o++;
                }

                if (M == month && D == day)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[Row].Cells[0].Value = r["PrestamoID"].ToString();
                    dataGridView1.Rows[Row].Cells[1].Value = r["LibroID"].ToString();
                    dataGridView1.Rows[Row].Cells[2].Value = r["Titulo"].ToString();
                    dataGridView1.Rows[Row].Cells[3].Value = r["nombreAlumno"].ToString();
                    dataGridView1.Rows[Row].Cells[4].Value = r["Fecha_Vencimiento"].ToString();
                    Row = Row + 1;
                }
            }
        }

        private void lkHistorial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Historial c = new Historial(sUsuario, sPassword);
            c.ShowDialog();
        }

        private void btnNPrestamo_Click(object sender, EventArgs e)
        {
            this.Hide();
            NPrestamo c = new NPrestamo(sUsuario, sPassword);
            c.ShowDialog();
        }


    }
}
