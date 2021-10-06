using Rope.Model;
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
using System.Windows.Shapes;

namespace Rope.View
{
    /// <summary>
    /// Interaction logic for LW2_PendulumWindow.xaml
    /// </summary>
    public partial class LW2_PendulumWindow : Window, IDialog
    {
        public LW2_PendulumWindow()
        {
            InitializeComponent();
           // myLine.X2 = myBorder.ActualWidth;
        }
    }
}
