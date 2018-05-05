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
    internal delegate void QuestionEventHandler(int i);

    public sealed partial class QuestionControl : UserControl
    {
        internal event QuestionEventHandler AnswerCorrectly;
        public string getChoiceRecord()
        {
            string record = "";
            for (int i = 0; i < questionList.Count; i++)
            {
                record += "问题描述： " + questionList[i].questionDescription+"\n";
                record += "您的选择：";
                for (int j = 0; j < questionList[i].choiceRecord.Count; j++)
                {
                    record += questionList[i].choiceRecord[j] + "\n";
                }
                record += "正确答案：" + questionList[i].RightAnswer+"\n\n";
            }
            return record;
        }
        public QuestionControl()
        {

            this.InitializeComponent();
            initQuestion();
            InitWithQuestion();
        }
        public int allpoints = 100;
        int index = 0;
        Question tempQuestion = new Question();
        List<Question> questionList = new List<Question>();
        public void decreasePoint(int point)
        {
            allpoints -= point;
            PointsAll.Text = "当前分数：" + Convert.ToString(allpoints);
        }
        void initQuestion()
        {
            Question q1 = new Question("试管应如何放置？", "水平向左下倾斜", "水平向右下倾斜", "完全水平放置", "完全竖直放置",
                "水平向左下倾斜", 20);
            Question q2 = new Question("如何使用酒精灯加热？", "直接加热", "先进行预热", "以上二者都可行", "以上二者均不可行",
                "先进行预热", 20);
            Question q3 = new Question("关于收集气体的说法中，正确的是？", "从开始有气泡就收集气体", "等气泡产生速度稳定后收集", "以上二者均可行", "以上二者均不可行",
                "等气泡产生速度稳定后收集", 10);
            Question q4 = new Question("熄灭酒精灯之前应做什么？", "先将收集的气体密封", "先将导管从液体出拿出", "先把UWP作业写完", "先洗手",
                "先将导管从液体出拿出", 10);
            this.questionList.Add(q1);
            this.questionList.Add(q2);
            this.questionList.Add(q3);
            this.questionList.Add(q4);
        }
        public void InitWithQuestion()
        {
            tempQuestion = this.questionList[index];
            index++;
            questionDescription.Text = tempQuestion.questionDescription;

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
            if (answer == tempQuestion.RightAnswer)
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
            questionList[index - 1].choiceRecord.Add(choose.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
                AnswerCorrectly(index);
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
            questionList[index - 1].choiceRecord.Add(choose.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
                AnswerCorrectly(index);
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
            questionList[index - 1].choiceRecord.Add(choose.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
                AnswerCorrectly(index);
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
            questionList[index - 1].choiceRecord.Add(choose.Content as string);
            
            Boolean is_right = IsRight(ChooseD.Content as string);
            if (is_right)
            {
                choose.Background = new SolidColorBrush(Colors.LightGreen);
                AnswerCorrectly(index);
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

