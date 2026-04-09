namespace Proyecto.Controles
{
    partial class ClientesControl3
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.Btnguardar = new System.Windows.Forms.Button();
            this.txtGmail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGmail1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new System.Drawing.Point(141, 61);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(217, 22);
            this.txtIdCliente.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 24);
            this.label3.TabIndex = 52;
            this.label3.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 24);
            this.label2.TabIndex = 51;
            this.label2.Text = "Gmail:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(141, 167);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(217, 22);
            this.txtTelefono.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 24);
            this.label5.TabIndex = 48;
            this.label5.Text = "Tipo archivo:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(141, 133);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(217, 22);
            this.txtApellido.TabIndex = 47;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(141, 99);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(217, 22);
            this.txtNombre.TabIndex = 46;
            // 
            // Btnguardar
            // 
            this.Btnguardar.BackColor = System.Drawing.Color.Lime;
            this.Btnguardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnguardar.Location = new System.Drawing.Point(9, 262);
            this.Btnguardar.Name = "Btnguardar";
            this.Btnguardar.Size = new System.Drawing.Size(380, 39);
            this.Btnguardar.TabIndex = 45;
            this.Btnguardar.Text = "Actualizar documento";
            this.Btnguardar.UseVisualStyleBackColor = false;
            // 
            // txtGmail
            // 
            this.txtGmail.AutoSize = true;
            this.txtGmail.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGmail.Location = new System.Drawing.Point(20, 167);
            this.txtGmail.Name = "txtGmail";
            this.txtGmail.Size = new System.Drawing.Size(49, 24);
            this.txtGmail.TabIndex = 44;
            this.txtGmail.Text = "URL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 43;
            this.label1.Text = "Nombre:";
            // 
            // txtGmail1
            // 
            this.txtGmail1.Location = new System.Drawing.Point(147, 217);
            this.txtGmail1.Name = "txtGmail1";
            this.txtGmail1.Size = new System.Drawing.Size(211, 22);
            this.txtGmail1.TabIndex = 54;
            // 
            // ClientesControl3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtGmail1);
            this.Controls.Add(this.txtIdCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.Btnguardar);
            this.Controls.Add(this.txtGmail);
            this.Controls.Add(this.label1);
            this.Name = "ClientesControl3";
            this.Size = new System.Drawing.Size(399, 363);
            this.Load += new System.EventHandler(this.ClientesControl3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button Btnguardar;
        private System.Windows.Forms.Label txtGmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGmail1;
    }
}
