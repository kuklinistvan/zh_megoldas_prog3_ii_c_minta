using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_c
{
    class ColorParallelDownloader
    {
        private object downloaderLock = new object();
        
        int numberOfDownloads = 0;
        ColorStatusWriter colorWriter;

        LinkedList<Download> quevedDownloads = new LinkedList<Download>();
        LinkedList<Task> startedDownloadTasks = new LinkedList<Task>();

        public ColorParallelDownloader(Object consoleLock = null)
        {
            if(consoleLock == null)
            {
                consoleLock = new object();
            }

            this.colorWriter = new ColorStatusWriter(consoleLock);
        }

        public ColorParallelDownloader QueveDownload(ImageData data)
        {
            lock(downloaderLock)
            {
                Download download = new Download(numberOfDownloads, data);
                numberOfDownloads++;
                quevedDownloads.AddLast(download);
            }

            return this;
        }

        public void StartAllThen(Action<IEnumerable<Download>> finishedDownloadHandler)
        {
            quevedDownloads.ToList().ForEach(d => StartDownloadAndDisplayingStatus(d));
            Task.WhenAll(startedDownloadTasks.ToArray()).ContinueWith(t => finishedDownloadHandler.Invoke(quevedDownloads));
        }

        private void StartDownloadAndDisplayingStatus(Download download)
        {
            Task task = new Task(() =>
            {
                colorWriter.Write(download.URL + "... Downloading", download.id + 1, download.id % 2 == 0 ? ConsoleColor.Green : ConsoleColor.Cyan);
                download.DoDownload();
                colorWriter.Write(download.URL + "... Success", download.id + 1, download.id % 2 == 0 ? ConsoleColor.Green : ConsoleColor.Cyan);
            });

            startedDownloadTasks.AddLast(task);

            task.Start();
        }
    }
}
