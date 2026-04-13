using System;
using System.Drawing;
using System.Windows.Forms;

namespace JeuPFC
{
    public partial class Form1 : Form
    {
        private GestionnaireMatch gestionnaire;
        private Image imgPierre;
        private Image imgPapier;
        private Image imgCiseaux;
        private Image imgPensee;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChargerImages();
            InitialiserPartie();
        }

        private void ChargerImages()
        {
            try
            {
                imgPierre = Image.FromFile("Assets/pierre.png");
                imgPapier = Image.FromFile("Assets/feuille.png");
                imgCiseaux = Image.FromFile("Assets/ciseaux.png");
                imgPensee = Image.FromFile("Assets/tinking.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des images: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitialiserPartie()
        {
            GestionnaireMatch.Reinitialiser();
            gestionnaire = GestionnaireMatch.Instance;

            picJoueur.Image = imgPensee;
            picOrdinateur.Image = imgPensee;

            MettreAJourScores();
            lblResultat.Text = "Faites votre choix !";
            lblResultat.ForeColor = Color.Purple;
        }

        private void BtnPierre_Click(object sender, EventArgs e)
        {
            JouerCoup(new Roche());
        }

        private void BtnPapier_Click(object sender, EventArgs e)
        {
            JouerCoup(new Papier());
        }

        private void BtnCiseaux_Click(object sender, EventArgs e)
        {
            JouerCoup(new Lame());
        }

        private void JouerCoup(Option choixJoueur)
        {
            gestionnaire.ParticipantUn.Selection = choixJoueur;

            picJoueur.Image = ObtenirImageOption(choixJoueur);

            gestionnaire.JouerManche();

            picOrdinateur.Image = ObtenirImageOption(gestionnaire.ParticipantDeux.Selection);

            AfficherResultatManche(choixJoueur, gestionnaire.ParticipantDeux.Selection);

            MettreAJourScores();
        }

        private Image ObtenirImageOption(Option option)
        {
            if (option is Roche)
                return imgPierre;
            if (option is Papier)
                return imgPapier;
            if (option is Lame)
                return imgCiseaux;
            return imgPensee;
        }

        private void AfficherResultatManche(Option joueur, Option ordinateur)
        {
            int resultat = joueur.Affronter(ordinateur);

            if (resultat > 0)
            {
                lblResultat.Text = "Vous avez gagné cette manche !";
                lblResultat.ForeColor = Color.Green;
            }
            else if (resultat < 0)
            {
                lblResultat.Text = "L'ordinateur a gagné cette manche !";
                lblResultat.ForeColor = Color.Red;
            }
            else
            {
                lblResultat.Text = "Égalité !";
                lblResultat.ForeColor = Color.Orange;
            }
        }

        private void MettreAJourScores()
        {
            lblScoreJoueur.Text = $"Score: {gestionnaire.ParticipantUn.Points}";
            lblScoreOrdinateur.Text = $"Score: {gestionnaire.ParticipantDeux.Points}";
        }

        private void BtnRejouer_Click(object sender, EventArgs e)
        {
            InitialiserPartie();
        }
    }
}
