using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Popups;
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
                    QuestionArea.decreasePoint(5);
                    return;
                }else               //正确器材
                {
                    i.Visibility = Visibility.Collapsed;
                    TB001.Text = "Correct!";
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
            await Task.Delay(1000);
            //播放音效
            TB001.Visibility = Visibility.Collapsed;
                ExpArea.SetValue(Canvas.TopProperty, 50);
            e1.Visibility = Visibility.Collapsed;
            e2.Visibility = Visibility.Collapsed;
            e3.Visibility = Visibility.Collapsed;
            e4.Visibility = Visibility.Collapsed;
            e5.Visibility = Visibility.Collapsed;
            e6.Visibility = Visibility.Collapsed;
            MessageDialog d = new MessageDialog("器材组装完毕！");
           await  d.ShowAsync();


            QuestionArea.Visibility = Visibility.Visible;


            return 0;
        }



        private async void StartExp_Click(object sender, RoutedEventArgs e)
        {
            QuestionDialog dialog = new QuestionDialog("2", "K2MnO4", "1", "1");
           await dialog.ShowAsync();
            QuestionArea.Visibility = Visibility.Collapsed;
            Title.Visibility = Visibility.Collapsed;
            ExpBorder.Visibility = Visibility.Visible;
            await NextSessionAsync();
            QuestionArea.AnswerCorrectly += selectAnimation;



        }

        /*
         * private void test()
        {
            MessageDialog d = new MessageDialog("aha");
            d.ShowAsync();
        }
        */



        private async void Animation1() //旋转试管的动画,用于回答试管问题之后
        {
            TubeAni.Begin();
            RubberAni.Begin();
            PipeAni.Begin();
            KMnO4.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            TubeAni.Completed += (s, e1) => QuestionArea.InitWithQuestion();
        }

        private async void Animation2() //移动瓶子的动画,用于回答预热的问题之后
        {
            Flame.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            JarAni.Begin();
            WaterAni.Begin();
            await Task.Delay(2000);
            WaterAni.Completed += (s, e1) => QuestionArea.InitWithQuestion();
        }

        private async void Animation3() //冒气泡的动画，用于回答何时收集的问题之后
        {
            Bubble.Visibility = Visibility.Visible;
            BubbleUp.Begin();
            await Task.Delay(3000);
            QuestionArea.InitWithQuestion();

        }

        private void Animation4() //收集完毕的动画,用于移走导管的问题后
        {

            BubbleUp.Stop();
            Bubble.Visibility = Visibility.Collapsed;
            Oxygen.Visibility = Visibility.Visible;
            Air1.Begin();
            O2.Begin();
            O2.Completed += (s, e1) => showFinal();

        }

        private void selectAnimation(int index)
        {
            switch (index)
            {
                case 1: Animation1();break;
                case 2: Animation2(); break;
                case 3: Animation3(); break;
                case 4: Animation4(); break;
                default:break;
            }
        }

        private async void showFinal()
        {
            await Task.Delay(2000);
            MessageDialog d = new MessageDialog("你的分数是"+Convert.ToString(QuestionArea.allpoints));
            await d.ShowAsync();
            QuestionArea.Visibility = Visibility.Collapsed;
            ExpArea.Visibility = Visibility.Collapsed;
            Title.Visibility = Visibility.Visible;
            StartExp.Content = "已完成！";
            StartExp.IsEnabled = false;
        }

    }
}
