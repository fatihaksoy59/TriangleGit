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
    public partial class Search : ContentPage
    {
        Test t = new Test();
        ObservableCollection<Users> user;
        public Search(ObservableCollection<Users> users)
        {
            user = users;
            InitializeComponent();
            Frame f = new Frame
            {

                HeightRequest = 15,
                WidthRequest = 15,
                Content = t.searchImg,

                OutlineColor = Color.Black
            };

            t.g.Children.Add(f, 3, 0);


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
    }
}
