using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace App8
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => App.Current.MainPage.DisplayAlert("Error", "Invalid Login, try again", "OK");
            vm.DisplayOK += () => App.Current.MainPage.DisplayAlert("Komunikat", "Zalogowaleś się", "OK");
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };

            
        }

        private void OpenScanner(object sender, EventArgs e)
        {
            Scanner();
        }

        public async void Scanner()
        {

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
              
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    ResultLabel.Text = result.Text;
                    DisplayAlert("Wynik", result.Text, "OK");
                });
            };


            await Navigation.PushAsync(ScannerPage);

        }
    }
}
