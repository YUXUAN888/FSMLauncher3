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
using Microsoft.Win32;
using Gac;
using static FSMLauncher_3.Core.xzItem;
using ControlzEx.Theming;

namespace FSMLauncher_3
{
    /// <summary>
    /// GoStart.xaml 的交互逻辑
    /// </summary>
    public partial class GoStart
    {
        static String ZongX = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); //获取APPDATA
        public string UpdateD;
        String ZongW = ZongX + @"\.fsm";
        SquareMinecraftLauncher.Minecraft.Tools tools = new SquareMinecraftLauncher.Minecraft.Tools();
        public GoStart()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            //software = hkim.OpenSubKey("SOFTWARE", true);
            //FSM = software.CreateSubKey("FSM");
            //Mojang = FSM.CreateSubKey("Mojang");
            //Y = FSM.CreateSubKey("Y");
            //LX = FSM.CreateSubKey("LX");
            //WR = FSM.CreateSubKey("WR");
            
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
        public static int id = 0;
        internal int Download(string path, string ly, string url)
        {

            dlf.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);//增加下载
            dlf.StartDown(3);//开始下载
            id++;
            Core.xzItem xzItem = new Core.xzItem(System.IO.Path.GetFileName(path), 0, ly, "等待中", url, path);
            DIYvar.xzItems.Add(xzItem);


