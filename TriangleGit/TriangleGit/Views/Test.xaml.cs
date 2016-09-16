using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriangleGit.Views
{
    public partial class Test : ContentPage
    {
        public Image homeImg = new Image();
        public Image mapImage = new Image();
        public Image eventImg = new Image();
        public Image searchImg = new Image();
        public Image profileImg = new Image();


        public Grid g = new Grid();

        public RelativeLayout layout = new RelativeLayout();

        public StackLayout stkTop = new StackLayout
        {
            BackgroundColor = Color.FromHex("#FCFCFC")

        };


        public StackLayout stkBottom = new StackLayout();

        public StackLayout stkCenter = new StackLayout();
        public Test()
        {
            InitializeComponent();






            //     stkTop.VerticalOptions = LayoutOptions.Center;

            Label l = new Label();
            l.Text = "Triangle";
            Frame frm = new Frame
            {
                OutlineColor = Color.FromHex("#1C9A4A")
            };

            l.HorizontalTextAlignment = TextAlignment.Center;
            l.VerticalTextAlignment = TextAlignment.Center;
            l.FontSize = 24;
            l.TextColor = Color.FromHex("#1C9A4A");
            stkTop.Children.Add(l);
            stkTop.Children.Add(frm);
            layout.Children.Add(stkTop, Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }), Constraint.Constant(40));




            stkCenter.BackgroundColor = Color.FromHex("#FCFCFC");

            layout.Children.Add(stkCenter, Constraint.Constant(0),
           Constraint.RelativeToView(stkTop, (parent, sibling) =>
           {
               return sibling.Height;
           }),
           Constraint.RelativeToParent((parent) =>
           {
               return parent.Width;
           }), Constraint.RelativeToParent((parent) =>
           {
               return parent.Height - 90;
           }));




            stkBottom.BackgroundColor = Color.FromHex("#1C9A4A");
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            homeImg.Source = ("house.png");
            homeImg.WidthRequest = 50;
            homeImg.HeightRequest = 50;


            mapImage.Source = ("mapImg.png");
            mapImage.WidthRequest = 50;
            mapImage.HeightRequest = 50;


            eventImg.Source = ("triangle.png");
            eventImg.WidthRequest = 50;
            eventImg.HeightRequest = 50;



            searchImg.Source = ("loupe.png");
            searchImg.WidthRequest = 50;
            searchImg.HeightRequest = 50;


            profileImg.Source = ("boy.png");
            profileImg.WidthRequest = 50;
            profileImg.HeightRequest = 50;

            g.Children.Add(homeImg, 0, 0);
            g.Children.Add(mapImage, 1, 0);
            g.Children.Add(eventImg, 2, 0);
            g.Children.Add(searchImg, 3, 0);
            g.Children.Add(profileImg, 4, 0);
            g.Padding = new Thickness(0, 4, 0, 10);
            stkBottom.Children.Add(g);


            layout.Children.Add(stkBottom, Constraint.Constant(0),
               Constraint.RelativeToParent((parent) =>
               {
                   return parent.Height - 60;
               }), Constraint.RelativeToParent((parent) =>
               {
                   return parent.Width;
               }), Constraint.Constant(130));

        }
        public void ImageClick(Image img, Page pge)
        {

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Navigation.PushModalAsync(pge);
            };
        }
    }
}
