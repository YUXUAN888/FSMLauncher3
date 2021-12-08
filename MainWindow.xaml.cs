using ControlzEx.Theming;
using Gac;
using MahApps;
using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.ValueBoxes;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ProjBobcat;
using SquareMinecraftLauncher;
using SquareMinecraftLauncher.Core;
using SquareMinecraftLauncher.Core.fabricmc;
using SquareMinecraftLauncher.Core.OAuth;
using SquareMinecraftLauncher.Minecraft;
using SquareMinecraftLauncher.Minecraft.MCServerPing;
using SquareMinecraftLauncherWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static FSMLauncher_3.Core;
using static FSMLauncher_3.DIYvar;
using SquareMinecraftLauncher;
using System.Windows.Media.Animation;
using static FSMLauncher_3.MyAni;
using System.Windows.Media.Effects;
using EaseMoveDemo;
using FSMLauncher_3.About_List;

namespace FSMLauncher_3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>




    public partial class MetroWindow


    {
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Close();



        }

        public void DownloadSourceInitialization(DownloadSource downloadSource)
        {

        }


        internal ObservableCollection<T> ItemAdd<T>(T[] items)
        {
            ObservableCollection<T> personalInfoList = new ObservableCollection<T>();
            foreach (var i in items)
            {
                personalInfoList.Add(i);
            }
            return personalInfoList;
        }

        private void KillProcess(string processName)
        {
            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName == processName)
                {
                    item.Kill();
                }
            }
        }
        MaterialDesignColor color = new MaterialDesignColor();
        public Gac.DownLoadFile dlf = new DownLoadFile();
        internal static bool iniwv = false;
        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public string IniWriteValue(string Section, string Key, string Value)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            if (iniwv)
            {

                WritePrivateProfileString(Section, Key, Value, File_);
                return Value;
            }
            string a = IniReadValue(Section, Key);
            if (a == "")
            {
                return Value;
            }
            return a;
        }
        [DllImport("IpHlpApi.dll")]
        extern static public uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);

        [DllImport("User32")]
        private extern static int GetWindow(int hWnd, int wCmd);

        [DllImport("User32")]
        private extern static int GetWindowLongA(int hWnd, int wIndx);

        [DllImport("user32.dll")]
        private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static int GetWindowTextLength(IntPtr hWnd);
        private int m_ProcessorCount = 0;
        RegistryKey hkim = Registry.LocalMachine;

        public long PhysicalMemory
        {

            get
            {
                return m_ProcessorCount;
            }
        }
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory"); 
                //foreach (ManagementObject mo in mos.Get()) 
                //{ 
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString()); 
                //} 
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                    }
                }
                return availablebytes;
            }
        }

        public int GetRAM
        {
            get
            {
                int ram;
                int r1;
                r1 = (int)(MemoryAvailable / 1024 / 1024);

                if (r1 <= 1024)
                    ram = r1;
                else
                    ram = r1 - 1024;



                return ram;
            }


        }


        public RegistryKey software;
        public RegistryKey Mojang;
        public RegistryKey Y;
        public RegistryKey LX;
        public RegistryKey WR;
        public RegistryKey XK;
        RegistryKey FSM;
        public static void setTag(System.Windows.Forms.Control cons)
        {

            foreach (System.Windows.Forms.Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;

                if (con.Controls.Count > 0)

                    setTag(con);

            }
        }

        public static void setControls(float newx, float newy, System.Windows.Forms.Control cons)
        {

            foreach (System.Windows.Forms.Control con in cons.Controls)
            {
                try
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });

                    float a = Convert.ToSingle(mytag[0]) * newx;

                    con.Width = (int)a;

                    a = Convert.ToSingle(mytag[1]) * newy;

                    con.Height = (int)(a);

                    a = Convert.ToSingle(mytag[2]) * newx;

                    con.Left = (int)(a);

                    a = Convert.ToSingle(mytag[3]) * newy;

                    con.Top = (int)(a);

                    Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);

                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);

                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
                catch
                {

                }
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
        public String FileOnlineServer = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server";
        public String FileOnlineKEHU = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client";
        public String FileS = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValue(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, File_);
            return temp.ToString();
        }
        public static int ssti;
        public static String Update;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public MetroWindow()
        {





            //Thread WRD1 = new Thread(WRD);
            //WRD1.Start();
            //Thread y = new Thread(yy);
            //y.Start();
            ServicePointManager.DefaultConnectionLimit = 512;
            Update = "Beta2";///每次更新启动器设置，启动器当前版本号
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM");
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";
            StringBuilder temp = new StringBuilder();
            GetPrivateProfileString("ZTSY", "ZTSY", "", temp, 255, File_ + @"\FSM.slx");

            //DownloadSourceInitialization(DownloadSource.MCBBSSource);//改源方法

            this.ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();


            software = hkim.OpenSubKey("SOFTWARE", true);
            FSM = software.CreateSubKey("FSM");
            Mojang = FSM.CreateSubKey("Mojang");
            Y = FSM.CreateSubKey("Y");
            LX = FSM.CreateSubKey("LX");
            WR = FSM.CreateSubKey("WR");
            XK = FSM.CreateSubKey("XK");
            rams.Maximum = MemoryAvailable / 1024 / 1024;
            rams.Value = int.Parse(Bit.Text);
            dminecraft_text.Text = System.AppDomain.CurrentDomain.BaseDirectory + @".minecraft";
            try
            {
                Java_list.SelectedIndex = int.Parse(IniReadValue("Java", "List"));
            }
            catch
            {

            }
            try
            {
                for (int i = 2; i <= int.Parse(IniReadValue("VPath1", "1")); i++)
                {
                    pathlist.Items.Add(IniReadValue("VPath", i.ToString()));
                }
            }
            catch
            {

            }
            try
            {
                if (IniReadValue("Vlist", "Path") == null)
                {

                }
                else
                {
                    pathlist.SelectedItem = int.Parse(IniReadValue("Vlist", "Path"));
                    pathlist.SelectedIndex = int.Parse(IniReadValue("Vlist", "Path"));
                    pathlist.SelectedItems.Add(pathlist.Items[int.Parse(IniReadValue("Vlist", "Path"))]);
                    pathlist.Focus();
                    pathlist.SelectedIndex = int.Parse(IniReadValue("Vlist", "Path"));
                    pathlist.SelectedItems.Add(pathlist.Items[int.Parse(IniReadValue("Vlist", "Path"))]);
                }

                try
                {

                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    if(pathlist.SelectedIndex == 0)
                    {
                        tools.SetMinecraftFilesPath(dminecraft_text.Text);
                        t = tools.GetAllTheExistingVersion();

                        List<DoItem> item1 = new List<DoItem>();
                        for (int i = 0; i < t.Length; i++)
                        {

                            //vlist.Items.Add(t[i]);
                            DoItem item = new DoItem();
                            item.DverV.Content = t[i].version;
                            item1.Add(item);
                        }
                        DIYvar.lw = item1;
                        vlist.ItemsSource = item1.ToArray();
                    }
                    else
                    {
                        string mcPath = (pathlist as ListBox).SelectedItem.ToString();
                        tools.SetMinecraftFilesPath(mcPath);
                        t = tools.GetAllTheExistingVersion();

                        List<DoItem> item1 = new List<DoItem>();
                        for (int i = 0; i < t.Length; i++)
                        {

                            //vlist.Items.Add(t[i]);
                            DoItem item = new DoItem();
                            item.DverV.Content = t[i].version;
                            item1.Add(item);
                        }
                        DIYvar.lw = item1;
                        vlist.ItemsSource = item1.ToArray();
                    }

                }
                catch
                {

                }


            }
            catch
            {

            }
            List<SetsItem> itemw = new List<SetsItem>();
            for (int i = 0;i <= 5;++i)
            {
                if(i == 1)
                {
                    SetsItem item = new SetsItem();
                    item.SetName.Content = "通用";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\通用设置.PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.SetImage.Source = bi;
                    itemw.Add(item);
                }
                else if (i == 2)
                {
                    SetsItem item = new SetsItem();
                    item.SetName.Content = "下载";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\下载 (5).PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.SetImage.Source = bi;
                    itemw.Add(item);
                }
                else if (i == 3)
                {
                    SetsItem item = new SetsItem();
                    item.SetName.Content = "软件更新";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\更新.PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.SetImage.Source = bi;
                    itemw.Add(item);
                }
                else if (i == 4)
                {
                    SetsItem item = new SetsItem();
                    item.SetName.Content = "个性化";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\个性化模板.PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.SetImage.Source = bi;
                    itemw.Add(item);
                }
                else if (i == 5)
                {
                    SetsItem item = new SetsItem();
                    item.SetName.Content = "语言/Lang";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\语言.PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.SetImage.Source = bi;
                    itemw.Add(item);
                }

            }
            SetBox.ItemsSource = itemw.ToArray();
            //this.ShowMessageAsync("欢迎测试(Debug0.4.1)(Beta1铺垫版本)!", "很高兴您能参加FSM3的早期测试(Debug)！\n此版本可以实现：联机的多线程完整初始化，下载列表获取，Mojang完整登录，微软完整登录(免密,正常),关于,个性化,版本列表的算法以及获取,开始向导,内存获取,外置登录,公告获取,启动游戏,启动错误跟踪器,下载补全游戏(不稳定),联机!!,背景音乐,一点点小动画\nDebug版本是功能完全没有开发好的版本，请多多向交流群举报Bug");
            Border border = new Border();
            VisualBrush brush = new VisualBrush();
            brush.Visual = Bod1;
            brush.Stretch = Stretch.Uniform;
            border.Background = brush;
            border.Effect = new BlurEffect()
            {
                Radius = 80,
                RenderingBias = RenderingBias.Performance
            };
border.Margin = new Thickness(-this.Margin.Left, -this.Margin.Top, 0, 0);




            if (IniReadValue("Start", "Start") == "1")
            {

            }
            else
            {
                DIYvar.Main1.ShowDialog();

            }



            /*
            if (Mojang.GetValue("Mail") == null)
            {

            }
            else
            {
                try
                {
                    MojangMail = Mojang.GetValue("Mail").ToString();

                    MojangPassWord = Mojang.GetValue("PassWord").ToString();
                    var login = tools.MinecraftLogin(MojangMail, MojangPassWord);

                    Mojangname = login.name;
                    MojangUUID = login.uuid;
                    MojangToken = login.token;
                    LB.Content = Mojangname;
                    loginmode = "mojang";
                    mojangyes = "888";
                    HttpDownloadFile(tools.GetMinecraftSkin(MojangUUID), System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                    System.Drawing.Point point = new System.Drawing.Point(8, 8);
                    System.Drawing.Size size = new System.Drawing.Size(8, 8);
                    Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                    var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                    Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);

                    //i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                    System.Drawing.Image img = i;

                    IM.Source = BitmapToBitmapImage(i);

                    IDTab.SelectedIndex = 2;
                }
                catch
                {

                }

            }
            */
            List<JavaVersion> aa = tools.GetJavaPath();
            Java_list.ItemsSource = aa;

            NZDM[0] = "FSM的第一个内部版本发布于2021年7月3日!";
            NZDM[1] = "FSM的联机由初梦的端口映射提供";
            NZDM[2] = "FSM的联机测试最低延迟是18ms!";
            NZDM[3] = "把鼠标停留在一些按钮上，你可以看到它的提示";
            NZDM[4] = "作者在启动器藏有3+个彩蛋！";
            NZDM[5] = "在版本列表右键版本就可以转到这个版本的设置！";
            NZDM[6] = "FSM最早在2020年2月发布";

            //获取UUID  MessageBox.Show(KMCCC.Pro.Modules.MojangAPI.MojangAPI.NameToUUID("CHINA_YUXUAN").ToString());

            tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);


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
                String pageData3 = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=notice")); //从指定网站下载数据
                String pageHtml3 = pageData3;
                after = pageHtml.Split(new char[] { '|' });
                UpLog_and_GG = pageHtml1.Split(new char[] { '|' });
                UpdateD = pageHtml2;
                String pageData4 = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=force")); //从指定网站下载数据
                String pageHtml4 = pageData4;
            
                String pageData5 = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=version")); //从指定网站下载数据
                String pageHtml5 = pageData5;
                if (after[0] == "true")
                {
                    this.ShowMessageAsync("检测到启动器有新的版本！", "新版本为:" + after[4] + "\n" + "请到设置进行更新！");
                    Thread th = new Thread(ThreadSendKey);
                    th.Start(); //启动线程 

                }
                else if(pageHtml4 == "true" && pageHtml5 != Update)
                {
                    this.ShowMessageAsync("检测到启动器有新的版本！", "新版本为:" + after[4] + "\n" + "请到设置进行更新！");
                    Thread th = new Thread(ThreadSendKey);
                    th.Start(); //启动线程

                }
                else
                {
                    UpdateButton.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            catch
            {
                UpdateButton.Visibility = System.Windows.Visibility.Hidden;
                this.ShowMessageAsync("未能与FSM服务器建立通信", "这可能是是网络未连接导致" + "\n" + "可能会导致无法下载游戏，无法更新启动器等问题");
            }
            //StringBuilder temp = new StringBuilder();
            //GetPrivateProfileString("ZTSY", "ZTSY", "", temp, 255, IntFilePath);

            Bit.Text = GetRAM.ToString();
            if (temp.ToString() == "1")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Green");
                WritePrivateProfileString("ZTSY", "ZTSY", "1", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Red;
                Java_list_Copy.SelectedIndex = 1;
            }
            else if (temp.ToString() == "2")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
                WritePrivateProfileString("ZTSY", "ZTSY", "2", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Blue;
                Java_list_Copy.SelectedIndex = 2;
            }
            else if (temp.ToString() == "3")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Purple");
                WritePrivateProfileString("ZTSY", "ZTSY", "3", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Purple;
                Java_list_Copy.SelectedIndex = 3;
            }
            else if (temp.ToString() == "4")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Orange");
                WritePrivateProfileString("ZTSY", "ZTSY", "4", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Orange;
                Java_list_Copy.SelectedIndex = 4;

            }
            else if (temp.ToString() == "5")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Lime");
                WritePrivateProfileString("ZTSY", "ZTSY", "5", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Lime;
                Java_list_Copy.SelectedIndex = 5;
            }
            else if (temp.ToString() == "6")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Emerald");
                WritePrivateProfileString("ZTSY", "ZTSY", "6", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Amber;
                Java_list_Copy.SelectedIndex = 6;
            }
            else if (temp.ToString() == "7")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Teal");

                WritePrivateProfileString("ZTSY", "ZTSY", "7", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Teal;
                Java_list_Copy.SelectedIndex = 7;
            }
            else if (temp.ToString() == "8")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cyan");
                WritePrivateProfileString("ZTSY", "ZTSY", "8", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Cyan;
                Java_list_Copy.SelectedIndex = 8;
            }
            else if (temp.ToString() == "9")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
                WritePrivateProfileString("ZTSY", "ZTSY", "9", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.BlueGrey;
                Java_list_Copy.SelectedIndex = 9;
            }
            else if (temp.ToString() == "10")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Indigo");
                WritePrivateProfileString("ZTSY", "ZTSY", "10", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Indigo;
                Java_list_Copy.SelectedIndex = 10;
            }
            else if (temp.ToString() == "11")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Violet");
                WritePrivateProfileString("ZTSY", "ZTSY", "11", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Grey;
                Java_list_Copy.SelectedIndex = 11;
            }
            else if (temp.ToString() == "12")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Pink");
                WritePrivateProfileString("ZTSY", "ZTSY", "12", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Pink;
                Java_list_Copy.SelectedIndex = 12;
            }
            else if (temp.ToString() == "13")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Magenta");
                WritePrivateProfileString("ZTSY", "ZTSY", "13", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.DeepPurple;
                Java_list_Copy.SelectedIndex = 13;
            }
            else if (temp.ToString() == "14")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
                Java_list_Copy.SelectedIndex = 14;
                WritePrivateProfileString("ZTSY", "ZTSY", "14", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Grey;
            }
            else if (temp.ToString() == "15")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Amber");
                WritePrivateProfileString("ZTSY", "ZTSY", "15", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Amber;
                Java_list_Copy.SelectedIndex = 15;
            }
            else if (temp.ToString() == "16")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Yellow");
                WritePrivateProfileString("ZTSY", "ZTSY", "16", File_ + @"\FSM.slx");
                MaterialDesignThemes.Wpf.BundledTheme a1a = new MaterialDesignThemes.Wpf.BundledTheme();
                a1a.PrimaryColor = PrimaryColor.Yellow;
                Java_list_Copy.SelectedIndex = 16;
            }
            try
            {
                if (IniReadValue("Vlist", "V") == "")
                {
                    NowV.Content = "当前没有版本";
                }
                else
                {

                    vlist.SelectedIndex = int.Parse(IniReadValue("Vlist", "V"));
                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    string mcPath = (pathlist as ListBox).SelectedItem.ToString();
                    tools.SetMinecraftFilesPath(mcPath);
                    t = tools.GetAllTheExistingVersion();
                    NowV.Content = t[vlist.SelectedIndex].version;

                }
            }
            catch
            {

            }
            //Bit.Text = tools.GetMemorySize(Java_list.Text).TotalMemory.ToString();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public string wratoken;

        private void WRD()
        {



            Application.Current.Dispatcher.Invoke(
    async delegate
     {
         //Code


         if (WR.GetValue("Atoken") == null)
         {

         }
         else
         {
             String Minecraft_Token;
             try
             {
                 MicrosoftLogin microsoftLogin = new MicrosoftLogin();
                 Xbox XboxLogin = new Xbox();
                 await Task.Run(() =>
                 {
                     Minecraft_Token = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(microsoftLogin.RefreshingTokens(WR.GetValue("Atoken").ToString()))));
                     MinecraftLogin minecraftlogin = new MinecraftLogin();
                     var Minecraft = minecraftlogin.GetMincraftuuid(Minecraft_Token);
                     IDTab.SelectedIndex = 2;
                     wruuid = Minecraft.uuid;
                     wrname = Minecraft.name;
                     wrtoken = Minecraft_Token;
                 });
                 
                 
                 
                 LB.Content = wrname;
                 loginmode = "wr";
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
                 IM.Source = BitmapToBitmapImage(i);
                 wryes = "888";

             }
             catch
             {

             }
         }







     });





        }
        int dw;
        private void MMJ()
        {



            Application.Current.Dispatcher.Invoke(
       delegate
     {
         //Code
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
             ///WritePrivateProfileString("Mojang", "Mail", Mojang1.Text, File_);
             ///WritePrivateProfileString("Mojang", "PassWord", Mojang2.Password, File_);
             Mojang.SetValue("Mail", Mojang1.Text);
             Mojang.SetValue("PassWord", Mojang2.Password);
             IDTab.SelectedIndex = 2;
         }
         catch (SquareMinecraftLauncherException ex)
         {
             this.ShowMessageAsync("登陆失败！", ex.Message);

         }

     });
        }









        private void WR1()
        {



            Application.Current.Dispatcher.Invoke(
        async delegate
     {
         //Code
         




     });




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
        public string MojangMail, MojangPassWord;
        private void MojangL()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code

     });




        }
        private void OffLine1()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         

     });




        }
        private void yy()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         try
         {
             if (Y.GetValue("IDD") == null)
             {
                 ///////////////////////////////////////////////////
             }
             else
             {
                 string yip = Y.GetValue("IP").ToString();
                 string yidd = Y.GetValue("IDD").ToString();
                 string yiddp = Y.GetValue("IPPPassWord").ToString();
                 skin = tools.GetAuthlib_Injector(yip, yidd, yiddp);
                 Ylist.ItemsSource = skin.NameItem;
                 //IDTab.SelectedIndex = 4;
                 Ylist.SelectedIndex = int.Parse(IniReadValue("Y", "Ylist"));
                 JS.Text = skin.NameItem[vlist.SelectedIndex].Name;
                 Yyes = "888";
                 Yuuid = skin.NameItem[Ylist.SelectedIndex].uuid;
                 Yname = skin.NameItem[Ylist.SelectedIndex].Name;
                 Ytoken = skin.accessToken;
             }

         }
         catch
         {

         }

     });




        }
        private void KillProcessw(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                try
                {
                    // 杀掉这个进程。
                    process.Kill();

                    // 等待进程被杀掉。你也可以在这里加上一个超时时间（毫秒整数）。
                    process.WaitForExit();
                }
                catch (Win32Exception ex)
                {
                    // 无法结束进程，可能有很多原因。
                    // 建议记录这个异常，如果你的程序能够处理这里的某种特定异常了，那么就需要在这里补充处理。
                    // Log.Error(ex);
                }
                catch (InvalidOperationException)
                {
                    // 进程已经退出，无法继续退出。既然已经退了，那这里也算是退出成功了。
                    // 于是这里其实什么代码也不需要执行。
                }
            }
        }
        private float XX;

        private float YY;

        private void OffLine2()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         

     });




        }
        static int wa;
        private void UpdateLauncher()
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {
         //Code
         加载L.Content = "正在为您准备新的启动器!";
         Tab1.SelectedIndex = 6;
         String File_ = System.AppDomain.CurrentDomain.BaseDirectory + "[Update]FSM.exe";
         Tab1.SelectedIndex = 6;
         wa = Download(File_, "QWQ", UpdateD);
         UPDATEW = Core5.timer(UPDATEWW, 2333);
         UPDATEW.Start();
         ///string aa = DIYvar.xzItems[wa].xzwz;
         //HttpDownloadFile(UpdateD, File_, 6, Tab1);




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

            Tab1.SelectedIndex = 7;

            var palette = new PaletteHelper();








        }








        MCVersionList[] mc = new MCVersionList[0];


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            IDTab.SelectedItem = 3;







        }


        private void Java_list_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM";

            if (Java_list_Copy.SelectedIndex == 0)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "0", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Red");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Red;
            }
            else if (Java_list_Copy.SelectedIndex == 1)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "1", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Green");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Green;
            }
            else if (Java_list_Copy.SelectedIndex == 2)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "2", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();

                aa.PrimaryColor = PrimaryColor.Blue;
            }
            else if (Java_list_Copy.SelectedIndex == 3)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "3", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Purple");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Purple;
            }
            else if (Java_list_Copy.SelectedIndex == 4)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "4", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Orange");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Orange;
            }
            else if (Java_list_Copy.SelectedIndex == 5)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "5", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Lime");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Lime;
            }
            else if (Java_list_Copy.SelectedIndex == 6)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "6", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Emerald");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Amber;
            }
            else if (Java_list_Copy.SelectedIndex == 7)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "7", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Teal");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Teal;
            }
            else if (Java_list_Copy.SelectedIndex == 8)
            {

                WritePrivateProfileString("ZTSY", "ZTSY", "8", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Cyan");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Cyan;
            }
            else if (Java_list_Copy.SelectedIndex == 9)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "9", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Amber;
            }
            else if (Java_list_Copy.SelectedIndex == 10)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "10", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Indigo");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Indigo;
            }
            else if (Java_list_Copy.SelectedIndex == 11)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "11", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Violet");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Amber;
            }
            else if (Java_list_Copy.SelectedIndex == 12)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "12", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Pink");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Pink;
            }
            else if (Java_list_Copy.SelectedIndex == 13)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "13", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Magenta");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.DeepPurple;
            }
            else if (Java_list_Copy.SelectedIndex == 14)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "14", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Cyan;
            }
            else if (Java_list_Copy.SelectedIndex == 15)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "15", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Amber");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Amber;
            }
            else if (Java_list_Copy.SelectedIndex == 16)
            {
                WritePrivateProfileString("ZTSY", "ZTSY", "16", File_ + @"\FSM.slx");
                ThemeManager.Current.ChangeTheme(this, "Light.Yellow");
                MaterialDesignThemes.Wpf.BundledTheme aa = new MaterialDesignThemes.Wpf.BundledTheme();
                aa.PrimaryColor = PrimaryColor.Yellow;
            }
        }







        private string IniFilePath;


        private void Java_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WritePrivateProfileString("Java", "List", Java_list.SelectedIndex.ToString(), FileS);
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

        private async void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 1;
            yxlx.SelectedIndex = 0;
            //await this.ShowMessageAsync("下载提示", "现在最好别用FSM的下载\n因为还不稳定,FSM会卡死");
            //  Thread.Sleep(111);
            ForgeB.BeginAnimation(WidthProperty, CatC(0, 149, 0.8));
            //Thread.Sleep(555);
            FabricB.BeginAnimation(WidthProperty, CatC(0, 149, 1));
            //Thread.Sleep(555);
            OptifineB.BeginAnimation(WidthProperty, CatC(0, 149, 1.2));
            //Thread.Sleep(555);
            LiteB.BeginAnimation(WidthProperty, CatC(0, 149, 1.4));
            StartDownLoad.BeginAnimation(WidthProperty, CatC(0, 183.2,1.5)) ;
            Tools tools = new Tools();
            MCVersionList[] mc = new MCVersionList[0];
            try
            {
                mc = await tools.GetMCVersionList();
            }
            catch
            {

                await this.ShowMessageAsync("未获取到游戏下载列表，请重试", "请检查网络，或重试一遍");

                return;
            }
            
            foreach (var i in mc)
            {
                switch (i.type)
                {
                    case "正式版":
                        McVersionList a = new McVersionList(i.id, i.type);
                        DIYvar.minecraft1.Add(a);
                        break;
                    case "快照版":
                        McVersionList b = new McVersionList(i.id, i.type);
                        DIYvar.minecraft2.Add(b);
                        break;
                    case "基岩版":
                        McVersionList c = new McVersionList(i.id, "早期测试");
                        DIYvar.minecraft3.Add(c);
                        break;
                    case "远古版":
                        McVersionList d = new McVersionList(i.id, i.type);
                        DIYvar.minecraft4.Add(d);
                        break;
                }
                var DT = DateTime.Parse(i.releaseTime);
                if (DT.ToString("MM-dd") == "04-01")
                {
                    McVersionList s = new McVersionList(i.id, "愚人节版本");
                    DIYvar.minecraft5.Add(s);
                }

            }
            mcVersionLists = DIYvar.minecraft1.ToArray();
            List<Item> item1 = new List<Item>();


            for (int i = 0; i < mcVersionLists.Length; i++)
            {
                Item item = new Item();
                item.Dver.Text = mcVersionLists[i].version;
                item1.Add(item);
            }
            DIYvar.l = item1;
            MCV.ItemsSource = item1.ToArray();


        }


        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            SetBox.SelectedIndex = 0;
            Java_list.BeginAnimation(WidthProperty, CatC(0, 396, 0.8));
            //Thread.Sleep(555);
            //Bit.BeginAnimation(WidthProperty, CatC(0, 486.4, 1));
            //Thread.Sleep(555);
            rams.BeginAnimation(WidthProperty, CatC(0, 489, 0.9));
            //Thread.Sleep(555);
            SDXZ.BeginAnimation(WidthProperty, CatC(0, 86, 1.1));
            //StartDownLoad.BeginAnimation(WidthProperty, CatC(0, 183.2, 1.01));
            Tab1.SelectedIndex = 2;
        }
        private void oonn(DownMsg msg)
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                加载L.Content = "正在为第一次联机做准备...";
                Tab1.SelectedIndex = 6;
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\frpc.exe";
                String File__ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client\frpc.exe";
                String File___ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\StartForFSM.zip";
                String File____ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client\StartForFSM.zip";
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server");
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client");



                int did;
                DownStatus tag = msg.Tag;
                did = Download(File_, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=Mjd8ZDNlYjgyZmR8MTYzMzI3NzMxOHwxfDI4");
                did = Download(File__, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=Mjh8NmE4MDM1MTN8MTYzMzI3NzQ1NHwxfDI5");
                did = Download(File___, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=MzB8ZTU1YjYyYmJ8MTYzMzI3NzYwM3wxfDMw");
                did = Download(File____, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=MzB8ZTU1YjYyYmJ8MTYzMzI3NzYwM3wxfDMw");

                Gac.DownLoadFile downLoadFile = new Gac.DownLoadFile();
                if (tag == DownStatus.End)
                {
                    Tab1.SelectedIndex = 3;
                }
            });



        }
        private void Tile_Click_4(object sender, RoutedEventArgs e, DownMsg msg)
        {

            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM");
            dlf.doSendMsg += new DownLoadFile.dlgSendMsg(oonn);
            StringBuilder temp1 = new StringBuilder();








            //Thread th = new Thread(OnLine());
            //th.Start(); //启动线程 







        }

        public void Exit()
        {
            System.Environment.Exit(0);

        }
        public void FooClosed(object sender, System.EventArgs e)

        {
            System.Environment.Exit(0);
        }


        private void OnLine(object sender, RoutedEventArgs e, DownMsg msg)
        {



            Application.Current.Dispatcher.Invoke(
     delegate
     {

         //Code
         加载L.Content = "正在为第一次联机做准备...";
         Tab1.SelectedIndex = 6;
         String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\frpc.exe";
         String File__ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client\frpc.exe";
         String File___ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\StartForFSM.zip";
         String File____ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client\StartForFSM.zip";
         Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server");
         Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client");



         int did;
         DownStatus tag = msg.Tag;
         did = Download(File_, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=Mjd8ZDNlYjgyZmR8MTYzMzI3NzMxOHwxfDI4");
         did = Download(File__, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=Mjh8NmE4MDM1MTN8MTYzMzI3NzQ1NHwxfDI5");
         did = Download(File___, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=MzB8ZTU1YjYyYmJ8MTYzMzI3NzYwM3wxfDMw");
         did = Download(File____, "", "http://yuxuanbbs.cn/forum.php?mod=attachment&aid=MzB8ZTU1YjYyYmJ8MTYzMzI3NzYwM3wxfDMw");

         Gac.DownLoadFile downLoadFile = new Gac.DownLoadFile();
         if (did == 1)
         {
             Tab1.SelectedIndex = 3;
         }



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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 6;

            Thread th = new Thread(UpdateLauncher);
            th.Start(); //启动线程 


        }
        private void UPDATEWW(object ob, EventArgs a)
        {

            string aa = DIYvar.xzItems[wa].xzwz;
            //HttpDownloadFile(UpdateD, File_, 6, Tab1);
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + "[Update]FSM.exe";


            if (aa == "完成")
            {


                OpenFile(File_);
                Close();
            }




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



        //public List iClassList { get; set; }


        private void Button_Click_10(object sender, RoutedEventArgs e)
        {


            OpenFileDialog fileDialog = new OpenFileDialog();//提示用户打开文件窗体
            fileDialog.Title = "选择Java路径";//文件对话框标题
            fileDialog.Filter = "Java路径|*javaw.exe";//文件格式筛选字符串
            if (fileDialog.ShowDialog() == true)//判断对话框返回值，点击打开
            {
                //fileDialog.FileName.ToString()

                Java_list.IsEditable = false;

                //listBoxSS.DataContext = this;
                Java_list.Text = fileDialog.FileName.ToString();
                //MessageBox.Show(fileDialog.FileName.ToString());


            }
        }

        private void Tile_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_11(object sender, RoutedEventArgs e)
        {

        }








        private void OffLinebaocun_Click(object sender, RoutedEventArgs e)
        {
            
            //开发中...


        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        internal static McVersionList[] mcVersionLists = new McVersionList[0];
        private void yxlx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (yxlx.SelectedIndex != -1)
                {
                    switch (yxlx.SelectedIndex)
                    {
                        case 0:

                            mcVersionLists = DIYvar.minecraft1.ToArray();
                            List<Item> item1 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E885BB0.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "正式版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                item1.Add(item);
                            }
                            DIYvar.l = item1;
                            MCV.ItemsSource = item1.ToArray();
                            break;
                        case 1:
                            mcVersionLists = DIYvar.minecraft2.ToArray();
                            List<Item> item11 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                item.Dimage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/0E8348D8.PNG"));
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E8348D8.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "快照版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                item11.Add(item);
                            }
                            DIYvar.l = item11;
                            MCV.ItemsSource = item11.ToArray();
                            break;
                        case 2:
                            mcVersionLists = DIYvar.minecraft3.ToArray();
                            List<Item> item111 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E8DB058.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "早期测试";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                //item.Dimage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/0E8DB058.PNG"));
                                item111.Add(item);
                            }
                            DIYvar.l = item111;
                            MCV.ItemsSource = item111.ToArray();
                            break;
                        case 3:
                            mcVersionLists = DIYvar.minecraft4.ToArray();
                            List<Item> item1111 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E830D70.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "远古版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                //item.Dimage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/0E830D70.PNG"));
                                item1111.Add(item);
                            }
                            DIYvar.l = item1111;
                            MCV.ItemsSource = item1111.ToArray();
                            break;
                        case 4:
                            mcVersionLists = DIYvar.minecraft5.ToArray();
                            List<Item> item11111 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E826620.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "愚人节版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                item11111.Add(item);
                            }
                            DIYvar.l = item11111;
                            MCV.ItemsSource = item11111.ToArray();
                            break;
                    }
                    if (MCV != null)
                    {
                        List<Item> item1 = new List<Item>();


                        for (int i = 0; i < mcVersionLists.Length; i++)
                        {
                            Item item = new Item();
                            item.Dver.Text = mcVersionLists[i].version;
                            BitmapImage bi = new BitmapImage();
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            bi.BeginInit();
                            //bi.UriSource = new Uri(@"\Image\0E885BB0.PNG", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            //item.Dimage.Source = bi;
                            item1.Add(item);
                        }
                        DIYvar.l = item1;
                        MCV.ItemsSource = item1.ToArray();
                        //MCV.ItemsSource = ItemAdd(mcVersionLists);
                    }


                }

            }
            catch
            {

            }



        }

        private void MCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
        public bool KillProcExec(int procId)
        {
            string cmd = string.Format("taskkill /f /t /im {0}", procId); //强制结束指定进程

            Process ps = null;
            try
            {
                //ps = ExecCmd();
                ps.Start();
                ps.StandardInput.WriteLine(cmd + "&exit");
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                ps.Close();
            }


            return false;
        }
        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("fprc");
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }
            System.Diagnostics.Process[] process11 = System.Diagnostics.Process.GetProcessesByName("frpc.exe");
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }
            string criteriaOne = "frpc.exe";

            string str = null;
            try
            {
                foreach (processId1 item in GetProcessId(criteriaOne))
                {
                    KillProcessid(item.processId01);
                    str += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str = "Error";
            }
            try
            {
                foreach (processId1 item in GetProcessId(criteriaOne))
                {
                    KillProcessid(item.processId01);
                    str += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str = "Error";
            }
            try
            {
                foreach (processId1 item in GetProcessId(criteriaOne))
                {
                    KillProcessid(item.processId01);
                    str += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str = "Error";
            }
            try
            {
                foreach (processId1 item in GetProcessId(criteriaOne))
                {
                    KillProcessid(item.processId01);
                    str += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str = "Error";
            }
            try
            {
                foreach (processId1 item in GetProcessId(criteriaOne))
                {
                    KillProcessid(item.processId01);
                    str += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str = "Error";
                try
                {
                    foreach (processId1 item in GetProcessId(criteriaOne))
                    {
                        KillProcessid(item.processId01);
                        str += "已结束   " + item.processId01 + "\n";
                    }
                }
                catch
                {

                    str = "Error";

                }
                try
                {
                    foreach (processId1 item in GetProcessId(criteriaOne))
                    {
                        KillProcessid(item.processId01);
                        str += "已结束   " + item.processId01 + "\n";
                    }
                }
                catch
                {

                    str = "Error";

                }
                try
                {
                    foreach (processId1 item in GetProcessId(criteriaOne))
                    {
                        KillProcessid(item.processId01);
                        str += "已结束   " + item.processId01 + "\n";
                    }
                }
                catch
                {

                    str = "Error";
                }
            }
            string criteriaOne1 = "FSM3.exe";

            string str1 = null;
            try
            {
                foreach (processId1 item in GetProcessId(criteriaOne1))
                {
                    KillProcessid(item.processId01);
                    str1 += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str1 = "Error";
            }
            try
            {
                foreach (processId1 item in GetProcessId("frpc.exe"))
                {
                    KillProcessid(item.processId01);
                    str1 += "已结束   " + item.processId01 + "\n";
                }
            }
            catch
            {

                str1 = "Error";
            }
            System.Diagnostics.Process[] process121 = System.Diagnostics.Process.GetProcessesByName("frpc");
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }
            System.Diagnostics.Process[] process122221 = System.Diagnostics.Process.GetProcessesByName("FSM3");
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }
            System.Diagnostics.Process[] process2222121 = System.Diagnostics.Process.GetProcessesByName("frpc.exe");
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }
            KillProcessw("frpc.exe");
            KillProcessw("frpc");
            KillProcessw("FSM3");
            System.Environment.Exit(0);
        }
        //loginmode 都为小写！"mojang" "wr" "offline"
        //ly来源，path保存路径，url下载地址

        public static int id = 0;
        internal int Download(string path, string ly, string url)
        {

            dlf.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);//增加下载
            dlf.StartDown(3);//开始下载
            id++;
            xzItem xzItem = new xzItem(System.IO.Path.GetFileName(path), 0, ly, "等待中", url, path);
            xzItems.Add(xzItem);


            return id - 1;
        }

        public void SendMsgHander(DownMsg msg)
        {

            Dispatcher.Invoke((Action)delegate ()
            {
                DownStatus tag = msg.Tag;

                if (tag == DownStatus.Start)
                {
                    xzItems[msg.Id].xzwz = "开始下载";

                    return;
                }
                if (tag == DownStatus.End)
                {
                    xzItems[msg.Id].xzwz = "完成";

                    xzItems[msg.Id].Template = 100;

                    return;
                }
                if (tag == DownStatus.Error)
                {
                    xzItems[msg.Id].xzwz = msg.ErrMessage;



                    return;
                }
                if (tag == DownStatus.DownLoad)
                {
                    xzItems[msg.Id].xzwz = "下载中";

                    xzItems[msg.Id].Template = msg.Progress;

                    Console.WriteLine("test");

                    return;
                }
            });
        }
        private void IniTest()
        {

            

            


        }
        public System.Windows.Media.Animation.DoubleAnimation CatC (int From,double To,double Time)
        {
            var widthAnimation = new DoubleAnimation()
            {
                From = From,
                To = To,
                Duration = TimeSpan.FromSeconds(Time),
                EasingFunction = new BackEase()
                {
                    Amplitude = 0.43,
                    EasingMode = EasingMode.EaseOut,
                },
            };
            return widthAnimation;
        }
        internal static bool JarTimerBool = false;
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();
        /// <summary>
        /// 程序等待延迟执行
        /// </summary>
        /// <param name="ms"></param>
        static void MySleep(uint ms)
        {
            uint start = GetTickCount();
            while (GetTickCount() - start < ms)
            {
                //Application.DoEvents();
            }
        }
        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
            
            
            OpenOnline.Visibility = Visibility.Hidden;
            dfjr.Visibility = Visibility.Visible;
            try
            {
                File.Delete(FileOnlineServer + @"\frpc.ini");
                File.Delete(FileOnlineKEHU + @"\frpc.ini");
            }
            catch
            {

            }

            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\frpc.exe";
            String File__ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client\frpc.exe";
            if (File.Exists(File_) == true && File.Exists(File__) == true)
            {
                Tab1.SelectedIndex = 3;
                Stat.BeginAnimation(WidthProperty, CatC(0, 634,0.8));
                
                //OpenOnline.Visibility = Visibility.Visible;
             //   OpenOnline.BeginAnimation(WidthProperty, CatC(0, 634));
                //Thread.Sleep(666);
                OnlineTEXT.BeginAnimation(WidthProperty, CatC(0, 634, 0.8));
            }
            else
            {
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);

                加载L.Content = "正在为第一次联机做准备...";
                Tab1.SelectedIndex = 6;

                String File___ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server\StartForFSM.exe";
                String File____ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client\StartForFSM.exe";
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Server");
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Client");



                did1 = Download(File_, "OnLine", "http://www.baibaoblog.cn:81/frpc/Server/frpc.exe");
                did2 = Download(File__, "OnLine", "http://www.baibaoblog.cn:81/frpc/Client/frpc.exe");
                did3 = Download(File___, "OnLine", "http://www.baibaoblog.cn:81/frpc/Client/StartForFSM.exe");
                did4 = Download(File____, "OnLine", "http://www.baibaoblog.cn:81/frpc/Client/StartForFSM.exe");
                ONLINEW = Core5.timer(OnLineI, 5555);
                ONLINEW.Start();
                Gac.DownLoadFile downLoadFile = new Gac.DownLoadFile();
                JarTimerBool = true;


            }



        }

        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
        static System.Windows.Threading.DispatcherTimer ONLINEW = new System.Windows.Threading.DispatcherTimer();
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        static int did1, did2, did3, did4;
        private void Test_Resize(object sender, EventArgs e)
        {
            if (XX == 0)
            {
                //load(new object(), new EventArgs());
            }
            else
            {
                float newx = (float)(Width / XX);

                float newy = (float)(Height / YY);

                //setControls(newx, newy);
            }
        }
        private async void load(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                
                String pageData = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://yuxuanbbs.cn/FSM/FSMGX.txt")); //从指定网站下载数据
                String pageHtml = pageData;
                
                byte[] c = Convert.FromBase64String(pageHtml);
                String ww = System.Text.Encoding.Default.GetString(c);
                string ggxx = IniReadValue("GG", "DQGG");
                if (ggxx == ww)
                {

                }
                else
                {
                    this.ShowModalMessageExternal("新公告", "公告内容:" + ww);
                    WritePrivateProfileString("GG", "DQGG", ww, FileS);
                }
            }
            catch
            {

            }
            //this.Test_Resize += new EventHandler(Test_Resize);
            MetroDialogSettings settings = new MetroDialogSettings();
            if(XK.GetValue("XK") == null )
            {
                settings.NegativeButtonText = "同意";
                settings.AffirmativeButtonText = "拒绝";
                settings.FirstAuxiliaryButtonText = "阅读用户协议与免责声明";
                var loading = await this.ShowMessageAsync("用户协议与免责声明", "同意以使用本软件", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, settings);
                if (loading != MessageDialogResult.Negative)
                {
                    //MessageBox.Show("不同意"); ;
                    System.Diagnostics.Process.Start("https://www.kancloud.cn/yu_xuan/fsm3_/2548731");
                    Close();
                }
                else if (loading != MessageDialogResult.Affirmative)
                {
                    //MessageBox.Show("同意");
                    XK.SetValue("XK", "1");

                }
            }
            else
            {
                
            }
            
            
            
            
            XX = (float)this.Width;

            YY = (float)this.Height;

            //setTag(this);

            Test_Resize(new object(), new EventArgs());
            //asc.controllInitializeSize(this);
            if (IniReadValue("Music", "lj") == null && IniReadValue("Music", "lj") == "")
            {
                yyxhbf = 1;
                bjyybf.IsChecked = false;
            }
            else
            {
                try
                {
                    string Music1 = IniReadValue("Music", "lj");
                    xzdyy.Text = Music1;
                    yyxhbf = int.Parse(IniReadValue("Music", "XHBF"));
                    if (yyxhbf == 9)
                    {
                        XHBFw.IsOn = true;
                    }
                    else
                    {
                        XHBFw.IsOn = false;
                    }
                    bjyybf.IsChecked = true;
                    PlaySound(xzdyy.Text, 0, yyxhbf);
                }
                catch
                {

                }
            }
                

                Thread WRD1 = new Thread(WRD);
                WRD1.Start();
                Thread y = new Thread(yy);
                y.Start();
                if (Mojang.GetValue("Mail") == null)
                {

                }
                else
                {
                    try
                    {
                        MojangMail = Mojang.GetValue("Mail").ToString();

                        MojangPassWord = Mojang.GetValue("PassWord").ToString();
                        var login = tools.MinecraftLogin(MojangMail, MojangPassWord);

                        Mojangname = login.name;
                        MojangUUID = login.uuid;
                        MojangToken = login.token;
                        LB.Content = Mojangname;
                        loginmode = "mojang";
                        mojangyes = "888";
                        HttpDownloadFile(tools.GetMinecraftSkin(MojangUUID), System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                        System.Drawing.Point point = new System.Drawing.Point(8, 8);
                        System.Drawing.Size size = new System.Drawing.Size(8, 8);
                        Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                        var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                        Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);

                        //i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                        System.Drawing.Image img = i;

                        IM.Source = BitmapToBitmapImage(i);

                        IDTab.SelectedIndex = 2;
                    }
                    catch
                    {

                    }

                
            }
        }
        public string wrtoken;
        public string wruuid;
        public string wrname;
        private async void Tile_Click_8(object sender, RoutedEventArgs e)
        {
            //免密登录

            try
            {
                var loading = await this.ShowProgressAsync("正在微软登录", "请稍后...");
                MicrosoftLogin microsoftLogin = new MicrosoftLogin();
                    MinecraftLogin minecraftlogin = new MinecraftLogin();
                 loading.SetIndeterminate();
                    加载L.Content = "正在登录...";
                    try
                    {

                        Xbox XboxLogin = new Xbox();
                    
                        var token = microsoftLogin.GetToken(await microsoftLogin.Login(true));
                        wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));
                        string refresh_token = token.refresh_token;
                        WR.SetValue("Atoken", refresh_token);
                   

                    await loading.CloseAsync();

                        var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);
                    dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                    wruuid = Minecraft.uuid;
                        wrname = Minecraft.name;

                        LB.Content = wrname;
                        loginmode = "wr";
                        String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";

                        wryes = "888";
                        
                        dw = Download( System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png","", tools.GetMinecraftSkin(wruuid));
                    ONLINEW = Core5.timer(OnQw, 2333);
                    ONLINEW.Start();
                    IDTab.SelectedIndex = 2;

                    wryes = "888";

                    }
                    catch
                    {

                    };
                 
                
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void mojang(object sender, RoutedEventArgs e)
        {

        }



        private void Tile_Click_9(object sender, RoutedEventArgs e)
        {
            IDTab.SelectedIndex = 1;
            loginmode = "offline";
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
                loginmode = "";
            }


        }

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
                loginmode = "";
            }

        }
        public string mojangyes;
        public string wryes;
        public string lxyes;
        private void login_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void login_Loadedmj(object sender, RoutedEventArgs e)
        {

        }
        public static string[] NZDM = new string[8];

        private async void Button_Click_start(object sender, RoutedEventArgs e)
        {
            Random ra = new Random();

            SquareMinecraftLauncher.Minecraft.Game game = new SquareMinecraftLauncher.Minecraft.Game();//声明对象
            var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
            switch (loginmode)
            {
                case "mojang":
                    try
                    {
                        try
                        {
                            //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n"+"你知道吗？"+NZDM[ra.Next(0, 6)]);
                            loading.SetIndeterminate();

                            await game.StartGame(NowV.Content.ToString(), Java_list.Text, int.Parse(Bit.Text), Mojangname, MojangUUID, MojangToken, JVM.Text, EY.Text);
                            await loading.CloseAsync();
                            
                        }
                        catch(Exception ex)
                        {
                            await loading.CloseAsync();
                            
                            Ejava.Content = Java_list.Text;
                            ERAM.Content = "RAM：" + Bit.Text;
                            EFSMV.Content = Update;
                            EJVM.Content = "JVM:" + JVM.Text;
                            DGYC.Content = ex.Message;
                            YCPC.Text = ex.ToString();
                            Tab1.SelectedIndex = 5;
                        }
                    }
                    catch (Exception ex)
                    {
                        YCPC.Text = ex.ToString();
                    }
                    break;
                case "wr":
                    try
                    {
                        try
                        {
                            //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
                            loading.SetIndeterminate();
                            await game.StartGame(NowV.Content.ToString(), Java_list.Text, int.Parse(Bit.Text), wrname, wruuid, wrtoken, JVM.Text, EY.Text);
                            
                            
                            await loading.CloseAsync();
                        }
                        catch(Exception ex)
                        {
                            await loading.CloseAsync();
                            

                            Ejava.Content = Java_list.Text;
                            ERAM.Content = "RAM：" + Bit.Text;
                            EFSMV.Content = Update;
                            EJVM.Content = "JVM:" + JVM.Text;
                            DGYC.Content = ex.Message;
                            YCPC.Text = ex.ToString();
                            Tab1.SelectedIndex = 5;
                        }
                    }
                    catch (Exception ex)
                    {
                        YCPC.Text = ex.ToString();
                    }
                    break;
                case "offline":
                    try
                    {
                        try
                        {
                            //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
                            loading.SetIndeterminate();
                            await game.StartGame(NowV.Content.ToString(), Java_list.Text, int.Parse(Bit.Text), OfflineName.Text, JVM.Text, EY.Text);
                            
                            await loading.CloseAsync();
                        }
                        catch (Exception ex)
                        {
                            await loading.CloseAsync();
                            
                            Ejava.Content = Java_list.Text;
                            ERAM.Content = "RAM：" + Bit.Text;
                            EFSMV.Content = Update;
                            EJVM.Content = "JVM:" + JVM.Text;
                            DGYC.Content = ex.Message;
                            YCPC.Text = ex.ToString();
                            Tab1.SelectedIndex = 5;
                        }
                    }
                    catch (Exception ex)
                    {
                        YCPC.Text = ex.ToString();
                    }
                    break;
                case "y":
                    try
                    {
                        try
                        {

                            //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
                            loading.SetIndeterminate();
                            //MessageBox.Show(Y.GetValue("IP").ToString());
                            await game.StartGame(NowV.Content.ToString(), Java_list.Text, int.Parse(Bit.Text), Yname, Yuuid, Ytoken, Y.GetValue("IP").ToString(), JVM.Text, EY.Text, AuthenticationServerMode.yggdrasil);
                            await loading.CloseAsync();
                        }
                        catch (Exception ex)
                        {
                            await loading.CloseAsync();
                            
                            Ejava.Content = Java_list.Text;
                            ERAM.Content = "RAM：" + Bit.Text;
                            EFSMV.Content = Update;
                            EJVM.Content = "JVM:" + JVM.Text;
                            DGYC.Content = ex.Message;
                            YCPC.Text = ex.ToString();
                            Tab1.SelectedIndex = 5;
                        }
                    }
                    


                    catch (Exception ex)
                    {
                        YCPC.Text = ex.ToString();
                    }
                    break;
                default:
                    await loading.CloseAsync();
                    await this.ShowMessageAsync("启动错误", "你尚未登录或选择账户") ;
                    break;
            }

        }

        private void Tile_Click_12(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_13(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_14(object sender, RoutedEventArgs e)
        {

        }

        private void QW(object sender, RoutedEventArgs e)
        {

        }

        private void LO(object sender, RoutedEventArgs e)
        {

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
        private void OnLineI(object ob, EventArgs a)
        {

            string aa = DIYvar.xzItems[did1].xzwz;
            string bb = DIYvar.xzItems[did2].xzwz;
            string cc = DIYvar.xzItems[did3].xzwz;
            string dd = DIYvar.xzItems[did4].xzwz;
            if (aa == "完成" && bb == "完成" && cc == "完成" && dd == "完成")
            {
                Tab1.SelectedIndex = 3;
                Tab1.SelectedIndex = 3;
                Tab1.SelectedIndex = 3;
                Tab1.SelectedIndex = 3;
                Tab1.SelectedIndex = 3;
                Tab1.SelectedIndex = 3;
                aa = "0";
                bb = "0";
                cc = "0";
                dd = "0";
                Tab1.SelectedIndex = 3;
                ONLINEW.Stop();
                Tab1.SelectedIndex = 3;
                ONLINEW.Stop();
                ONLINEW.Stop();
                ONLINEW.Stop();
                ONLINEW.Stop();
                Tab1.SelectedIndex = 3;
                ONLINEW.Stop();
                Tab1.SelectedIndex = 3;
            }

            JarTimerBool = false;


        }
        public string Mojangname;
        public string MojangUUID;
        public string MojangToken;

        private async void Button_Click_13(object sender, RoutedEventArgs e)
        {
            

            /// AniStart(new AniData{
            /// OpAni(22, 2, st, 0)
            ///});
            // Ce.BeginAnimation(WidthProperty, CatC(0, 634, 0.8));
            //EaseMoveAnimation animation1 = new EaseMoveAnimation();
            //animation1.From = 820;
            //animation1.To = 560;
            //animation1.Duration = TimeSpan.FromSeconds(2);
            //this.Ce.BeginAnimation(Canvas.LeftProperty, animation1);
            //List <processId1> aa = new List <processId1>(GetProcessId("frpc.exe"));
            //  ResourceDictionary resource = new ResourceDictionary();
            // resource.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Red.xaml");
            // BundledTheme aa = new BundledTheme();
            // aa.PrimaryColor = MaterialDesignColors.PrimaryColor.Amber;

            System.Windows.Forms.NotifyIcon fyIcon = new System.Windows.Forms.NotifyIcon();
            //fyIcon.Icon = new Icon("nihao.ico");/*找一个ico图标将其拷贝到 debug 目录下*/
            fyIcon.BalloonTipText = "FSMCloud尚未完成";/*必填提示内容*/
            fyIcon.BalloonTipTitle = "通知";
            //fyIcon.Icon = new Icon(@"D:\下载文件夹\PCL1-master\PCL1-master\Plain Craft Launcher\Images\icon.ico");/*找一个ico图标将其拷贝到 debug 目录下*/
            fyIcon.Visible = true;/*必须设置显隐，因为默认值是 false 不显示通知*/
            fyIcon.ShowBalloonTip(0);

            this.ShowMessageAsync("FSM Cloud", "FSMCloud尚未制作完成，作者正在制作中...");
        }
        /// <summary>
        /// 通过pid杀进程
        /// </summary>
        /// <param name="strProcid"></param>
        public void KillProcessid(string strProcid)
        {
            try
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.Id.Equals(Int32.Parse(strProcid)))
                    {
                        if (!p.CloseMainWindow())
                        {
                            p.Kill();
                        }
                        else
                        {
                            p.Kill();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        public class processId1
        {
            public string CommandLine
            {
                get;
                set;
            }
            public string processId01
            {
                get;
                set;
            }
            public string name
            { get; set; }
        }
        /// <summary>
        /// 通过筛选CommandLine查出pid
        /// </summary>
        /// <param name="thanName1">条件one</param>
        /// <returns></returns>
        private static List<processId1> GetProcessId(string thanName1)
        {
            List<processId1> results = new List<processId1>();

            SelectQuery query1 = new SelectQuery("Select * from Win32_Process WHERE CommandLine like '%" + thanName1 + "%'");
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(query1);
            try
            {
                foreach (ManagementObject disk in searcher1.Get())
                {
                    results.Add(new processId1()
                    {
                        processId01 = disk["ProcessId"].ToString(),
                        CommandLine = disk["CommandLine"].ToString(),
                        name = disk["Name"].ToString()
                    });
                }
            }
            catch
            {
                return null;
            }
            return results;
        }
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

        private async void Tile_Click_888(object sender, RoutedEventArgs e)
        {
            //正常登录
            MicrosoftLogin microsoftLogin = new MicrosoftLogin();
            MinecraftLogin minecraftlogin = new MinecraftLogin();
            var loading = await this.ShowProgressAsync("正在微软登录", "请稍后...");
            加载L.Content = "正在登录...";
            try
            {
                loading.SetIndeterminate();
                Xbox XboxLogin = new Xbox();
                
                    var token = microsoftLogin.GetToken(await microsoftLogin.Login(false));
                    wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));
                    string refresh_token = token.refresh_token;
                    WR.SetValue("Atoken", refresh_token);
                
                await loading.CloseAsync();

                IDTab.SelectedIndex = 2;
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);

                wruuid = Minecraft.uuid;
                wrname = Minecraft.name;

                LB.Content = wrname;
                loginmode = "wr";
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                IDTab.SelectedIndex = 2;

                wryes = "888";
                wryes = "888";


                Download( System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png","",tools.GetMinecraftSkin(wruuid));
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

        private void IDTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tt(object sender, RoutedEventArgs e)
        {
            dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
        }

        private void OfflineName_TextChanged(object sender, TextChangedEventArgs e)
        {
            offlinename = OfflineName.Text;
            loginmode = "offline";
        }

        private void Tile_Click_16(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/YUXUAN888/FSMLauncher3");
        }

        private async void Tile_Click_15(object sender, RoutedEventArgs e)
        {
            try 
            {
                Thread WRD1 = new Thread(MMJ);
                WRD1.Start();
            }
            catch
            {

            }
            




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
        private void Forge_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Forge_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_17(object sender, RoutedEventArgs e)
        {
            
        }

        private void Start_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_18(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();  //选择文件夹
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();//提示用户打开文件窗体
            dialog.Description = "请选择.minecraft文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathlist.Items.Add(dialog.SelectedPath);
            }
            try
            {

                WritePrivateProfileString("VPath1", "1", pathlist.Items.Count.ToString(), File_);
                WritePrivateProfileString("VPath", pathlist.Items.Count.ToString(), dialog.SelectedPath, File_);

            }
            catch
            {

            }
        }
        String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        private void pathlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // vlist.Items.Clear();
            
            AllTheExistingVersion[] t = new AllTheExistingVersion[0];
            

            
            try
            {
                if(pathlist.SelectedIndex == 0)
                {
                    WritePrivateProfileString("Vlist", "Path", pathlist.SelectedIndex.ToString(), File_);
                    tools.SetMinecraftFilesPath(dminecraft_text.Text);
                    t = tools.GetAllTheExistingVersion();
                    //List<DoItem> item1 = new List<DoItem>();



                    //DIYvar.l = item1;
                    
                    List<DoItem> user1 = new List<DoItem>();
                    for (int i = 0; i < t.Length; i++)
                    {
                        DoItem user = new DoItem();
                        user.DverV.Content = t[i].version;
                        user1.Add(user);
                    }
                    DIYvar.lw = user1;
                    vlist.ItemsSource = user1.ToArray();
                    
                    
                }
                else
                {
                    WritePrivateProfileString("Vlist", "Path", pathlist.SelectedIndex.ToString(), File_);
                    string mcPath = ((sender as ListBox).SelectedItem as TextBlock).Text;
                    tools.SetMinecraftFilesPath(mcPath);
                    t = tools.GetAllTheExistingVersion();
                    List<DoItem> user1 = new List<DoItem>();
                    for (int i = 0; i < t.Length; i++)
                    {
                        DoItem user = new DoItem();
                        user.DverV.Content = t[i].version;
                        user1.Add(user);
                    }
                    DIYvar.lw = user1;
                    vlist.ItemsSource = user1.ToArray();
                }
               // string mcPath = (sender as ListBox).SelectedItem.ToString();
                

            }
            catch
            {

            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vlist.SelectedIndex != -1)
            {
                MinecraftDownload minecraft = new MinecraftDownload();
                AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                string mcPath = (pathlist.SelectedItem as TextBlock).Text;
                tools.SetMinecraftFilesPath(mcPath);
                t = tools.GetAllTheExistingVersion();
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                WritePrivateProfileString("Vlist", "V", vlist.SelectedIndex.ToString(), File_);


                NowV.Content = t[vlist.SelectedIndex].version;
            }










        }






        private void Tile_Click_19(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_20(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_21(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_22(object sender, RoutedEventArgs e)
        {

        }

        private void JVM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_23(object sender, RoutedEventArgs e)
        {


            ResourceDictionary zh_cn = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/L/zh cn.xaml", UriKind.RelativeOrAbsolute)
            };
            ResourceDictionary en_us = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/L/en us.xaml", UriKind.RelativeOrAbsolute)
            };

            this.Resources = en_us;

        }
        UpdateD.Update up = new UpdateD.Update();
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {

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
                Yyes = "888";

            }
            catch (SquareMinecraftLauncherException ex)
            {
                await this.ShowMessageAsync("登录失败", ex.Message);
            }
        }
        public string Ytoken;
        public string Yname;
        public string Yuuid;
        public string Yyes;
        private void Ylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                JS.Text = skin.NameItem[Ylist.SelectedIndex].Name;
                WritePrivateProfileString("Y", "Ylist", Ylist.SelectedIndex.ToString(), FileS);
                Yyes = "888";
            }

            catch
            {

            }

        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            Yuuid = skin.NameItem[Ylist.SelectedIndex].uuid;
            Yname = skin.NameItem[Ylist.SelectedIndex].Name;
            Ytoken = skin.accessToken;
            Yyes = "888";
            loginmode = "y";



        }

        private void ImageMosedown(object sender, MouseButtonEventArgs e)
        {
            

            
            
        }

        private void e(object sender, RoutedEventArgs e)
        {

            while (2 > 1)
            {
                MessageBox.Show("奥利给奥利给");
            }
        }

        private void rams_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Bit.Text = rams.Value.ToString();
        }

        private void Bit_TextChanged(object sender, TextChangedEventArgs e)
        {
//rams.Value = double.Parse(Bit.Text);
        }
        public static int iddd = 0;
        internal int Downloadw(string path, string ly, string url)
        {
            this.dlf.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);
            this.dlf.StartDown(3);
            iddd++;
            xzItem xzItem = new xzItem(System.IO.Path.GetFileName(path), 0, ly, "等待中", url, path);
            xzItems.Add(xzItem);
            
            
            return id - 1;
        }

        private void Tile_Click_24(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(loginmode);
        }
        internal DispatcherTimer timer(EventHandler tick, int Interval)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(Interval);
            timer1.Tick += tick;
            return timer1;
        }
        SquareMinecraftLauncher.MinecraftDownload MinecraftDownload = new SquareMinecraftLauncher.MinecraftDownload();
        static int JarID, JsonID;
        static System.Windows.Threading.DispatcherTimer JarTimer = new System.Windows.Threading.DispatcherTimer();
        public async Task<bool> libraries(string version)
        {
            
            
              //  tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
            
            //libraries1 = mcVersionLists[MCV.SelectedIndex].version;
            MCDownload[] File = tools.GetMissingFile(version);
            if (File.Length != 0)
            {

                foreach (var i in File)
                {
                    Download(i.path, "补全", i.Url);
                }
                //libraries2 = sz.id;
                return false;
            }
            
            return true;
        }
        MCDownload[] downloads = new MCDownload[0];
        public async Task<bool> assetAsync(string version)
        {
            try
            {
                downloads = tools.GetMissingAsset(version);
            }
            catch
            {
                return true;
            }
            DispatcherTimer AssetTimer = new DispatcherTimer();
            if (downloads.Length != 0)
            {


                tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);


                //zy[t] = Download(downloads[t].path, "bq", downloads[t].Url);
                GacDownload gd = new GacDownload(44, downloads);
                gd.StartDownload();

                //zy[i] = Download(downloads[i].path, "资源补全", downloads[i].Url);
                //GacDownload gdd = new GacDownload(10, downloads);

                //gdd.StartDownload();


                AssetTimer.Start();
                Asset = true;
                return false;
            }
            return true;
        }
        
        int[] zy = new int[3];
        static int d = 0;
        public static bool Asset = false;
        public bool AssetTimerEnvent(ref string pdc)
        {
            for (int i = 0; i < zy.Length; i++)
            {
                
                    if (downloads.Length - d < 3)
                    {
                        for (int t = d - 1; t < downloads.Length; t++)
                        {
                            //DIYvar.Assetxz.Text = d + "/" + downloads.Length;
                            //Download(downloads[t].path, "资源补全", downloads[t].Url);
                            ///GacDownload gd = new GacDownload(10, downloads);
                            ///gd.StartDownload();
                        }
                        return true;
                    }
                    else
                    {
                        for (int t = 0; t < 3; t++, d++)
                        {
                            //Assetxz.Text = d + "/" + downloads.Length;
                            pdc = d + "/" + downloads.Length;
                            //zy[i] = Download(downloads[d].path, "资源补全", downloads[d].Url);
                            ///GacDownload gd = new GacDownload(10,downloads);
                            ///gd.StartDownload();
                        }
                    }
                
            }
            return false;
        }
        //bool Mi = true;
        public bool librariesTimer(ref string lt)
        {
            int ap = 0;
            if (libraries2 == 0)
            {
                return true;
            }
            for (int i = libraries1; i < libraries2; i++)
            {
                if (DIYvar.xzItems[i].xzwz.IndexOf("无法") >= 0 || DIYvar.xzItems[i].xzwz.IndexOf("失败") >= 0 || DIYvar.xzItems[i].xzwz.IndexOf("完成") >= 0)
                {
                    ap++;
                }
            }
            if (ap == libraries2 - libraries1)
            {
                for (int i = 0; i < libraries2; i++)
                {
                    if (DIYvar.xzItems[i].xzwz.IndexOf("无法") >= 0 || DIYvar.xzItems[i].xzwz.IndexOf("失败") >= 0)
                    {
                        //tools.DownloadSourceInitialization(DownloadSource.MinecraftSource);
                        
                        //libraries1 = sz.id;
                        //MCDownload[] File = tools.GetMissingFile(DIYvar.Main1.GameVersion.Text);
                        lt = ap + "/" + Convert.ToString(libraries2 - libraries1);
                        MCDownload[] File = tools.GetMissingFile(mcVersionLists[MCV.SelectedIndex].version);
                        if (File.Length != 0)
                        {

                            foreach (var s in File)
                            {
                                Download(s.path, "补全", s.Url);
                            }
                            //libraries2 = sz.id;
                            return false;
                        }
                        return true;
                    }
                }
                return true;
            }
            lt = ap + "/" + Convert.ToString(libraries2 - libraries1);
            return false;
        }
        public async Task lib(ProgressDialogController pdc)
        {
            bool al = true;
            bool asset = true;
            bool lib = false;
            string at = "0/0", lt = "0/0";
            await Task.Factory.StartNew(() =>
            {
                while (al)
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        if (Asset)
                        {
                            if (AssetTimerEnvent(ref at))
                            {
                                asset = true;
                            }
                            else
                            {
                                asset = false;
                            }
                        }
                        if (!lib)
                        {
                            if (!librariesTimer(ref lt))
                            {
                                lib = false;
                            }
                            else
                            {

                                lib = true;
                            }
                        }
                        pdc.SetMessage("正在下载补全...\n已下载的资源文件：" + at + "\n已下载的依赖库：" + lt);
                        if (lib == true && asset == true)
                        {
                            al = false;
                        }
                    }));
                    Thread.Sleep(2000);
                }
            });
        }
        static int libraries1 = 0;
        static int libraries2 = 0;
        private async void Button_Click_20(object sender, RoutedEventArgs e)
        {
            try
            {
                tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
                MCDownload download = MinecraftDownload.MCjarDownload(mcVersionLists[MCV.SelectedIndex].version);
                JarID = Download(download.path, "", download.Url);
                download = MinecraftDownload.MCjsonDownload(mcVersionLists[MCV.SelectedIndex].version);
                JsonID = Download(download.path, "", download.Url);
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                Jarw = Core5.timer(MCjarInstall, 5555);
                Jarw.Start();
                JarTimerBool = true;
                Tab1.SelectedIndex = 8;
                

                ///以上是Asset补全

                //var loading = await this.ShowProgressAsync("提示", "正在补全中...");
                //loading.SetIndeterminate();
                //await lib(loading);
                //await loading.CloseAsync();

            }
            catch
            {

            }

        }
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="Log"></param>
       // static int logpro;
        public void AssetDownload_DownloadProgressChanged(AssetDownload.DownloadIntermation Log)
        {
            
                Console.WriteLine(Log.FinishFile + "/" + Log.AllFile + "  " + Log.Progress + "  " + Log.Speed);

            this.Dispatcher.Invoke(new Action(delegate { DownPro.Value = Log.Progress; }));
                if (Log.Progress == 100)
                    {
                this.Dispatcher.Invoke(new Action(delegate { Tab1.SelectedIndex = 0; }));
                
                
                    }
                
            
            
            
        }
        
        private async void MCjarInstall(object ob, EventArgs a)
        {
            //MessageBox.Show(DIYvar.xzItems[JsonID].xzwz);
            string aa = DIYvar.xzItems[JarID].xzwz;
            string bb = DIYvar.xzItems[JsonID].xzwz;
            
            if (aa == "完成" && bb == "完成")
            {

                
                JarTimerBool = false;

                
                Jarw.Stop();
                try
                {
                    //tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);

                    AssetDownload assetDownload = new AssetDownload();//asset下载类
                    assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                    await libraries(mcVersionLists[MCV.SelectedIndex].version);
                    await assetDownload.BuildAssetDownload(9, mcVersionLists[MCV.SelectedIndex].version);//构建下载
                    Jarw.Stop();
                    Jarw.Stop();
                }
                catch
                {

                }

            }
            

        }

        private async void Button_Click_21(object sender, RoutedEventArgs e)
        {
            //tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
            //await libraries(mcVersionLists[MCV.SelectedIndex].version);
            //await assetAsync(mcVersionLists[MCV.SelectedIndex].version);
            //
            //var loading = await this.ShowProgressAsync("提示", "正在补全中...");
            //loading.SetIndeterminate();
            //await lib(loading);
            //await loading.CloseAsync();
        }
        public string onlinename;
        public string onlineduankou;
        public string onlinezijiqq;
        public string onlineduifangqq;
        private async void Button_Click_22(object sender, RoutedEventArgs e)
        {
            onlinename = await this.ShowInputAsync("请输入房间名", "房间名会在对方启动器内显示\n你也可以不填写");
            onlinezijiqq = await this.ShowInputAsync("请输入你的QQ", "QQ作为您的验证凭证,这是安全的\n你必须填写,请勿乱写,乱写有可能导致联机有问题");
            onlineduankou = await this.ShowInputAsync("请输入游戏端口", "打开Minecraft单人游戏,按esc到菜单,然后开放局域网\n你必须填写,请勿乱写,乱写有可能导致联机有问题");
            OnlineTEXT.Text = "房间名:"+onlinename+"\n" + "你的QQ:" + onlinezijiqq + "\n" + "你的端口:" + onlineduankou + "\n";
            OpenOnline.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 执行外部命令
        /// </summary>
        /// <param name="argument">命令参数</param>
        /// <param name="application">命令程序路径</param>
        /// <returns>执行结果</returns>
        public static string ExecuteOutCmd(string argument, string applocaltion)
        {
            using (var process = new Process())
            {
                process.StartInfo.Arguments = argument;
                process.StartInfo.FileName = applocaltion;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine("exit");

                //获取cmd窗口的输出信息  
                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                process.Close();

                return output;
            }
        }
        /// <summary>
        /// 执行内部命令（cmd.exe 中的命令）
        /// </summary>
        /// <param name="cmdline">命令行</param>
        /// <returns>执行结果</returns>
        public static string ExecuteInCmd(string cmdline)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine(cmdline + "&exit");

                //获取cmd窗口的输出信息  
                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                process.Close();

                return output;
            }
        }
        public short lj = 1;
            private async void Button_Click_23(object sender, RoutedEventArgs e)
        {
            if (lj == 1)
            {
                WritePrivateProfileString("common", "server_addr", "sh.qwq.one", FileOnlineServer + @"\frpc.ini");
                WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                WritePrivateProfileString("common", "dns", "223.5.5.5", FileOnlineServer + @"\frpc.ini");
                WritePrivateProfileString(onlinezijiqq, "type", "stcp", FileOnlineServer + @"\frpc.ini");
                WritePrivateProfileString(onlinezijiqq, "sk", "12345678", FileOnlineServer + @"\frpc.ini");
                WritePrivateProfileString(onlinezijiqq, "local_port", onlineduankou, FileOnlineServer + @"\frpc.ini");
                WritePrivateProfileString(onlinezijiqq, "remote_port", "32423", FileOnlineServer + @"\frpc.ini");
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = FileOnlineServer + @"\StartForFSM.exe";
                info.Arguments = "";
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process pro = Process.Start(info);
                //pro.WaitForExit();
                Stat.Visibility = Visibility.Hidden;
                await this.ShowMessageAsync("房间创建完毕", "复制链接给他人加入吧!");
                OpenOnline.Content = "复制链接";
                lj = 2;
            }
            else
            {
                
                byte[] b = System.Text.Encoding.Default.GetBytes(onlinezijiqq + "|" + onlinename);
                
                Clipboard.SetDataObject(Convert.ToBase64String(b));
                await this.ShowMessageAsync("已复制链接", "分享给他人吧!");
            }
            
        

            
        }
        public static bool StartProcess(string filename, string[] args)
         {
             try
             {
                 string s = "";
                 foreach (string arg in args)
                 {
                     s = s + arg + " ";
                 }
                 s = s.Trim();
                 var myprocess = new Process();
                 var startInfo = new ProcessStartInfo(filename, s);
                 myprocess.StartInfo = startInfo;
                 //通过以下参数可以控制exe的启动方式，具体参照 myprocess.StartInfo.下面的参数，如以无界面方式启动exe等
                 //myprocess.StartInfo.UseShellExecute = true;
                myprocess.StartInfo.CreateNoWindow = true;
                 myprocess.Start();
                 return true;
             }
             catch (Exception ex)
             {
                     Console.WriteLine("启动应用程序时出错！原因：" + ex.Message);
                 }
            return false;
         }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click_25(object sender, RoutedEventArgs e)
        {
            try
            {
                String yqm = await this.ShowInputAsync("请输入联机邀请码", "对方发来的邀请码");
                String xcqq = await this.ShowInputAsync("请输入你的QQ", "请注意，");
                byte[] c = Convert.FromBase64String(yqm);
                String ww = System.Text.Encoding.Default.GetString(c);
                after = ww.Split(new char[] { '|' });
                fjm.Content = "您已加入房间:" + after[1];
                onlineduifangqq = after[0];
                WritePrivateProfileString("common", "server_addr", "sh.qwq.one", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString("common", "server_port", "7000", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString("common", "dns", "223.5.5.5", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString(xcqq, "server_name", onlineduifangqq, FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString(xcqq, "type", "stcp", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString(xcqq, "bind_addr", "127.0.0.1", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString(xcqq, "bind_port", "32423", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString(xcqq, "role", "visitor", FileOnlineKEHU + @"\frpc.ini");
                WritePrivateProfileString(xcqq, "sk", "12345678", FileOnlineKEHU + @"\frpc.ini");
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = FileOnlineKEHU + @"\StartForFSM.exe";
                info.Arguments = "";
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process pro = Process.Start(info);
                //pro.WaitForExit();
                OpenOnline.Visibility = Visibility.Hidden;
                dfjr.Visibility = Visibility.Hidden;
                drfz.Visibility = Visibility.Visible;
                await this.ShowMessageAsync("连接成功", "进入游戏吧!");
            }
            catch
            {
                await this.ShowMessageAsync("连接失败", "您的邀请码错误");
            }
        }

        private async void Button_Click_26(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject("127.0.0.1:32423");
            await this.ShowMessageAsync("已复制地址","快进入游戏联机吧!\n多人游戏，欢乐多又多");
        }

        private void CheckBox2(object sender, RoutedEventArgs e)
        {
            





        }

        private void CheckBox1(object sender, RoutedEventArgs e)
        {

        }
        [DllImport("winmm.dll")]
        public static extern bool PlaySound(String Filename, int Mod, int Flags);
        public const int SND_FILENAME = 0x00020000;
        public const int SND_ASYNC = 0x0001;
        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();//提示用户打开文件窗体
            fileDialog.Title = "选择FSM的背景音乐";//文件对话框标题
            fileDialog.Filter = "音乐格式|*.mp3;*.wav";//文件格式筛选字符串
            if (fileDialog.ShowDialog() == true)//判断对话框返回值，点击打开
            {

                xzdyy.Text = fileDialog.FileName;
                /////PlaySound(xzdyy.Text, 0, 1); //第3个形参，把1换为9，连续播放
                WritePrivateProfileString("Music","lj",xzdyy.Text,FileS);
            }
        }
        public int yyxhbf;
        private void XHBF(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    //选择
                    yyxhbf = 9;
                    WritePrivateProfileString("Music","XHBF","9",FileS);
                }
                else
                {
                    //不选择
                    yyxhbf = 1;
                    WritePrivateProfileString("Music", "XHBF", "1", FileS);
                }
            }
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox233(object sender, RoutedEventArgs e)
        {
            if (bjyybf.IsChecked == true)
            {
                PlaySound(xzdyy.Text, 0, yyxhbf);

            }
            else
            {
                PlaySound(null, (int)IntPtr.Zero, SND_ASYNC);

                WritePrivateProfileString("Music", "lj", "", FileS);

            }
        }
        static string ReverseString(string original)
        {
            char[] arr = original.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public short inforge;
        public short inopt;
        public short infab;
        public short inlite;
        private async void Tile_Click_25(object sender, RoutedEventArgs e)
        {
            if (infab == 1 || inlite == 1)
            {
                await this.ShowMessageAsync("不能进行Forge安装", "Forge与Fabric，LiteLoader不兼容", MessageDialogStyle.Affirmative);
            }
            else
            {
                try
                {
                    ForgeList.Items.Clear();
                    MCVersionList[] mcvv = new MCVersionList[0];


                    AZForgeV.Text = mcVersionLists[MCV.SelectedIndex].version;

                    var a = await tools.GetForgeList(AZForgeV.Text);
                    // sort forge versions


                    foreach (var i in a)
                    {
                        ForgeList.Items.Add("Forge - " + i.ForgeVersion);
                    }
                    var b = await tools.GetMaxForge(AZForgeV.Text);
                    MaxForge.Content = "建议的Forge版本:" + b.ForgeVersion + " (点击选择)";
                    DTB.SelectedIndex = 1;
                }
                catch
                {

                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Button_Click_30(object sender, RoutedEventArgs e)
        {
            
        }

        private void ForgeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTB.SelectedIndex = 0;
            ForgeB.Content = "安装Forge\n" + ForgeList.SelectedItem ;
            inforge = 1;
        }

        private async void MaxForge_Click(object sender, RoutedEventArgs e)
        {
            DTB.SelectedIndex = 0;
            var b = await tools.GetMaxForge(AZForgeV.Text);
            inforge = 1;
            ForgeB.Content = "安装Forge\n" + "Forge - "+b.ForgeVersion;
        }

        private async void Tile_Click_26(object sender, RoutedEventArgs e)
        {
            if(inforge == 1 || inopt == 1 || inlite == 1)
            {
                await this.ShowMessageAsync("不能进行Fabric安装","Fabric与Forge，Optifine，LiteLoader不兼容",MessageDialogStyle.Affirmative);
            }
            else
            {

            }
        }

        

        private void DB_Help(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169/faqs-more/");
        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {
            
        }

        private void Size(object sender, SizeChangedEventArgs e)
        {
            //asc.controlAutoSize(this);
            System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
            RectangleGeometry gm = new RectangleGeometry(r, 7, 7);
            ((UIElement)sender).Clip = gm; 
        }

        private void Tile_Click_LXSkin(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_XZJ(object sender, RoutedEventArgs e)
        {
            Tab1.SelectedIndex = 4;
            MoreTab.SelectedIndex = 0;
        }

        private async void Button_Click_32(object sender, RoutedEventArgs e)
        {
            
        }

        private void SetBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SetBox.SelectedIndex == 0)
            {
                SetTab.SelectedIndex = 0;
            }
            else if(SetBox.SelectedIndex == 1)
            {
                SetTab.SelectedIndex = 1;
            }
            else if (SetBox.SelectedIndex == 2)
            {
                SetTab.SelectedIndex = 2;
            }
            else if (SetBox.SelectedIndex == 3)
            {
                SetTab.SelectedIndex = 3;
            }
            else if (SetBox.SelectedIndex == 4)
            {
                SetTab.SelectedIndex = 4;
            }
        }

        private void BT1B_Click(object sender, RoutedEventArgs e)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(BT1.Text);

            Clipboard.SetDataObject(Convert.ToBase64String(b));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

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
        public string offlinename;
        public string loginmode;
        public bool ifoffilneskin;

    }
    public class MyAni
    {
        //颜色动画
        public static AniData ColorAni(System.Windows.Media.Color Over, int Time, System.Windows.Media.Brush AniColor, int WT = 0)
        {
            return new AniData
            {
                Wt = WT,
                Canvas = AniColor,
                Dp = new DependencyProperty[]
                {
                    SolidColorBrush.ColorProperty
                },
                At = new ColorAnimation[]
                {
                    new ColorAnimation(Over, new Duration(TimeSpan.FromMilliseconds(Time)))
                }
            };
        }

        //正方形大小动画
        public static AniData SizeAni(double Over, int Time, UIElement AniCanvas, int WT = 0)
        {
            Duration d = new Duration(TimeSpan.FromMilliseconds(Time));
            return new AniData
            {
                Wt = WT,
                Canvas1 = AniCanvas,
                Dp = new DependencyProperty[]
                {
                    FrameworkElement.HeightProperty,
                    FrameworkElement.WidthProperty
                },
                At = new DoubleAnimation[]
                {
                    new DoubleAnimation(Over, d),
                    new DoubleAnimation(Over, d)
                }
            };
        }

        //长方形大小动画
        public static AniData SizeAni(double WOver, double HOver, int Time, UIElement AniCanvas, int WT = 0)
        {
            Duration d = new Duration(TimeSpan.FromMilliseconds(Time));
            return new AniData
            {
                Wt = WT,
                Canvas1 = AniCanvas,
                Dp = new DependencyProperty[]
                {
                    FrameworkElement.WidthProperty,
                    FrameworkElement.HeightProperty
                },
                At = new DoubleAnimation[]
                {
                    new DoubleAnimation(WOver, d),
                    new DoubleAnimation(HOver, d)
                }
            };
        }

        //透明度动画
        public static AniData OpAni(double Over, int Time, UIElement AniCanvas, int WT = 0)
        {
            return new AniData
            {
                Wt = WT,
                Canvas1 = AniCanvas,
                Dp = new DependencyProperty[]
                {
                    UIElement.OpacityProperty
                },
                At = new DoubleAnimation[]
                {
                    new DoubleAnimation(Over, new Duration(TimeSpan.FromMilliseconds(Time)))
                }
            };
        }

        //位置动画
        public static AniData ThiAni(Thickness Over, int Time, UIElement AniCanvas, int WT = 0)
        {
            return new AniData
            {
                Wt = WT,
                Canvas1 = AniCanvas,
                Dp = new DependencyProperty[]
                {
                    FrameworkElement.MarginProperty
                },
                At = new ThicknessAnimation[]
                {
                    new ThicknessAnimation(Over, new Duration(TimeSpan.FromMilliseconds(Time)))
                }
            };
        }

        //代码动画
        public static AniData CodeAni(ThreadStart th, int WT = 0)
        {
            return new AniData
            {
                Ts = th,
                Wt = WT
            };
        }

        //运行动画
        public static void AniStart(AniData[] Ad)
        {
            foreach (AniData ad in Ad)
            {
                AniStart(ad);
            }
        }

        //主运行动画
        private static void AniStart(AniData Ad)
        {
            new Thread(() =>
            {
                Thread.Sleep(Ad.Wt);
                int index = -1;
                if (Ad.Ts == null)
                {
                    for (int i = 0; i < Ad.At.Length; i++)
                    {
                        index++;
                        if (Ad.Canvas == null)
                        {
                            Ad.Canvas1.Dispatcher.Invoke(() =>
                            {
                                Ad.Canvas1.BeginAnimation(Ad.Dp[index], Ad.At[index]);
                            });
                        }
                        else
                        {
                            Ad.Canvas.Dispatcher.Invoke(() =>
                            {
                                Ad.Canvas.BeginAnimation(Ad.Dp[index], Ad.At[index]);
                            });
                        }
                    }
                }
                else
                {
                    new Thread(Ad.Ts).Start();
                }
            }).Start();
        }

        //动画数据
        public struct AniData
        {
            public int Wt;
            public Animatable Canvas;
            public UIElement Canvas1;
            public DependencyProperty[] Dp;
            public AnimationTimeline[] At;
            public ThreadStart Ts;
        }
    }
}
