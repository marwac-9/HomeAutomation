using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Interfaces
{
    class NavigatorIView : INavigationService<Page, IView>
    {
        public IReadOnlyList<Page> NavigationStack
        {
            get
            {
                return navigation.NavigationStack;
            }
        }

        public IReadOnlyList<Page> ModalStack
        {
            get
            {
                return navigation.ModalStack;
            }
        }

        private INavigation navigation { get; set; }

        private static NavigatorViewMeta instance;

        public static NavigatorViewMeta Instance()
        {
            if (instance == null)
            {
                instance = new NavigatorViewMeta();
            }

            return instance;
        }

        public void SetNavigation(INavigation nav)
        {
            navigation = nav;
        }

        public void RemovePage(IView view)
        {
            navigation.RemovePage(view.Page);
        }

        public void InsertPageBefore(IView view, IView before)
        {
            navigation.InsertPageBefore(view.Page, before.Page);
        }

        public Task PushAsync(IView view)
        {
            return navigation.PushAsync(view.Page);
        }

        public Task<Page> PopAsync()
        {
            return navigation.PopAsync();
        }

        public Task PopToRootAsync()
        {
            return navigation.PopToRootAsync();
        }

        public Task PushModalAsync(IView view)
        {
            return navigation.PushModalAsync(view.Page);
        }

        public Task<Page> PopModalAsync()
        {
            return navigation.PopModalAsync();
        }

        public Task PushAsync(IView view, bool animated)
        {
            return navigation.PushAsync(view.Page, animated);
        }

        public Task<Page> PopAsync(bool animated)
        {
            return navigation.PopAsync(animated);
        }

        public Task PopToRootAsync(bool animated)
        {
            return navigation.PopToRootAsync(animated);
        }

        public Task PushModalAsync(IView view, bool animated)
        {
            return navigation.PushModalAsync(view.Page, animated);
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return navigation.PopModalAsync(animated);
        }
    }
}
