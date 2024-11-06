using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;//mover el formulario
using System.Data.SqlClient;//servicios sql
using Capa_Gestion_de_seguridad;

namespace Sistema_de__inventarioGP2
{
    public partial class Menu_Principal_GP2 : Form
    {
        public Menu_Principal_GP2()
        {
            InitializeComponent();
        }

      
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd,int wmsg,int wparam,int lparam);


        //variable para que el menu aparezca lentamente
        int vista = 0;

        public void Notificacion(){
            LoginGP2 conexion = new LoginGP2();
            SqlConnection conlog = new SqlConnection(conexion.cnn());
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;
            DataTable tabla = new DataTable();
            int faltante = 0;
            conlog.Open();
            comando.Connection = conlog;
            comando.CommandText = "SELECT * FROM INVENTARIO WHERE CANTIDAD <=5";
            comando.CommandType = CommandType.Text;
            reader = comando.ExecuteReader();
            tabla.Load(reader);
            faltante = tabla.Rows.Count;
            conlog.Close();
            lblnotificacion.Text = faltante.ToString();
          
        }
        public void privilegios()
        {
            if (cachelogin.privilegios=="Standar")
            {
                usuariosToolStripMenuItem.Enabled = false;
                Btproveedores.Enabled = false;
                Btinventario.Enabled = false;
                Btreportes.Enabled = false;

            }


        }
                
        private void button5_Click(object sender, EventArgs e)
        {
            Clientes_GP2 Fclientes = Clientes_GP2.obtener_instancia();
            Fclientes.Show();
            
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InventarioGp2 Finventario = InventarioGp2.obtener_instancia();
            Finventario.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Proveedores_GP2 Fproveedores = Proveedores_GP2.obtener_instancia();
            Fproveedores.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Facturacion_GP2 Ffacturacion = Facturacion_GP2.obtener_instancia();
            try
            {
                Ffacturacion.Show();
            }
            catch (ObjectDisposedException ex)
            {

               MessageBox.Show(ex.Message);
            }
           

                   }

      
        private void administrarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            Usuarios_Gp2 Fusuarios = Usuarios_Gp2.obtener_instancia();
            Fusuarios.Show();
        }

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea Cerrar Sesion?" , "Advertencia", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning ) == DialogResult.Yes)
            {
                this.Close();
                LoginGP2 Flogin = new LoginGP2();
                Flogin.Show();

            }


        }

        private void Menu_Principal_GP2_Load(object sender, EventArgs e)
        {
            datosusuario();
            this.Opacity = 0.0;
            tmaparecermenu.Start();
            Notificacion();
            privilegios();
        }
        private void datosusuario()
        {
            lblnombreusuario.Text = cachelogin.nombre;
            lblcargo.Text = cachelogin.cargo;
            lblprivilegios.Text = cachelogin.privilegios;
            
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //this.Close();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // algunos efectos para cuando el mouse pase por encima
        private void Btfacturacion_MouseMove(object sender, MouseEventArgs e)
        {
            Btfacturacion.BackColor = Color.Teal;
            Btfacturacion.ForeColor = Color.DarkGray;
        }

        private void Btclientes_MouseMove(object sender, MouseEventArgs e)
        {
            Btclientes.BackColor = Color.Teal;
            Btclientes.ForeColor = Color.DarkGray;
        }

        private void Btinventario_MouseMove(object sender, MouseEventArgs e)
        {
            Btinventario.BackColor = Color.Teal;
            Btinventario.ForeColor = Color.DarkGray;
        }

        private void Btproveedores_MouseMove(object sender, MouseEventArgs e)
        {
            Btproveedores.BackColor = Color.Teal;
            Btproveedores.ForeColor = Color.DarkGray;
        }

        private void Btfacturacion_MouseLeave(object sender, EventArgs e)
        {
            Btfacturacion.BackColor = Color.DarkGray;
            Btfacturacion.ForeColor = Color.Teal;
        }

        private void Btproveedores_MouseLeave(object sender, EventArgs e)
        {
            Btproveedores.BackColor = Color.DarkGray;
            Btproveedores.ForeColor = Color.Teal;
        }

        private void Btclientes_MouseLeave(object sender, EventArgs e)
        {
            Btclientes.BackColor = Color.DarkGray;
            Btclientes.ForeColor = Color.Teal;
        }

        private void Btinventario_MouseLeave(object sender, EventArgs e)
        {
            Btinventario.BackColor = Color.DarkGray;
            Btinventario.ForeColor = Color.Teal;
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Btreportes_MouseMove(object sender, MouseEventArgs e)
        {
            Btreportes.BackColor = Color.Teal;
            Btreportes.ForeColor = Color.DarkGray;
        }

        private void Btreportes_MouseLeave(object sender, EventArgs e)
        {
            Btreportes.BackColor = Color.DarkGray;
            Btreportes.ForeColor = Color.Teal;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToLongTimeString();
            lblfecha.Text = DateTime.Now.ToLongDateString();
        }

        private void tmaparecermenu_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            vista += 1;
            if (vista == 100)
            {
                tmaparecermenu.Stop();
                
            }

        }

        private void Btreportes_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private void Btcotizaciones_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private void Btcotizaciones_MouseLeave(object sender, EventArgs e)
        {
            Btcotizaciones.ForeColor = Color.Teal;
            Btcotizaciones.BackColor = Color.DarkGray;
        }

        private void Btcotizaciones_MouseMove(object sender, MouseEventArgs e)
        {
            Btcotizaciones.ForeColor = Color.DarkGray;
            Btcotizaciones.BackColor = Color.Teal;
        }
    }
}
