using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Capa_Gestion_de_seguridad;//capa de seguridad

namespace Sistema_de__inventarioGP2
{
    public partial class Proveedores_GP2 : Form
    {
        private Proveedores_GP2()
        {
            InitializeComponent();
        }

        private static Proveedores_GP2 instancia = null;
        public static Proveedores_GP2 obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new Proveedores_GP2();

            }
            return instancia;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public void limpiar()
        {
            txtidproveedor.Clear();
            txtrnc.Clear();
            txtnombre.Clear();
            txttelefonos.Clear();
            txtdireccion.Clear();
            txtemail.Clear();
            txtsuministro.Clear();
            
            txtrnc.Focus();


        }
        private void refrescartablaprov() {
            LoginGP2 conexionlog = new LoginGP2();
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM PROVEEDORES", con);
            DataTable dtproveedores = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtproveedores);
            dgvproveedores.DataSource = dtproveedores;

        }
        public void Consultaagregarprov(string conexion, string query)
        {

            SqlConnection conn;
            SqlCommand com;

            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }


            try
            {
                com = new SqlCommand(query, conn);

                com.ExecuteNonQuery();
                MessageBox.Show("Se han Ingresado los datos");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {

                conn.Close();
            }
        }

        public void Consultaeupdateprov(string conexionupdt, string queryupdt)
        {

            SqlConnection connupdt;
            SqlCommand comupdt;

            try
            {
                connupdt = new SqlConnection(conexionupdt);
                connupdt.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }


            try
            {
                comupdt = new SqlCommand(queryupdt, connupdt);

                comupdt.ExecuteNonQuery();
                MessageBox.Show("Datos actualizados correctamente");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {

                connupdt.Close();
            }
        }

        public void Consultaeliminarprov(string conexioneli, string queryeli)
        {

            SqlConnection conneli;
            SqlCommand comeli;

            try
            {
                conneli = new SqlConnection(conexioneli);
                conneli.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }


            try
            {
                comeli = new SqlCommand(queryeli, conneli);

                comeli.ExecuteNonQuery();
                MessageBox.Show("Datos eliminados correctamente");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {

                conneli.Close();
            }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Proveedores_GP2_Load(object sender, EventArgs e)
        {

            refrescartablaprov();
        }

      
        private void Detallesproveedores_Click(object sender, EventArgs e)
        {

        }
               
       
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void blimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void bagregar_Click_1(object sender, EventArgs e)
        {
            //consulta 
            LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
            string conexion = conexionlog.cnn();
            string query = "insert into PROVEEDORES (RNC,NOMBRE,TELEFONO,DIRECCION,EMAIL,SUMINISTRO) values('" + txtrnc.Text + "','" + txtnombre.Text + "','" + txttelefonos.Text + "','" + txtdireccion.Text + "','" + txtemail.Text + "','" + txtsuministro.Text + "');";
            Proveedores_GP2 obj = new Proveedores_GP2();
            obj.Consultaagregarprov(conexion, query);
            //refrescar datagridview            
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM PROVEEDORES", con);
            DataTable dtproveedores = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtproveedores);
            dgvproveedores.DataSource = dtproveedores;

            //LIMPIAR LOS TEXTBOX
            limpiar();
        }

        private void bmodificar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea actualizar?", "Aviso", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexionupdt = conexionlog.cnn();
                string queryupdt = "UPDATE PROVEEDORES SET RNC ='" + txtrnc.Text + "',NOMBRE = '" + txtnombre.Text + "',TELEFONO = '" + txttelefonos.Text + "', DIRECCION ='" + txtdireccion.Text + "',EMAIL = '" + txtemail.Text + "',SUMINISTRO = '" + txtsuministro.Text + "' WHERE ID_PROVEEDOR = '" + txtidproveedor.Text + "'";
                Proveedores_GP2 obj = new Proveedores_GP2();
                obj.Consultaeupdateprov(conexionupdt, queryupdt);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM PROVEEDORES", con);
                DataTable dtproveedores = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtproveedores);
                dgvproveedores.DataSource = dtproveedores;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                refrescartablaprov();
            }
            //LIMPIAR LOS TEXTBOX
            limpiar();

        }

        private void beliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar?", "Aviso", MessageBoxButtons.YesNo,MessageBoxIcon.Error);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexioneli = conexionlog.cnn();
                string queryeli = "delete from PROVEEDORES where ID_PROVEEDOR= '" + txtidproveedor.Text + "'";
                Proveedores_GP2 obj = new Proveedores_GP2();
                obj.Consultaeliminarprov(conexioneli, queryeli);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM PROVEEDORES", con);
                DataTable dtproveedores = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtproveedores);
                dgvproveedores.DataSource = dtproveedores;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                refrescartablaprov();
            }

            //LIMPIAR LOS TEXTBOX
            limpiar();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtbuscarprov_TextChanged(object sender, EventArgs e)
        {  // buscar proveedor
            if (txtbuscarprov.Text != "")
            {
                dgvproveedores.CurrentCell = null;
                foreach (DataGridViewRow r in dgvproveedores.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvproveedores.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtbuscarprov.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                refrescartablaprov();
            }
        }

        private void bagregar_MouseMove(object sender, MouseEventArgs e)
        {
            bagregar.BackColor = Color.Teal;
            bagregar.ForeColor = Color.DarkGray;
        }

        private void bagregar_MouseLeave(object sender, EventArgs e)
        {
            bagregar.BackColor = Color.DarkGray;
            bagregar.ForeColor = Color.Teal;

        }

        private void blimpiar_MouseMove(object sender, MouseEventArgs e)
        {
            blimpiar.BackColor = Color.Teal;
            blimpiar.ForeColor = Color.DarkGray;
        }

        private void blimpiar_MouseLeave(object sender, EventArgs e)
        {
            blimpiar.BackColor = Color.DarkGray;
            blimpiar.ForeColor = Color.Teal;
        }

        private void bmodificar_MouseMove(object sender, MouseEventArgs e)
        {
            bmodificar.BackColor = Color.Teal;
            bmodificar.ForeColor = Color.DarkGray;
        }

        private void bmodificar_MouseLeave(object sender, EventArgs e)
        {
            bmodificar.BackColor = Color.DarkGray;
            bmodificar.ForeColor = Color.Teal;
        }

        private void beliminar_MouseMove(object sender, MouseEventArgs e)
        {
            beliminar.BackColor = Color.DarkRed;
            beliminar.ForeColor = Color.DarkGray;
        }

        private void beliminar_MouseLeave(object sender, EventArgs e)
        {
            beliminar.BackColor = Color.DarkGray;
            beliminar.ForeColor = Color.DarkRed;
        }

        private void dgvproveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtidproveedor.Text = dgvproveedores.CurrentRow.Cells[0].Value.ToString();
            txtrnc.Text = dgvproveedores.CurrentRow.Cells[1].Value.ToString();
            txtnombre.Text = dgvproveedores.CurrentRow.Cells[2].Value.ToString();
            txttelefonos.Text = dgvproveedores.CurrentRow.Cells[3].Value.ToString();
            txtdireccion.Text = dgvproveedores.CurrentRow.Cells[4].Value.ToString();
            txtemail.Text = dgvproveedores.CurrentRow.Cells[5].Value.ToString();
            txtsuministro.Text = dgvproveedores.CurrentRow.Cells[6].Value.ToString();

        }

        private void Proveedores_GP2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Proveedores_GP2.instancia = null;
        }
    }
}
