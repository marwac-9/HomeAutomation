using Interfaces;
using System.Composition;
using System.Composition.Hosting;
using System.Reflection;

using Xamarin.Forms;

namespace HomeAutomation
{
    public class App : Application
    {
        [Import]
        public ICalculator calculator { get; set; }

        public App()
        {
            // Handle when your app starts
            var configuration = new ContainerConfiguration().WithAssembly(typeof(App).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(ExtendedOperations.Mod).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(Operations.Add).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(Calculators.MySimpleCalculator).GetTypeInfo().Assembly);


            configuration.WithAssembly(typeof(DeviceList.DeviceListView).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(BasicAuth2.BasicAuth2).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(FirstDeviceItem.DeviceItemView).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(SecondDeviceItem.DeviceItemView).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(ThirdDeviceXaml.DeviceItemViewModel).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(FourthDevice.DeviceItemViewModel).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(FifthDeviceXamlLayout.FifthDeviceLayout).GetTypeInfo().Assembly);

            configuration.WithAssembly(typeof(DevicePage.DevicePageXaml).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(NewPageOldViewModel.OldViewModel).GetTypeInfo().Assembly);
            configuration.WithAssembly(typeof(MyCustomPage.MyCustomPage).GetTypeInfo().Assembly);

            using (var container = configuration.CreateContainer())
            {
                container.SatisfyImports(this);
                container.SatisfyImports(ViewResolver.Instance());
                container.SatisfyImports(ViewModelResolver.Instance());
                container.SatisfyImports(ViewComponentResolver.Instance());
            }

            //gets here after [OnImportSatisfied]

            calculator.Calculate("4%4");
            calculator.Calculate("4+4");

            MainPage = new NavigationPage(ViewResolver.Instance().Resolve(ViewCategory.Main, 1).Value.Page);
            NavigatorViewMeta.Instance().SetNavigation(Current.MainPage.Navigation);
            /*
            var conventions = new ConventionBuilder();

            conventions

                .ForTypesDerivedFrom<IPlugin>()

                .Export<IPlugin>()

                .Shared();
                */
        }

        [OnImportsSatisfied]
        public void OnImportsSatisfied()
        {
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
