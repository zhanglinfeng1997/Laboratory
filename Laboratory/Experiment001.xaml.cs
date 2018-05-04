using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Laboratory
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Experiment001 : Page
    {
        public Experiment001()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e) //旋转试管的动画,用于回答试管问题之后
        {
            TubeAni.Begin();
            RubberAni.Begin();
            PipeAni.Begin();
            KMnO4.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //移动瓶子的动画,用于回答预热的问题之后
        {
            JarAni.Begin();
            WaterAni.Begin();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //冒气泡的动画，用于回答何时收集的问题之后
        {
            Bubble.Visibility = Visibility.Visible;
            BubbleUp.Begin();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //收集完毕的动画,用于移走导管的问题开始前
        {
            BubbleUp.Stop();
            Bubble.Visibility = Visibility.Collapsed;
            Oxygen.Visibility = Visibility.Visible;
            Air1.Begin();
            O2.Begin();
            
            
        }
    }
}
