using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Interfaces
{
    public class NavMetaData
    {
        int Major { get; set; }
        int Minor { get; set; }
        NavCategory Category { get; set; }
    }
    public enum NavCategory
    {
        Xamarin,
        ViewModelFirst
    }

    public interface INavigationService<Tout, Tin>
    {
        IReadOnlyList<Tout> NavigationStack { get; }

        IReadOnlyList<Tout> ModalStack { get; }

        void SetNavigation(INavigation nav);

        void RemovePage(Tin meta);

        void InsertPageBefore(Tin meta, Tin before);

        Task PushAsync(Tin meta);

        Task<Tout> PopAsync();

        Task PopToRootAsync();

        Task PushModalAsync(Tin meta);

        Task<Tout> PopModalAsync();

        Task PushAsync(Tin meta, bool animated);

        Task<Tout> PopAsync(bool animated);

        Task PopToRootAsync(bool animated);

        Task PushModalAsync(Tin meta, bool animated);

        Task<Tout> PopModalAsync(bool animated);
    }
}
