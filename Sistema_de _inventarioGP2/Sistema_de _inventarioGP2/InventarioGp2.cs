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
using Capa_Gestion_de_seguridad;
using System.Drawing;
using ZXing;


namespace Sistema_de__inventarioGP2
{
    public partial class InventarioGp2 :Form
    {
        private InventarioGp2()
        {
            InitializeComponent();
        }
        private static InventarioGp2 instancia = null;
        public static InventarioGp2 obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new InventarioGp2();

            }
            return instancia;
        }



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


        public void limpiar()
        {
            txtid.Clear();
            cbidproveedor.SelectedIndex = 0;
            txtproducto.Clear();
            txtprecio.Clear();
            txtcantidad.Clear();
            dtpfecha.Value = DateTime.Now;//fecha a la fecha actual
            txtubicacion.Clear();
            txtdescripcion.Clear();
            cbidproveedor.Focus();
        }

        public void cargartablainventario()
        {
            LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM INVENTARIO", con);
            DataTable dtinventario = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtinventario);
            dgvinventario.DataSource = dtinventario;

        }
        SqlCommand cmd;
        SqlDataReader reader;
        public void agregaritem(ComboBox cb)
        { //llenar combobox con id proveedor
            try
            {
                LoginGP2 conlog = new LoginGP2();
                SqlConnection conexionlog = new SqlConnection(conlog.cnn());
                conexionlog.Open();
                cmd = new SqlCommand("SELECT ID_PROVEEDOR FROM PROVEEDORES",conexionlog);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cb.Items.Add(reader["ID_PROVEEDOR"].ToString());
                }
                reader.Close();          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }

        }
        public void Consultaagregarinv(string conexion, string query)
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
        public void Consultaeliminarinv(string conexioneli, string queryeli)
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

        public void Consultaeupdateinv(string conexionupdt, string queryupdt)
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
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void InventarioGp2_Load(object sender, EventArgs e)
        {
            cargartablainventario();
            txtbuscar.Focus();
            agregaritem(cbidproveedor);

                   
        }

        private void bagregar_Click(object sender, EventArgs e)
        {
            //consulta 
            LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
            string conexion = conexionlog.cnn();
            //id producto no va en la consulta ya que es autoincrementable
            string query = "insert into INVENTARIO (ID_PROVEEDOR,PRODUCTO,PRECIO,CANTIDAD,FECHA_VENCIMIENTO,UBICACION,DESCRIPCION,FECHA_INGRESO,USUARIO) values(" + cbidproveedor.Text + ",'" + txtproducto.Text + "',"+txtprecio.Text+"," + txtcantidad.Text + ",'" + dtpfecha.Value.ToString("yyyy-MM-dd") + "','" + txtubicacion.Text + "','"+ txtdescripcion.Text +"','"+ DateTime.Now.ToString("yyyy-MM-dd") +"','"+"Agregado por:"+cachelogin.nombre.ToString()+"');";
            InventarioGp2 obj = new InventarioGp2();
            obj.Consultaagregarinv(conexion, query);
            //refrescar datagridview            

            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM INVENTARIO", con);
            DataTable dtinventario = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtinventario);
            dgvinventario.DataSource = dtinventario;

            //LIMPIAR LOS TEXTBOX
            limpiar();
        }

        private void dgvinventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dgvinventario.CurrentRow.Cells[0].Value.ToString();
            cbidproveedor.Text = dgvinventario.CurrentRow.Cells[1].Value.ToString();
            txtproducto.Text = dgvinventario.CurrentRow.Cells[2].Value.ToString();
            txtprecio.Text = dgvinventario.CurrentRow.Cells[3].Value.ToString();
            //verificar si el metodo es text y no otro.
            txtcantidad.Text = dgvinventario.CurrentRow.Cells[4].Value.ToString();
            dtpfecha.Text= dgvinventario.CurrentRow.Cells[5].Value.ToString();
            txtubicacion.Text = dgvinventario.CurrentRow.Cells[6].Value.ToString();
            txtdescripcion.Text = dgvinventario.CurrentRow.Cells[7].Value.ToString();
        }

        private void beliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar?", "Aviso", MessageBoxButtons.YesNo,MessageBoxIcon.Error);
            if (dialogResult == DialogResult.Yes)
            {
                //Haz esto si es si
                LoginGP2 conexionlog = new LoginGP2();//esta linea hace referencia a login porque ahi esta el cnn 
                string conexioneli = conexionlog.cnn();
                string queryeli = "delete from INVENTARIO where ID_PRODUCTO = '" + txtid.Text + "'";
                InventarioGp2 obj = new InventarioGp2();
                obj.Consultaeliminarinv(conexioneli, queryeli);

                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM INVENTARIO", con);
                DataTable dtinventario = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtinventario);
                dgvinventario.DataSource = dtinventario; 
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                cargartablainventario();
            }
            
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
                string queryupdt = "UPDATE INVENTARIO SET ID_PROVEEDOR =" + cbidproveedor.Text + ",PRODUCTO = '" + txtproducto.Text + "',PRECIO = " + txtprecio.Text + ", CANTIDAD = " + txtcantidad.Text + ",FECHA_VENCIMIENTO ='" + dtpfecha.Value.ToString("yyyy-MM-dd") + "',UBICACION = '" + txtubicacion.Text + "',DESCRIPCION = '"+ txtdescripcion.Text +"',FECHA_INGRESO= '"+DateTime.Now.ToString("yyyy-MM-dd")+ "',USUARIO='" + "Modificado por:" + cachelogin.nombre.ToString() + "' WHERE ID_PRODUCTO = '" + txtid.Text + "'";
                InventarioGp2 obj = new InventarioGp2();
                obj.Consultaeupdateinv(conexionupdt, queryupdt);
                SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM INVENTARIO", con);
                DataTable dtinventario = new DataTable(); //Creando tabla virtual 
                sda.Fill(dtinventario);
                dgvinventario.DataSource = dtinventario;
            }
            else if (dialogResult == DialogResult.No)
            {
                //hacer esto si es no

                //refrescar datagridview 
                cargartablainventario();
            }
            //LIMPIAR LOS TEXTBOX
            limpiar();


        }

        private void dgvinventario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //condicion de la celda
            if (this.dgvinventario.Columns[e.ColumnIndex].Name == "CANTIDAD")
            {
                try
                {
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {

                        if (Convert.ToInt32(e.Value) <= 5)
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.BackColor = Color.Orange;
                        }
                    }
                }
                catch (NullReferenceException ex)
                {

                    MessageBox.Show(ex.Message);
                }
                

            }
            

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
         
            //busqueda por lo que se escriba
            if (txtbuscar.Text!="")
            {
                dgvinventario.CurrentCell = null;
                foreach (DataGridViewRow r in dgvinventario.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvinventario.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtbuscar.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    } 
                }
            }


            else
            {
                cargartablainventario();
            }
        }

        

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (this.Width == 835)
            {
                this.Width = 1135;
            }
            else
            {
                this.Width = 835;
            }*/
            
            //instanciar 
           // enviaremail(txtfaltante.Text,txtcuerpomensaje.Text,txtpara.Text);


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void blimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        
        private void bmodificar_MouseMove(object sender, MouseEventArgs e)
        {
            bmodificar.BackColor = Color.Teal;
            bmodificar.ForeColor = Color.DarkGray;
        }

        private void bmodificar_MouseLeave(object sender, EventArgs e)
        {
            bmodificar.ForeColor = Color.Teal;
            bmodificar.BackColor = Color.DarkGray;
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

        private void bagregar_MouseMove(object sender, MouseEventArgs e)
        {
            bagregar.BackColor = Color.Teal;
            bagregar.ForeColor = Color.DarkGray;
        }

        private void bagregar_MouseLeave(object sender, EventArgs e)
        {
            bagregar.ForeColor = Color.Teal;
            bagregar.BackColor = Color.DarkGray;
        }

        private void bnuevo_MouseMove(object sender, MouseEventArgs e)
        {
            bnuevo.BackColor = Color.Teal;
            bnuevo.ForeColor = Color.DarkGray;
        }

        private void bnuevo_MouseLeave(object sender, EventArgs e)
        {
            bnuevo.ForeColor = Color.Teal;
            bnuevo.BackColor = Color.DarkGray;
        }

        private void InventarioGp2_FormClosed(object sender, FormClosedEventArgs e)
        {
            InventarioGp2.instancia = null;
            if (InventarioGp2.instancia==null)
            {
                Menu_Principal_GP2 notf = new Menu_Principal_GP2();
                notf.Notificacion();
            }
           
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {
            if (txtid.Text != "")
            {
                BarcodeWriter br = new BarcodeWriter();
                br.Format = BarcodeFormat.CODE_128;
                Bitmap bm = new Bitmap(br.Write(txtid.Text), 260,80 );
                pbcodigobarras.Image = bm;
            }
        }
    }
    }
    

