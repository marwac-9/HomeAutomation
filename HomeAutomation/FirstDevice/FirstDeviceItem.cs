using System.Windows.Input;
using Interfaces;
using System.Composition;
using Xamarin.Forms;

namespace FirstDeviceItem
{
    [Export(typeof(DeviceItemViewModel))]
    public class DeviceItemViewModel : ObservableObject
    {
        #region PropertyNames
        public const string DevNameProperty = "Name";
        public const string DevCounterProperty = "Counter";
        #endregion

        Color color = new Color(1, 0, 0);
        string name = "hejho";
        int counter = 0;
        DeviceItemModel deviceItemModel;
        public ICommand DeviceSelected;
        public ICommand DeviceDetailsSelected;
        public DeviceItemViewModel()
        {
            InitializeCommands();
            deviceItemModel = new DeviceItemModel();
            Name = deviceItemModel.Name;
        }

        #region Properties
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
        public int Counter
        {
            get { return counter; }
            set
            {
                if (value == counter)
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                counter = value;
                OnPropertyChanged();
                ((Command)DeviceSelected).ChangeCanExecute();
            }
        }
        #endregion

        void InitializeCommands()
        {
            DeviceSelected = new Command(
                       execute =>
                       {
                           Name = "holaa"; //changing the DName causes OnPropertyChanged notification to the view
                                           //update model information
                           deviceItemModel.Name = name;
                       },
                       canExecute => Counter % 2 == 0);
            DeviceDetailsSelected = new Command(
                    () =>
                    {
                        Name = "wat";
                        deviceItemModel.Name = name;
                        Counter++;
                        //NAVIGATE TO THE DEVICE VIEW
                    },
                    () => true);
        }
    }

    //Model
    public class DeviceItemModel : ObservableObject
    {
        public const string NamePropertyName = "Name";
        string name = "hej";

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
    [ViewComponent(1, 0, ViewComponentCategory.Device)]
    public class DeviceItemView : IViewComponent
    {
        //so the view should contain only the gui elements and set up their structure
        public Label label = new Label();
        public Image icon = new Image();
        public Button button = new Button();
        public Button button2 = new Button();
        public Label counterLabel = new Label();
        public StackLayout layout;
        public View Content { get; set; }

        public View ViewComponent
        {
            get
            {
                return layout;
            }
        }

        public int ItemHeight { get { return 90; } }

        //public FirstDevice.DeviceView deviceView;

        [ImportingConstructor]
        public DeviceItemView(DeviceItemViewModel viewModel)
        {
            layout = new StackLayout
            {
                Children =
                {
                    counterLabel,
                    icon,
                    label,
                    button,
                    button2
                }
            };

            layout.BindingContext = viewModel;
            counterLabel.SetBinding(Label.TextProperty, DeviceItemViewModel.DevCounterProperty);
            counterLabel.BackgroundColor = new Color(1,0,1);
            button.Text = "Device 1: clickz me!";
            button2.Text = "don't clickz me!";
            label.SetBinding(Label.TextProperty, DeviceItemViewModel.DevNameProperty);

            button.Command = viewModel.DeviceSelected;
            button2.Command = viewModel.DeviceDetailsSelected;
            layout.BackgroundColor = new Color(1, 0, 0);

        }

        public View GetComponent()
        {
            return layout;
        }
    }
}
