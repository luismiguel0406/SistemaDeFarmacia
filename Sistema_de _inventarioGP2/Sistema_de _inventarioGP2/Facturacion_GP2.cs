using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;//consultas sql
using Capa_Gestion_de_seguridad; //capa de sgeuridad

namespace Sistema_de__inventarioGP2
{
    public partial class Facturacion_GP2 : Form
    {
        private Facturacion_GP2()
        {
            InitializeComponent();
             
        }
        private static Facturacion_GP2 instancia = null;
        public static Facturacion_GP2 obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new Facturacion_GP2();

            }
            return instancia;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


        public string facidarticulo = "";
        public string facnombre = "";
        public string facprecio ="";
        // variables para calculr cantidad en la tabla dgvfacturas
         int cantidaddgvfac;
         decimal preciodgcfac;
         decimal subtotaldgvfac;

        //Matriz para crear la lista a facturar
        string[,] ListaVenta = new string[200, 6];
        int Fila = 0;

        public void cargarfacturacion()
        {

            LoginGP2 conexionlog = new LoginGP2();
            SqlConnection con = new SqlConnection(conexionlog.cnn()); // cadena de conexion   
            con.Open();
            //la tabla del formulario inventario es la misma quE esta pero con nommbre diferente,por eso se consulta esa tabla
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM INVENTARIO", con);
            DataTable dtfacturacion = new DataTable(); //Creando tabla virtual 
            sda.Fill(dtfacturacion);
            dgvfacturacion.DataSource = dtfacturacion;
        }
        

        public void clienteregistrado (){


            LoginGP2 conexionlog = new LoginGP2();
            SqlConnection conlog = new SqlConnection(conexionlog.cnn());
            conlog.Open();
            using (var comando = new SqlCommand())
            {
                comando.Connection = conlog;
                comando.CommandText = "SELECT * FROM CLIENTES WHERE ID_CLIENTE ='" + txtclientereg.Text + "'";
               // comando.Parameters.AddWithValue(idcliente, txtclientereg.Text);
                comando.CommandType = CommandType.Text;

                try
                {
                    SqlDataReader clientes = comando.ExecuteReader();
                    if (clientes.HasRows)
                    {
                        while (clientes.Read())
                        {
                            lblcliente.Text = (clientes.GetString(1)+" "+clientes.GetString(2)).ToUpperInvariant();
                            lblseguro.Text = clientes.GetString(6).ToUpperInvariant();
                        }
                        
                        ;

                    }
                    //else
                    //{
                        
                    //   // return false;

                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //return false;
                }


            }

        }
        public void CostoApagar()
        {
            float SubTotal = 0;
            int Conteo = 0;
            float tempo = 0;

            Conteo = dgvfactura.RowCount;

            for (int i = 0; i < Conteo; i++)
            {
                tempo = float.Parse(dgvfactura.Rows[i].Cells[4].Value.ToString());
                SubTotal = SubTotal + tempo;


            }
            lblsubtotal.Text = SubTotal.ToString();

        }

       
      
        private void Facturacion_GP2_Load(object sender, EventArgs e)
        {
            bvender.Enabled = false;
            // usuario conectado
            lblusuario.Text = cachelogin.nombre.ToString();

            cargarfacturacion();
        }

