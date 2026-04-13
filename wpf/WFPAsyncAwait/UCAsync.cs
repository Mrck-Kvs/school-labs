using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WFPAsyncAwait
{
    public partial class UCAsync : UserControl
    {
        private const string DEFAULT_URI = "http://ipv4.download.thinkbroadband.com/100MB.zip";
        private Downloader _downloader;
        public UCAsync()
        {
            InitializeComponent();
            txtUri.Text = DEFAULT_URI;
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (txtUri.Text != "")
            {
                _downloader = new Downloader(txtUri.Text);
            }
            else
            {
                _downloader = new Downloader(DEFAULT_URI);
            }
            _downloader.TimeOut += message;
            _downloader.Downloaded += message;
            await _downloader.DownloadStringWithTimeout(10000);
        }

        private void message(object sender, DownloadEventArgs eventArgs)
        {
            if (eventArgs.Statut == DownloadEventArgs.ValidStatut.Error)
            {
                MessageBox.Show("We are sorry, failed to downloading", eventArgs.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text File|*.txt";
                saveFileDialog1.Title = "Save our download";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    File.WriteAllText(saveFileDialog1.FileName, _downloader.DownloadContent);
                }
            }
        }
    }
}
