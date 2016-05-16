using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Composition;
using Xamarin.Forms;

namespace Interfaces
{
    public interface IView : IPlugin
    {
        Page Page { get; }
        View PageContent { get; }
    }
    public class ViewMetaData : IMetaData
    {
        public int Major { get; set; } = -1;
        public int Minor { get; set; } = -1;
        public ViewCategory Category { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewAttribute : ExportAttribute
    {
        public ViewAttribute(int major, int minor, ViewCategory cat) : base(typeof(IView)) { Major = major; Minor = minor; Category = cat; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public ViewCategory Category { get; set; }
    }

    public enum ViewCategory
    {
        Main,
        Device,
        Devices,
        About,
        DeviceTwoSettings,
        DeviceFourSettings
    }
    
    public class ViewResolver
    {
        private static ViewResolver instance;
        public static ViewResolver Instance()
        {
            if (instance == null)
            {
                instance = new ViewResolver();
            }

            return instance;
        }

        [ImportMany]
        public IEnumerable<Lazy<IView, ViewMetaData>> Views { get; set; }

        [ImportMany]
        public IEnumerable<ExportFactory<IView, ViewMetaData>> ViewsFactory { get; set; }

        //returns all versions of same category
        public IEnumerable<Lazy<IView, ViewMetaData>> ResolveMany(ViewCategory category, int major = -1, int minor = -1)
        {
            List<Lazy<IView, ViewMetaData>> compatible = new List<Lazy<IView, ViewMetaData>>();
            foreach (var view in Views)
            {
                if (CheckVersion(view.Metadata, category, major, minor)) // major compatibility check
                {
                    compatible.Add(view);
                }
            }
            if (major == -1) return compatible.OrderByDescending(lazy => lazy.Metadata.Major);
            else return compatible.OrderByDescending(lazy => lazy.Metadata.Minor);
        }

        //returns latest version of specified category
        public Lazy<IView, ViewMetaData> Resolve(ViewCategory category, int major = -1, int minor = -1)
        {
            return ResolveMany(category, major, minor).First();
        }

        //returns factory of all versions of same category
        public IEnumerable<ExportFactory<IView, ViewMetaData>> NewResolveMany(ViewCategory category, int major = -1, int minor = -1)
        {
            List<ExportFactory<IView, ViewMetaData>> compatible = new List<ExportFactory<IView, ViewMetaData>>();
            foreach (var view in ViewsFactory)
            {
                if (CheckVersion(view.Metadata, category, major, minor)) // major compatibility check
                {
                    compatible.Add(view);
                }
            }
            if (major == -1) return compatible.OrderByDescending(lazy => lazy.Metadata.Major);
            else return compatible.OrderByDescending(lazy => lazy.Metadata.Minor);
        }

        //returns factory of latest version of specified category
        public ExportFactory<IView, ViewMetaData> NewResolve(ViewCategory category, int major = -1, int minor = -1)
        {
            return NewResolveMany(category, major, minor).First();
        }

        bool CheckVersion(ViewMetaData metadata, ViewCategory category, int major, int minor)
        {
            if (metadata.Category == category && (major == -1 || metadata.Major == major)) // major compatibility check
            {
                if (minor == -1 || metadata.Minor <= minor) // minor compatibility check
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
