namespace Proyecto.Controles
{
    partial class UserControl2
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
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Presupuestotxt = new System.Windows.Forms.TextBox();
            this.Nombretxt = new System.Windows.Forms.TextBox();
            this.Btnguardar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGmail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Descripciontxt = new System.Windows.Forms.RichTextBox();
            this.Estadotxt = new System.Windows.Forms.TextBox();
            this.Fecha1txt = new System.Windows.Forms.DateTimePicker();
            this.Fecha2txt = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(149, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Editar proyecto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 24);
            this.label5.TabIndex = 20;
            this.label5.Text = "Fecha_fin:";
            // 
            // Presupuestotxt
            // 
            this.Presupuestotxt.Location = new System.Drawing.Point(152, 85);
            this.Presupuestotxt.Name = "Presupuestotxt";
            this.Presupuestotxt.Size = new System.Drawing.Size(145, 22);
            this.Presupuestotxt.TabIndex = 17;
            // 
            // Nombretxt
            // 
            this.Nombretxt.Location = new System.Drawing.Point(116, 47);
            this.Nombretxt.Name = "Nombretxt";
            this.Nombretxt.Size = new System.Drawing.Size(100, 22);
            this.Nombretxt.TabIndex = 16;
            // 
            // Btnguardar
            // 
            this.Btnguardar.BackColor = System.Drawing.Color.Lime;
            this.Btnguardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnguardar.Location = new System.Drawing.Point(0, 273);
            this.Btnguardar.Name = "Btnguardar";
            this.Btnguardar.Size = new System.Drawing.Size(369, 39);
            this.Btnguardar.TabIndex = 15;
            this.Btnguardar.Text = "Agregar";
            this.Btnguardar.UseVisualStyleBackColor = false;
            this.Btnguardar.Click += new System.EventHandler(this.Btnguardar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Descripción:";
            // 
            // txtGmail
            // 
            this.txtGmail.AutoSize = true;
            this.txtGmail.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGmail.Location = new System.Drawing.Point(31, 147);
            this.txtGmail.Name = "txtGmail";
            this.txtGmail.Size = new System.Drawing.Size(112, 24);
            this.txtGmail.TabIndex = 13;
            this.txtGmail.Text = "Fecha_inicio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nombre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "Presupuesto:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 24);
            this.label6.TabIndex = 23;
            this.label6.Text = "Estado:";
            // 
            // Descripciontxt
            // 
            this.Descripciontxt.Location = new System.Drawing.Point(116, 224);
            this.Descripciontxt.Name = "Descripciontxt";
            this.Descripciontxt.Size = new System.Drawing.Size(253, 43);
            this.Descripciontxt.TabIndex = 24;
            this.Descripciontxt.Text = "";
            // 
            // Estadotxt
            // 
            this.Estadotxt.Location = new System.Drawing.Point(106, 186);
            this.Estadotxt.Name = "Estadotxt";
            this.Estadotxt.Size = new System.Drawing.Size(100, 22);
            this.Estadotxt.TabIndex = 25;
            // 
            // Fecha1txt
            // 
            this.Fecha1txt.Location = new System.Drawing.Point(127, 122);
            this.Fecha1txt.Name = "Fecha1txt";
            this.Fecha1txt.Size = new System.Drawing.Size(200, 22);
            this.Fecha1txt.TabIndex = 26;
            // 
            // Fecha2txt
            // 
            this.Fecha2txt.Location = new System.Drawing.Point(141, 150);
            this.Fecha2txt.Name = "Fecha2txt";
            this.Fecha2txt.Size = new System.Drawing.Size(200, 22);
            this.Fecha2txt.TabIndex = 27;
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.Fecha2txt);
            this.Controls.Add(this.Fecha1txt);
            this.Controls.Add(this.Estadotxt);
            this.Controls.Add(this.Descripciontxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Presupuestotxt);
            this.Controls.Add(this.Nombretxt);
            this.Controls.Add(this.Btnguardar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGmail);
            this.Controls.Add(this.label1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(399, 363);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Presupuestotxt;
        private System.Windows.Forms.TextBox Nombretxt;
        private System.Windows.Forms.Button Btnguardar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtGmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox Descripciontxt;
        private System.Windows.Forms.TextBox Estadotxt;
        private System.Windows.Forms.DateTimePicker Fecha1txt;
        private System.Windows.Forms.DateTimePicker Fecha2txt;
    }
}
