using System.Windows.Input;
using Interfaces;
using System.Composition;
using Xamarin.Forms;
using System;

namespace FirstDevice
{
    /*
    [Export(typeof(DeviceViewModel))]
    public class DeviceViewModel : ObservableObject
    {
        public const string DevNameProperty = "Name";
        public const string DevSurnameProperty = "Surname";
        public Color color = new Color(1, 0, 0);
        string name = "hejho";
        string surname = "lalla";

        DeviceModel deviceItemModel;

        public DeviceViewModel()
        {
            deviceItemModel = new DeviceModel();
            Name = deviceItemModel.Name;
            Surname = deviceItemModel.Surname;
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
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value.Equals(surname))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                surname = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeviceSelected
        {
            get
            {
                return new Command(
                    () =>
                    {
                        Name = "holaa"; //changing the DName causes OnPropertyChanged notification to the view
                        //update model information
                        deviceItemModel.Name = name;
                    },
                    () => true);
            }
        }
        public ICommand DeviceUnselected
        {
            get
            {
                return new Command(
                    () =>
                    {
                        Surname = "nejjj"; 
                        deviceItemModel.Surname = surname;
                    },
                    () => true);
            }
        }
    }

    //View
    public class DeviceView : IViewComponent
    {

        public Label label = new Label();
        public Label label2 = new Label();
        public Image icon = new Image();
        public Button button1 = new Button();
        public Button button2 = new Button();

        public StackLayout layout;
        public View Content { get; set; }

        public View ViewComponent
        {
            get
            {
                return layout;
            }
        }

        [ImportingConstructor]
        public DeviceView(DeviceViewModel viewModel)
        {
            layout = new StackLayout
            {
                Children =
                {
                    icon,
                    label,
                    label2,
                    button1,
                    button2
                }
            };

            button1.Text = "clickz me!";
            button2.Text = "don't clickz me!";
            label.BindingContext = viewModel;
            label.SetBinding(Label.TextProperty, DeviceViewModel.DevNameProperty);
            label2.BindingContext = viewModel;
            label2.SetBinding(Label.TextProperty, DeviceViewModel.DevSurnameProperty);

            button1.Command = viewModel.DeviceSelected;
            button2.Command = viewModel.DeviceUnselected;

        }

        public View GetComponent()
        {
            return layout;
        }
    }

    //Model
    public class DeviceModel : ObservableObject
    {
        public const string NamePropertyName = "Name";
        public const string SurnamePropertyName = "Surname";
        string name = "hej";
        string surname = "bye";
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
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value.Equals(surname))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                surname = value;
                OnPropertyChanged();
            }
        }
    }
    */
}
