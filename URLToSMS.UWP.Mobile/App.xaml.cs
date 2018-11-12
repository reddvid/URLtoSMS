using System;
using System.Net;
using System.Text.RegularExpressions;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace URLtoSMS.UWP.Mobile
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public Parameter p = new Parameter();

        public string uridata { get; set; }
        public string query { get; set; }
        public string msg { get; set; }
        public string re { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            this.Suspending += OnSuspending;
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            DataPackage dataPackage = new DataPackage();

            try
            {
                if (args.Kind == ActivationKind.Protocol)
                {
                    ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs;
                    // TODO: Handle URI activation
                    // The received URI is eventArgs.Uri.AbsoluteUri
                    var sms = new ChatMessage();

                    uridata = WebUtility.HtmlDecode(eventArgs.Uri.OriginalString);
                    query = WebUtility.HtmlDecode(eventArgs.Uri.Query);

                    if (!String.IsNullOrEmpty(query)) // Has content
                    {
                        // Try to copy number and content between ? and =
                        // Try to get number
                        sms.Recipients.Add(getbetween(uridata, ":", "?"));
                        sms.Body = getbetween(uridata, "=");

                        p.rec = getbetween(uridata, ":", "?");
                        p.msg = (uridata.Substring(uridata.IndexOf("="))).Replace("=", "");
                    }
                    else
                    {
                        string n = (uridata.Substring(uridata.IndexOf(":"))).Replace(":", "");

                        if (Regex.Match(n, @"^[0-9]+(-[0-9]+)+$").Success)
                        {
                            // Just copy number
                            sms.Recipients.Add(n);
                            p.rec = n;
                            p.msg = "";
                        }
                        else
                        {
                            sms.Body = n;
                            p.rec = "";
                            p.msg = n;
                        }
                    }

                    dataPackage.RequestedOperation = DataPackageOperation.Copy;
                    dataPackage.SetText(p.rec + " " + p.msg);

                    Clipboard.SetContent(dataPackage);

                    await ChatMessageManager.ShowComposeSmsMessageAsync(sms);
                }
            }
            catch { }

        }

        private string getbetween(string uridata, string v1)
        {
            int _start, _end;
            if (uridata.Contains(v1))
            {
                _start = uridata.IndexOf(v1, 0) + v1.Length;
                _end = uridata.Length;
                return uridata.Substring(_start, _end - _start);
            }
            else
            {
                return "";
            }
        }

        private string getbetween(string uridata, string v1, string v2)
        {
            int _start, _end;
            if (uridata.Contains(v1) && uridata.Contains(v2))
            {
                _start = uridata.IndexOf(v1, 0) + v1.Length;
                _end = uridata.IndexOf(v2, _start);
                return uridata.Substring(_start, _end - _start);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            //if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    var statusBar = StatusBar.GetForCurrentView();
            //    if (statusBar != null)
            //    {
            //        statusBar.BackgroundOpacity = 1;
            //        statusBar.BackgroundColor = Colors.Black;
            //        statusBar.ForegroundColor = Colors.White;
            //    }
            //}

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;

                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    rootFrame.CanGoBack ?
                    AppViewBackButtonVisibility.Visible :
                    AppViewBackButtonVisibility.Collapsed;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter

                rootFrame.Navigate(typeof(MainPage), e.Arguments);

            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        public class Parameter
        {
            public string msg { get; set; }
            public string rec { get; set; }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Each time a navigation event occurs, update the Back button's visibility
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
    }
}
