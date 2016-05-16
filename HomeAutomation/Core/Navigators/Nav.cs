using Xamarin.Forms;

namespace Interfaces
{
    public class Nav 
    {
        public INavigation Navigation { get; set; }

        private static Nav instance;
        public static Nav Instance()
        {
            if (instance == null)
            {
                instance = new Nav();
            }

            return instance;
        }

        public void SetNavigation(INavigation nav)
        {
            Navigation = nav;
        }
    }
}
