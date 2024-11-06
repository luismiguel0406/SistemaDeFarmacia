using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;


namespace Sistema_de__inventarioGP2
{
    public partial class Usuarios_Gp2 : Form
    {
                
        private Usuarios_Gp2()
        {
            InitializeComponent();
        }

        private static Usuarios_Gp2 instancia = null;
        public static Usuarios_Gp2 obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new Usuarios_Gp2();

            }
            return instancia;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public void limpiar()
        {
            txtcargo.Clear();
            txtcodigo.Clear();
            txtcontrasena.Clear();
            txtnombreusuario.Clear();
            cbprivilegios.Text = "";
            txtnombreusuario.Focus();
                 }
        public void refrescartablausuario()
        {
            LoginGP2 conexionlog = new LoginGP2();
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM USUARIOS", con);
            DataTable dtusuario = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtusuario);
            dgvusuarios.DataSource = dtusuario;
        }
            
            
        public void Consulta(string conexion, string query)
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

        public void Consultaeliminar(string conexioneli, string queryeli)
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

        public void Consultaeupdate(string conexionupdt, string queryupdt)
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

        private void dgvusuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
            txtcodigo.Text = dgvusuarios.CurrentRow.Cells[0].Value.ToString();
            txtnombreusuario.Text = dgvusuarios.CurrentRow.Cells[1].Value.ToString();
            txtcontrasena.Text = dgvusuarios.CurrentRow.Cells[2].Value.ToString();
            txtcargo.Text = dgvusuarios.CurrentRow.Cells[3].Value.ToString();
            cbprivilegios.Text= dgvusuarios.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void beliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexioneli = conexionlog.cnn();
                string queryeli = "delete from USUARIOS where ID_USUARIO = '" + txtcodigo.Text + "'";
                Usuarios_Gp2 obj = new Usuarios_Gp2();
                obj.Consultaeliminar(conexioneli, queryeli);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM USUARIOS", con);
                DataTable dtusuario = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtusuario);
                dgvusuarios.DataSource = dtusuario;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                refrescartablausuario();
            }
                      

            //LIMPIAR LOS TEXTBOX
            limpiar();

            
        }

        private void Usuarios_Gp2_Load(object sender, EventArgs e)
        {   //cargar datos a la tabla
            refrescartablausuario();
             
                     
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bagregar_Click(object sender, EventArgs e)
        {
                    
            //consulta 
            LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
            string conexion = conexionlog.cnn();
            string query = "insert into USUARIOS (NOMBRE,CONTRASENA,CARGO,PRIVILEGIOS) values('" + txtnombreusuario.Text + "' , '" + txtcontrasena.Text + "','" + txtcargo.Text + "','" + cbprivilegios.Text + "');";
            Usuarios_Gp2 obj = new Usuarios_Gp2();
            obj.Consulta(conexion, query);
            //refrescar datagridview            
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM USUARIOS", con);
            DataTable dtusuario = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtusuario);
            dgvusuarios.DataSource = dtusuario;

            //LIMPIAR LOS TEXTBOX
            limpiar();
                        
        }

        private void bmodificar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea actualizar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexionupdt = conexionlog.cnn();
                string queryupdt = "UPDATE USUARIOS SET NOMBRE ='"+txtnombreusuario.Text+"', CONTRASENA = '"+txtcontrasena.Text+"', CARGO ='" + txtcargo.Text + "', PRIVILEGIOS='"+cbprivilegios.Text+"' WHERE ID_USUARIO='"+txtcodigo.Text+"'";
                Usuarios_Gp2 obj = new Usuarios_Gp2();
                obj.Consultaeupdate(conexionupdt, queryupdt);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM USUARIOS", con);
                DataTable dtusuario = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtusuario);
                dgvusuarios.DataSource = dtusuario;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                refrescartablausuario();
            }
              //LIMPIAR LOS TEXTBOX
               limpiar();


        }

        private void Usuarios_Gp2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            limpiar();
            
        }

        private void bagregar_MouseMove(object sender, MouseEventArgs e)
        {
            bagregar.BackColor = Color.Teal;
            bagregar.ForeColor = Color.DarkGray;
        }

              

        private void bnuevo_MouseMove(object sender, MouseEventArgs e)
        {
            bnuevo.BackColor = Color.Teal;
            bnuevo.ForeColor = Color.DarkGray;
        }

        private void bmodificar_MouseMove(object sender, MouseEventArgs e)
        {
            bmodificar.BackColor = Color.Teal;
            bmodificar.ForeColor = Color.DarkGray;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void beliminar_MouseMove(object sender, MouseEventArgs e)
        {
            beliminar.BackColor = Color.DarkRed;
            beliminar.ForeColor = Color.DarkGray;
        }

        private void txtbuscarusu_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscarusu.Text != "")
            {
                dgvusuarios.CurrentCell = null;
                foreach (DataGridViewRow r in dgvusuarios.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvusuarios.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtbuscarusu.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                LoginGP2 conuser2 = new LoginGP2();
                SqlConnection con = new SqlConnection(conuser2.cnn());
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM USUARIOS", con);
                DataTable dtusuario = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtusuario);
                dgvusuarios.DataSource = dtusuario;
            }
        }

        private void bagregar_MouseLeave(object sender, EventArgs e)
        {
            bagregar.BackColor = Color.DarkGray;
            bagregar.ForeColor = Color.Teal;
        }

        private void bnuevo_MouseLeave(object sender, EventArgs e)
        {
            bnuevo.BackColor = Color.DarkGray;
            bnuevo.ForeColor = Color.Teal;
        }

        private void bmodificar_MouseLeave(object sender, EventArgs e)
        {
            bmodificar.BackColor = Color.DarkGray;
            bmodificar.ForeColor = Color.Teal;
        }

        private void beliminar_MouseLeave(object sender, EventArgs e)
        {
            beliminar.BackColor = Color.DarkGray;
            beliminar.ForeColor = Color.DarkRed;
        }

        private void Usuarios_Gp2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Usuarios_Gp2.instancia = null;
        }
    }
}
