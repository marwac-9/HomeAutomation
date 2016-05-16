using System.Composition;
using Xamarin.Forms;
using System.Collections.Generic;
using Interfaces;
using System;

namespace MyCustomPage
{
    /*
    [Export("TEST", typeof(MyTest))] //when importing both attributes must match exactly
    public class MyCustomLayoutButtonO : MyTest
    {
        public MyCustomLayoutButtonO()
        {
            button.Text = "Go to Inherited";
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.CenterAndExpand;
        }
    }

    [Export(typeof(MyTest))]
    public class MyCustomLayoutButtonX : MyTest
    {
        public MyCustomLayoutButtonX()
        {
            button.Text = "Go to Inherited";
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.CenterAndExpand;
        }
    }

    [Export("TEST", typeof(IUIElement<Button>))]
    public class MyCustomLayoutButton : MyTest
    {
        public MyCustomLayoutButton()
        {
            button.Text = "Go to Modeless Page";
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.CenterAndExpand;
        }
    }

    [Export("TEST", typeof(IUIElement<Button>))]
    [ExportMetadata("Symbol", '-')]
    public class MyCustomLayoutButtonTwo : IUIElement<Button>
    {
        protected Button button = new Button();
        
        public MyCustomLayoutButtonTwo()
        {
            button.Text = "Go to Model Page";
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.CenterAndExpand;
        }

        public Button GetElement()
        {
            return button;
        }
    }

    [Export(typeof(IUIElement<Button>))]
    public abstract class MyTest : IUIElement<Button>
    {
        protected Button button = new Button();
        public Button GetElement()
        {
            return button;
        }
    }
    
    [Export(typeof(IView<Layout<View>>))]
    public class MyCustomLayoutTwo : IView<Layout<View>>
    {
        StackLayout layout = new StackLayout();

        [ImportingConstructor]
        public MyCustomLayoutTwo([ImportMany] IEnumerable<MyTest> buttons)//this can import model data instead and the constructor will build views for it
        {
            if (buttons != null)
            {
                foreach (IUIElement<Button> button in buttons)
                {
                    layout.Children.Add(button.GetElement());
                }
            }
        }
        public Layout<View> GetElement()
        {
            return layout;
        }
    }

    [Export(typeof(IUIElement<Layout<View>>))]
    public class MyCustomLayout : IUIElement<Layout<View>>
    {
        StackLayout layout = new StackLayout();

        [ImportingConstructor]
        public MyCustomLayout([ImportMany("TEST")] IEnumerable<IUIElement<Button>> buttons)//this can import model data instead and the constructor will build views for it
        {
            if (buttons != null)
            {
                foreach (IUIElement<Button> button in buttons)
                {
                    layout.Children.Add(button.GetElement());
                }
            }
        }
        public Layout<View> GetElement()
        {
            return layout;
        }
    }

    [Export(typeof(IUIElement<ContentPage>))]
    public class MyCustomPageX : IUIElement<ContentPage>
    {
        ContentPage page = new ContentPage();
        Button test = new Button {  Text = "lol" };
        public ContentPage GetElement()
        {
            page.Content = new StackLayout { Children = { test } };
            return page;
        }
    }
*/
    [View(1,1,ViewCategory.Devices)]
    public class MyCustomPage : ContentPage, IView
    {
        [ImportingConstructor]
        public MyCustomPage()
        {
            StackLayout stackLayout = new StackLayout();
            var listView = new ListView
            {
                RowHeight = 40
            };
            listView.ItemsSource = new string[]
            {
                "Buy pears", "Buy oranges", "Buy mangos", "Buy apples", "Buy bananas"
            };
            listView.ItemSelected += async (sender, e) => {

                /*
                Label lab = new Label();
                lab.Text = (string)e.SelectedItem;

                StackLayout layout = new StackLayout
                {
                    Children =
                    {
                        lab,
                    }
                };
                */
                //await Nav.Instance().Navigation.PushAsync(PageResolver.Instance().Resolve(Nav.Instance().pages,UIPageCategory.Main,1).Value.Page);
                await NavigatorViewMeta.Instance().PopAsync();
                //await page.DisplayAlert("Tapped!", e.SelectedItem + " was tapped.", "OK");
            };

            
            stackLayout.Children.Add(listView);

            Button btn = new Button { Text = "ClickME!" };
            btn.Clicked += async (sender, e) => {
                await this.DisplayAlert("Tapped!", btn.Text + " was tapped.", "OK");
            };
            stackLayout.Children.Add(btn);

            Content =  stackLayout;
        }

        public Page Page
        {
            get
            {
                return this;
            }
        }

        public View PageContent
        {
            get
            {
                return Content;
            }
        }
    }
    
}

