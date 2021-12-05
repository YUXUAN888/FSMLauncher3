using MahApps.Metro.Controls.Dialogs;
using SquareMinecraftLauncher.Core.OAuth;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SquareMinecraftLauncher;
using SquareMinecraftLauncher;
using Microsoft.Win32;

namespace FSMLauncher_3
{
    /// <summary>
    /// GoStart.xaml 的交互逻辑
    /// </summary>
    public partial class GoStart
    {
        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        public GoStart()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            software = hkim.OpenSubKey("SOFTWARE", true);
            FSM = software.CreateSubKey("FSM");
            Mojang = FSM.CreateSubKey("Mojang");
            Y = FSM.CreateSubKey("Y");
            LX = FSM.CreateSubKey("LX");
            WR = FSM.CreateSubKey("WR");
            TT.SelectedIndex = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TT.SelectedIndex = 2;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            TT.SelectedIndex = 3;
        }
        public string Mojangname;
        public string MojangUUID;
        public string MojangToken;
        RegistryKey hkim = Registry.LocalMachine;
        public static string HttpDownloadFile(string url, string path)
        {

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
        public enum ZoomType { NearestNeighborInterpolation, BilinearInterpolation }
        /// <summary>
        /// 图像缩放
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="width">目标图像宽度</param>
        /// <param name="height">目标图像高度</param>
        /// <param name="dstBmp">目标图像</param>
        /// <param name="GetNearOrBil">缩放选用的算法</param>
        /// <returns>处理成功 true 失败 false</returns>
        public static bool Zoom(Bitmap srcBmp, double ratioW, double ratioH, out Bitmap dstBmp, ZoomType zoomType)
        {//ZoomType为自定义的枚举类型
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            //若缩放大小与原图一样，则返回原图不做处理
            if ((ratioW == 1.0) && ratioH == 1.0)
            {
                dstBmp = new Bitmap(srcBmp);
                return true;
            }
            //计算缩放高宽
            double height = ratioH * (double)srcBmp.Height;
            double width = ratioW * (double)srcBmp.Width;
            dstBmp = new Bitmap((int)width, (int)height);

            BitmapData srcBmpData = srcBmp.LockBits(new System.Drawing.Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData dstBmpData = dstBmp.LockBits(new System.Drawing.Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* srcPtr = null;
                byte* dstPtr = null;
                int srcI = 0;
                int srcJ = 0;
                double srcdI = 0;
                double srcdJ = 0;
                double a = 0;
                double b = 0;
                double F1 = 0;//横向插值所得数值
                double F2 = 0;//纵向插值所得数值
                if (zoomType == ZoomType.NearestNeighborInterpolation)
                {//邻近插值法

                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcI = (int)(i / ratioH);//srcI是此时的i对应的原图像的高
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            dstPtr[j * 3] = srcPtr[(int)(j / ratioW) * 3];//j / ratioW求出此时j对应的原图像的宽
                            dstPtr[j * 3 + 1] = srcPtr[(int)(j / ratioW) * 3 + 1];
                            dstPtr[j * 3 + 2] = srcPtr[(int)(j / ratioW) * 3 + 2];
                        }
                    }
                }
                else if (zoomType == ZoomType.BilinearInterpolation)
                {//双线性插值法
                    byte* srcPtrNext = null;
                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcdI = i / ratioH;
                        srcI = (int)srcdI;//当前行对应原始图像的行数
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;//指原始图像的当前行
                        srcPtrNext = (byte*)srcBmpData.Scan0 + (srcI + 1) * srcBmpData.Stride;//指向原始图像的下一行
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;//指向当前图像的当前行
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            srcdJ = j / ratioW;
                            srcJ = (int)srcdJ;//指向原始图像的列
                            if (srcdJ < 1 || srcdJ > srcBmp.Width - 1 || srcdI < 1 || srcdI > srcBmp.Height - 1)
                            {//避免溢出（也可使用循环延拓）
                                dstPtr[j * 3] = 255;
                                dstPtr[j * 3 + 1] = 255;
                                dstPtr[j * 3 + 2] = 255;
                                continue;
                            }
                            a = srcdI - srcI;//计算插入的像素与原始像素距离（决定相邻像素的灰度所占的比例）
                            b = srcdJ - srcJ;
                            for (int k = 0; k < 3; k++)
                            {//插值    公式：f(i+p,j+q)=(1-p)(1-q)f(i,j)+(1-p)qf(i,j+1)+p(1-q)f(i+1,j)+pqf(i+1, j + 1)
                                F1 = (1 - b) * srcPtr[srcJ * 3 + k] + b * srcPtr[(srcJ + 1) * 3 + k];
                                F2 = (1 - b) * srcPtrNext[srcJ * 3 + k] + b * srcPtrNext[(srcJ + 1) * 3 + k];
                                dstPtr[j * 3 + k] = (byte)((1 - a) * F1 + a * F2);
                            }
                        }
                    }
                }
            }
            srcBmp.UnlockBits(srcBmpData);
            dstBmp.UnlockBits(dstBmpData);
            return true;
        }
        private Bitmap crop(Bitmap src, System.Drawing.Point point, System.Drawing.Size size)
        {
            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(point, size);
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new System.Drawing.Rectangle(0, 0, target.Width, target.Height),
                      cropRect,
                      GraphicsUnit.Pixel);
            }
            return target;
        }
        public string  loginmode;
        public string mojangyes;
        private void Tile_Click_15(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = tools.MinecraftLogin(Mojang1.Text, Mojang2.Password);
                Mojangname = login.name;
                MojangUUID = login.uuid;
                MojangToken = login.token;
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin");

                //Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.GetMinecraftSkin(MojangUUID));
                HttpDownloadFile(tools.GetMinecraftSkin(MojangUUID), System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                System.Drawing.Point point = new System.Drawing.Point(8, 8);
                System.Drawing.Size size = new System.Drawing.Size(8, 8);
                Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                System.Drawing.Image img = i;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                bi.BeginInit();

                bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                bi.EndInit();
                IM.Source = bi;
                LB.Content = Mojangname;
                loginmode = "mojang";
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                StringBuilder temp = new StringBuilder(500);
                mojangyes = "888";
                WritePrivateProfileString("Mojang", "Mail", Mojang1.Text, File_);
                WritePrivateProfileString("Mojang", "PassWord", Mojang2.Password, File_);
                IDTab.SelectedIndex = 2;
            }
            catch(SquareMinecraftLauncher.SquareMinecraftLauncherException ex)
            {
                this.ShowMessageAsync("登陆失败", ex.Message);

            }
            
        }
        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);
        private void Tile_Click_11(object sender, RoutedEventArgs e)
        {
            if (mojangyes == "888")
            {
                IDTab.SelectedIndex = 2;
                LB.Content = Mojangname;
                loginmode = "mojang";
            }
            else
            {
                IDTab.SelectedIndex = 0;

            }
        }

        private void Tile_Click_10(object sender, RoutedEventArgs e)
        {
            if (wryes == "888")
            {
                IDTab.SelectedIndex = 2;
                LB.Content = wrname;
                loginmode = "wr";
            }
            else
            {
                IDTab.SelectedIndex = 3;

            }
        }

        private void Tile_Click_9(object sender, RoutedEventArgs e)
        {
            IDTab.SelectedIndex = 1;
        }

        private void Tile_Click_8(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread tha = new Thread(WR1);
                tha.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public string wrtoken;
        public string wruuid;
        public string wrname;
        public string wryes;
        private void WR1()
        {



            Application.Current.Dispatcher.Invoke(
     async delegate
     {
         //Code

         MicrosoftLogin microsoftLogin = new MicrosoftLogin();
         MinecraftLogin minecraftlogin = new MinecraftLogin();
         
         try
         {

             Xbox XboxLogin = new Xbox();
             var token = microsoftLogin.GetToken(await microsoftLogin.Login(true));
             wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));

             string refresh_token = token.refresh_token;


             var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);

             wruuid = Minecraft.uuid;
             wrname = Minecraft.name;

             LB.Content = wrname;
             loginmode = "wr";
             String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";

             wryes = "888";
             HttpDownloadFile(tools.GetMinecraftSkin(wruuid), System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
             System.Drawing.Point point = new System.Drawing.Point(8, 8);
             System.Drawing.Size size = new System.Drawing.Size(8, 8);
             Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
             var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
             Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
             i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
             System.Drawing.Image img = i;
             BitmapImage bi = new BitmapImage();
             // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
             bi.BeginInit();

             bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
             bi.EndInit();
             IM.Source = bi;

             WritePrivateProfileString("wr", "Atoken", refresh_token, File_);


             IDTab.SelectedIndex = 2;

         }
         catch
         {

         }




     });




        }

        private async void Tile_Click_888(object sender, RoutedEventArgs e)
        {
            //正常登录
            MicrosoftLogin microsoftLogin = new MicrosoftLogin();
            MinecraftLogin minecraftlogin = new MinecraftLogin();
            
            try
            {

                Xbox XboxLogin = new Xbox();
                var token = microsoftLogin.GetToken(await microsoftLogin.Login(false));
                wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));

                string refresh_token = token.refresh_token;


                var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);

                wruuid = Minecraft.uuid;
                wrname = Minecraft.name;

                LB.Content = wrname;
                loginmode = "wr";
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";

                wryes = "888";

                HttpDownloadFile(tools.GetMinecraftSkin(wruuid), System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                System.Drawing.Point point = new System.Drawing.Point(8, 8);
                System.Drawing.Size size = new System.Drawing.Size(8, 8);
                Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                System.Drawing.Image img = i;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                bi.BeginInit();

                bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                bi.EndInit();
                IM.Source = bi;
                WritePrivateProfileString("wr", "Atoken", refresh_token, File_);


                IDTab.SelectedIndex = 2;

            }
            catch
            {

            }
        }

        private void Button_Clicwwk1(object sender, RoutedEventArgs e)
        {
            TT.SelectedIndex = 4;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "0", File_ + @"\FSM.slx");
        }
        public String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "2", File_ + @"\FSM.slx");
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "1", File_ + @"\FSM.slx");
        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "3", File_ + @"\FSM.slx");
        }

        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "4", File_ + @"\FSM.slx");
        }

        private void Tile_Click_5(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "5", File_ + @"\FSM.slx");
        }

        private void Tile_Click_6(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "6", File_ + @"\FSM.slx");
        }

        private void Tile_Click_7(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "7", File_ + @"\FSM.slx");
        }

        private void Tile_Click_12(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "8", File_ + @"\FSM.slx");
        }

        private void Tile_Click_13(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "9", File_ + @"\FSM.slx");
        }

        private void Tile_Click_14(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "10", File_ + @"\FSM.slx");
        }

        private void Tile_Click_16(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "11", File_ + @"\FSM.slx");
        }

        private void Tile_Click_17(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "12", File_ + @"\FSM.slx");
        }

        private void Tile_Click_18(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "13", File_ + @"\FSM.slx");
        }

        private void Tile_Click_19(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "14", File_ + @"\FSM.slx");
        }

        private void Tile_Click_20(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "16", File_ + @"\FSM.slx");
        }

        private void Button_Clicw1wk1(object sender, RoutedEventArgs e)
        {
            TT.SelectedIndex = 5;
        }

        private void Button_Cli1cw1wk1(object sender, RoutedEventArgs e)
        {
            String Filew_ = System.AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess().ProcessName;
            WritePrivateProfileString("Start", "Start", "1", File_ + @"\FSM.slx");
            OpenFile(Filew_);
            Thread.Sleep(1000);
            Close();
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

        private void IDTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OfflineName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void s(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void ip(object sender, TextChangedEventArgs e)
        {

        }

        private void idd(object sender, TextChangedEventArgs e)
        {

        }
        SquareMinecraftLauncher.Minecraft.Skin skin = new SquareMinecraftLauncher.Minecraft.Skin();
        private async void Button_Click_18(object sender, RoutedEventArgs e)
        {
            try
            {
                skin = tools.GetAuthlib_Injector(IP.Text, IDD.Text, IDDPassWord.Password);
                Ylist.ItemsSource = skin.NameItem;
                IDTab.SelectedIndex = 4;

                Y.SetValue("IP", IP.Text);
                Y.SetValue("IDD", IDD.Text);
                Y.SetValue("IPPPassWord", IDDPassWord.Password);

            }
            catch (SquareMinecraftLauncherException ex)
            {
                await this.ShowMessageAsync("登录失败", ex.Message);
            }
        }
        public RegistryKey software;
        public RegistryKey Mojang;
        public RegistryKey Y;
        public RegistryKey LX;
        public RegistryKey WR;
        RegistryKey FSM;
        private void Ylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JS.Text = Ylist.SelectedItems[Ylist.SelectedIndex].ToString();
            WritePrivateProfileString("Y", "Ylist", Ylist.SelectedIndex.ToString(), FileS);
        }
        public string Yyes;
        public string Yname;
        public string Ytoken;
        public string Yuuid;
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            Yuuid = skin.NameItem[Ylist.SelectedIndex].uuid;
            Yname = skin.NameItem[Ylist.SelectedIndex].Name;
            Ytoken = skin.accessToken;
            Yyes = "888";
            loginmode = "Y";
        }

        private void Tile_Click_91(object sender, RoutedEventArgs e)
        {
            if (Yyes == "888")
            {
                IDTab.SelectedIndex = 4;
                JS.Text = Yname;
                loginmode = "y";
            }
            else
            {
                IDTab.SelectedIndex = 5;
                loginmode = "";
            }
        }
        String FileS = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        private void Tile_Click_exit(object sender, RoutedEventArgs e)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            switch (loginmode)
            {
                case "mojang":
                    loginmode = "";
                    ///WritePrivateProfileString("Mojang", "Mail", "", File_);
                    ///WritePrivateProfileString("Mojang", "PassWord", "", File_);
                    Mojang.DeleteValue("Mail");
                    Mojang.DeleteValue("PassWord");
                    IDTab.SelectedIndex = 0;
                    mojangyes = "";
                    Mojangname = "";
                    MojangUUID = "";
                    MojangToken = "";
                    break;
                case "wr":
                    loginmode = "";
                    ///WritePrivateProfileString("wr", "Atoken", "", File_);
                    WR.DeleteValue("Atoken");
                    IDTab.SelectedIndex = 3;
                    wryes = "";
                    wrname = "";
                    wruuid = "";
                    wrtoken = "";

                    break;



            }
        }

        private void Button_ClickJava17(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://fsm.ft2.club/Java/jdk-17_windows-x64_bin.exe");
        }

        private void Button_ClickJava8(object sender, RoutedEventArgs e)
        {

        }
    }
}
