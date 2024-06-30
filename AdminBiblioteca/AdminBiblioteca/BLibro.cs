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
    public partial class BLibro : Form
    {

        public dbSqlServer conexion = null;

        public String sData = "";
        string sUsuario, sPassword;


        public BLibro(string Usuario, string Pasword)
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

        private void BLibro_MouseDown(object sender, MouseEventArgs e)
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


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == "")
            {
                MessageBox.Show("Termine de rellenar los apartados");
                return;
            }

            DataTable nose = new DataTable();
            nose = conexion.BuscarLibro(cbCriterio.Text, txtBusqueda.Text);
            dataGridView1.DataSource = nose;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                conexion.EliminarLibro(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                DataTable nose = new DataTable();
                nose = conexion.BuscarLibro(cbCriterio.Text, txtBusqueda.Text);
                dataGridView1.DataSource = nose;

                MessageBox.Show("libro a Eliminado Correctamente");
            }

            else
            {
                MessageBox.Show("Seleccion El libro a Eliminar");
            }
        }

        private void BLibro_Load(object sender, EventArgs e)
        {
            cbCriterio.SelectedIndex = 0;
        }

        private void cbCriterio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnPrestar_Click(object sender, EventArgs e)
        {
            this.Hide();
            NPrestamo c = new NPrestamo(sUsuario, sPassword, dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            c.ShowDialog();
        }


    }
}