        private void txtbuscarproducto_TextChanged(object sender, EventArgs e)
        {
            
            //busqueda por lo que se escriba
            if (txtbuscarproducto.Text != "")
            {
                dgvfacturacion.CurrentCell = null;
                foreach (DataGridViewRow r in dgvfacturacion.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvfacturacion.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtbuscarproducto.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                cargarfacturacion();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //
        
        private void bagregar_Click(object sender, EventArgs e)
        {
            if (facidarticulo!="" && facnombre!="" &&facprecio !="")
            {
                ListaVenta[Fila, 0] = facidarticulo;
                ListaVenta[Fila, 1] = facnombre;
                ListaVenta[Fila, 2] = facprecio;


                dgvfactura.Rows.Add(ListaVenta[Fila, 0], ListaVenta[Fila, 1], ListaVenta[Fila, 2]);

                Fila++;
            }           
                
           

        }

            
        // costo a pagar si aplica

            
        

        private void dgvfacturacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //estas son las posiciones corresposndientes a la tabla inventario y a su vez a la de facturacion
            // para que las variables tomen ese valor
            facidarticulo = dgvfacturacion.CurrentRow.Cells[0].Value.ToString();
            facnombre = dgvfacturacion.CurrentRow.Cells[2].Value.ToString();
            facprecio = dgvfacturacion.CurrentRow.Cells[3].Value.ToString();
          
            
        }

        

        private void lblsubtotal_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie hara el calculo automatico del itbis
            if (lblsubtotal.Text!="0.00")
            {
                lblitbis.Text = (0.18 * float.Parse(lblsubtotal.Text)).ToString();
                lbltotal.Text = (float.Parse(lblsubtotal.Text) + float.Parse(lblitbis.Text)).ToString();// - double.Parse(lbldescuento.Text)).ToString());
            }
            if (lbldescuento.Text!="0.00")
            {
                lbltotal.Text = (float.Parse(lbltotal.Text) - double.Parse(lbldescuento.Text)).ToString();
            }
            
                        
        }

        
        
        private void dgvfactura_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           // calculo del subtotal cuando la cantidad sea elegida


            if (dgvfactura.Columns[e.ColumnIndex].Name == "Column4")
            {
                try
                {
                    cantidaddgvfac = int.Parse(dgvfactura.Rows[e.RowIndex].Cells[3].Value.ToString());
                }
                catch (Exception )
                {
                   MessageBox.Show("Agregue un producto a la lista");
                    
                }
                try
                {
                    preciodgcfac = decimal.Parse(dgvfactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
                catch (Exception )
                {

                    MessageBox.Show("Agregue un producto a la lista");
                }
                try
                {
                    if (cantidaddgvfac != 0 && preciodgcfac != 0)
                    {
                        subtotaldgvfac = cantidaddgvfac * preciodgcfac;
                        dgvfactura.Rows[e.RowIndex].Cells[4].Value = subtotaldgvfac.ToString();
                        CostoApagar();

                        //if (dgvfactura.CurrentRow.Cells["Column4"].Selected.ToString()!= "0")
                        //{
                        //    SendKeys.Send("{ENTER}");
                        //}
                        
                    
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Parece hubo un error con el Subtotal"+ex);
                }
               
                
            }
        }

       
        private void dgvfacturacion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //condicion de la celda
            if (this.dgvfacturacion.Columns[e.ColumnIndex].Name == "CANTIDAD")
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

        private void bquitar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvfactura.Rows.Remove(dgvfactura.CurrentRow);
                if (dgvfactura.RowCount == 0)
                {
                    lblsubtotal.Text = "0.00";
                    lblitbis.Text = "0.00";
                    lbldescuento.Text = "0.00";
                    lbltotal.Text = "0.00";
                    //
                    
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Lista vacia","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

                    
        }
               

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            clienteregistrado();
        }
        public double totaltemp = 0.00;
        public double descuentotemp = 0.00;
        private void txtclientereg_TextChanged(object sender, EventArgs e)
        {// si este texto cambia significa que la venta no sera efectuada con el seguro que tenia si no con otro
            if (lblseguro.Text!="...")
            {
                descuentotemp = Convert.ToDouble(lbldescuento.Text);
                totaltemp = Convert.ToDouble(lbltotal.Text);
                lbltotal.Text = (descuentotemp + totaltemp).ToString();
                totaltemp = 0.00;
                descuentotemp = 0.00;
            }
                      
            
            //  esta primera linea devolvera el valor que tenia el total ya que el descuento cambiara sumandole el mismo descuento
            //  que le habia quitado y aunque el txt cambie nuevamente el valor que tendra sera cero
             
            lblcliente.Text = "...";
            lblseguro.Text = "...";
        }

        private void lblseguro_TextChanged(object sender, EventArgs e)
        {
            
            // seguro automamtico
            if (lblseguro.Text == "...")
            {
                rbninguno.Checked = true;
                if (lbltotal.Text!="0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0).ToString();
                }
                
            }

            else if (lblseguro.Text == "SENASA")
            {
                rbsenasa.Checked = true;
                if (lbltotal.Text != "0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0.1).ToString();
                }
                

            }
            else if (lblseguro.Text == "UNIVERSAL")
            {
                rbuniversal.Checked = true;
                if (lbltotal.Text != "0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0.2).ToString();
                }
                
            }
            else if (lblseguro.Text== "PALIC")
            {
                rbpalic.Checked = true;
                if (lbltotal.Text != "0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0.3).ToString();
                }
                
            }
            else if (lblseguro.Text == "SENASA PRIV")
            {
                rbsenasacomp.Checked = true;
                if (lbltotal.Text != "0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0.4).ToString();
                }
                
            }
            else if (lblseguro.Text == "RENACER")
            {
                rbrenacer.Checked = true;
                if (lbltotal.Text != "0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0.25).ToString();
                }
                
            }
            else if (lblseguro.Text == "HUMANO")
            {
                rbhumano.Checked = true;
                if (lbltotal.Text != "0.00")
                {
                    lbldescuento.Text = (float.Parse(lbltotal.Text) * 0.18).ToString();
                }
                
            }
            
            

        }

        private void txtclientereg_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = linkLabel1;
        }

        private void txtclientereg_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void bvender_MouseMove(object sender, MouseEventArgs e)
        {
            bvender.BackColor = Color.Teal;
            bvender.ForeColor = Color.DarkGray;
        }

        private void bvender_MouseLeave(object sender, EventArgs e)
        {
            bvender.BackColor = Color.DarkGray;
            bvender.ForeColor = Color.Teal;
        }

        private void fecha_Tick(object sender, EventArgs e)
        {
            lblhoraf.Text = DateTime.Now.ToLongTimeString();
            lblfechaf.Text = DateTime.Now.ToShortDateString();
        }

        

        private void lbldescuento_TextChanged(object sender, EventArgs e)
        {     
            //condicion de total
            if (lbltotal.Text!="0.00")
            {  // devuelve el valor que el total tenia antes de que se le reste el proximo descuento si aplica
                
                lbltotal.Text = (float.Parse(lbltotal.Text) - double.Parse(lbldescuento.Text)).ToString();
            }
            


        }


        private void bvender_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dialogo = MessageBox.Show("Desea facturar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogo == DialogResult.Yes)
                                
                {
                    
                    
                    /* reduce lo que se factura de la tabla inventario/facturacion*/
                    EliminarFaCturados();
                    Creador_de_factura.clsFuncion.CreaTicket Ticket1 = new Creador_de_factura.clsFuncion.CreaTicket();
                   
                    Ticket1.TextoCentro(" Farmacia Yudelka "); //imprime una linea de descripcion
                    Ticket1.TextoCentro("**********************************");

                    Ticket1.TextoIzquierda("Dirc: xxxx");
                    Ticket1.TextoIzquierda("Tel: xxxx");
                    Ticket1.TextoIzquierda("Rnc: xxxx");
                    Ticket1.TextoIzquierda("");
                    Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
                    Ticket1.TextoIzquierda("No Fac: 000");
                    Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
                    Ticket1.TextoIzquierda("Le Atendio:" + cachelogin.nombre.ToString());///////////////////////////////////
                    Ticket1.TextoIzquierda("");
                    Creador_de_factura.clsFuncion.CreaTicket.LineasGuion();

                    Creador_de_factura.clsFuncion.CreaTicket.EncabezadoVenta();
                    Creador_de_factura.clsFuncion.CreaTicket.LineasGuion();
                    try
                    {

                        foreach (DataGridViewRow r in dgvfactura.Rows)

                        // LA TABLA FACTURA DEBE TENER ESTAS COLUMNAS PARA QUE COINCIDA CON LO QUE PUSE AQUI EL NUMERO DE LAS CELDAS
                        //ID FACTURA AUTOINCREMENTABLE POSICION 0
                        // NOMBRE DEL ARTICULO POSICION 1
                        //PRECIO POSICION 2
                        //CANTIDAD POSICION 3
                        // SUBTOTAL  POSICION 4

                        { //                        Nombre del articulo                Precio                                   Cantidad                                SubTotal 


                            Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
                        }

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Complete los datos en la lista de venta","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }

                    Creador_de_factura.clsFuncion.CreaTicket.LineasGuion();
                    //                                 CAMBIE EL VALOR "0" POR EL VALOR DEL LABEL SUBTOTAL
                    Ticket1.AgregaTotales("Sub-Total", double.Parse(lblsubtotal.Text)); // imprime linea con total
                    Ticket1.AgregaTotales("Menos Descuento", double.Parse(lbldescuento.Text)); // imprime linea con total
                                                                                               //                                 CAMBIE EL VALOR "0" POR EL VALOR DEL LABEL ITBIS
                    Ticket1.AgregaTotales("Mas ITBIS", double.Parse(lblitbis.Text)); // imprime linea con total
                    Ticket1.TextoIzquierda(" ");
                    Ticket1.AgregaTotales("Total", double.Parse(lbltotal.Text)); // imprime linea con total
                    Ticket1.TextoIzquierda(" ");
                    Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(txtefectivo.Text));
                    Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(lbldevuelta.Text));


                    // Ticket1.LineasTotales(); // imprime linea 

                    Ticket1.TextoIzquierda(" ");
                    Ticket1.TextoCentro("**********************************");
                    Ticket1.TextoCentro("*     Gracias por preferirnos     *");

                    Ticket1.TextoCentro("**********************************");
                    Ticket1.TextoIzquierda(" ");
                    string impresora = /*"Microsoft XPS Document Writer"*/"EPSON L355 Series"; //usar variable
                    Ticket1.ImprimirTiket(impresora);
                    //hasta aqui el codigo de imprimir


                    //reinicia el conteo de las filas en la tabla de factura
                    Fila = 0;
                    while (dgvfactura.RowCount > 0)//limpia el dgv
                    { dgvfactura.Rows.Remove(dgvfactura.CurrentRow); }

                    lblsubtotal.Text = "0.00";
                    lblitbis.Text = "0.00";
                    lbldevuelta.Text = "0.00";
                    lbldescuento.Text = "0.00";
                    lbltotal.Text = "0.00";

                    txtefectivo.Clear();
                    bvender.Enabled = false;



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void Facturacion_GP2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Facturacion_GP2.instancia = null;
            Menu_Principal_GP2 notf = new Menu_Principal_GP2();
            notf.Notificacion();
        }

