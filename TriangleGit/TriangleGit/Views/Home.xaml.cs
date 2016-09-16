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
    public partial class Home : ContentPage
    {
        ActivityIndicator indicator = new ActivityIndicator();
        ObservableCollection<Users> user;
        Constant c = new Constant();
        Test t = new Test();
        public Home(ObservableCollection<Users> users)
        {

            user = users;
            InitializeComponent();
            Content = t.layout;
            indicator.IsRunning = false;
            t.stkCenter.Children.Add(indicator);
            GetEvents();

            Frame f = new Frame
            {

                HeightRequest = 15,
                WidthRequest = 15,
                Content = t.homeImg,

                OutlineColor = Color.Black
            };

            t.g.Children.Add(f, 0, 0);


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

        }

        public async void GetEvents()
        {


            Label l = new Label();
            l.Text = "Hadi bi etkinlik bulalım";
            l.Margin = new Thickness(55, 0, 5, 0);
            l.FontSize = 20;
            l.TextColor = Color.FromHex("#1C9A4A");
            indicator.IsRunning = true;
            try
            {
                List<Events> items = await c.eventsTable
                                   .Where(eventsItem => eventsItem.eventName != null)
                                   .ToListAsync();

                //ListView lw = new ListView();
                //lw.BackgroundColor = Color.FromHex("#1C9A4A");
                //lw.SeparatorColor = Color.FromHex("#FCFCFC");
                //lw.ItemsSource = items;

                ListView listView = new ListView() { ItemsSource = items };
                listView.BackgroundColor = Color.FromHex("#1C9A4A");
                listView.SeparatorColor = Color.FromHex("#FCFCFC");
                listView.ItemTemplate = new DataTemplate(typeof(ImageCell));
                listView.ItemTemplate.SetBinding(ImageCell.TextProperty, "eventName");
                listView.ItemTemplate.SetBinding(ImageCell.DetailProperty, "placeName");
                listView.Margin = new Thickness(5, 5, 5, 40);

                //   listView.ItemSelected += ListView_ItemSelected;
                listView.ItemSelected += ListView_ItemSelected;
                indicator.IsRunning = false;
                t.stkCenter.Children.Add(l);
                t.stkCenter.Children.Add(listView);
            }
            catch (Exception aas)
            {
                await DisplayAlert("a", aas.ToString(), "a");
            }

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            ListView lw = (ListView)sender;
            var a = (Events)e.SelectedItem;

            Navigation.PushModalAsync(new EventDetail(user, a.id));

        }
    }
}