            return id - 1;
        }
        public Gac.DownLoadFile dlf = new DownLoadFile();
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        int dw;
        static System.Windows.Threading.DispatcherTimer ONLINEW = new System.Windows.Threading.DispatcherTimer();
        private void OnQ(object ob, EventArgs a)
        {

            if (DIYvar.xzItems[dw].xzwz == "完成")
            {
                Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                System.Drawing.Image img = i;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                IM.Source = BitmapToBitmapImage(i);

            }


        }
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
        class AutoSizeFormClass
        {
            //(1).声明结构,只记录窗体和其控件的初始位置和大小。
            public struct controlRect
            {
                public int Left;
                public int Top;
                public int Width;
                public int Height;
            }
            //(2).声明 1个对象
            //注意这里不能使用控件列表记录 List nCtrl;，因为控件的关联性，记录的始终是当前的大小。
            //      public List oldCtrl= new List();//这里将西文的大于小于号都过滤掉了，只能改为中文的，使用中要改回西文
            public List<controlRect> oldCtrl = new List<controlRect>();
            int ctrlNo = 0;//1;
                           //(3). 创建两个函数
                           //(3.1)记录窗体和其控件的初始位置和大小,
            public void controllInitializeSize(System.Windows.Forms.Control mForm)
            {
                controlRect cR;
                cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                AddControl(mForm);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                                  //this.WindowState = (System.Windows.Forms.FormWindowState)(2);//记录完控件的初始位置和大小后，再最大化
                                  //0 - Normalize , 1 - Minimize,2- Maximize
            }
            private void AddControl(System.Windows.Forms.Control ctl)
            {
                foreach (System.Windows.Forms.Control c in ctl.Controls)
                {  //**放在这里，是先记录控件的子控件，后记录控件本身
                   //if (c.Controls.Count > 0)
                   //    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                    controlRect objCtrl;
                    objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                    oldCtrl.Add(objCtrl);
                    //**放在这里，是先记录控件本身，后记录控件的子控件
                    if (c.Controls.Count > 0)
                        AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                }
            }
            //(3.2)控件自适应大小,
            public void controlAutoSize(System.Windows.Forms.Control mForm)
            {
                if (ctrlNo == 0)
                { //*如果在窗体的Form1_Load中，记录控件原始的大小和位置，正常没有问题，但要加入皮肤就会出现问题，因为有些控件如dataGridView的的子控件还没有完成，个数少
                  //*要在窗体的Form1_SizeChanged中，第一次改变大小时，记录控件原始的大小和位置,这里所有控件的子控件都已经形成
                    controlRect cR;
                    //  cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                    cR.Left = 0; cR.Top = 0; cR.Width = mForm.PreferredSize.Width; cR.Height = mForm.PreferredSize.Height;

                    oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                    AddControl(mForm);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
                }
                float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
                float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;
                ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始
                AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
            private void AutoScaleControl(System.Windows.Forms.Control ctl, float wScale, float hScale)
            {
                int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
                //int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
                foreach (System.Windows.Forms.Control c in ctl.Controls)
                { //**放在这里，是先缩放控件的子控件，后缩放控件本身
                  //if (c.Controls.Count > 0)
                  //   AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                    ctrLeft0 = oldCtrl[ctrlNo].Left;
                    ctrTop0 = oldCtrl[ctrlNo].Top;
                    ctrWidth0 = oldCtrl[ctrlNo].Width;
                    ctrHeight0 = oldCtrl[ctrlNo].Height;
                    //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例
                    //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;
                    c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                    c.Top = (int)((ctrTop0) * hScale);//
                    c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                    c.Height = (int)(ctrHeight0 * hScale);//
                    ctrlNo++;//累加序号
                             //**放在这里，是先缩放控件本身，后缩放控件的子控件
                    if (c.Controls.Count > 0)
                        AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用

                    if (ctl is System.Windows.Forms.DataGridView)
                    {
                        System.Windows.Forms.DataGridView dgv = ctl as System.Windows.Forms.DataGridView;
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                        int widths = 0;
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            dgv.AutoResizeColumn(i, System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽  
                            widths += dgv.Columns[i].Width;   // 计算调整列后单元列的宽度和                       
                        }
                        if (widths >= ctl.Size.Width)  // 如果调整列的宽度大于设定列宽  
                            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动  
                        else
                            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充  

                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                }


            }
        }
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
                dw = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.GetMinecraftSkin(MojangUUID));
                ONLINEW = Core5.timer(OnQ, 2333);
                ONLINEW.Start();
                System.Drawing.Point point = new System.Drawing.Point(8, 8);
                System.Drawing.Size size = new System.Drawing.Size(8, 8);
                LB.Content = Mojangname;
                loginmode = "mojang";
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                StringBuilder temp = new StringBuilder(500);
                mojangyes = "888";
                WritePrivateProfileString("Mojang", "Mail", Mojang1.Text, ZongW + @"\ConsoleW.qwq");
                WritePrivateProfileString("Mojang", "PassWord", Mojang2.Password, ZongW + @"\ConsoleW.qwq");
                ///Mojang.SetValue("Mail", Mojang1.Text);
                ///Mojang.SetValue("PassWord", Mojang2.Password);
                IDTab.SelectedIndex = 2;
            }
            catch (SquareMinecraftLauncherException ex)
            {
                this.ShowMessageAsync("登陆失败！", ex.Message);

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
        private void OnQw(object ob, EventArgs a)
        {

            if (DIYvar.xzItems[dw].xzwz == "完成")
            {
                System.Drawing.Point point = new System.Drawing.Point(8, 8);
                System.Drawing.Size size = new System.Drawing.Size(8, 8);
                Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                System.Drawing.Image img = i;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                IM.Source = BitmapToBitmapImage(i);

                ///WritePrivateProfileString("wr", "Atoken", refresh_token, File_);

            }
        }
        private async void Tile_Click_8(object sender, RoutedEventArgs e)
        {
            try
            {
                var loading = await this.ShowProgressAsync("正在微软登录", "请稍后...");
                MicrosoftLogin microsoftLogin = new MicrosoftLogin();
                MinecraftLogin minecraftlogin = new MinecraftLogin();
                loading.SetIndeterminate();
                //加载L.Content = "正在登录...";
                try
                {

                    Xbox XboxLogin = new Xbox();

                    var token = microsoftLogin.GetToken(await microsoftLogin.Login(true));
                    wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));
                    string refresh_token = token.refresh_token;
                    ///WR.SetValue("Atoken", refresh_token);
                    WritePrivateProfileString("wr", "Atoken", refresh_token, ZongW + @"\ConsoleW.qwq");

                    await loading.CloseAsync();

                    var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);
                    //dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                    wruuid = Minecraft.uuid;
                    wrname = Minecraft.name;

                    LB.Content = wrname;
                    loginmode = "wr";
                    String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";

                    wryes = "888";

                    dw = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.GetMinecraftSkin(wruuid));
                    ONLINEW = Core5.timer(OnQw, 2333);
                    ONLINEW.Start();
                    IDTab.SelectedIndex = 2;

                    wryes = "888";

                }
                catch
                {

                };
            }
            catch
            {

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
            var loading = await this.ShowProgressAsync("正在微软登录", "请稍后...");
            //加载L.Content = "正在登录...";
            try
            {
                loading.SetIndeterminate();
                Xbox XboxLogin = new Xbox();

                var token = microsoftLogin.GetToken(await microsoftLogin.Login(false));
                wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));
                string refresh_token = token.refresh_token;
                //WR.SetValue("Atoken", refresh_token);
                WritePrivateProfileString("wr", "Atoken", refresh_token, ZongW + @"\ConsoleW.qwq");
                await loading.CloseAsync();

                IDTab.SelectedIndex = 2;
                //dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);

                wruuid = Minecraft.uuid;
                wrname = Minecraft.name;

                LB.Content = wrname;
                loginmode = "wr";
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                IDTab.SelectedIndex = 2;

                wryes = "888";
                wryes = "888";


                Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.GetMinecraftSkin(wruuid));
                ONLINEW = Core5.timer(OnQw, 2333);
                ONLINEW.Start();

                /*
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
                ///WritePrivateProfileString("wr", "Atoken", refresh_token, File_);
                */



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
            ThemeManager.Current.ChangeTheme(this, "Light.Red");
        }
        public String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "2", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Blue");
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "1", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Green");
        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "3", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Purple");
        }

        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "4", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Orange");
        }

        private void Tile_Click_5(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "5", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Lime");

        }

        private void Tile_Click_6(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "6", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Emerald");

        }

        private void Tile_Click_7(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "7", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Teal");
        }

        private void Tile_Click_12(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "8", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Cyan");
        }

        private void Tile_Click_13(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "9", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
        }

        private void Tile_Click_14(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "10", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Indigo");
        }

        private void Tile_Click_16(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "11", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Violet");
        }

        private void Tile_Click_17(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "12", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Pink");
        }

        private void Tile_Click_18(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "13", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Magenta");
        }

        private void Tile_Click_19(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "14", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
        }

        private void Tile_Click_20(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ZTSY", "ZTSY", "16", File_ + @"\FSM.slx");
            ThemeManager.Current.ChangeTheme(this, "Light.Yellow");
        }

        private void Button_Clicw1wk1(object sender, RoutedEventArgs e)
        {
            TT.SelectedIndex = 5;
        }

        private void Button_Cli1cw1wk1(object sender, RoutedEventArgs e)
        {
            String Filew_ = System.AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess().ProcessName;
            WritePrivateProfileString("Start", "Start", "1", ZongW + @"\ConsoleW.qwq");
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
            loginmode = "offline";
            WritePrivateProfileString("OffLine", "ID", OfflineName.Text, ZongW + @"\ConsoleW.qwq");
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
                WritePrivateProfileString("wz", "IP", IP.Text, ZongW + @"\ConsoleW.qwq");
                WritePrivateProfileString("wz", "IDD", IDD.Text, ZongW + @"\ConsoleW.qwq");
                WritePrivateProfileString("wz", "IDDPassWord", IDDPassWord.Password, ZongW + @"\ConsoleW.qwq");
                ///Y.SetValue("IP", IP.Text);
                ///Y.SetValue("IDD", IDD.Text);
                ///Y.SetValue("IPPPassWord", IDDPassWord.Password);
                Yyes = "888";

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

        private void Tile_Click_LXSkin(object sender, RoutedEventArgs e)
        {

        }

        private async void l(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1188);
            });
            TT.SelectedIndex = 1;
        }
    }
}
