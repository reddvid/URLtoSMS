using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace URLtoSMS.UWP.Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }      

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareTextHandler);

            base.OnNavigatedTo(e);
        }
       
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareTextHandler);

            base.OnNavigatedFrom(e);
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request; request = e.Request;
            request.Data.Properties.Title = "URLtoSMS for Windows 10";
            request.Data.Properties.Description = "Share URLtoSMS";
            request.Data.Properties.ApplicationName = "URLtoSMS";
            request.Data.Properties.PackageFamilyName = "3ff06698-af07-473d-904d-d2433d5cbfad_5nhfv7867x3ar";
            request.Data.SetWebLink(new Uri("https://www.microsoft.com/store/apps/9nblggh4qlv0"));
            request.Data.SetText("Download URLtoSMS for Windows 10. #URLtoSMS #UWP");
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Publisher?name=Red David"));
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("redappsupport@outlook.com"));

            emailMessage.Body = String.Empty;

            emailMessage.Subject = "[FEEDBACK] URLtoSMS";

            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private void Tinify_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Tinify));
        }

        private async void Button_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://reddavidapps.azurewebsites.net/URLtoSMS-TestPage.html"));
        }


        private async void OpenSamplePage_Click(object sender, RoutedEventArgs e)
        {
            string htmlfile = @"TestHTML.html";

            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(htmlfile);

            if (file != null)
            {
                // Launch the retrieved file
                var success = await Launcher.LaunchFileAsync(file);
            }
            else
            {
                await Launcher.LaunchUriAsync(new Uri("https://1drv.ms/u/s!AngBQuA6rJL-kpQ5IFhOC31KTkbuXg"));
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            //Load Version
            VersionTextBlock.Text = new DeviceInfo().AppVersion;
        }

        private void hl_feedback_Click(object sender, RoutedEventArgs e)
        {
            SendFeedback();
        }

        private async void SendFeedback()
        {
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
            else
            {
                var emailMessage = new EmailMessage();
                emailMessage.To.Add(new EmailRecipient("redappsupport@outlook.com"));
                emailMessage.Subject = "[FEEDBACK] URLtoSMS" + GetAppVersion();
                await EmailManager.ShowComposeNewEmailAsync(emailMessage);
            }
        }

        private string GetAppVersion()
        {
            return " version " + new DeviceInfo().AppVersion;
        }

        private async void btn_rate_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9nblggh4qlv0"));
        }

        private void btn_share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private async void btn_fb_Click(object sender, RoutedEventArgs e)
        {
            var fbSuccess = await Launcher.LaunchUriAsync(new Uri("fb://profile?id=reddvidapps"));
            if (!fbSuccess)
            {
                await Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/reddvidapps"));
            }
        }

        private async void btn_twitter_Click(object sender, RoutedEventArgs e)
        {
            var tweeSuccess = await Launcher.LaunchUriAsync(new Uri("twitter://user?screen_name=reddvid"));
            if (!tweeSuccess)
            {
                await Launcher.LaunchUriAsync(new Uri("https://www.twitter.com/reddvid"));
            }
        }

        private async void showmoreapps_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Publisher?name=Red David"));
        }

        // Additional Codes for About Page


    }
}
