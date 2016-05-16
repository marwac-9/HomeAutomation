using System.Windows.Input;
using Interfaces;
using System.Composition;
using Xamarin.Forms;
using System;

namespace SecondDeviceItem
{
    [Export(typeof(DeviceItemViewModel))]
    public class DeviceItemViewModel : ObservableObject
    {
        public const string DevNameProperty = "Name";
        public Color color = new Color(1, 0, 0);
        string name = "hejHA";

        DeviceItemModel deviceItemModel;

        public DeviceItemViewModel()
        {
            deviceItemModel = new DeviceItemModel();
            Name = deviceItemModel.Name;
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Equals(name))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                name = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeviceSelected
        {
            get
            {
                return new Command(
                    async execute =>
                    {
                        Name = "holzz"; //changing the DName causes OnPropertyChanged notification to the view
                        //update model information
                        deviceItemModel.Name = name;
                        //await Nav.Instance().Navigation.PushAsync(ViewResolver.Instance().Resolve(ViewCategory.Device, 1,0).Value.Page);
                        await NavigatorViewMeta.Instance().PushAsync(new ViewMetaData { Category = ViewCategory.DeviceTwoSettings, Major = 1, Minor = 0 });
                    },
                    canExecute => true
                    );
            }
        }
    }

    //Model
    public class DeviceItemModel : ObservableObject
    {
        public const string NamePropertyName = "Name";
        string name = "hejjjj";

        public string Name
        {
            get { return name; }
            set
            {
                if (value.Equals(name))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                name = value;
                OnPropertyChanged();
            }
        }
    }

    //View
    [ViewComponent(1,0,ViewComponentCategory.Device)]
    public class DeviceItemView : IViewComponent
    {
        //so the view should contain only the gui elements and set up their structure
        public Label label = new Label();
        public Image icon = new Image();
        public Button button = new Button();
        public StackLayout layout;

        public View Content { get; set; }

        public View ViewComponent
        {
            get
            {
                return layout;
            }
        }

        public int ItemHeight { get { return 40; } }

        [ImportingConstructor]
        public DeviceItemView(DeviceItemViewModel viewModel)
        {
            layout = new StackLayout
            {
                Children =
                {
                    icon,
                    label,
                    button
                }
            };

            button.Text = "Device 2: clickEN me!";
            label.BindingContext = viewModel;
            label.SetBinding(Label.TextProperty, DeviceItemViewModel.DevNameProperty);

            button.Command = viewModel.DeviceSelected;
            layout.BackgroundColor = new Color(0,1,0);
        }

        public View GetComponent()
        {
            return layout;
        }
    }
}
