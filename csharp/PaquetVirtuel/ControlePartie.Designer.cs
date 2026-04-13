namespace PaquetVirtuel
{
    partial class ControlePartie
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

        #region Code généré par le Concepteur de composants

        private void InitializeComponent()
        {
            this.btnOrdonne = new System.Windows.Forms.Button();
            this.btnMelange = new System.Windows.Forms.Button();
            this.zoneAffichage = new PaquetVirtuel.TableDeJeu();
            ((System.ComponentModel.ISupportInitialize)(this.zoneAffichage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOrdonne
            // 
            this.btnOrdonne.Location = new System.Drawing.Point(13, 615);
            this.btnOrdonne.Name = "btnOrdonne";
            this.btnOrdonne.Size = new System.Drawing.Size(218, 46);
            this.btnOrdonne.TabIndex = 0;
            this.btnOrdonne.Text = "Générer un paquet";
            this.btnOrdonne.UseVisualStyleBackColor = true;
            this.btnOrdonne.Click += new System.EventHandler(this.BtnOrdonne_Click);
            // 
            // btnMelange
            // 
            this.btnMelange.Location = new System.Drawing.Point(569, 615);
            this.btnMelange.Name = "btnMelange";
            this.btnMelange.Size = new System.Drawing.Size(218, 46);
            this.btnMelange.TabIndex = 2;
            this.btnMelange.Text = "Mélanger le paquet";
            this.btnMelange.UseVisualStyleBackColor = true;
            this.btnMelange.Click += new System.EventHandler(this.BtnMelange_Click);
            // 
            // zoneAffichage
            // 
            this.zoneAffichage.BackColor = System.Drawing.Color.Crimson;
            this.zoneAffichage.Location = new System.Drawing.Point(0, 0);
            this.zoneAffichage.Name = "zoneAffichage";
            this.zoneAffichage.Size = new System.Drawing.Size(800, 600);
            this.zoneAffichage.TabIndex = 3;
            this.zoneAffichage.TabStop = false;
            // 
            // ControlePartie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zoneAffichage);
            this.Controls.Add(this.btnMelange);
            this.Controls.Add(this.btnOrdonne);
            this.Name = "ControlePartie";
            this.Size = new System.Drawing.Size(800, 675);
            ((System.ComponentModel.ISupportInitialize)(this.zoneAffichage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOrdonne;
        private System.Windows.Forms.Button btnMelange;
        private TableDeJeu zoneAffichage;
    }
}
