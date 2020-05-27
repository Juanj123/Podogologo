namespace ProyectoMovistar
{
    partial class CitasDia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Atendida = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Nombre_PAciente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hora_Atención = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Suspendida = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Atendida,
            this.Nombre_PAciente,
            this.Hora_Atención,
            this.Suspendida});
            this.dataGridView1.Location = new System.Drawing.Point(3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(445, 447);
            this.dataGridView1.TabIndex = 0;
            // 
            // Atendida
            // 
            this.Atendida.HeaderText = "Atendida";
            this.Atendida.Name = "Atendida";
            this.Atendida.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Nombre_PAciente
            // 
            this.Nombre_PAciente.HeaderText = "Nombre Paciente";
            this.Nombre_PAciente.Name = "Nombre_PAciente";
            this.Nombre_PAciente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Nombre_PAciente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Hora_Atención
            // 
            this.Hora_Atención.HeaderText = "Hora_Atención";
            this.Hora_Atención.Name = "Hora_Atención";
            this.Hora_Atención.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Hora_Atención.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Suspendida
            // 
            this.Suspendida.HeaderText = "Suspendida";
            this.Suspendida.Name = "Suspendida";
            this.Suspendida.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Suspendida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Suspendida.Text = "Suspendida";
            // 
            // CitasDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CitasDia";
            this.Text = "CitasDia";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Atendida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_PAciente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hora_Atención;
        private System.Windows.Forms.DataGridViewButtonColumn Suspendida;
    }
}