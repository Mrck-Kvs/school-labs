/**
 * Projet : WFPPetroliere
 * Author : Mrck-Kvs
 * Date : 13.03.2026
 * Description : The purpose of this project is to assess my programming algorithm skills.
 * The task is to read a text file containing characters; if the character @ appears, it is considered to represent oil.
 * The goal is then to detect oil fields consisting of @ characters that are adjacent, diagonally adjacent, vertically adjacent, or horizontally adjacent. 
 */
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WFPPetroliere
{
    /**
     * This class will be used to manage the application; it allows us to read a text file and locate oil fields.
     */
    internal class ManagerApp
    {
        // Attribut
        private string _path;
        private OpenFileDialog _ofd;
        private string[] _content;
        private List<StringBuilder> _listStrBuilders = new List<StringBuilder>();

        // Properties
        public string[] Content { get { return _content; } }

        //
        public ManagerApp()
        {
            string path = @"C:\\Users\\Admin\\Documents\\Récap 2026-20260318\\Enoncés et grille\\pétrole\\gisements";
            if (Directory.Exists(path))
            {
                _path = path;
            }
            else
            {
                _path = Directory.GetCurrentDirectory();
            }
            _ofd = new OpenFileDialog()
            {
                FileName = "Choisis ton fichier !",
                Filter = "Text Files (*.txt) | *.txt",
                Title = "Ouvre uniquement des fichiers textes",
                InitialDirectory = _path,
            };
        }

        public bool OpenYourFile()
        {
            _listStrBuilders.Clear();
            if (_ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _path = _ofd.FileName;
                    _content = File.ReadAllLines(_path);
                    foreach (string line in _content)
                    {
                        _listStrBuilders.Add(new StringBuilder(line));
                    }
                    return true;
                } catch (FileLoadException exception)
                {
                    MessageBox.Show($"Erreur durant le chargement du fichier message : {exception.Message}\n\n" +
                        $"Details : \n\n {exception.StackTrace}");
                    return false;
                }
            }
            return false;
        }

        public string[] LoadSearchForOil()
        {
            int OilFields = 0;
            for (int row = 0; row < _content.Length; row++)
            {
                StringBuilder currentRow = @_listStrBuilders[row];
                for (int column = 0; column < currentRow.Length; column++)
                {
                    char currentChar = currentRow[column];
                    if (currentChar == '@')
                    {
                        OilFields++;
                        currentRow.Remove(column, 1);
                        currentRow.Insert(column, OilFields);
                        CheckNeighbour(row, column, OilFields);
                    }
                }
            }
            List<string> result = new List<string>();
            _listStrBuilders.ForEach(x => result.Add(x.ToString()));
            result.Add($"\n\n\n{OilFields} gisements");
            return result.ToArray();
        }

        private void CheckNeighbour(int row, int column, int OilFields)
        {
            (int, int)[] patternNeighbour = { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
            StringBuilder currentRow = @_listStrBuilders[row];
            for (int indexPattern = 0; indexPattern < patternNeighbour.Length; indexPattern++)
            {

                (int x, int y) currentPattern = patternNeighbour[indexPattern];
                int x = row + currentPattern.x;
                int y = column + currentPattern.y;
                if (x >= 0 && x < _content.Length && y >= 0 && y < _content[x].Length)
                {
                    if (_listStrBuilders[x][y] == '@')
                    {
                        StringBuilder strBuilder = _listStrBuilders[x];
                        strBuilder.Remove(y, 1);
                        strBuilder.Insert(y, OilFields);
                        CheckNeighbour(x, y, OilFields);
                    }
                }
            }
        }
    }
}
