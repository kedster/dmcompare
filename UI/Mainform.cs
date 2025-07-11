using System;
using System.Windows.Forms;
using DMSRuntimeComparer.Models;
using DMSRuntimeComparer.Services;

namespace DMSRuntimeComparer.UI
{
    public partial class Mainform : Form
    {
        private TextBox txtFolderA;
        private TextBox txtFolderB;
        private DataGridView gridA;
        private DataGridView gridB;
        private ProgressBar progressBar;

        private FolderComparer folderComparer;
        private ReportGenerator reportGenerator;

        public Mainform()
        {
            folderComparer = new FolderComparer(new MetadataParser());
            reportGenerator = new ReportGenerator();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "DMS Runtime Comparer";
            this.Width = 1200;
            this.Height = 800;

            // Folder A controls
            var lblFolderA = new Label { Text = "Folder A:", Top = 20, Left = 20, Width = 60 };
            txtFolderA = new TextBox { Top = 18, Left = 90, Width = 400 };
            var btnBrowseA = new Button { Text = "Browse...", Top = 16, Left = 500, Width = 80 };
            btnBrowseA.Click += (s, e) => BrowseFolder(txtFolderA);

            // Folder B controls
            var lblFolderB = new Label { Text = "Folder B:", Top = 60, Left = 20, Width = 60 };
            txtFolderB = new TextBox { Top = 58, Left = 90, Width = 400 };
            var btnBrowseB = new Button { Text = "Browse...", Top = 56, Left = 500, Width = 80 };
            btnBrowseB.Click += (s, e) => BrowseFolder(txtFolderB);

            // Run, Clear, Export buttons
            var btnRun = new Button { Text = "Run", Top = 100, Left = 90, Width = 100 };
            var btnClear = new Button { Text = "Clear", Top = 100, Left = 200, Width = 100 };
            var btnExport = new Button { Text = "Export", Top = 100, Left = 310, Width = 100 };

            btnRun.Click += (s, e) => RunComparison();
            btnClear.Click += (s, e) => ClearResults();
            btnExport.Click += (s, e) => ExportResults();

            // DataGrids
            gridA = new DataGridView
            {
                Top = 150,
                Left = 20,
                Width = 550,
                Height = 550,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            gridB = new DataGridView
            {
                Top = 150,
                Left = 600,
                Width = 550,
                Height = 550,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            SyncScroll(gridA, gridB);

            // Progress Bar
            progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Continuous,
                Left = 20,
                Top = 720,
                Width = 1130,
                Height = 20
            };

            // Add all controls
            this.Controls.AddRange(new Control[] {
                lblFolderA, txtFolderA, btnBrowseA,
                lblFolderB, txtFolderB, btnBrowseB,
                btnRun, btnClear, btnExport,
                gridA, gridB,
                progressBar
            });
        }

        private void BrowseFolder(TextBox targetTextBox)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    targetTextBox.Text = dialog.SelectedPath;
            }
        }

        private void RunComparison()
        {
            string pathA = txtFolderA.Text;
            string pathB = txtFolderB.Text;

            if (string.IsNullOrWhiteSpace(pathA) || string.IsNullOrWhiteSpace(pathB))
            {
                MessageBox.Show("Please select both folders.");
                return;
            }

            progressBar.Value = 0;
            gridA.Rows.Clear();
            gridB.Rows.Clear();

            try
            {
                // Build folder trees
                FolderNode treeA = folderComparer.BuildFolderTree(pathA);
                FolderNode treeB = folderComparer.BuildFolderTree(pathB);

                // Compare folders
                var results = folderComparer.Compare(treeA, treeB);

                // Populate DataGrids
                foreach (var result in results)
                {
                    gridA.Rows.Add(result.FileAPath, result.FileAMetadata?.Size, result.FileAMetadata?.LastModified, result.FileAMetadata?.Checksum);
                    gridB.Rows.Add(result.FileBPath, result.FileBMetadata?.Size, result.FileBMetadata?.LastModified, result.FileBMetadata?.Checksum);
                }

                progressBar.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during comparison: " + ex.Message);
            }
        }

        private void ClearResults()
        {
            gridA.Rows.Clear();
            gridB.Rows.Clear();
            progressBar.Value = 0;
        }

        private void ExportResults()
        {
            using (var dialog = new SaveFileDialog { Filter = "CSV Files (*.csv)|*.csv" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Assuming you keep the last comparison results in a field
                    var results = folderComparer.LastResults;
                    reportGenerator.GenerateCsv(dialog.FileName, results);
                    MessageBox.Show("Exported successfully.");
                }
            }
        }

        private void SyncScroll(DataGridView grid1, DataGridView grid2)
        {
            grid1.Scroll += (s, e) => grid2.FirstDisplayedScrollingRowIndex = grid1.FirstDisplayedScrollingRowIndex;
            grid2.Scroll += (s, e) => grid1.FirstDisplayedScrollingRowIndex = grid2.FirstDisplayedScrollingRowIndex;
        }
    }
}
