﻿using Interfaces;
using System.Composition;
using System.Windows.Input;
using Xamarin.Forms;

namespace FifthDeviceXamlLayout
{
    [ViewModel(1, 0, ViewModelCategory.DeviceFive)]
    public class DeviceItemViewModel : ObservableObject, IViewModel
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
                    () =>
                    {
                        Name = "holEEEE"; //changing the DName causes OnPropertyChanged notification to the view
                        //update model information
                        deviceItemModel.Name = name;
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
