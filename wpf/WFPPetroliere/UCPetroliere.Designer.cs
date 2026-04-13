namespace WFPPetroliere
{
    partial class UCPetroliere
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReadFile = new System.Windows.Forms.Button();
            this.tbxDataRead = new System.Windows.Forms.TextBox();
            this.tbxResult = new System.Windows.Forms.TextBox();
            this.lblDataRead = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(3, 3);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 23);
            this.btnReadFile.TabIndex = 0;
            this.btnReadFile.Text = "Lecture fichier...";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.BtnReadFile_Click);
            // 
            // tbxDataRead
            // 
            this.tbxDataRead.AcceptsReturn = true;
            this.tbxDataRead.Enabled = false;
            this.tbxDataRead.Location = new System.Drawing.Point(3, 81);
            this.tbxDataRead.Multiline = true;
            this.tbxDataRead.Name = "tbxDataRead";
            this.tbxDataRead.Size = new System.Drawing.Size(236, 407);
            this.tbxDataRead.TabIndex = 1;
            // 
            // tbxResult
            // 
            this.tbxResult.AcceptsReturn = true;
            this.tbxResult.Enabled = false;
            this.tbxResult.Location = new System.Drawing.Point(260, 81);
            this.tbxResult.Multiline = true;
            this.tbxResult.Name = "tbxResult";
            this.tbxResult.Size = new System.Drawing.Size(247, 407);
            this.tbxResult.TabIndex = 2;
            // 
            // lblDataRead
            // 
            this.lblDataRead.AutoSize = true;
            this.lblDataRead.Location = new System.Drawing.Point(4, 62);
            this.lblDataRead.Name = "lblDataRead";
            this.lblDataRead.Size = new System.Drawing.Size(78, 13);
            this.lblDataRead.TabIndex = 3;
            this.lblDataRead.Text = "Données lues :";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(257, 62);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(52, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Résultat :";
            // 
            // UCPetroliere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblDataRead);
            this.Controls.Add(this.tbxResult);
            this.Controls.Add(this.tbxDataRead);
            this.Controls.Add(this.btnReadFile);
            this.Name = "UCPetroliere";
            this.Size = new System.Drawing.Size(510, 507);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.TextBox tbxDataRead;
        private System.Windows.Forms.TextBox tbxResult;
        private System.Windows.Forms.Label lblDataRead;
        private System.Windows.Forms.Label lblResult;
    }
}
