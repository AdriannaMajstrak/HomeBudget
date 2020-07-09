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

namespace HomeBudget.Client.View.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        public Tile()
        {
            //intToColorConverter
            
            InitializeComponent();

        }

        #region Dependency Properties

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TileText", typeof(String), typeof(Tile), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty BackgroundColorProperty =
           DependencyProperty.Register("TileBackgroundColor", typeof(int), typeof(Tile), new FrameworkPropertyMetadata(null));


        public string TileText
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public int TileBackgroundColor
        {
            get { return (int)GetValue(BackgroundColorProperty);}
            set { SetValue(BackgroundColorProperty, value); }
        }

        #endregion
    }
}
