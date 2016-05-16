using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeviceList
{
    public class MyNewButton : Button, IViewComponent
    {
        public MyNewButton()
        {
            Text = "New Item";
        }

        public View ViewComponent
        {
            get
            {
                return this;
            }
        }

        public View GetComponent()
        {
            return this;
        }

        public int ItemHeight { get { return 40; } }
    }

    [ViewModel(1,1,ViewModelCategory.Main)]
    public class DeviceListViewModel : ObservableObject, IViewModel
    {
        public const string DevNameProperty = "Name";
        public Color color = new Color(1, 0, 0);
        string name = "hejHA";
        int counter = 0;

        DeviceListModel deviceItemModel;
        ObservableCollection<IViewComponent> views;

        public ObservableCollection<IViewComponent> Views
        {
            get { return views; }
            set { if (views != value) { views = value; OnPropertyChanged(); } }
        }
        public DeviceListViewModel()
        {
            deviceItemModel = new DeviceListModel();
            Name = deviceItemModel.Name;

            IEnumerable<Lazy<IViewComponent, ViewComponentMetaData>> devices = ViewComponentResolver.Instance().ResolveMany(ViewComponentCategory.Device, 1);
            Views = new ObservableCollection<IViewComponent>();
            foreach (var device in devices)
            {
                Views.Add(device.Value);
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
                    () =>
                    {
                        counter++;
                        Name = "WOOHOOO " + counter.ToString(); //changing the DName causes OnPropertyChanged notification to the view
                        //update model information
                        deviceItemModel.Name = name;

                        Views.Add((IViewComponent)new MyNewButton());
                    },
                    () => true);
            }
        }
    }
}
