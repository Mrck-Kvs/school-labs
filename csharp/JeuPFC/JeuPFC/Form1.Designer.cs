namespace JeuPFC
{
    partial class Form1
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
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblJoueur = new System.Windows.Forms.Label();
            this.lblOrdinateur = new System.Windows.Forms.Label();
            this.picJoueur = new System.Windows.Forms.PictureBox();
            this.picOrdinateur = new System.Windows.Forms.PictureBox();
            this.btnPierre = new System.Windows.Forms.Button();
            this.btnPapier = new System.Windows.Forms.Button();
            this.btnCiseaux = new System.Windows.Forms.Button();
            this.lblScoreJoueur = new System.Windows.Forms.Label();
            this.lblScoreOrdinateur = new System.Windows.Forms.Label();
            this.lblResultat = new System.Windows.Forms.Label();
            this.lblVS = new System.Windows.Forms.Label();
            this.btnRejouer = new System.Windows.Forms.Button();
            this.grpChoixJoueur = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picJoueur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOrdinateur)).BeginInit();
            this.grpChoixJoueur.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitre
            //
            this.lblTitre.Font = new System.Drawing.Font("Arial Black", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitre.Location = new System.Drawing.Point(0, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(800, 50);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Pierre - Feuille - Ciseaux";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblJoueur
            //
            this.lblJoueur.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblJoueur.Location = new System.Drawing.Point(50, 80);
            this.lblJoueur.Name = "lblJoueur";
            this.lblJoueur.Size = new System.Drawing.Size(200, 30);
            this.lblJoueur.TabIndex = 1;
            this.lblJoueur.Text = "Joueur";
            this.lblJoueur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblOrdinateur
            //
            this.lblOrdinateur.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblOrdinateur.Location = new System.Drawing.Point(550, 80);
            this.lblOrdinateur.Name = "lblOrdinateur";
            this.lblOrdinateur.Size = new System.Drawing.Size(200, 30);
            this.lblOrdinateur.TabIndex = 2;
            this.lblOrdinateur.Text = "Ordinateur";
            this.lblOrdinateur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // picJoueur
            //
            this.picJoueur.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picJoueur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picJoueur.Location = new System.Drawing.Point(75, 115);
            this.picJoueur.Name = "picJoueur";
            this.picJoueur.Size = new System.Drawing.Size(150, 150);
            this.picJoueur.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picJoueur.TabIndex = 3;
            this.picJoueur.TabStop = false;
            //
            // picOrdinateur
            //
            this.picOrdinateur.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picOrdinateur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOrdinateur.Location = new System.Drawing.Point(575, 115);
            this.picOrdinateur.Name = "picOrdinateur";
            this.picOrdinateur.Size = new System.Drawing.Size(150, 150);
            this.picOrdinateur.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOrdinateur.TabIndex = 4;
            this.picOrdinateur.TabStop = false;
            //
            // lblVS
            //
            this.lblVS.Font = new System.Drawing.Font("Arial Black", 36F, System.Drawing.FontStyle.Bold);
            this.lblVS.ForeColor = System.Drawing.Color.Red;
            this.lblVS.Location = new System.Drawing.Point(300, 150);
            this.lblVS.Name = "lblVS";
            this.lblVS.Size = new System.Drawing.Size(200, 60);
            this.lblVS.TabIndex = 5;
            this.lblVS.Text = "VS";
            this.lblVS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblScoreJoueur
            //
            this.lblScoreJoueur.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblScoreJoueur.ForeColor = System.Drawing.Color.Green;
            this.lblScoreJoueur.Location = new System.Drawing.Point(75, 270);
            this.lblScoreJoueur.Name = "lblScoreJoueur";
            this.lblScoreJoueur.Size = new System.Drawing.Size(150, 30);
            this.lblScoreJoueur.TabIndex = 6;
            this.lblScoreJoueur.Text = "Score: 0";
            this.lblScoreJoueur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblScoreOrdinateur
            //
            this.lblScoreOrdinateur.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblScoreOrdinateur.ForeColor = System.Drawing.Color.Green;
            this.lblScoreOrdinateur.Location = new System.Drawing.Point(575, 270);
            this.lblScoreOrdinateur.Name = "lblScoreOrdinateur";
            this.lblScoreOrdinateur.Size = new System.Drawing.Size(150, 30);
            this.lblScoreOrdinateur.TabIndex = 7;
            this.lblScoreOrdinateur.Text = "Score: 0";
            this.lblScoreOrdinateur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblResultat
            //
            this.lblResultat.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblResultat.ForeColor = System.Drawing.Color.Purple;
            this.lblResultat.Location = new System.Drawing.Point(150, 310);
            this.lblResultat.Name = "lblResultat";
            this.lblResultat.Size = new System.Drawing.Size(500, 40);
            this.lblResultat.TabIndex = 8;
            this.lblResultat.Text = "Faites votre choix !";
            this.lblResultat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // grpChoixJoueur
            //
            this.grpChoixJoueur.Controls.Add(this.btnPierre);
            this.grpChoixJoueur.Controls.Add(this.btnPapier);
            this.grpChoixJoueur.Controls.Add(this.btnCiseaux);
            this.grpChoixJoueur.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.grpChoixJoueur.Location = new System.Drawing.Point(200, 360);
            this.grpChoixJoueur.Name = "grpChoixJoueur";
            this.grpChoixJoueur.Size = new System.Drawing.Size(400, 120);
            this.grpChoixJoueur.TabIndex = 9;
            this.grpChoixJoueur.TabStop = false;
            this.grpChoixJoueur.Text = "Votre choix";
            //
            // btnPierre
            //
            this.btnPierre.BackColor = System.Drawing.Color.LightGray;
            this.btnPierre.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnPierre.Location = new System.Drawing.Point(20, 30);
            this.btnPierre.Name = "btnPierre";
            this.btnPierre.Size = new System.Drawing.Size(110, 75);
            this.btnPierre.TabIndex = 0;
            this.btnPierre.Text = "Pierre";
            this.btnPierre.UseVisualStyleBackColor = false;
            this.btnPierre.Click += new System.EventHandler(this.BtnPierre_Click);
            //
            // btnPapier
            //
            this.btnPapier.BackColor = System.Drawing.Color.LightGray;
            this.btnPapier.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnPapier.Location = new System.Drawing.Point(145, 30);
            this.btnPapier.Name = "btnPapier";
            this.btnPapier.Size = new System.Drawing.Size(110, 75);
            this.btnPapier.TabIndex = 1;
            this.btnPapier.Text = "Papier";
            this.btnPapier.UseVisualStyleBackColor = false;
            this.btnPapier.Click += new System.EventHandler(this.BtnPapier_Click);
            //
            // btnCiseaux
            //
            this.btnCiseaux.BackColor = System.Drawing.Color.LightGray;
            this.btnCiseaux.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnCiseaux.Location = new System.Drawing.Point(270, 30);
            this.btnCiseaux.Name = "btnCiseaux";
            this.btnCiseaux.Size = new System.Drawing.Size(110, 75);
            this.btnCiseaux.TabIndex = 2;
            this.btnCiseaux.Text = "Ciseaux";
            this.btnCiseaux.UseVisualStyleBackColor = false;
            this.btnCiseaux.Click += new System.EventHandler(this.BtnCiseaux_Click);
            //
            // btnRejouer
            //
            this.btnRejouer.BackColor = System.Drawing.Color.Orange;
            this.btnRejouer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnRejouer.Location = new System.Drawing.Point(300, 490);
            this.btnRejouer.Name = "btnRejouer";
            this.btnRejouer.Size = new System.Drawing.Size(200, 40);
            this.btnRejouer.TabIndex = 10;
            this.btnRejouer.Text = "Nouvelle Partie";
            this.btnRejouer.UseVisualStyleBackColor = false;
            this.btnRejouer.Click += new System.EventHandler(this.BtnRejouer_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.btnRejouer);
            this.Controls.Add(this.grpChoixJoueur);
            this.Controls.Add(this.lblResultat);
            this.Controls.Add(this.lblScoreOrdinateur);
            this.Controls.Add(this.lblScoreJoueur);
            this.Controls.Add(this.lblVS);
            this.Controls.Add(this.picOrdinateur);
            this.Controls.Add(this.picJoueur);
            this.Controls.Add(this.lblOrdinateur);
            this.Controls.Add(this.lblJoueur);
            this.Controls.Add(this.lblTitre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pierre Feuille Ciseaux";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picJoueur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOrdinateur)).EndInit();
            this.grpChoixJoueur.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblJoueur;
        private System.Windows.Forms.Label lblOrdinateur;
        private System.Windows.Forms.PictureBox picJoueur;
        private System.Windows.Forms.PictureBox picOrdinateur;
        private System.Windows.Forms.Button btnPierre;
        private System.Windows.Forms.Button btnPapier;
        private System.Windows.Forms.Button btnCiseaux;
        private System.Windows.Forms.Label lblScoreJoueur;
        private System.Windows.Forms.Label lblScoreOrdinateur;
        private System.Windows.Forms.Label lblResultat;
        private System.Windows.Forms.Label lblVS;
        private System.Windows.Forms.Button btnRejouer;
        private System.Windows.Forms.GroupBox grpChoixJoueur;
    }
}
