using Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Xamarin.Forms;

namespace DeviceList
{
    [View(1,0,ViewCategory.Main)]
    public partial class DeviceListView : ContentPage, IView
    {
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

        public DeviceListView()
        {
            InitializeComponent();
            BindingContext = ViewModelResolver.Instance().Resolve(ViewModelCategory.Main, 1).Value;
        }
    }
}
