namespace WFPAsyncAwait
{
    partial class FrmAsynAwait
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
            this.ucAsync1 = new WFPAsyncAwait.UCAsync();
            this.SuspendLayout();
            // 
            // ucAsync1
            // 
            this.ucAsync1.Location = new System.Drawing.Point(102, 12);
            this.ucAsync1.Name = "ucAsync1";
            this.ucAsync1.Size = new System.Drawing.Size(627, 448);
            this.ucAsync1.TabIndex = 0;
            // 
            // FrmAsynAwait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ucAsync1);
            this.Name = "FrmAsynAwait";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UCAsync ucAsync1;
    }
}

