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
    public partial class AddEventsOnMap : ContentPage
    {
        Test t = new Test();
        Constant c = new Constant();
        Map map;
        ObservableCollection<Users> user;
        public AddEventsOnMap(ObservableCollection<Users> users)
        {
            InitializeComponent();
            users = user;
            Content = t.layout;


            Frame f = new Frame
            {

                HeightRequest = 15,
                WidthRequest = 15,
                Content = t.mapImage,

                OutlineColor = Color.Black
            };

            t.g.Children.Add(f, 1, 0);


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

            GetData();

        }
        public async void GetData()
        {

            map = new Map(MapSpan.FromCenterAndRadius(new Position(38.963745, 35.243322), Distance.FromKilometers(550)))
            {
                Margin = new Thickness(10, 0, 10, 100),
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            try
            {
                Entry a = new Entry();
                var aa = a.Text;
                List<Events> items = await c.eventsTable
                                 .Where(eventItem => eventItem.deleted == false)
                                 .ToListAsync();

                for (int i = 0; i < items.Count; i++)
                {

                    var position = new Position(items[i].lat, items[i].lng); // Latitude, Longitude
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = items[i].placeName,
                        Address = items[i].eventName
                    };
                    map.Pins.Add(pin);
                }

                t.stkCenter.Children.Add(map);
            }
            catch (Exception e)
            {
                DisplayAlert("aa", e.ToString(), "a");
            }
        }

    }
}
