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
    public partial class ProfilePage : ContentPage
    {
        Test t = new Test();
        Constant c = new Constant();
        ObservableCollection<Users> user;


        public ProfilePage(ObservableCollection<Users> users)
        {

            user = users;
            InitializeComponent();
            Content = t.layout;

            Frame f = new Frame
            {

                HeightRequest = 15,
                WidthRequest = 15,
                Content = t.profileImg,

                OutlineColor = Color.Black
            };

            t.g.Children.Add(f, 4, 0);


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




            CreateProfile();





        }
        public void CreateProfile()
        {
            TableView tableView = new TableView
            {
                Margin = new Thickness(20, 10, 20, 0),
                Intent = TableIntent.Form,
                Root = new TableRoot
                {

                    new TableSection
                    {
                        new ImageCell
                        {

                            ImageSource = "user.png",

                            Text = "        This is an ImageCell",
                        }
                    }
                }
            };
            t.stkCenter.Children.Add(tableView);

        }
        public async void GetData()
        {
            List<Going> items = await c.goingTable
                               .Where(userItem => userItem.userID == user[0].Id)
                               .ToListAsync();
            ListView lw = new ListView();
            lw.ItemsSource = items;
            lw.SetBinding(ListView.ItemsSourceProperty,
                new Binding
                {
                    Path = "eventID"
                });
            t.stkCenter.Children.Add(lw);

        }
    }
}
