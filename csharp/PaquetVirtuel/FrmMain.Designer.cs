namespace PaquetVirtuel
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.controleJeu = new PaquetVirtuel.ControlePartie();
            this.SuspendLayout();
            //
            // controleJeu
            //
            this.controleJeu.Location = new System.Drawing.Point(12, 12);
            this.controleJeu.Name = "controleJeu";
            this.controleJeu.Size = new System.Drawing.Size(800, 670);
            this.controleJeu.TabIndex = 0;
            //
            // FrmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 690);
            this.Controls.Add(this.controleJeu);
            this.Name = "FrmMain";
            this.Text = "Simulateur de Paquet";
            this.ResumeLayout(false);
        }

        #endregion

        private ControlePartie controleJeu;
    }
}
