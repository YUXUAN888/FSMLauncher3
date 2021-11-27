using Gac;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GacDownload
{
    Thread[] Threads = new Thread[0];
    SquareMinecraftLauncher.Minecraft.MCDownload[] download = new SquareMinecraftLauncher.Minecraft.MCDownload[0];
    int EndDownload = 0;
    public GacDownload(int thread, SquareMinecraftLauncher.Minecraft.MCDownload[] download)
    {
        Threads = new Thread[thread];
        this.download = download;
    }

    public GacDownload(SquareMinecraftLauncher.Minecraft.MCDownload[] download)
    {
        Threads = new Thread[3];
        this.download = download;
    }

    int ADindex = 0;
    private SquareMinecraftLauncher.Minecraft.MCDownload AssignedDownload()
    {
        if (ADindex == download.Length) return null;
        ADindex++;
        return download[ADindex - 1];
    }

    public void StartDownload()
    {
        for (int i = 0; i < Threads.Length; i++)
        {
            Threads[i] = new Thread(DownloadProgress);
            Threads[i].IsBackground = true;
            Threads[i].Start();//启动线程
        }
    }

    private async void DownloadProgress()
    {
        List<FileDownloader> files = new List<FileDownloader>();
        for (int i = 0; i < 3; i++)
        {
            SquareMinecraftLauncher.Minecraft.MCDownload download = AssignedDownload();//分配下载任务
            try
            {
                if (download != null)
                {
                    FileDownloader fileDownloader = new FileDownloader(download.Url, download.path.Replace(System.IO.Path.GetFileName(download.path), ""), System.IO.Path.GetFileName(download.path));//增加下载
                    fileDownloader.download(null);
                    files.Add(fileDownloader);
                }
            }
            catch (Exception ex)//当出现下载失败时，忽略该文件
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (files.Count == 0) return;
        await Task.Factory.StartNew(() =>
        {
            while (true)//循环检测当前线程files.Count个下载任务是否下载完毕
            {
                int end = 0;
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].download(null) == files[i].getFileSize())
                    {
                        end++;
                    }
                }
                Console.WriteLine(EndDownload);

                if (end == files.Count)//完成则递归当前函数
                {
                    EndDownload += files.Count;
                    DownloadProgress();//递归
                    return;
                }
                Thread.Sleep(1000);
            }
        });
    }

    public bool GetEndDownload()
    {
        return EndDownload == download.Length ? true : false;
    }
}
