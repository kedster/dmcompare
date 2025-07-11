namespace DMSRuntimeComparer.UI
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtFolderA;
        private System.Windows.Forms.TextBox txtFolderB;
        private System.Windows.Forms.Button btnBrowseA;
        private System.Windows.Forms.Button btnBrowseB;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView gridA;
        private System.Windows.Forms.DataGridView gridB;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblFolderA;
        private System.Windows.Forms.Label lblFolderB;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.txtFolderA = new System.Windows.Forms.TextBox();
            this.txtFolderB = new System.Windows.Forms.TextBox();
            this.btnBrowseA = new System.Windows.Forms.Button();
            this.btnBrowseB = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.gridA = new System.Windows.Forms.DataGridView();
            this.gridB = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblFolderA = new System.Windows.Forms.Label();
            this.lblFolderB = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.gridA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridB)).BeginInit();
            this.SuspendLayout();

            // 
            // Mainform
            // 
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Text = "DMS Runtime Comparer";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 
            // lblFolderA
            // 
            this.lblFolderA.Text = "Folder A:";
            this.lblFolderA.Location = new System.Drawing.Point(20, 20);
            this.lblFolderA.Size = new System.Drawing.Size(60, 20);

            // 
            // txtFolderA
            // 
            this.txtFolderA.Location = new System.Drawing.Point(90, 18);
            this.txtFolderA.Size = new System.Drawing.Size(400, 22);

            // 
            // btnBrowseA
            // 
            this.btnBrowseA.Text = "Browse...";
            this.btnBrowseA.Location = new System.Drawing.Point(500, 16);
            this.btnBrowseA.Size = new System.Drawing.Size(80, 26);
            this.btnBrowseA.Click += new System.EventHandler(this.BtnBrowseA_Click);

            // 
            // lblFolderB
            // 
            this.lblFolderB.Text = "Folder B:";
            this.lblFolderB.Location = new System.Drawing.Point(20, 60);
            this.lblFolderB.Size = new System.Drawing.Size(60, 20);

            // 
            // txtFolderB
            // 
            this.txtFolderB.Location = new System.Drawing.Point(90, 58);
            this.txtFolderB.Size = new System.Drawing.Size(400, 22);

            // 
            // btnBrowseB
            // 
            this.btnBrowseB.Text = "Browse...";
            this.btnBrowseB.Location = new System.Drawing.Point(500, 56);
            this.btnBrowseB.Size = new System.Drawing.Size(80, 26);
            this.btnBrowseB.Click += new System.EventHandler(this.BtnBrowseB_Click);

            // 
            // btnRun
            // 
            this.btnRun.Text = "Run";
            this.btnRun.Location = new System.Drawing.Point(90, 100);
            this.btnRun.Size = new System.Drawing.Size(100, 30);
            this.btnRun.Click += new System.EventHandler(this.BtnRun_Click);

            // 
            // btnClear
            // 
            this.btnClear.Text = "Clear";
            this.btnClear.Location = new System.Drawing.Point(200, 100);
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);

            // 
            // btnExport
            // 
            this.btnExport.Text = "Export";
            this.btnExport.Location = new System.Drawing.Point(310, 100);
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            // 
            // gridA
            // 
            this.gridA.Location = new System.Drawing.Point(20, 150);
            this.gridA.Size = new System.Drawing.Size(540, 540);
            this.gridA.ReadOnly = true;
            this.gridA.AllowUserToAddRows = false;
            this.gridA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // gridB
            // 
            this.gridB.Location = new System.Drawing.Point(600, 150);
            this.gridB.Size = new System.Drawing.Size(540, 540);
            this.gridB.ReadOnly = true;
            this.gridB.AllowUserToAddRows = false;
            this.gridB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 710);
            this.progressBar.Size = new System.Drawing.Size(1120, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            // 
            // Add Controls
            // 
            this.Controls.Add(this.lblFolderA);
            this.Controls.Add(this.txtFolderA);
            this.Controls.Add(this.btnBrowseA);
            this.Controls.Add(this.lblFolderB);
            this.Controls.Add(this.txtFolderB);
            this.Controls.Add(this.btnBrowseB);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gridA);
            this.Controls.Add(this.gridB);
            this.Controls.Add(this.progressBar);

            ((System.ComponentModel.ISupportInitialize)(this.gridA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
