using System;
using Interfaces;

using Xamarin.Forms;

namespace NewPageOldViewModel
{
    [View(1, 1, ViewCategory.DeviceFourSettings)]
    public partial class OldViewModel : ContentPage, IView
    {
        public OldViewModel()
        {
            InitializeComponent();
            BindingContext = ViewModelResolver.Instance().Resolve(ViewModelCategory.DeviceTwoSettings, 1).Value;
        }

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
    }
}
