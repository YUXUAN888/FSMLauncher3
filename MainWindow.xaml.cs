using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.ValueBoxes;
using System.IO;
using System.Net;
using MahApps.Metro.Controls.Dialogs;
using ControlzEx.Theming;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation;
using MahApps;
using SquareMinecraftLauncherWPF;
using SquareMinecraftLauncher.Core;
using SquareMinecraftLauncher.Minecraft;
using SquareMinecraftLauncher.Core.fabricmc;
using ProjBobcat;
using Gac;
using KMCCC.Launcher;
using KMCCC.Authentication;

namespace FSMLauncher_3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    
    public partial class MetroWindow


    {
        public static LauncherCore Core = LauncherCore.Create();
        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        public bool OffLineSkin;
        public string[] after;
        public string[] UpLog_and_GG;
        public static void ListFiles(FileSystemInfo info)
        {

        }

        public string UpdateD;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int WinExec(string exeName, int operType);
        public static void getDirectory(StreamWriter sw, string path, int indent)
        {
            getFileName(sw, path, indent);
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                for (int i = 0; i < indent; i++)
                {
                    sw.Write("  ");
                }
                sw.WriteLine("文件夹：" + d.Name);
                getDirectory(sw, d.FullName, indent + 2);
                sw.WriteLine();
                MessageBox.Show(d.Name);
            }
        }
        /// <summary>
        /// 获得指定路径下所有文件名
        /// </summary>
        /// <param name="sw">文件写入流</param>
        /// <param name="path">文件写入流</param>
        /// <param name="indent">输出时的缩进量</param>
        public static void getFileName(StreamWriter sw, string path, int indent)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                for (int i = 0; i < indent; i++)
                {
                    sw.Write("  ");
                }
                sw.WriteLine(f.Name);

            }
        }


        public void DownloadSourceInitialization(DownloadSource downloadSource)
        {

        }


         



        public MetroWindow()
        {

            ServicePointManager.DefaultConnectionLimit = 512;
            String Update = "3.0";//每次更新启动器设置，启动器当前版本号
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM");
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";
            StringBuilder temp = new StringBuilder();
            GetPrivateProfileString("ZTSY", "ZTSY", "", temp, 255, File_ + @"\FSM.slx");

            //DownloadSourceInitialization(DownloadSource.MCBBSSource);//改源方法

            

            this.ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();

            

            


            var javaList = KMCCC.Tools.SystemTools.FindJava();
            Java_list.ItemsSource = javaList;
            Java_list.Text = Java_list.Items[0].ToString();


            Label1.Content = "您好！" + Environment.UserName;

            UpdateButton.Visibility = System.Windows.Visibility.Visible;
            if (System.IO.File.Exists(File_ + @"\FSM.slx"))
            {

                //存在文件
            }
            else
            {
                //不存在文件
                using (File.Create(File_ + @"\FSM.slx")) ;
            }

            

            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                StringBuilder sb = new StringBuilder();
                String pageData = MyWebClient.DownloadString("http://2018k.cn/api/checkVersion?id=acdbe11aceff42a599113997cbb74103&version=" + Update); //从指定网站下载数据
                pageData = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://2018k.cn/api/checkVersion?id=acdbe11aceff42a599113997cbb74103&version=" + Update));

                String pageHtml = pageData;


                String pageData1 = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=remark|notice")); //从指定网站下载数据
                String pageHtml1 = pageData1;
                String pageData2 = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=url")); //从指定网站下载数据
                String pageHtml2 = pageData2;

                after = pageHtml.Split(new char[] { '|' });
                UpLog_and_GG = pageHtml1.Split(new char[] { '|' });
                UpdateD = pageHtml2;
                if (after[0] == "true")
                {
                    this.ShowMessageAsync("检测到启动器有新的版本！", "新版本为:" + after[4] + "\n" + "请到设置进行更新！");
                    Thread th = new Thread(ThreadSendKey);
                    th.Start(); //启动线程 

                }
            }
            catch
            {
                this.ShowMessageAsync("未能与FSM服务器建立通信", "这可能是是网络未连接导致" + "\n" + "可能会导致无法下载游戏，无法更新启动器等问题");
            }
            //StringBuilder temp = new StringBuilder();
            //GetPrivateProfileString("ZTSY", "ZTSY", "", temp, 255, IntFilePath);


            if (temp.ToString() == "1")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Green");
                WritePrivateProfileString("ZTSY", "ZTSY", "1", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 1;
            }
            else if (temp.ToString() == "2")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
                WritePrivateProfileString("ZTSY", "ZTSY", "2", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 2;
            }
            else if (temp.ToString() == "3")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Purple");
                WritePrivateProfileString("ZTSY", "ZTSY", "3", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 3;
            }
            else if (temp.ToString() == "4")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Orange");
                WritePrivateProfileString("ZTSY", "ZTSY", "4", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 4;
            }
            else if (temp.ToString() == "5")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Lime");
                WritePrivateProfileString("ZTSY", "ZTSY", "5", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 5;
            }
            else if (temp.ToString() == "6")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Emerald");
                WritePrivateProfileString("ZTSY", "ZTSY", "6", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 6;
            }
            else if (temp.ToString() == "7")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Teal");

                WritePrivateProfileString("ZTSY", "ZTSY", "7", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 7;
            }
            else if (temp.ToString() == "8")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cyan");
                WritePrivateProfileString("ZTSY", "ZTSY", "8", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 8;
            }
            else if (temp.ToString() == "9")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
                WritePrivateProfileString("ZTSY", "ZTSY", "9", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 9;
            }
            else if (temp.ToString() == "10")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Indigo");
                WritePrivateProfileString("ZTSY", "ZTSY", "10", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 10;
            }
            else if (temp.ToString() == "11")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Violet");
                WritePrivateProfileString("ZTSY", "ZTSY", "11", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 11;
            }
            else if (temp.ToString() == "12")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Pink");
                WritePrivateProfileString("ZTSY", "ZTSY", "12", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 12;
            }
            else if (temp.ToString() == "13")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Magenta");
                WritePrivateProfileString("ZTSY", "ZTSY", "13", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 13;
            }
            else if (temp.ToString() == "14")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
                Java_list_Copy.SelectedIndex = 14;
                WritePrivateProfileString("ZTSY", "ZTSY", "14", File_ + @"\FSM.slx");
            }
            else if (temp.ToString() == "15")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Amber");
                WritePrivateProfileString("ZTSY", "ZTSY", "15", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 15;
            }
            else if (temp.ToString() == "16")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Yellow");
                WritePrivateProfileString("ZTSY", "ZTSY", "16", File_ + @"\FSM.slx");
                Java_list_Copy.SelectedIndex = 16;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ThreadSendKey()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         UpdateButton.Visibility = System.Windows.Visibility.Visible;
         UpdateLabel.Content = "启动器有新版本 FSM" + after[4];
         Update_Log.Content = "更新日志:" + UpLog_and_GG[0];

     });




        }
        private void OffLine1()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         OffLinebaocun.Visibility = Visibility.Hidden;
         TextBoxOffline.Visibility = Visibility.Hidden;
         LabelOffline.Visibility = Visibility.Hidden;

     });




        }
        private void OffLine2()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         OffLinebaocun.Visibility = Visibility.Visible;
         TextBoxOffline.Visibility = Visibility.Visible;
         LabelOffline.Visibility = Visibility.Visible;

     });




        }
        private void UpdateLauncher()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         Tab1.SelectedIndex = 6;
         String File_ = System.AppDomain.CurrentDomain.BaseDirectory + "[Update]FSM.exe";
         Tab1.SelectedIndex = 6;
         HttpDownloadFile(UpdateD, File_, 6, Tab1);
         OpenFile(File_);
         Close();


     });
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void xianshibanben_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {







        }


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

            
        }

        
        
        public sealed class MCDownload
        {
            public MCDownload()
            {

            }

            //
            // 摘要:
            //     下载网址
            public string Url { get; }
            //
            // 摘要:
            //     下载路径
            public string path { get; }
        }
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            

        }


        private void Java_list_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";

            if (Java_list_Copy.SelectedIndex == 0)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "0", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Red");
            }
            else if (Java_list_Copy.SelectedIndex == 1)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "1", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Green");
            }
            else if (Java_list_Copy.SelectedIndex == 2)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "2", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            }
            else if (Java_list_Copy.SelectedIndex == 3)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "3", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Purple");
            }
            else if (Java_list_Copy.SelectedIndex == 4)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "4", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Orange");
            }
            else if (Java_list_Copy.SelectedIndex == 5)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "5", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Lime");
            }
            else if (Java_list_Copy.SelectedIndex == 6)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "6", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Emerald");
            }
            else if (Java_list_Copy.SelectedIndex == 7)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "7", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Teal");
            }
            else if (Java_list_Copy.SelectedIndex == 8)
            {

                WritePrivateProfileString("ZTSY", "ZTSY", "8", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Cyan");
            }
            else if (Java_list_Copy.SelectedIndex == 9)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "9", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
            }
            else if (Java_list_Copy.SelectedIndex == 10)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "10", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Indigo");
            }
            else if (Java_list_Copy.SelectedIndex == 11)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "11", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Violet");
            }
            else if (Java_list_Copy.SelectedIndex == 12)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "12", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Pink");
            }
            else if (Java_list_Copy.SelectedIndex == 13)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "13", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Magenta");
            }
            else if (Java_list_Copy.SelectedIndex == 14)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "14", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
            }
            else if (Java_list_Copy.SelectedIndex == 15)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "15", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Amber");
            }
            else if (Java_list_Copy.SelectedIndex == 16)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "16", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Yellow");
            }
        }







        private string IniFilePath;


        private void Java_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void shenqianse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 5;
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 4;
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 1;
        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 2;
        }
        
        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM");
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";
            StringBuilder temp1 = new StringBuilder();
            GetPrivateProfileString("OnLine", "OnLine", "", temp1, 255, File_ + @"\FSM.slx");
            if (temp1.ToString() == "1")
            {
                WritePrivateProfileString("OnLine", "OnLine", "1", File_ + @"\FSM.slx");
                Tab1.SelectedIndex = 3;
            }
            else
            {
                Tab1.SelectedIndex = 6;

            }


            


        }

        private void OnLine()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\frpc.exe";
         Tab1.SelectedIndex = 6;
         HttpDownloadFile("", File_, 6, Tab1);


     });
        }



            private void Tile_Click_5(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 6;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 0;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            IDTab.SelectedIndex = 0;
        }

        private void Tile_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            IDTab.SelectedIndex = 3;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            IDTab.SelectedIndex = 1;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string HttpDownloadFile(string url, string path, int tabc, TabControl tt)
        {
            tt.SelectedIndex = tabc;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //加上这一句
            // 设置参数(FSM.V)
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取FSM_Core

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //发送Post请求
            Stream responseStream = response.GetResponseStream();

            //创建本地文件写入流FSM启动器的FSMCore
            Stream stream = new FileStream(path, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            return path;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 6;
            Thread.Sleep(2000);
            Thread th = new Thread(UpdateLauncher);
            th.Start(); //启动线程 


        }
        private void OpenFile(string NewFileName)
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo(NewFileName);
            process.StartInfo = processStartInfo;
            #region 下面这段被注释掉代码（可以用来全屏打开代码）
            //建立新的系统进程
            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //设置文件名，此处为图片的真实路径 + 文件名（需要有后缀）    
            //process.StartInfo.FileName = NewFileName;
            //此为关键部分。设置进程运行参数，此时为最大化窗口显示图片。    
            //process.StartInfo.Arguments = "rundll32.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
            //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true
            //process.StartInfo.UseShellExecute = true;
            #endregion
            try
            {
                process.Start();
                try
                {
                    // process.WaitForExit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (process != null)
                    {
                        process.Close();
                        process = null;
                    }
                }
                catch { }
            }


        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OffLineSkin = false;

            Thread th = new Thread(OffLine1);
            th.Start(); //启动线程 
            






        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            OffLineSkin = true;
            Thread th = new Thread(OffLine2);
            th.Start(); //启动线程 
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {

            Mojangname = tools.MinecraftLogin(Mojang1.Text, Mojang2.Password).name;
            MojangUUID = tools.MinecraftLogin(Mojang1.Text, Mojang2.Password).uuid;
            MojangToken = tools.MinecraftLogin(Mojang1.Text, Mojang2.Password).token;
            loginmode = "mojang";
        }

        




        public string Mojangname;
        public string MojangUUID;
        public string MojangToken;
        public string offlinename;
        public string loginmode;
        public bool ifoffilneskin;

        private void OffLinebaocun_Click(object sender, RoutedEventArgs e)
        {
            string offlinenameonline;
            offlinenameonline = TextBoxOffline.Text;
            //开发中...
            

        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            offlinename = OfflineName.Text;
            loginmode = "offline";
        }
        //loginmode 都为小写！"mojang" "wr" "offline"
    }
}
