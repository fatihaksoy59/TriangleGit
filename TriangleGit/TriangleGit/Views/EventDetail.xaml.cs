using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangleGit.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TriangleGit.Views
{
    public partial class EventDetail : ContentPage
    {
        Button btn = new Button();
        Test t = new Test();
        Constant c = new Constant();
        ObservableCollection<Users> user;
        string idLocal = "";
        public EventDetail(ObservableCollection<Users> users, string id)
        {
            InitializeComponent();
            user = users;
            idLocal = id;
            Content = t.layout;
            getData();

            btn.Margin = new Thickness(10, 5, 10, 5);
            btn.Text = "Etkinliğe katıl";
            btn.BackgroundColor = Color.FromHex("#1C9A4A");
            btn.Clicked += Btn_Clicked;



        }

        private async void Btn_Clicked(object sender, EventArgs e)
        {
            Going g = new Going { userID = user[0].Id, EventID = idLocal };
            await c.goingTable.InsertAsync(g);
            await DisplayAlert("Tamamlandı", "Etkinliğe katılım işlemi tamamlandı", "Tamam");
        }

        public async void getData()
        {
            List<Events> items = await c.eventsTable
                                   .Where(eventsItem => eventsItem.id == idLocal)
                                   .ToListAsync();

            Label l = new Label();
            l.Text = items[0].eventName;
            l.TextColor = Color.FromHex("#1C9A4A");

            Frame frm = new Frame
            {
                Margin = new Thickness(10, 5, 10, 15),
                Content = l,
                OutlineColor = Color.FromHex("#1C9A4A")

            };

            var lat = items[0].lat;
            var lng = items[0].lng;


            var map = new Map(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromKilometers(550)))
            {
                Margin = new Thickness(10, 0, 10, 50),
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var position = new Position(lat, lng); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = items[0].placeName,
                Address = items[0].eventName
            };
            map.Pins.Add(pin);
            t.stkCenter.Children.Add(frm);
            t.stkCenter.Children.Add(map);
            t.stkCenter.Children.Add(btn);
        }
    }
}
