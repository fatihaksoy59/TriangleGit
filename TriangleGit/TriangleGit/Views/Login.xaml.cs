using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangleGit.Tables;
using Xamarin.Forms;


namespace TriangleGit.Views
{
    public partial class Login : ContentPage
    {
        ActivityIndicator indicator = new ActivityIndicator();

        Constant c = new Constant();
        Test t = new Test();
        Entry email = new Entry
        {
            Placeholder = "Email",
            PlaceholderColor = Color.FromHex("#1C9A4A"),
            TextColor = Color.FromHex("#1C9A4A")

        };
        Entry password = new Entry
        {
            Placeholder = "Password",
            PlaceholderColor = Color.FromHex("#1C9A4A"),
            TextColor = Color.FromHex("#1C9A4A"),
            IsPassword = true

        };
        public Login()
        {
            InitializeComponent();
            Content = t.layout;
            t.stkBottom.BackgroundColor = Color.FromHex("FCFCFC");
            t.eventImg.IsVisible = false;
            t.homeImg.IsVisible = false;
            t.searchImg.IsVisible = false;
            t.profileImg.IsVisible = false;
            t.mapImage.IsVisible = false;
            AddEntries();
            indicator.IsRunning = false;
        }

        public void AddEntries()
        {

            Frame frm = new Frame
            {
                Margin = new Thickness(24, 50, 24, 0),
                Content = email,
                OutlineColor = Color.FromHex("#1C9A4A")

            };
            Frame frm2 = new Frame
            {
                Margin = new Thickness(24, 15, 24, 0),
                Content = password,
                OutlineColor = Color.FromHex("#1C9A4A")
            };
            Button btn = new Button();
            btn.Margin = new Thickness(20, 15, 20, 0);
            btn.BackgroundColor = Color.FromHex("#1C9A4A");
            btn.Text = "LOGIN !";
            btn.Clicked += Btn_Clicked;
            Button btnRegister = new Button
            {
                Margin = new Thickness(20, 10, 20, 10),
                BackgroundColor = Color.FromHex("#1C9A4A"),
                Text = "REGISTER"
            };

            t.stkCenter.Children.Add(indicator);
            t.stkCenter.Children.Add(frm);
            t.stkCenter.Children.Add(frm2);
            t.stkCenter.Children.Add(btn);
            t.stkCenter.Children.Add(btnRegister);
        }

        private async void Btn_Clicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            try
            {
                var data = await GetData();


                if (data.Count > 0)
                {
                    await Navigation.PushModalAsync(new Home(data));

                }
                else
                {
                    await DisplayAlert("Hatalı Giriş", "Belirtilen Email Yok veya Şifre Yanlış", "OK");
                }
            }
            catch (Exception Hata)
            {

                await DisplayAlert("Oops", Hata.Message + " Sanırım Bir Hata Oluştu :(", "Okey");

            }
            indicator.IsRunning = false;
        }
        public async Task<ObservableCollection<Users>> GetData()
        {
            List<Users> items = await c.usersTable
                               .Where(userItem => userItem.Email == email.Text && userItem.Password == password.Text)
                               .ToListAsync();
            return new ObservableCollection<Users>(items);
        }
    }
}
