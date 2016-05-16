using System.Windows.Input;
using Interfaces;
using System.Composition;
using Xamarin.Forms;

namespace ThirdDeviceXaml
{
    [ViewModel(1,0,ViewModelCategory.DeviceThree)]
    public class DeviceItemViewModel : ObservableObject, IViewModel
    {
        public const string DevNameProperty = "Name";
        public Color color = new Color(0, 0, 1);
        string name = "hejHA";

        DeviceItemModel deviceItemModel;

        public DeviceItemViewModel()
        {
            deviceItemModel = new DeviceItemModel();
            Name = deviceItemModel.Name;
        }

        public Color Color
        {
            get { return color; }
            set
            {
                if (value.Equals(color))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                color = value;
                OnPropertyChanged();
            }
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
                        await NavigatorViewMeta.Instance().PushAsync(new ViewMetaData { Category = ViewCategory.Devices });
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
        string name = "ejjjj";

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
}
