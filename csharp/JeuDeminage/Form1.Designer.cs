namespace JeuDeminage
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
            this.controleDemineur = new JeuDeminage.ControleDemineur();
            this.SuspendLayout();
            //
            // controleDemineur
            //
            this.controleDemineur.Location = new System.Drawing.Point(12, 12);
            this.controleDemineur.Name = "controleDemineur";
            this.controleDemineur.Size = new System.Drawing.Size(1164, 777);
            this.controleDemineur.TabIndex = 0;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1564, 845);
            this.Controls.Add(this.controleDemineur);
            this.Name = "Form1";
            this.Text = "Démineur";
            this.ResumeLayout(false);

        }

        #endregion

        private ControleDemineur controleDemineur;
    }
}

