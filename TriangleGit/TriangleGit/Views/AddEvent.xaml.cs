using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TriangleGit.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TriangleGit.Views
{
    public partial class AddEvent : ContentPage
    {
        Test t = new Test();
        Constant c = new Constant();
        Map map;
        ObservableCollection<Users> users;
        Entry eventname = new Entry
        {
            Placeholder = "Etkinlik Adı",
            PlaceholderColor = Color.FromHex("#1C9A4A"),
            TextColor = Color.FromHex("#1C9A4A")

        };
        Entry name2 = new Entry
        {
            Placeholder = "Etkinlik Yeri",
            Margin = new Thickness(10, 2, 10, 0),
            PlaceholderColor = Color.FromHex("#1C9A4A"),
            TextColor = Color.FromHex("#1C9A4A")

        };
        public AddEvent(ObservableCollection<Users> user)
        {
            users = user;
            InitializeComponent();
            Content = t.layout;

            Frame f = new Frame
            {

                HeightRequest = 15,
                WidthRequest = 15,
                Content = t.eventImg,

                OutlineColor = Color.Black
            };

            t.g.Children.Add(f, 2, 0);


            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new Home(user));

            };

            t.homeImg.GestureRecognizers.Add(tapGestureRecognizer);

            var tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += (s, e) => {
                Navigation.PushModalAsync(new AddEventsOnMap(user));
            };

            t.mapImage.GestureRecognizers.Add(tapGestureRecognizer2);


            var tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new ProfilePage(user));
            };

            t.profileImg.GestureRecognizers.Add(tapGestureRecognizer3);

            var tapGestureRecognizer4 = new TapGestureRecognizer();
            tapGestureRecognizer4.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new AddEvent(user));
            };

            t.eventImg.GestureRecognizers.Add(tapGestureRecognizer4);

            var tapGestureRecognizer5 = new TapGestureRecognizer();
            tapGestureRecognizer5.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new Search(user));
            };

            t.searchImg.GestureRecognizers.Add(tapGestureRecognizer5);
            CreateEvent();



        }
        public void CreateEvent()
        {
            map = new Map(MapSpan.FromCenterAndRadius(new Position(38.963745, 35.243322), Distance.FromKilometers(550)))
            {
                Margin = new Thickness(10, 0, 10, 0),
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };





            DatePicker date = new DatePicker();
            date.TextColor = Color.FromHex("#1C9A4A");


            Button btn = new Button();
            btn.Margin = new Thickness(20, 20, 20, 10);
            btn.BackgroundColor = Color.FromHex("#1C9A4A");
            btn.Text = "OLUSTUR !";


            Frame frm = new Frame
            {
                Margin = new Thickness(10, 10, 10, 0),
                Content = eventname,
                OutlineColor = Color.FromHex("#1C9A4A")
            };
            Frame frm2 = new Frame
            {
                Margin = new Thickness(10, 10, 10, 0),
                Content = date,
                OutlineColor = Color.FromHex("#1C9A4A")
            };


            t.stkCenter.Children.Add(frm);
            t.stkCenter.Children.Add(frm2);
            t.stkCenter.Children.Add(name2);
            t.stkCenter.Children.Add(map);
            t.stkCenter.Children.Add(btn);
            btn.Clicked += Btn_Clicked;
        }

        private async void Btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                await getData(name2.Text);

            }
            catch (Exception ea)
            {
                await DisplayAlert("a", ea.ToString(), "ok");
            }
        }
        public async Task getData(string search)
        {

            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + search + "&key=AIzaSyByrvRy7zsuaexF5uynFLFq4wmNumvMkR4";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Model.RootObject>(response);


            try
            {
                var lat = data.results[0].geometry.location.lat;
                var lng = data.results[0].geometry.location.lng;
                var adress = data.results[0].formatted_address;
                var name = data.results[0].name;

                Events events = new Events { userID = users[0].Id, eventName = eventname.Text, lat = lat, lng = lng, Time = DateTime.Today, placeName = name };
                await c.eventsTable.InsertAsync(events);

                var position = new Position(lat, lng); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = name,
                    Address = adress
                };
                map.Pins.Add(pin);
                await DisplayAlert("Tamamdır", "Etkinlik Oluşturuldu", "Ok");
            }
            catch (Exception e)
            {
                await DisplayAlert(e.Message + "Bulamadık", "Maalesef Aradığınız Yeri Bulamadık", "Tamam :(");
            }




        }
    }
}
