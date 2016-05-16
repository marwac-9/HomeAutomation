using Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;

using Xamarin.Forms;

namespace FifthDeviceXamlLayout
{
    [ViewComponent(1, 0, ViewComponentCategory.Device)]
    public partial class FifthDeviceLayout : StackLayout, IViewComponent
    {
        public FifthDeviceLayout()
        {
            InitializeComponent();
            BindingContext = ViewModelResolver.Instance().NewResolve(ViewModelCategory.DeviceFive, 1).CreateExport().Value;
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
    }
}