        private void bcotizar_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dialogo = MessageBox.Show("Se enviara a cotizaciones?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogo == DialogResult.Yes)

                {
                   
                    

                    Creador_de_factura.clsFuncion.CreaTicket Ticket1 = new Creador_de_factura.clsFuncion.CreaTicket();
                   
                    Ticket1.TextoCentro(" Farmacia Yudelka "); //imprime una linea de descripcion
                    Ticket1.TextoCentro("**********************************");
                    Ticket1.TextoCentro("COTIZACION SIN FINES CONTABLES");
                    Ticket1.TextoIzquierda("Dirc: xxxx");
                    Ticket1.TextoIzquierda("Tel: xxxx");
                    Ticket1.TextoIzquierda("Rnc: xxxx");
                    Ticket1.TextoIzquierda("");
                   // Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
                   // Ticket1.TextoIzquierda("No Fac: 000");
                    Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
                    Ticket1.TextoIzquierda("Le Atendio:" + cachelogin.nombre.ToString());///////////////////////////////////
                    Ticket1.TextoIzquierda("");
                    Creador_de_factura.clsFuncion.CreaTicket.LineasGuion();

                    Creador_de_factura.clsFuncion.CreaTicket.EncabezadoVenta();
                    Creador_de_factura.clsFuncion.CreaTicket.LineasGuion();
                    try
                    {

                        foreach (DataGridViewRow r in dgvfactura.Rows)

                        // LA TABLA FACTURA DEBE TENER ESTAS COLUMNAS PARA QUE COINCIDA CON LO QUE PUSE AQUI EL NUMERO DE LAS CELDAS
                        //ID FACTURA AUTOINCREMENTABLE POSICION 0
                        // NOMBRE DEL ARTICULO POSICION 1
                        //PRECIO POSICION 2
                        //CANTIDAD POSICION 3
                        // SUBTOTAL  POSICION 4

                        { //                        Nombre del articulo                Precio                                   Cantidad                                SubTotal 


                            Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
                        }

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Complete la lista de venta");
                    }

                    Creador_de_factura.clsFuncion.CreaTicket.LineasGuion();
                    //                                 CAMBIE EL VALOR "0" POR EL VALOR DEL LABEL SUBTOTAL
                    Ticket1.AgregaTotales("Sub-Total", double.Parse(lblsubtotal.Text)); // imprime linea con total
                    Ticket1.AgregaTotales("Menos Descuento", double.Parse(lbldescuento.Text)); // imprime linea con total
                                                                                               //                                 CAMBIE EL VALOR "0" POR EL VALOR DEL LABEL ITBIS
                    Ticket1.AgregaTotales("Mas ITBIS", double.Parse(lblitbis.Text)); // imprime linea con total
                    Ticket1.TextoIzquierda(" ");
                    Ticket1.AgregaTotales("Total", double.Parse(lbltotal.Text)); // imprime linea con total
                    Ticket1.TextoIzquierda(" ");
                   // Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(txtefectivo.Text));
                   // Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(lbldevuelta.Text));


                    // Ticket1.LineasTotales(); // imprime linea 

                    Ticket1.TextoIzquierda(" ");
                    Ticket1.TextoCentro("COTIZACION SIN FINES CONTABLES");
                    Ticket1.TextoCentro("**********************************");
                    Ticket1.TextoCentro("*     Gracias por preferirnos    *");

                    Ticket1.TextoCentro("**********************************");
                    Ticket1.TextoIzquierda(" ");
                    string impresora = "Microsoft XPS Document Writer"; //usar variable
                    Ticket1.ImprimirTiket(impresora);
                    //hasta aqui el codigo de imprimir


                    Fila = 0;
                    while (dgvfactura.RowCount > 0)//limpia el dgv
                    { dgvfactura.Rows.Remove(dgvfactura.CurrentRow); }

                    lblsubtotal.Text = "0.00";
                    lblitbis.Text = "0.00";
                    lbldevuelta.Text = "0.00";
                    lbldescuento.Text = "0.00";
                    lbltotal.Text = "0.00";

                    txtefectivo.Clear();



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message+"verifique si aplico efectivo");
            }

        }

        private void bquitar_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void bquitar_MouseMove(object sender, MouseEventArgs e)
        {
            bquitar.ForeColor = Color.DarkGray;
            bquitar.BackColor = Color.DarkRed;
        }

        private void bquitar_MouseLeave(object sender, EventArgs e)
        {
            bquitar.ForeColor = Color.DarkRed;
            bquitar.BackColor = Color.DarkGray;
        }

        private void bagregar_MouseMove(object sender, MouseEventArgs e)
        {
            bagregar.ForeColor = Color.DarkGray;
            bagregar.BackColor = Color.Teal;
        }

        private void bagregar_MouseLeave(object sender, EventArgs e)
        {
            bagregar.ForeColor = Color.Teal;
            bagregar.BackColor = Color.DarkGray;
        }

        public void EliminarFaCturados()
        {//variables

            int idproductoFACTURA = 0;
            int idproductofacturacion = 0;
            int cantidadproductoFACTURA = 0;
            int cantidadproductofacturacion = 0;
            int cantidadRESTANTE = 0;
            int conteofilas = 0;
            // numero de filas
            conteofilas = dgvfactura.RowCount;
            //conexion
            LoginGP2 conexion = new LoginGP2();
            SqlConnection conexionlog = new SqlConnection(conexion.cnn());
            

            //ciclo for


            for (int i = 0; i < conteofilas; i++)
            {
                conexionlog.Open();
                idproductoFACTURA = int.Parse(dgvfactura.Rows[i].Cells[0].Value.ToString());
                cantidadproductoFACTURA = int.Parse(dgvfactura.Rows[i].Cells[3].Value.ToString());

                //// CONSULTAS 
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexionlog;
                    comando.CommandText = "SELECT * FROM INVENTARIO WHERE ID_PRODUCTO=" + idproductoFACTURA + "";
                    comando.CommandType = CommandType.Text;

                    try
                    {
                        SqlDataReader reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                idproductofacturacion = reader.GetInt32(0);
                                cantidadproductofacturacion = reader.GetInt32(4);
                            }
                        }

                        cantidadRESTANTE = cantidadproductofacturacion - cantidadproductoFACTURA;
                        conexionlog.Close();
                        reader.Close();
                        //se agrego esto


                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                    ///////////////////////////////////////
                    comando.Connection = conexionlog;
                    conexionlog.Open();
                    comando.CommandText = "UPDATE INVENTARIO SET CANTIDAD =" + cantidadRESTANTE + " WHERE ID_PRODUCTO = " + idproductofacturacion + "";
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader2 = comando.ExecuteReader();
                    // se agrego esto
                    conexionlog.Close();
                    reader2.Close();
                    
                }
            }
            cargarfacturacion();
        }  


        private void txtefectivo_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (txtefectivo.Text != "")
                {
                    if (float.Parse(txtefectivo.Text) >= float.Parse(lbltotal.Text))
                    {
                        bvender.Enabled = true;
                    }
                    else 
                    {
                        bvender.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {

               return;
            }

                        
            
                  
            try
            {
                lbldevuelta.Text = (float.Parse(txtefectivo.Text) - float.Parse(lbltotal.Text)).ToString();
            }
            catch
            {

                lbldevuelta.Text = "0.00";
            }
        }

       
    }
}
