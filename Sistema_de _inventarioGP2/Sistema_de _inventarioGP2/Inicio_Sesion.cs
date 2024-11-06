using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;// se agrega este using para poder usar las consultas de SQL
using Capa_Gestion_de_seguridad;// referencia capa de seguridad

namespace Sistema_de__inventarioGP2
{
    public partial class LoginGP2 : Form
    {
        public LoginGP2()
        {
            InitializeComponent();
            
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void label3_Click(object sender, EventArgs e)
        {
            
        }
        public string cnn()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        }


        public bool login (string nombre,string contraseña)
        {
         
            
            SqlConnection conlog = new SqlConnection(cnn());
            conlog.Open();
            using (var comando = new SqlCommand())
            {
                comando.Connection = conlog;
                comando.CommandText = "SELECT * FROM USUARIOS WHERE NOMBRE ='" + txtusuario.Text + "' AND CONTRASENA ='" + txtcontraseña.Text + "' ";
                comando.Parameters.AddWithValue(nombre,txtusuario.Text);
                comando.Parameters.AddWithValue(contraseña,txtcontraseña.Text);
                comando.CommandType = CommandType.Text;

                try
                {
                    SqlDataReader usuarios = comando.ExecuteReader();
                    if (usuarios.HasRows)
                    {
                        while (usuarios.Read())
                        {
                            cachelogin.idusuario = usuarios.GetInt32(0);//id de usuario 
                            cachelogin.nombre = usuarios.GetString(1);//nombre de usuario
                            cachelogin.cargo = usuarios.GetString(3);//cargo
                            cachelogin.privilegios = usuarios.GetString(4);//privilegios

                        }
                     
                        // En esta linea de abajo se oculta el formulario login si las credenciales estan correctas
                        this.Hide();
                        Bienvenida bienvenida = new Bienvenida();
                        bienvenida.ShowDialog();
                        Menu_Principal_GP2 Fmenu = new Menu_Principal_GP2();
                        Fmenu.Show();
                        return true;

                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña invalida", "Advertencia", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                        txtcontraseña.Clear();
                        txtcontraseña.UseSystemPasswordChar = true;
                        txtusuario.Clear();
                        txtusuario.Focus();
                        conlog.Close();
                        return false;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    txtcontraseña.Clear();
                    txtusuario.Clear();
                    txtusuario.Focus();
                    conlog.Close();
                    return false;
                }
                

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            login(txtusuario.Text,txtcontraseña.Text);
              
        }  
  


        private void LoginGP2_Load(object sender, EventArgs e)
        {
           // txtusuario.Focus();
          /*  btentrar.Enabled = false;

             if (txtusuario.Text != null && txtcontraseña.Text != null)
             {
                 btentrar.Enabled = true;
             }
            */
                }


        private void label1_Click(object sender, EventArgs e)
        {
        Application.Exit();
                                
        }

       
        private void LoginGP2_MouseDown(object sender, MouseEventArgs e)
        {
            
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btentrar_MouseMove(object sender, MouseEventArgs e)
        {
            btentrar.BackColor = Color.Teal;
            btentrar.ForeColor = Color.DarkGray;
        }

        private void Btcancelar_MouseMove(object sender, MouseEventArgs e)
        {
            Btcancelar.BackColor = Color.DarkRed;
            Btcancelar.ForeColor = Color.DimGray;
        }

        private void btentrar_MouseLeave(object sender, EventArgs e)
        {
            btentrar.BackColor = Color.DarkGray;
            btentrar.ForeColor = Color.Teal;
        }

        private void Btcancelar_MouseLeave(object sender, EventArgs e)
        {
            Btcancelar.BackColor = Color.DarkGray;
            Btcancelar.ForeColor = Color.DarkRed;
        }

        private void txtusuario_Enter(object sender, EventArgs e)
        {
            if (txtusuario.Text =="Usuario")
            {
                txtusuario.Text = "";
                txtusuario.ForeColor = Color.DimGray;
            }
        }

        private void txtusuario_Leave(object sender, EventArgs e)
        {
            if (txtusuario.Text =="")
            {
                txtusuario.Text = "Usuario";
                txtusuario.ForeColor = Color.Gray;
            }
        }

        private void txtcontraseña_Enter(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "Contraseña")
            {
                txtcontraseña.Text = "";
                txtcontraseña.ForeColor = Color.DimGray;
                txtcontraseña.UseSystemPasswordChar = true;

            }
        }

        private void txtcontraseña_Leave(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "")
            {
                txtcontraseña.Text = "Contraseña";
                txtcontraseña.ForeColor = Color.Gray;
                txtcontraseña.UseSystemPasswordChar = false;

            }
        }

        private void Btcancelar_Click(object sender, EventArgs e)
        {
            txtusuario.Clear();
            txtcontraseña.Clear();
        }
    }
}
