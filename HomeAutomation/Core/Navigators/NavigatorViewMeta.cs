using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Interfaces
{
    public class NavigatorViewMeta : INavigationService<Page,ViewMetaData>
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

        public void RemovePage(ViewMetaData metadata)
        {
            navigation.RemovePage(ViewResolver.Instance().Resolve(metadata.Category,metadata.Major,metadata.Minor).Value.Page);
        }

        public void InsertPageBefore(ViewMetaData metadata, ViewMetaData metadataBefore)
        {
            navigation.InsertPageBefore(ViewResolver.Instance().Resolve(metadata.Category, metadata.Major, metadata.Minor).Value.Page, ViewResolver.Instance().Resolve(metadataBefore.Category, metadataBefore.Major, metadataBefore.Minor).Value.Page);
        }

        public Task PushAsync(ViewMetaData metadata)
        {
            return navigation.PushAsync(ViewResolver.Instance().Resolve(metadata.Category, metadata.Major, metadata.Minor).Value.Page);
        }

        public Task<Page> PopAsync()
        {
            return navigation.PopAsync();
        }

        public Task PopToRootAsync()
        {
            return navigation.PopToRootAsync();
        }

        public Task PushModalAsync(ViewMetaData metadata)
        {
            return navigation.PushModalAsync(ViewResolver.Instance().Resolve(metadata.Category, metadata.Major, metadata.Minor).Value.Page);
        }

        public Task<Page> PopModalAsync()
        {
            return navigation.PopModalAsync();
        }

        public Task PushAsync(ViewMetaData metadata, bool animated)
        {
            return navigation.PushAsync(ViewResolver.Instance().Resolve(metadata.Category, metadata.Major, metadata.Minor).Value.Page, animated);
        }

        public Task<Page> PopAsync(bool animated)
        {
            return navigation.PopAsync(animated);
        }

        public Task PopToRootAsync(bool animated)
        {
            return navigation.PopToRootAsync(animated);
        }

        public Task PushModalAsync(ViewMetaData metadata, bool animated)
        {
            return navigation.PushModalAsync(ViewResolver.Instance().Resolve(metadata.Category, metadata.Major, metadata.Minor).Value.Page, animated);
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return navigation.PopModalAsync(animated);
        }
    }
}
