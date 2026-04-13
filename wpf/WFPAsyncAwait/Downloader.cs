using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WFPAsyncAwait
{
    public class Downloader
    {
        private string _downloadContent;
        private string _uri;


        public string DownloadContent { get => _downloadContent; }

        public event EventHandler<DownloadEventArgs> TimeOut;
        public event EventHandler<DownloadEventArgs> Downloaded;

        public delegate void timeOutHandler(string e, DownloadEventArgs a);

        public Downloader(string uri)
        {
            _uri = uri;
        }



        public async Task<string> DownloadStringWithTimeout(int timeOutInMs)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Other");
                var downloadTask = client.GetStringAsync(_uri);
                var timeoutTask = Task.Delay(timeOutInMs);
                var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
                if (completedTask == timeoutTask)
                {
                    TimeOut?.Invoke(this, new DownloadEventArgs(_uri, "TimeOut", DownloadEventArgs.ValidStatut.Error));

                    return null;
                }

                _downloadContent = await downloadTask;

                Downloaded?.Invoke(this, new DownloadEventArgs(_uri, "Done", DownloadEventArgs.ValidStatut.Done));

                return _downloadContent;
            }
        }

    }

    public class DownloadEventArgs : EventArgs
    {
        private string _message;
        private string _uri;
        private ValidStatut _statut;
        public enum ValidStatut
        {
            Done,
            Error
        }


        public DownloadEventArgs(string uri, string message, ValidStatut statut)
        {
            _uri = uri;
            _message = message;
            _statut = statut;
        }




        public string Message { get => _message; set => _message = value; }

        public string Uri {  get => _uri; set => _uri = value; }

        public ValidStatut Statut { get => _statut; set => _statut = value; }
    }

}
