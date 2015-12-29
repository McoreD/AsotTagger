namespace AsotTagger
{
    partial class Form1
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
            this.panelFileNames = new System.Windows.Forms.Panel();
            this.pgTrack = new System.Windows.Forms.PropertyGrid();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.btnDownloadCue = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.lblFileNames = new System.Windows.Forms.TextBox();
            this.panelFileNames.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFileNames
            // 
            this.panelFileNames.AutoScroll = true;
            this.panelFileNames.Controls.Add(this.pgTrack);
            this.panelFileNames.Controls.Add(this.lbFiles);
            this.panelFileNames.Controls.Add(this.btnDownloadCue);
            this.panelFileNames.Controls.Add(this.btnCopy);
            this.panelFileNames.Controls.Add(this.txtDest);
            this.panelFileNames.Controls.Add(this.lblFileNames);
            this.panelFileNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileNames.Location = new System.Drawing.Point(0, 0);
            this.panelFileNames.MinimumSize = new System.Drawing.Size(642, 510);
            this.panelFileNames.Name = "panelFileNames";
            this.panelFileNames.Size = new System.Drawing.Size(1184, 510);
            this.panelFileNames.TabIndex = 0;
            // 
            // pgTrack
            // 
            this.pgTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTrack.HelpVisible = false;
            this.pgTrack.Location = new System.Drawing.Point(16, 232);
            this.pgTrack.Name = "pgTrack";
            this.pgTrack.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgTrack.Size = new System.Drawing.Size(1150, 192);
            this.pgTrack.TabIndex = 2;
            this.pgTrack.ToolbarVisible = false;
            // 
            // lbFiles
            // 
            this.lbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(16, 112);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(1150, 108);
            this.lbFiles.TabIndex = 1;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // btnDownloadCue
            // 
            this.btnDownloadCue.Location = new System.Drawing.Point(1032, 464);
            this.btnDownloadCue.Name = "btnDownloadCue";
            this.btnDownloadCue.Size = new System.Drawing.Size(136, 22);
            this.btnDownloadCue.TabIndex = 5;
            this.btnDownloadCue.Text = "&Download cue files";
            this.btnDownloadCue.UseVisualStyleBackColor = true;
            this.btnDownloadCue.Click += new System.EventHandler(this.btnDownloadCue_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(1032, 432);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(136, 23);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "Add to &Library";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtDest
            // 
            this.txtDest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtDest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtDest.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AsotTagger.Properties.Settings.Default, "DestFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDest.Location = new System.Drawing.Point(16, 432);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(1008, 20);
            this.txtDest.TabIndex = 3;
            this.txtDest.Text = global::AsotTagger.Properties.Settings.Default.DestFileName;
            // 
            // lblFileNames
            // 
            this.lblFileNames.Location = new System.Drawing.Point(16, 16);
            this.lblFileNames.Multiline = true;
            this.lblFileNames.Name = "lblFileNames";
            this.lblFileNames.ReadOnly = true;
            this.lblFileNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblFileNames.Size = new System.Drawing.Size(1152, 88);
            this.lblFileNames.TabIndex = 0;
            this.lblFileNames.TabStop = false;
            this.lblFileNames.Text = "Supported file name patterns";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnDownloadCue;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 502);
            this.Controls.Add(this.panelFileNames);
            this.MinimumSize = new System.Drawing.Size(1200, 540);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asot Tagger - Drag n Drop files that match any of the following patterns";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.panelFileNames.ResumeLayout(false);
            this.panelFileNames.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFileNames;
        private System.Windows.Forms.TextBox lblFileNames;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnDownloadCue;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.PropertyGrid pgTrack;


    }
}

