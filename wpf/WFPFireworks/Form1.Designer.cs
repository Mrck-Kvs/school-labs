namespace WFPFireworks
{
    partial class Form1
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
            this.screen1 = new WFPFireworks.Screen();
            ((System.ComponentModel.ISupportInitialize)(this.screen1)).BeginInit();
            this.SuspendLayout();
            // 
            // screen1
            // 
            this.screen1.Location = new System.Drawing.Point(55, 56);
            this.screen1.Name = "screen1";
            this.screen1.Size = new System.Drawing.Size(597, 292);
            this.screen1.TabIndex = 0;
            this.screen1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.screen1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.screen1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Screen screen1;
    }
}

