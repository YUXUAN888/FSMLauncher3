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

        private void w(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
            RectangleGeometry gm = new RectangleGeometry(r);
            ((UIElement)sender).Clip = gm; 
        }
    }
}
