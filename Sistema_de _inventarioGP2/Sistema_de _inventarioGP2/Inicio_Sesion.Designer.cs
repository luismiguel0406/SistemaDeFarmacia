﻿namespace Sistema_de__inventarioGP2
{
    partial class LoginGP2
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btentrar = new System.Windows.Forms.Button();
            this.Btcancelar = new System.Windows.Forms.Button();
            this.txtusuario = new System.Windows.Forms.TextBox();
            this.txtcontraseña = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btentrar
            // 
            this.btentrar.BackColor = System.Drawing.Color.DarkGray;
            this.btentrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btentrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btentrar.ForeColor = System.Drawing.Color.Teal;
            this.btentrar.Location = new System.Drawing.Point(133, 306);
            this.btentrar.Name = "btentrar";
            this.btentrar.Size = new System.Drawing.Size(138, 35);
            this.btentrar.TabIndex = 3;
            this.btentrar.Text = "Entrar";
            this.btentrar.UseVisualStyleBackColor = false;
            this.btentrar.Click += new System.EventHandler(this.button1_Click);
            this.btentrar.MouseLeave += new System.EventHandler(this.btentrar_MouseLeave);
            this.btentrar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btentrar_MouseMove);
            // 
            // Btcancelar
            // 
            this.Btcancelar.BackColor = System.Drawing.Color.DarkGray;
            this.Btcancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btcancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btcancelar.ForeColor = System.Drawing.Color.DarkRed;
            this.Btcancelar.Location = new System.Drawing.Point(133, 363);
            this.Btcancelar.Name = "Btcancelar";
            this.Btcancelar.Size = new System.Drawing.Size(138, 35);
            this.Btcancelar.TabIndex = 4;
            this.Btcancelar.Text = "Cancelar";
            this.Btcancelar.UseVisualStyleBackColor = false;
            this.Btcancelar.Click += new System.EventHandler(this.Btcancelar_Click);
            this.Btcancelar.MouseLeave += new System.EventHandler(this.Btcancelar_MouseLeave);
            this.Btcancelar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btcancelar_MouseMove);
            // 
            // txtusuario
            // 
            this.txtusuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtusuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusuario.ForeColor = System.Drawing.Color.Gray;
            this.txtusuario.Location = new System.Drawing.Point(112, 160);
            this.txtusuario.Name = "txtusuario";
            this.txtusuario.Size = new System.Drawing.Size(181, 19);
            this.txtusuario.TabIndex = 1;
            this.txtusuario.Text = "Usuario";
            this.txtusuario.Enter += new System.EventHandler(this.txtusuario_Enter);
            this.txtusuario.Leave += new System.EventHandler(this.txtusuario_Leave);
            // 
            // txtcontraseña
            // 
            this.txtcontraseña.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcontraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontraseña.ForeColor = System.Drawing.Color.Gray;
            this.txtcontraseña.Location = new System.Drawing.Point(112, 219);
            this.txtcontraseña.Name = "txtcontraseña";
            this.txtcontraseña.Size = new System.Drawing.Size(181, 19);
            this.txtcontraseña.TabIndex = 2;
            this.txtcontraseña.Text = "Contraseña";
            this.txtcontraseña.Enter += new System.EventHandler(this.txtcontraseña_Enter);
            this.txtcontraseña.Leave += new System.EventHandler(this.txtcontraseña_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Teal;
            this.label4.Location = new System.Drawing.Point(154, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Inicie sesion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 440);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sistema de Inventario Farmaceutico";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Sistema_de__inventarioGP2.Properties.Resources.medicine_1_;
            this.pictureBox1.Location = new System.Drawing.Point(182, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Sistema_de__inventarioGP2.Properties.Resources.bloquear;
            this.pictureBox4.Location = new System.Drawing.Point(48, 207);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(40, 41);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Sistema_de__inventarioGP2.Properties.Resources.usuario;
            this.pictureBox2.Location = new System.Drawing.Point(48, 149);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Sistema_de__inventarioGP2.Properties.Resources.cancel;
            this.pictureBox3.Location = new System.Drawing.Point(380, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 17;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // LoginGP2
            // 
            this.AcceptButton = this.btentrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(404, 462);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtcontraseña);
            this.Controls.Add(this.txtusuario);
            this.Controls.Add(this.Btcancelar);
            this.Controls.Add(this.btentrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginGP2";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.LoginGP2_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginGP2_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btentrar;
        private System.Windows.Forms.Button Btcancelar;
        private System.Windows.Forms.TextBox txtusuario;
        private System.Windows.Forms.TextBox txtcontraseña;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

