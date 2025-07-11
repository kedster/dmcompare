using System;
using System.Windows.Forms;

namespace DMSRuntimeComparer.UI
{
    public class ComparisonView : UserControl
    {
        private Label lblTitle;
        private DataGridView gridLeft;
        private DataGridView gridRight;

        public DataGridView LeftGrid => gridLeft;
        public DataGridView RightGrid => gridRight;

        public ComparisonView(string title = "Comparison View")
        {
            InitializeComponent();
            lblTitle.Text = title;
            SyncScroll(gridLeft, gridRight);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.gridLeft = new DataGridView();
            this.gridRight = new DataGridView();

            // Label
            this.lblTitle.Text = "Comparison View";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);

            // Grid A
            this.gridLeft.Location = new System.Drawing.Point(10, 40);
            this.gridLeft.Size = new System.Drawing.Size(500, 400);
            this.gridLeft.ReadOnly = true;
            this.gridLeft.AllowUserToAddRows = false;
            this.gridLeft.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Grid B
            this.gridRight.Location = new System.Drawing.Point(520, 40);
            this.gridRight.Size = new System.Drawing.Size(500, 400);
            this.gridRight.ReadOnly = true;
            this.gridRight.AllowUserToAddRows = false;
            this.gridRight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Sync scroll will be handled in constructor

            // Add controls to UserControl
            this.Controls.Add(lblTitle);
            this.Controls.Add(gridLeft);
            this.Controls.Add(gridRight);

            // Resize this control
            this.Size = new System.Drawing.Size(1040, 460);
        }

        private void SyncScroll(DataGridView left, DataGridView right)
        {
            left.Scroll += (s, e) =>
            {
                try { right.FirstDisplayedScrollingRowIndex = left.FirstDisplayedScrollingRowIndex; } catch { }
            };
            right.Scroll += (s, e) =>
            {
                try { left.FirstDisplayedScrollingRowIndex = right.FirstDisplayedScrollingRowIndex; } catch { }
            };
        }
    }
}
