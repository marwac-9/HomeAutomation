using Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace DevicePage
{
    [ViewModel(1, 1, ViewModelCategory.DeviceTwoSettings)]
    public class MyCustomXamlPageViewModel : ObservableObject, IViewModel
    {
        public const string DevNameProperty = "Name";
        string name = "hejHA";
        int clicks = 0;

        MyCustomXamlPageModel deviceItemModel;

        public MyCustomXamlPageViewModel()
        {
            deviceItemModel = new MyCustomXamlPageModel();
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
                   async () =>
                    {
                        clicks++;
                        Name = "KABOOOOM" + clicks.ToString(); 
                        deviceItemModel.Name = name;

                        await  NavigatorViewMeta.Instance().PopAsync();

                    },
                    () => true);
            }
        }
    }


    public class MyCustomXamlPageModel : ObservableObject
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
