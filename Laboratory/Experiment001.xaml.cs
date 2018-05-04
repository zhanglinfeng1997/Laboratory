using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        private double originX;         //记录图片原来位置
        private double originY;         //记录图片原来位置
        private int next = 0;               //是否全部显示的标记

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

        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e) //按下某一器材的事件
        {
            Image i = sender as Image;
            originX = (double)i.GetValue(Canvas.LeftProperty);
            originY = (double)i.GetValue(Canvas.TopProperty);
            
        }


        private void Image_PointerMoved(object sender, PointerRoutedEventArgs e)    //拖动某一器材的事件,器材图片可以被拖动
        {
            Image i = sender as Image;
            var point = e.GetCurrentPoint(i);


            if (point.Properties.IsLeftButtonPressed)
            {
                double offsetX = (double)i.GetValue(Canvas.LeftProperty);
                double offsetY = (double)i.GetValue(Canvas.TopProperty);
                offsetX += point.Position.X - i.ActualWidth / 2;
                offsetY += point.Position.Y - i.ActualHeight / 2;
                (sender as Image).SetValue(Canvas.LeftProperty, offsetX);
                (sender as Image).SetValue(Canvas.TopProperty, offsetY);

            }
            

            
        }

        private void Image_PointerReleased(object sender, PointerRoutedEventArgs e) //放开左键的事件,假如对,显示相应器材;假如错,提示
        {
            Image i = sender as Image;
            double top = (double)i.GetValue(Canvas.TopProperty);
            if (top < 150)  //未拖到区域
            {
                (sender as Image).SetValue(Canvas.LeftProperty, originX);
                (sender as Image).SetValue(Canvas.TopProperty, originY);
                return;
            }else
            {
                if ((String)i.Tag == "0")           //错误器材
                {
                    i.Visibility = Visibility.Collapsed;
                    TB001.Text = "Wrong! -5";
                    return;
                }else               //正确器材
                {
                    i.Visibility = Visibility.Collapsed;
                    switch (i.Tag)
                    {
                        case "1":
                            Alcohol.Visibility = Visibility.Visible;
                            AlcoholBurner.Visibility = Visibility.Visible;
                            break;
                        case "2":
                            Sink.Visibility = Visibility.Visible;
                            Water0.Visibility = Visibility.Visible;
                            break;
                        case "3":
                            Pipe.Visibility = Visibility.Visible;
                            break;
                        case "4":
                            Tube.Visibility = Visibility.Visible;
                            break;
                        case "5":
                            Rubber.Visibility = Visibility.Visible;
                            break;
                        case "6":
                            Jar.Visibility = Visibility.Visible;
                            Water.Visibility = Visibility.Visible;
                            break;
                        case "7":
                            KMnO4.Visibility = Visibility.Visible;
                            break;
                    }
                    next++;
                }
            }


        }



        //以下为判断是否显示完毕的异步事件
        async Task<int> NextSessionAsync()
        {
            bool flag = true;

                while (flag)                //死循环,但因为异步不会卡死
            {
                await Task.Delay(10);
                if (next > 6)               //显示完毕则跳出死循环,等待两秒,进入后续步骤
                {
                    flag = false;
                }

              }
            await Task.Delay(2000);
            TB001.Visibility = Visibility.Collapsed;
                ExpArea.SetValue(Canvas.TopProperty, 50);

            return 0;
        }

        private async void Button_Start(object sender, RoutedEventArgs e)//此按钮事件为输入化学方程式完毕后的"下一步"按钮所需要完成的事件
        {
            await NextSessionAsync();
        }
    }
}
