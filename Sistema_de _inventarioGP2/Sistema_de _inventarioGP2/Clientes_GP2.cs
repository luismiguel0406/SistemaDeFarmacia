using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Sistema_de__inventarioGP2
{
    public partial class Clientes_GP2 : Form
    {
        private Clientes_GP2()
        {
            InitializeComponent();
        }
        private static Clientes_GP2 instancia = null;
        public static Clientes_GP2 obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new Clientes_GP2();

            }
            return instancia;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public void limpiar()
         {
            txtnombre.Clear();
            txtcodigo.Clear();
            txtapellidos.Clear();
            txttelefono.Clear();
            txtcedula.Clear();
            txtemail.Clear();
            cbseguro.Text = "";
            txtnombre.Focus();

        }
        private void refrescartablaclientes()
        {
            LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM CLIENTES", con);
            DataTable dtclientes = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtclientes);
            dgvclientes.DataSource = dtclientes;

        }
        public void Consultaagregarcli(string conexion, string query)
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
        public void Consultaeupdatecli(string conexionupdt, string queryupdt)
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

        public void Consultaeliminarcli(string conexioneli, string queryeli)
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

        private void Clientes_GP2_Load(object sender, EventArgs e)
        {

            refrescartablaclientes();
        }

        private void bagregar_Click(object sender, EventArgs e)
        {
            
            //consulta 
            LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
            string conexion = conexionlog.cnn();
            string query = "insert into CLIENTES (NOMBRE,APELLIDO,CEDULA,EMAIL,TELEFONO,SEGURO) values('" + txtnombre.Text + "' , '" + txtapellidos.Text + "','" + txtcedula.Text + "','" + txtemail.Text + "','" + txttelefono.Text + "','"+ cbseguro.Text +"');";
            Clientes_GP2 obj = new Clientes_GP2();
            obj.Consultaagregarcli(conexion, query);
            //refrescar datagridview            
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM CLIENTES", con);
            DataTable dtcliente = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtcliente);
            dgvclientes.DataSource = dtcliente;

            //LIMPIAR LOS TEXTBOX
            limpiar();


        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bmodificar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea actualizar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexionupdt = conexionlog.cnn();
                string queryupdt = "UPDATE CLIENTES SET NOMBRE ='" + txtnombre.Text + "', APELLIDO = '" + txtapellidos.Text + "', CEDULA ='" + txtcedula.Text + "', EMAIL='" + txtemail.Text + "',TELEFONO = '" + txttelefono.Text + "',SEGURO = '" + cbseguro.Text + "' WHERE ID_CLIENTE='" + txtcodigo.Text + "'";
                Clientes_GP2 obj = new Clientes_GP2();
                obj.Consultaeupdatecli(conexionupdt, queryupdt);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM CLIENTES", con);
                DataTable dtcliente = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtcliente);
                dgvclientes.DataSource = dtcliente;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                refrescartablaclientes();
            }
            //LIMPIAR LOS TEXTBOX
            limpiar();


        }

        private void bcancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            
        }

        private void beliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar?", "Aviso", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexioneli = conexionlog.cnn();
                string queryeli = "delete from CLIENTES where ID_CLIENTE = '" + txtcodigo.Text + "'";
                Clientes_GP2 obj = new Clientes_GP2();
                obj.Consultaeliminarcli(conexioneli, queryeli);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM CLIENTES", con);
                DataTable dtcliente = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtcliente);
                dgvclientes.DataSource = dtcliente;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                refrescartablaclientes();
            }
            
            //LIMPIAR LOS TEXTBOX
            limpiar();


        }

        private void dgvclientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtcodigo.Text = dgvclientes.CurrentRow.Cells[0].Value.ToString();
            txtnombre.Text = dgvclientes.CurrentRow.Cells[1].Value.ToString();
            txtapellidos.Text = dgvclientes.CurrentRow.Cells[2].Value.ToString();
            txtcedula.Text = dgvclientes.CurrentRow.Cells[3].Value.ToString();
            txtemail.Text = dgvclientes.CurrentRow.Cells[4].Value.ToString();
            txttelefono.Text = dgvclientes.CurrentRow.Cells[5].Value.ToString();
            cbseguro.Text = dgvclientes.CurrentRow.Cells[6].Value.ToString();
            
        }

        private void txtbuscarcliente_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscarcliente.Text != "")
            {
                dgvclientes.CurrentCell = null;
                foreach (DataGridViewRow r in dgvclientes.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvclientes.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtbuscarcliente.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                refrescartablaclientes();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void bnuevo_MouseMove(object sender, MouseEventArgs e)
        {
            bnuevo.BackColor = Color.Teal;
            bnuevo.ForeColor = Color.DarkGray;
        }

        private void bnuevo_MouseLeave(object sender, EventArgs e)
        {
            bnuevo.BackColor = Color.DarkGray;
            bnuevo.ForeColor = Color.Teal;
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

        private void Clientes_GP2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Clientes_GP2.instancia = null;
        }
    }
    }

