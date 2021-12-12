using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FSMLauncher_3.About_Message
{
    /// <summary>
    /// OSMessage.xaml 的交互逻辑
    /// </summary>
    public partial class OSMessage : UserControl
    {
        public OSMessage()
        {
            InitializeComponent();
        }
        public double TopFrom { get; set; }
        private void creat(object sender, RoutedEventArgs e)
        {
            OSMessage self = sender as OSMessage;
            if (self != null)
            {
                self.UpdateLayout();
                SystemSounds.Asterisk.Play();//播放提示声

                double right = System.Windows.SystemParameters.WorkArea.Right;//工作区最右边的值
                
                DoubleAnimation animation = new DoubleAnimation();
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(1));//NotifyTimeSpan是自己定义的一个int型变量，用来设置动画的持续时间
                animation.From = right;
                animation.To = right - self.ActualWidth;//设定通知从右往左弹出
                self.BeginAnimation(Window.LeftProperty, animation);//设定动画应用于窗体的Left属性

                Task.Factory.StartNew(delegate
                {
                    int seconds = 5;//通知持续5s后消失
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
                    //Invoke到主进程中去执行
                    Invoke(self, delegate
                    {
                        animation = new DoubleAnimation();
                        animation.Duration = new Duration(TimeSpan.FromMilliseconds(1));
                        //animation.Completed += (s, a) => { self.Close(); };//动画执行完毕，关闭当前窗体
                        animation.From = right - self.ActualWidth;
                        animation.To = right;//通知从左往右收回
                        self.BeginAnimation(Window.LeftProperty, animation);
                    });
                });
            }
        }
        static void Invoke(OSMessage win, Action a)
        {
            win.Dispatcher.Invoke(a);
        }
    }
}
