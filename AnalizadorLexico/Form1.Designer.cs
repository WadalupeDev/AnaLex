namespace AnalizadorLexico
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EntradaFilaNum = new System.Windows.Forms.TextBox();
            this.Entrada = new System.Windows.Forms.RichTextBox();
            this.Herramientas = new System.Windows.Forms.ToolStrip();
            this.NuevoX = new System.Windows.Forms.ToolStripButton();
            this.CargarX = new System.Windows.Forms.ToolStripButton();
            this.GuardarX = new System.Windows.Forms.ToolStripButton();
            this.GuardarComoX = new System.Windows.Forms.ToolStripButton();
            this.CompilarX = new System.Windows.Forms.ToolStripButton();
            this.Nuevo = new System.Windows.Forms.ToolStripButton();
            this.Cargar = new System.Windows.Forms.ToolStripButton();
            this.Guardar = new System.Windows.Forms.ToolStripButton();
            this.GuardarComo = new System.Windows.Forms.ToolStripButton();
            this.Compilar = new System.Windows.Forms.ToolStripButton();
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ConsolaUnused = new System.Windows.Forms.RichTextBox();
            this.Tabla = new System.Windows.Forms.DataGridView();
            this.Componente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lexema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Herramientas.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // EntradaFilaNum
            // 
            resources.ApplyResources(this.EntradaFilaNum, "EntradaFilaNum");
            this.EntradaFilaNum.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.EntradaFilaNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EntradaFilaNum.ForeColor = System.Drawing.Color.White;
            this.EntradaFilaNum.Name = "EntradaFilaNum";
            this.tableLayoutPanel2.SetRowSpan(this.EntradaFilaNum, 2);
            this.EntradaFilaNum.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Entrada
            // 
            resources.ApplyResources(this.Entrada, "Entrada");
            this.Entrada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Entrada.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Entrada.ForeColor = System.Drawing.Color.White;
            this.Entrada.Name = "Entrada";
            this.tableLayoutPanel2.SetRowSpan(this.Entrada, 2);
            this.Entrada.VScroll += new System.EventHandler(this.Entrada_VScroll);
            this.Entrada.TextChanged += new System.EventHandler(this.Entrada_TextChanged);
            this.Entrada.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Entrada_MouseDown);
            // 
            // Herramientas
            // 
            resources.ApplyResources(this.Herramientas, "Herramientas");
            this.MainPanel.SetColumnSpan(this.Herramientas, 2);
            this.Herramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NuevoX,
            this.CargarX,
            this.GuardarX,
            this.GuardarComoX,
            this.CompilarX});
            this.Herramientas.Name = "Herramientas";
            // 
            // NuevoX
            // 
            resources.ApplyResources(this.NuevoX, "NuevoX");
            this.NuevoX.Name = "NuevoX";
            this.NuevoX.Click += new System.EventHandler(this.NuevoX_Click);
            // 
            // CargarX
            // 
            resources.ApplyResources(this.CargarX, "CargarX");
            this.CargarX.Name = "CargarX";
            this.CargarX.Click += new System.EventHandler(this.CargarX_Click);
            // 
            // GuardarX
            // 
            resources.ApplyResources(this.GuardarX, "GuardarX");
            this.GuardarX.Name = "GuardarX";
            this.GuardarX.Click += new System.EventHandler(this.GuardarX_Click);
            // 
            // GuardarComoX
            // 
            resources.ApplyResources(this.GuardarComoX, "GuardarComoX");
            this.GuardarComoX.Name = "GuardarComoX";
            this.GuardarComoX.Click += new System.EventHandler(this.GuardarComoX_Click);
            // 
            // CompilarX
            // 
            resources.ApplyResources(this.CompilarX, "CompilarX");
            this.CompilarX.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CompilarX.Name = "CompilarX";
            this.CompilarX.Click += new System.EventHandler(this.CompilarX_Click);
            // 
            // Nuevo
            // 
            resources.ApplyResources(this.Nuevo, "Nuevo");
            this.Nuevo.Name = "Nuevo";
            this.Nuevo.Click += new System.EventHandler(this.Nuevo_Click);
            // 
            // Cargar
            // 
            resources.ApplyResources(this.Cargar, "Cargar");
            this.Cargar.Name = "Cargar";
            this.Cargar.Click += new System.EventHandler(this.Cargar_Click);
            // 
            // Guardar
            // 
            resources.ApplyResources(this.Guardar, "Guardar");
            this.Guardar.Name = "Guardar";
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // GuardarComo
            // 
            resources.ApplyResources(this.GuardarComo, "GuardarComo");
            this.GuardarComo.Name = "GuardarComo";
            this.GuardarComo.Click += new System.EventHandler(this.GuardarComo_Click);
            // 
            // Compilar
            // 
            resources.ApplyResources(this.Compilar, "Compilar");
            this.Compilar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Compilar.Name = "Compilar";
            this.Compilar.Click += new System.EventHandler(this.Compilar_Click);
            // 
            // MainPanel
            // 
            resources.ApplyResources(this.MainPanel, "MainPanel");
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MainPanel.Controls.Add(this.panel1, 0, 3);
            this.MainPanel.Controls.Add(this.Herramientas, 0, 1);
            this.MainPanel.Controls.Add(this.Tabla, 1, 2);
            this.MainPanel.Controls.Add(this.statusStrip1, 0, 4);
            this.MainPanel.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.ConsolaUnused);
            this.panel1.Name = "panel1";
            // 
            // ConsolaUnused
            // 
            resources.ApplyResources(this.ConsolaUnused, "ConsolaUnused");
            this.ConsolaUnused.BackColor = System.Drawing.Color.Black;
            this.ConsolaUnused.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsolaUnused.ForeColor = System.Drawing.Color.White;
            this.ConsolaUnused.Name = "ConsolaUnused";
            // 
            // Tabla
            // 
            resources.ApplyResources(this.Tabla, "Tabla");
            this.Tabla.AllowUserToAddRows = false;
            this.Tabla.AllowUserToDeleteRows = false;
            this.Tabla.AllowUserToResizeColumns = false;
            this.Tabla.AllowUserToResizeRows = false;
            this.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Componente,
            this.Lexema,
            this.Linea});
            this.Tabla.Name = "Tabla";
            this.Tabla.ReadOnly = true;
            this.MainPanel.SetRowSpan(this.Tabla, 2);
            this.Tabla.RowTemplate.Height = 25;
            // 
            // Componente
            // 
            resources.ApplyResources(this.Componente, "Componente");
            this.Componente.Name = "Componente";
            this.Componente.ReadOnly = true;
            // 
            // Lexema
            // 
            resources.ApplyResources(this.Lexema, "Lexema");
            this.Lexema.Name = "Lexema";
            this.Lexema.ReadOnly = true;
            // 
            // Linea
            // 
            resources.ApplyResources(this.Linea, "Linea");
            this.Linea.Name = "Linea";
            this.Linea.ReadOnly = true;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.MainPanel.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Name = "statusStrip1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.Entrada, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.EntradaFilaNum, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Herramientas.ResumeLayout(false);
            this.Herramientas.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStrip Herramientas;
        private ToolStripButton Nuevo;
        private ToolStripButton Cargar;
        private TableLayoutPanel MainPanel;
        private DataGridView Tabla;
        private DataGridViewTextBoxColumn Componente;
        private DataGridViewTextBoxColumn Lexema;
        private DataGridViewTextBoxColumn Linea;
        private ToolStripButton Guardar;
        private ToolStripButton GuardarComo;
        private ToolStripButton Compilar;
        private RichTextBox Entrada;
        private Panel panel1;
        private StatusStrip statusStrip1;
        private RichTextBox ConsolaUnused;
        private TextBox EntradaFilaNum;
        private TableLayoutPanel tableLayoutPanel2;
        private ToolStripButton NuevoX;
        private ToolStripButton CargarX;
        private ToolStripButton GuardarX;
        private ToolStripButton GuardarComoX;
        private ToolStripButton CompilarX;
    }
}