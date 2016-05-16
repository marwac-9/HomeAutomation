using Interfaces;
using System.Composition;
using Xamarin.Forms;
using System;
using System.Collections.Generic;

namespace ThirdDeviceXaml
{
    [ViewComponent(1, 0, ViewComponentCategory.Device)]
    public partial class Page1 : ContentPage, IViewComponent
    {
        public Page1()
        {
            InitializeComponent();
            Content.BindingContext = ViewModelResolver.Instance().NewResolve(ViewModelCategory.DeviceThree, 1).CreateExport().Value;
        }

        public View ViewComponent
        {
            get
            {
                return Content;
            }
        }

        public int ItemHeight { get { return 40; } }

        public View GetComponent()
        {
            return Content;
        }
    }

  
}
