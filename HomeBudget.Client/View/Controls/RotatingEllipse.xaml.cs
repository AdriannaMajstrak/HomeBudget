using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HomeBudget.Client.View.Controls
{
    /// <summary>
    /// Interaction logic for RotatingEllipse.xaml
    /// </summary>
    public partial class RotatingEllipse : UserControl
    {
        Storyboard faceForwardEllipseStoryBroad = new Storyboard();
        Storyboard reverseForwardEllipseStoryBroad = new Storyboard();
        Storyboard reverseBackEllipseStoryBroad = new Storyboard();
        Storyboard faceBackEllipseStoryBroad = new Storyboard();

        public RotatingEllipse()
        {
            InitializeComponent();

            DoubleAnimation faceForwardAnimation = new DoubleAnimation();
            DoubleAnimation reverseForwardAnimation = new DoubleAnimation();
            
            faceForwardAnimation.From = 0;
            faceForwardAnimation.To = 90;
            faceForwardAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            faceForwardAnimation.Completed += (s, e) => { reverseForwardEllipseStoryBroad.Begin(planeratorBack); };

            Storyboard.SetTargetName(faceForwardAnimation, planerator.Name);
            Storyboard.SetTargetProperty(faceForwardAnimation, new PropertyPath(Planerator.RotationYProperty));
            faceForwardEllipseStoryBroad.Children.Add(faceForwardAnimation);

            planeratorBack.RotationY = 270;

            reverseForwardAnimation.From = 270;
            reverseForwardAnimation.To = 360;
            reverseForwardAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            Storyboard.SetTargetName(reverseForwardAnimation, planeratorBack.Name);
            Storyboard.SetTargetProperty(reverseForwardAnimation, new PropertyPath(Planerator.RotationYProperty));
            reverseForwardEllipseStoryBroad.Children.Add(reverseForwardAnimation);

            frontGrid.MouseEnter += startRotating;

            reverseForwardAnimation.Completed += (s, e) => {
                rotation1Processing = false;
                sideIsFace = false;
                if (mouseLeft)
                {
                    rotation2Processing = true;
                    reverseBackEllipseStoryBroad.Begin(planeratorBack);
                }
            };
        

            
            DoubleAnimation reverseBackAnimation = new DoubleAnimation();
            DoubleAnimation faceBackAnimation = new DoubleAnimation();


            reverseBackAnimation.From = 360;
            reverseBackAnimation.To = 270;
            reverseBackAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            reverseBackAnimation.Completed += (s, e) => { faceBackEllipseStoryBroad.Begin(planerator); };

            Storyboard.SetTargetName(reverseBackAnimation, planeratorBack.Name);
            Storyboard.SetTargetProperty(reverseBackAnimation, new PropertyPath(Planerator.RotationYProperty));
            reverseBackEllipseStoryBroad.Children.Add(reverseBackAnimation);

            

            faceBackAnimation.From = 90;
            faceBackAnimation.To = 0;
            faceBackAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            Storyboard.SetTargetName(faceBackAnimation, planerator.Name);
            Storyboard.SetTargetProperty(faceBackAnimation, new PropertyPath(Planerator.RotationYProperty));
            faceBackEllipseStoryBroad.Children.Add(faceBackAnimation);

            frontGrid.MouseLeave += startBack;

            faceBackAnimation.Completed += (s, e) => 
            {
                sideIsFace = true;
                rotation2Processing = false;
            };
        }

        #region Dependency Properties

        public static readonly DependencyProperty FaceTextProperty =
            DependencyProperty.Register("FaceText", typeof(String), typeof(RotatingEllipse), new FrameworkPropertyMetadata(string.Empty));


        public string FaceText
        {
            get { return GetValue(FaceTextProperty).ToString(); }
            set { SetValue(FaceTextProperty, value); }
        }

        public static readonly DependencyProperty ReverseTextProperty =
            DependencyProperty.Register("ReverseText", typeof(String), typeof(RotatingEllipse), new FrameworkPropertyMetadata(string.Empty));


        public string ReverseText
        {
            get { return GetValue(ReverseTextProperty).ToString(); }
            set { SetValue(ReverseTextProperty, value); }
        }

        //public static readonly DependencyProperty InnerContentPropertyFace =
        //     DependencyProperty.Register("InnerContentFace", typeof(FrameworkElement), typeof(RotatingEllipse), new FrameworkPropertyMetadata(null));

        //public FrameworkElement InnerContentFace
        //{
        //    get { return GetValue(InnerContentPropertyFace) as FrameworkElement; }
        //    set { SetValue(InnerContentPropertyFace, value); }
        //}

        public static readonly DependencyProperty IconFaceProperty =
             DependencyProperty.Register("IconFace", typeof(Canvas), typeof(RotatingEllipse), new FrameworkPropertyMetadata(null));

        public Canvas IconFace
        {
            get { return GetValue(IconFaceProperty) as Canvas; }
            set { SetValue(IconFaceProperty, value); }
        }

        //public static readonly DependencyProperty InnerContentPropertyReverse =
        //     DependencyProperty.Register("InnerContentReverse", typeof(FrameworkElement), typeof(RotatingEllipse), new FrameworkPropertyMetadata(null));

        //public FrameworkElement InnerContentReverse
        //{
        //    get { return GetValue(InnerContentPropertyReverse) as FrameworkElement; }
        //    set { SetValue(InnerContentPropertyReverse, value); }
        //}
        #endregion


        #region Animations


        bool mouseLeft;
        bool rotation1Processing;
        bool rotation2Processing;
        bool sideIsFace = true;

        void startRotating(object o, MouseEventArgs s)
        {
            mouseLeft = false;
            if (!rotation1Processing && !rotation2Processing && sideIsFace)
            {
                rotation1Processing = true;
                faceForwardEllipseStoryBroad.Begin(planerator);
            }
        }

        void startBack(object o, MouseEventArgs s)
        {
            mouseLeft = true;

            if (!rotation1Processing && !rotation2Processing && !sideIsFace)
            {
                rotation2Processing = true;
                reverseBackEllipseStoryBroad.Begin(planeratorBack);
            }
        }

        #endregion



    }
}
