using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace URLtoSMS.UWP.Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tinify : Page
    {
        DataPackage dp = new DataPackage();

        public const string fbappid = "295723107448857";
        public const string fbsecret = "703bd59b1d19f44bbe22e2ca9899b150";

        public Tinify()
        {
            this.InitializeComponent();
            tb_tiny.Text = "";

            //SetUpPageAnimation();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_tiny.Text))
                btn_copy.IsEnabled = btn_share.IsEnabled = true;
            else
                btn_copy.IsEnabled = btn_share.IsEnabled = false;

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareTextHandler);


            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                Debug.WriteLine("BackRequested");
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }

                base.OnNavigatedTo(e);
            };
        }



        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }

        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Tinified!";
            request.Data.SetText(tb_tiny.Text);
        }




        private void tx_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ᵃ ᵇ ᶜ ᵈ ᵉ ᶠ ᵍ ʰ ᶦ ʲ ᵏ ᶫ ᵐ ᶰ ᵒ ᵖ ᑫ ʳ ˢ ᵗ ᵘ ᵛ ʷ ˣ ʸ ᶻ
            // ﹗@#﹩﹪^﹠﹡⁽⁾__  ⁼⁻⁰⁹⁸⁷⁶⁵⁴³²¹ ,⋅/<> [] |}{ ﹔'﹕" ﹔ ﹕
            //if (!String.IsNullOrEmpty(tx_input.Text))
            //    tx_input.Text = tx_input.Text.ToLower();

            tb_tiny.Text = tx_input.Text.ToLower().Replace('a', 'ᵃ').Replace('b', 'ᵇ').Replace('c', 'ᶜ').Replace('d', 'ᵈ').Replace('e', 'ᵉ').Replace('f', 'ᶠ').Replace('g', 'ᵍ').Replace('h', 'ʰ').Replace('i', 'ᶦ').Replace('j', 'ʲ').Replace('k', 'ᵏ').Replace('l', 'ᶫ').Replace('m', 'ᵐ').Replace('n', 'ᶰ').Replace('o', 'ᵒ').Replace('p', 'ᵖ').Replace('q', 'ᑫ').Replace('r', 'ʳ').Replace('s', 'ˢ').Replace('t', 'ᵗ').Replace('u', 'ᵘ').Replace('v', 'ᵛ').Replace('w', 'ʷ').Replace('x', 'ˣ').Replace('y', 'ʸ').Replace('z', 'ᶻ').Replace('!', '﹗').Replace('@', '@').Replace('#', '#').Replace('$', '﹩').Replace('%', '﹪').Replace('^', '^').Replace('&', '﹠').Replace('*', '﹡').Replace('(', '⁽').Replace(')', '⁾').Replace('-', '⁻').Replace('=', '⁼').Replace('+', '+').Replace('_', '_').Replace('1', '¹').Replace('2', '²').Replace('3', '³').Replace('4', '⁴').Replace('5', '⁵').Replace('6', '⁶').Replace('7', '⁷').Replace('8', '⁸').Replace('9', '⁹').Replace('0', '⁰').Replace(';', '﹔').Replace(':', '﹕').Replace('.', '⋅').Replace(',', ',');

            if (!String.IsNullOrEmpty(tb_tiny.Text))
                btn_copy.IsEnabled = btn_share.IsEnabled = true;
            else
                btn_copy.IsEnabled = btn_share.IsEnabled = false;

            dp.RequestedOperation = DataPackageOperation.Copy;
            dp.SetText(tb_tiny.Text);
        }

        private async void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetContent(dp);

            var dialog = new Windows.UI.Popups.MessageDialog(tb_tiny.Text + " succesfully copied!");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { Id = 0 });
            dialog.DefaultCommandIndex = 0;
            var result = await dialog.ShowAsync();
        }

        private void btn_share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void btn_tweet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
