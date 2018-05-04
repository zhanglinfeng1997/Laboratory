using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Laboratory
{
    internal delegate void QuestionEventHandler();

    public sealed partial class QuestionControl : UserControl
    {
        internal event QuestionEventHandler AnswerCorrectly;

        public QuestionControl()
        {

            this.InitializeComponent();
        }
        int allpoints = 100;
        Question tempQuestion = new Question();
        void InitWithQuestion()
        {
            //      tempQuestion = new Question();
            ChooseA.Background = new SolidColorBrush(Colors.AliceBlue);
            ChooseB.Background = new SolidColorBrush(Colors.AliceBlue);
            ChooseC.Background = new SolidColorBrush(Colors.AliceBlue);
            ChooseD.Background = new SolidColorBrush(Colors.AliceBlue);
            //      ChooseA.Content = "sasass";
            ChooseA.Content = tempQuestion.ChoiceA;
            ChooseB.Content = tempQuestion.ChoiceB;
            ChooseC.Content = tempQuestion.ChoiceC;
            ChooseD.Content = tempQuestion.ChoiceD;
            String a = "此题分数：" + Convert.ToString(tempQuestion.Points);
            PointsNow.Text = a;
        }

        public Boolean IsRight(string answer)
        {
            if (answer == /*tempQuestion.RightAnswer*/(string)ChooseA.Content)
                return true;
            else
            {
                return false;
            }
        }
        private void ChooseA_Click(object sender, RoutedEventArgs e)
        {
            var choose = (Button)sender;
            Boolean is_right = IsRight(ChooseA.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
                AnswerCorrectly();
            }
            else
            {
                choose.Background = new SolidColorBrush(Colors.LightGray);
                allpoints = allpoints - tempQuestion.Points;
                PointsAll.Text = "当前分数：" + Convert.ToString(allpoints);
            }
            //InitWithQuestion();
        }

        private void ChooseB_Click(object sender, RoutedEventArgs e)
        {
            var choose = (Button)sender;
            Boolean is_right = IsRight(ChooseB.Content as string);

            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                choose.Background = new SolidColorBrush(Colors.LightGray);
                allpoints = allpoints - tempQuestion.Points;
                PointsAll.Text = "当前分数：" + Convert.ToString(allpoints);
            }
        }

        private void ChooseC_Click(object sender, RoutedEventArgs e)
        {
            var choose = (Button)sender;
            Boolean is_right = IsRight(ChooseC.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                choose.Background = new SolidColorBrush(Colors.LightGray);
                allpoints = allpoints - tempQuestion.Points;
                PointsAll.Text = "当前分数：" + Convert.ToString(allpoints);
            }
        }

        private void ChooseD_Click(object sender, RoutedEventArgs e)
        {
            var choose = (Button)sender;
            Boolean is_right = IsRight(ChooseD.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                choose.Background = new SolidColorBrush(Colors.LightGray);
                allpoints = allpoints - tempQuestion.Points;
                PointsAll.Text = "当前分数：" + Convert.ToString(allpoints);
            }
        }
    }
}

