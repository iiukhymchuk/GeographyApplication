namespace Core
{
    partial class GeographyForm
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.plusButton = new System.Windows.Forms.Button();
            this.minusButton = new System.Windows.Forms.Button();
            this.roadMap = new System.Windows.Forms.RadioButton();
            this.satellite = new System.Windows.Forms.RadioButton();
            this.terrain = new System.Windows.Forms.RadioButton();
            this.radioButtonsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.radioButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1084, 661);
            this.webBrowser.TabIndex = 4;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(12, 23);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(222, 20);
            this.searchTextBox.TabIndex = 5;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(285, 23);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 580);
            this.pictureBox.TabIndex = 6;
            this.pictureBox.TabStop = false;
            // 
            // plusButton
            // 
            this.plusButton.Location = new System.Drawing.Point(891, 542);
            this.plusButton.Name = "plusButton";
            this.plusButton.Size = new System.Drawing.Size(20, 23);
            this.plusButton.TabIndex = 7;
            this.plusButton.Text = "+";
            this.plusButton.UseVisualStyleBackColor = true;
            this.plusButton.Click += new System.EventHandler(this.PlusButton_Click);
            // 
            // minusButton
            // 
            this.minusButton.Location = new System.Drawing.Point(891, 571);
            this.minusButton.Name = "minusButton";
            this.minusButton.Size = new System.Drawing.Size(20, 23);
            this.minusButton.TabIndex = 8;
            this.minusButton.Text = "-";
            this.minusButton.UseVisualStyleBackColor = true;
            this.minusButton.Click += new System.EventHandler(this.MinusButton_Click);
            // 
            // roadMap
            // 
            this.roadMap.AutoSize = true;
            this.roadMap.Location = new System.Drawing.Point(3, 13);
            this.roadMap.Name = "roadMap";
            this.roadMap.Size = new System.Drawing.Size(75, 17);
            this.roadMap.TabIndex = 9;
            this.roadMap.TabStop = true;
            this.roadMap.Text = "Road Map";
            this.roadMap.UseVisualStyleBackColor = true;
            this.roadMap.CheckedChanged += new System.EventHandler(this.RoadMap_CheckedChanged);
            // 
            // satellite
            // 
            this.satellite.AutoSize = true;
            this.satellite.Location = new System.Drawing.Point(3, 36);
            this.satellite.Name = "satellite";
            this.satellite.Size = new System.Drawing.Size(62, 17);
            this.satellite.TabIndex = 10;
            this.satellite.TabStop = true;
            this.satellite.Text = "Satellite";
            this.satellite.UseVisualStyleBackColor = true;
            this.satellite.CheckedChanged += new System.EventHandler(this.Satellite_CheckedChanged);
            // 
            // terrain
            // 
            this.terrain.AutoSize = true;
            this.terrain.Location = new System.Drawing.Point(3, 59);
            this.terrain.Name = "terrain";
            this.terrain.Size = new System.Drawing.Size(58, 17);
            this.terrain.TabIndex = 11;
            this.terrain.TabStop = true;
            this.terrain.Text = "Terrain";
            this.terrain.UseVisualStyleBackColor = true;
            this.terrain.CheckedChanged += new System.EventHandler(this.Terrain_CheckedChanged);
            // 
            // radioButtonsPanel
            // 
            this.radioButtonsPanel.Controls.Add(this.roadMap);
            this.radioButtonsPanel.Controls.Add(this.terrain);
            this.radioButtonsPanel.Controls.Add(this.satellite);
            this.radioButtonsPanel.Location = new System.Drawing.Point(891, 335);
            this.radioButtonsPanel.Name = "radioButtonsPanel";
            this.radioButtonsPanel.Size = new System.Drawing.Size(91, 87);
            this.radioButtonsPanel.TabIndex = 12;
            // 
            // GeographyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.radioButtonsPanel);
            this.Controls.Add(this.minusButton);
            this.Controls.Add(this.plusButton);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.webBrowser);
            this.Name = "GeographyForm";
            this.Text = "Map";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.radioButtonsPanel.ResumeLayout(false);
            this.radioButtonsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button plusButton;
        private System.Windows.Forms.Button minusButton;
        private System.Windows.Forms.RadioButton roadMap;
        private System.Windows.Forms.RadioButton satellite;
        private System.Windows.Forms.RadioButton terrain;
        private System.Windows.Forms.Panel radioButtonsPanel;
    }
}

