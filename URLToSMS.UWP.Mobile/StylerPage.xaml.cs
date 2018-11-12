using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URLtoSMS.UWP.Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StylerPage : Page
    {

        string[][] textStyles = {
            new string[] {"ᗩ", "ᗷ", "ᑕ", "ᗪ", "E", "ᖴ", "G", "ᕼ", "I", "ᒍ", "K", "ᒪ", "ᗰ", "ᑎ", "O", "ᑭ", "ᑫ", "ᖇ", "ᔕ", "T", "ᑌ", "ᐯ", "ᗯ", "᙭", "Y", "ᘔ"},
            new string[] {"[̲̅a̲̅]", "[̲̅b̲̅]", "[̲̅c̲̅]", "[̲̅d̲̅]", "[̲̅e̲̅]", "[̲̅f̲̅]", "[̲̅g̲̅]", "[̲̅h̲̅]", "[̲̅i̲̅]", "[̲̅j̲̅]", "[̲̅k̲̅]", "[̲̅l̲̅]", "[̲̅m̲̅]", "[̲̅n̲̅]", "[̲̅o̲̅]", "[̲̅p̲̅]", "[̲̅q̲̅]", "[̲̅r̲̅]", "[̲̅s̲̅]", "[̲̅t̲̅]", "[̲̅u̲̅]", "[̲̅v̲̅]", "[̲̅w̲̅]", "[̲̅x̲̅]", "[̲̅y̲̅]", "[̲̅z̲̅]"},
            new string[] {"α", "в", "¢", "∂", "є", "f", "g", "н", "ι", "נ", "к", "ℓ", "м", "и", "σ", "ρ", "q", "я", "ѕ", "т", "υ", "ν", "ω", "χ", "у", "z"},
            new string[] {"₳", "฿", "₵", "Đ", "Ɇ", "₣", "₲", "Ⱨ", "ł", "J", "₭", "Ⱡ", "₥", "₦", "Ø", "₱", "Q", "Ɽ", "₴", "₮", "Ʉ", "V", "₩", "Ӿ", "Ɏ", "Ⱬ"},
            new string[] {"ａ", "ｂ", "ｃ", "ｄ", "ｅ", "ｆ", "ｇ", "ｈ", "ｉ", "ｊ", "ｋ", "ｌ", "ｍ", "ｎ", "ｏ", "ｐ", "ｑ", "ｒ", "ｓ", "ｔ", "ｕ", "ｖ", "ｗ", "ｘ", "ｙ", "ｚ"},
            new string[] {"ᴀ", "ʙ", "ᴄ", "ᴅ", "ᴇ", "ᴈ", "ɢ", "ʜ", "ɪ", "ᴊ", "ᴋ", "ʟ", "ᴍ", "ɴ", "ᴏ", "ᴘ", "ᴓ", "ʀ", "s", "ᴛ", "ᴜ", "ᴠ", "ᴡ", "ᴥ", "ʏ", "ᴢ"},
            new string[] {"̶a", "̶b", "̶c", "̶d", "̶e", "̶f", "̶g", "̶h", "̶i", "̶j", "̶k", "̶l", "̶m", "̶n", "̶o", "̶p", "̶q", "̶r", "̶s", "̶t", "̶u", "̶v", "̶w", "̶x", "̶y", "̶z"},
            new string[] {"𝔞", "𝔟", "𝔠", "𝔡", "𝔢", "𝔣", "𝔤", "𝔥", "𝔦", "𝔧", "𝔨", "𝔩", "𝔪", "𝔫", "𝔬", "𝔭", "𝔮", "𝔯", "𝔰", "𝔱", "𝔲", "𝔳", "𝔴", "𝔵", "𝔶", "𝔷"},
            new string[] {"🅰", "🅱", "🅲", "🅳", "🅴", "🅵", "🅶", "🅷", "🅸", "🅹", "🅺", "🅻", "🅼", "🅽", "🅾", "🅿", "🆀", "🆁", "🆂", "🆃", "🆄", "🆅", "🆆", "🆇", "🆈", "🆉"},
            new string[] { "🄰", "🄱", "🄲", "🄳", "🄴", "🄵", "🄶", "🄷", "🄸", "🄹", "🄺", "🄻", "🄼", "🄽", "🄾", "🄿", "🅀", "🅁", "🅂", "🅃", "🅄", "🅅", "🅆", "🅇", "🅈", "🅉"},
            new string[] {"🅐", "🅑", "🅒", "🅓", "🅔", "🅕", "🅖", "🅗", "🅘", "🅙", "🅚", "🅛", "🅜", "🅝", "🅞", "🅟", "🅠", "🅡", "🅢", "🅣", "🅤", "🅥", "🅦", "🅧", "🅨", "🅩"},
            new string[] {"ⓐ", "ⓑ", "ⓒ", "ⓓ", "ⓔ", "ⓕ", "ⓖ", "ⓗ", "ⓘ", "ⓙ", "ⓚ", "ⓛ", "ⓜ", "ⓝ", "ⓞ", "ⓟ", "ⓠ", "ⓡ", "ⓢ", "ⓣ", "ⓤ", "ⓥ", "ⓦ", "ⓧ", "ⓨ", "ⓩ"},
            new string[] { "𝒶", "𝒷", "𝒸", "𝒹", "ℯ", "𝒻", "ℊ", "𝒽", "𝒾", "𝒿", "𝓀", "𝓁", "𝓂", "𝓃", "ℴ", "𝓅", "𝓆", "𝓇", "𝓈", "𝓉", "𝓊", "𝓋", "𝓌", "𝓍", "𝓎", "𝓏"},
            new string[] { "𝗮", "𝗯", "𝗰", "𝗱", "𝗲", "𝗳", "𝗴", "𝗵", "𝗶", "𝗷", "𝗸", "𝗹", "𝗺", "𝗻", "𝗼", "𝗽", "𝗾", "𝗿", "𝘀", "𝘁", "𝘂", "𝘃", "𝘄", "𝘅", "𝘆", "𝘇"},
new string[] {"ą", "β", "ȼ", "ď", "€", "ƒ", "ǥ", "h", "ɨ", "j", "Ќ", "ℓ", "ʍ", "ɲ", "๏", "ρ", "ǭ", "я", "$", "ţ", "µ", "˅", "ώ", "ж", "¥", "ƶ"},
new string[] {"å", "β", "ç", "ď", "£", "ƒ", "ğ", "ȟ", "ȋ", "j", "ķ", "Ƚ", "ɱ", "ñ", "¤", "ק", "ǭ", "ȑ", "§", "ț", "ɥ", "√", "Ψ", "×", "ÿ", "ž" },
new string[] {"ą", "þ", "ȼ", "ȡ", "ƹ", "ƒ", "ǥ", "ɦ", "ɨ", "ǰ", "ƙ", "Ł", "ʍ", "ɲ", "ǿ", "ρ", "ǭ", "ř", "ȿ", "Ʈ", "µ", "˅", "ώ", "ж", "¥", "ƶ"},
new string[] {"ά", "в", "ς", "ȡ", "έ", "ғ", "ģ", "ħ", "ί", "ј", "ķ", "Ļ", "м", "ή", "ό", "ρ", "q", "ŕ", "ş", "ţ", "ù", "ν", "ώ", "x", "ч", "ž"},
new string[] {"Ã", "β", "Č", "Ď", "Ẹ", "Ƒ", "Ğ", "Ĥ", "Į", "Ĵ", "Ќ", "Ĺ", "ϻ", "Ň", "Ỗ", "Ƥ", "Ǫ", "Ř", "Ŝ", "Ť", "Ǘ", "ϋ", "Ŵ", "Ж", "Ў", "Ż"},
new string[] {"α", "в", "¢", "∂", "є", "f", "g", "н", "ι", "נ", "к", "ℓ", "м", "и", "σ", "ρ", "q", "я", "ѕ", "т", "υ", "ν", "ω", "χ", "у", "z"},
new string[] {"ค", "๒", "ς", "๔", "є", "Ŧ", "ﻮ", "ђ", "เ", "ן", "к", "l", "๓", "ภ", "๏", "ק", "ợ", "г", "ร", "t", "ย", "ש", "ฬ", "ץ", "א", "z"},
new string[] {"Ă", "β", "Č", "Ď", "Ĕ", "Ŧ", "Ğ", "Ĥ", "Ĩ", "Ĵ", "Ķ", "Ĺ", "М", "Ń", "Ő", "Р", "Q", "Ŕ", "Ś", "Ť", "Ú", "V", "Ŵ", "Ж", "Ŷ", "Ź"},
new string[] {"4", "8", "(", "d", "3", "f", "9", "h", "!", "j", "k", "1", "m", "n", "0", "p", "q", "r", "5", "7", "u", "v", "w", "x", "y", "2"},
new string[] {"ɐ", "Q", "ɔ", "P", "ǝ", "ɟ", "ƃ", "ɥ", "!", "ɾ", "ʞ", "ן", "ɯ", "U", "o", "d", "b", "ɹ", "s", "ʇ", "n", "ʌ", "ʍ", "x", "ʎ", "z"},
new string[] {"მ", "ჩ", "ე", "ძ", "პ", "f", "ც", "h", "ἶ", "ქ", "κ", "l", "ო", "ῆ", "õ", "ρ", "გ", "Γ", "ჰ", "ན", "υ", "ὗ", "w", "ჯ", "ყ", "ɀ"},
new string[] {"Ä", "B", "Ċ", "Đ", "Ë", "₣", "Ġ", "Ȟ", "Ï", "Ĵ", "Ķ", "Ļ", "M", "Ņ", "Ö", "P", "Ǭ", "Ŗ", "Ś", "Ț", "Ů", "V", "Ŵ", "X", "Ÿ", "Ź"},
new string[] {"α", "в", "c", "∂", "ε", "ғ", "g", "н", "ι", "נ", "к", "ℓ", "м", "η", "σ", "ρ", "q", "я", "s", "т", "υ", "v", "ω", "x", "ү", "z"}
        };

        public StylerPage()
        {
            this.InitializeComponent();

            lv_results.Visibility = Visibility.Collapsed;

            List<string> _styles = new List<string>();
            _styles.Add("Tiny");
            _styles.Add("Bubble");
            _styles.Add("Dark Bubble");
            _styles.Add("Reverse");
            _styles.Add("Box");
            _styles.Add("Upside Down");
            _styles.Add("Superscript");
            _styles.Add("Kana");
            _styles.Add("Small Caps");            

            asb_input.Text = "URLtoSMS";
            TinyText("URLtoSMS");

            lv_results.Visibility = Visibility.Visible;
        }

        private void asb_input_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string val = sender.Text;

            // Tinify
            TinyText(val);
            // btn_zalgo.Content = LoadZalgo(val);

            lv_results.Visibility = Visibility.Visible;
        }

        string res;
        private void TinyText(string val)
        {
            List<string> _outlist = new List<string>();

            for (int x = 0; x < textStyles.Length; x++)
            {
                foreach (char c in val)
                {
                    if (Regex.IsMatch(c.ToString(), "[a-z]", RegexOptions.IgnoreCase))
                    {
                        int index = char.ToUpper(c) - 65; //index == 1

                        res += textStyles[x][index];
                    }
                    else
                    {
                        res += c;
                    }
                }

                _outlist.Add(res);
                res = string.Empty;
            }
            lv_results.ItemsSource = _outlist;
        }

        private void asb_input_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // Hide listview
                lv_results.Visibility = Visibility.Collapsed;
            }
        }

        private void lv_results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {  //Assign DataTemplate for selected items
                foreach (var item in e.AddedItems)
                {
                    ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                    lvi.ContentTemplate = (DataTemplate)this.Resources["Selected"];
                }
                //Remove DataTemplate for unselected items
                foreach (var item in e.RemovedItems)
                {
                    ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                    lvi.ContentTemplate = (DataTemplate)this.Resources["Normal"];
                }
            }
            catch { }
        }

        private void btn_share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareZipHandler);

            DataTransferManager.ShowShareUI();
        }

        private void ShareZipHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request; request = args.Request;
            request.Data.Properties.Title = lv_results.SelectedItem as string;
            request.Data.Properties.Description = "Share " + lv_results.SelectedItem as string;
            request.Data.Properties.ApplicationName = "URLtoSMS";
            request.Data.SetWebLink(new Uri("https://www.microsoft.com/store/apps/9nblggh4qlv0"));
            request.Data.SetText(lv_results.SelectedItem as string);
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            DataPackage data = new DataPackage();
            data.SetText(lv_results.SelectedItem as string);

            Clipboard.SetContent(data);

            // Show Toast
            ShowToast("Text copied!", lv_results.SelectedItem as string);
        }

        private void ShowToast(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(5);
            ToastNotifier.Show(toast);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_send_Click(object sender, RoutedEventArgs e)
        {
            var sms = new ChatMessage();
            sms.Body = lv_results.SelectedItem as string;

            await ChatMessageManager.ShowComposeSmsMessageAsync(sms);
        }

        private void btn_send_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
