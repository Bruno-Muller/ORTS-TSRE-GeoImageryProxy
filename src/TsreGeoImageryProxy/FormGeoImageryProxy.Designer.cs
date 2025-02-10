
namespace GeoImageryProxy
{
    partial class FormGeoImageryProxy
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxProvider = new System.Windows.Forms.ComboBox();
            this.labelProvider = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxProvider
            // 
            this.comboBoxProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProvider.FormattingEnabled = true;
            this.comboBoxProvider.Items.AddRange(new object[] {
            "Bing",
            "Google Map",
            "IGN",
            "IGN 1950"});
            this.comboBoxProvider.Location = new System.Drawing.Point(83, 6);
            this.comboBoxProvider.Name = "comboBoxProvider";
            this.comboBoxProvider.Size = new System.Drawing.Size(221, 21);
            this.comboBoxProvider.TabIndex = 1;
            this.comboBoxProvider.SelectedIndexChanged += new System.EventHandler(this.OnComboBoxProviderChanged);
            // 
            // labelProvider
            // 
            this.labelProvider.AutoSize = true;
            this.labelProvider.Location = new System.Drawing.Point(12, 9);
            this.labelProvider.Name = "labelProvider";
            this.labelProvider.Size = new System.Drawing.Size(46, 13);
            this.labelProvider.TabIndex = 2;
            this.labelProvider.Text = "Provider";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 33);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(505, 212);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            this.richTextBoxLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.OnLinkClicked);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 248);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(529, 22);
            this.statusStrip.TabIndex = 12;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.IsLink = true;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(151, 17);
            this.toolStripStatusLabel.Text = global::GeoImageryProxy.Properties.Settings.Default.ImageryFolder;
            this.toolStripStatusLabel.Click += new System.EventHandler(this.OnToolStripStatusLabelClicked);
            // 
            // FormGeoImageryProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 270);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.labelProvider);
            this.Controls.Add(this.comboBoxProvider);
            this.Controls.Add(this.richTextBoxLog);
            this.Name = "FormGeoImageryProxy";
            this.Text = "GeoImageryProxy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxProvider;
        private System.Windows.Forms.Label labelProvider;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}

