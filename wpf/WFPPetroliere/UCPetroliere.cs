/**
 * Projet : WFPPetroliere
 * Author : Mrck-Kvs
 * Date : 13.03.2026
 * Description : The purpose of this project is to assess my programming algorithm skills.
 * The task is to read a text file containing characters; if the character @ appears, it is considered to represent oil.
 * The goal is then to detect oil fields consisting of @ characters that are adjacent, diagonally adjacent, vertically adjacent, or horizontally adjacent. 
 */
using System;
using System.Windows.Forms;

namespace WFPPetroliere
{
    public partial class UCPetroliere : UserControl
    {
        private ManagerApp _app;
        public UCPetroliere()
        {
            InitializeComponent();
            _app = new ManagerApp();
        }

        private void BtnReadFile_Click(object sender, EventArgs e)
        {
            tbxDataRead.Clear();
            tbxResult.Clear();
            _app.OpenYourFile();
            tbxDataRead.Lines = _app.Content;

            tbxResult.Lines = _app.LoadSearchForOil();
        }
    }
}
