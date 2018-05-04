using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace Laboratory
{
    public sealed partial class QuestionDialog : ContentDialog
    {
        public QuestionDialog()
        {
            this.InitializeComponent();
            
        }
        ChemicalAnswer cm = new ChemicalAnswer();
        ChemicalAnswer ci = new ChemicalAnswer();
        bool isAnswered = false;
        class ChemicalAnswer
        {
            public string i_blank1;
            public string i_blank2;
            public string i_blank3;
            public string i_blank4;

        }
        public QuestionDialog(string blank1,string blank2,string blank3,string blank4)
        {
            this.InitializeComponent();
            
            cm.i_blank1 = blank1;
            cm.i_blank2 = blank2;
            cm.i_blank3 = blank3;
            cm.i_blank4 = blank4;
        }
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            isAnswered = true;
            var deferral = args.GetDeferral();
             args.Cancel = await ValidateForm();
            deferral.Complete();
      
        }
       // Returns true if the MessageDialog was shown, otherwise false
        private async Task<bool> ValidateForm()
        {
            ci.i_blank1 = this.x_KMNO4.Text;
            ci.i_blank2 =  this.x_K2MNO4.Text;
            ci.i_blank3 = this.x_MNO2.Text;
            ci.i_blank4 = this.x_O2.Text;

            var strci = JsonConvert.SerializeObject(ci);
            var strcm = JsonConvert.SerializeObject(cm);


            if (strci != strcm)
            {
                var dialog = new MessageDialog("答案错误，请重试！");
                //do something here
                await dialog.ShowAsync();
                this.x_KMNO4.Text = cm.i_blank1;
                this.x_K2MNO4.Text = cm.i_blank2;
                this.x_MNO2.Text = cm.i_blank3;
                this.x_O2.Text = cm.i_blank4;

               
                this.x_KMNO4.Foreground = new SolidColorBrush(Colors.Red);
                this.x_K2MNO4.Foreground = new SolidColorBrush(Colors.Red);
                this.x_MNO2.Foreground = new SolidColorBrush(Colors.Red);
                this.x_O2.Foreground = new SolidColorBrush(Colors.Red);

                return true;
            }
            else
            {
                var dialog = new MessageDialog("答案正确！");
                //do something here
                await dialog.ShowAsync();
                return false;
            }
          
        }
        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!isAnswered)

            //do something here
            {
                var dialog = new MessageDialog("放弃作答！");
                await dialog.ShowAsync();
            }
            else
            {

            }
            //这里做减分处理
        }
    }
}
