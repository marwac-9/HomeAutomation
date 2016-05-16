using Interfaces;
using System.Composition;
using Xamarin.Forms;
using System;
using System.Collections.Generic;

namespace FourthDevice
{
    [ViewComponent(1, 0, ViewComponentCategory.Device)]
    public partial class FourthDevice : ContentView, IViewComponent
    {

        IEnumerable<Lazy<IViewModel, ViewModelMetaData>> viewModel;
        [ImportMany]
        public IEnumerable<Lazy<IViewModel, ViewModelMetaData>> ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != value)
                {
                    viewModel = value;
                    BindingContext = ViewModelResolver.Instance().NewResolve(ViewModelCategory.DeviceFour, 1).CreateExport().Value;
                }
            }
        }

        public FourthDevice()
        {
            InitializeComponent();
        }

        public View ViewComponent
        {
            get
            {
                return this;
            }
        }

        public int ItemHeight { get { return 40; } }

        public View GetComponent()
        {
            return this;
        }
    }

    
}
