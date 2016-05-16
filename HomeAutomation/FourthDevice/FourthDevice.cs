using System.Windows.Input;
using Interfaces;
using System.Composition;
using Xamarin.Forms;

namespace FourthDevice
{
    [ViewModel(1, 0, ViewModelCategory.DeviceFour)]
    public class DeviceItemViewModel : ObservableObject, IViewModel
    {
        public const string DevNameProperty = "Name";
        public Color color = new Color(1, 0, 1);
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
                    async () =>
                    {
                        Name = "holEEEE"; //changing the DName causes OnPropertyChanged notification to the view
                        //update model information
                        deviceItemModel.Name = name;

                        await NavigatorViewMeta.Instance().PushAsync(new ViewMetaData { Category = ViewCategory.DeviceFourSettings, Major = 1 });
                    },
                    () => true);
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
