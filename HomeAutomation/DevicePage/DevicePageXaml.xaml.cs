using System;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Xamarin.Forms;
using System.Composition;

namespace DevicePage
{
    [View(1, 0, ViewCategory.DeviceTwoSettings)]
    public partial class DevicePageXaml : ContentPage, IView
    {
        public DevicePageXaml()
        {
            InitializeComponent();
            BindingContext = ViewModelResolver.Instance().Resolve(ViewModelCategory.DeviceTwoSettings, 1).Value;
            /*
             * 
             */
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
