using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Xamarin.Forms;

namespace Interfaces
{
    public interface IViewComponent : IPlugin
    {
        View ViewComponent { get; }

        View GetComponent();
    }
    public class ViewComponentMetaData : IMetaData
    {
        public int Major { get; set; } = -1;
        public int Minor { get; set; } = -1;
        public ViewComponentCategory Category { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewComponentAttribute : ExportAttribute
    {
        public ViewComponentAttribute(int major, int minor, ViewComponentCategory cat) : base(typeof(IViewComponent)) { Major = major; Minor = minor; Category = cat; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public ViewComponentCategory Category { get; set; }
    }

    public enum ViewComponentCategory
    {
        Device,
        DeviceList
    }

    public class ViewComponentResolver
    {
        private static ViewComponentResolver instance;
        public static ViewComponentResolver Instance()
        {
            if (instance == null)
            {
                instance = new ViewComponentResolver();
            }

            return instance;
        }

        [ImportMany]
        IEnumerable<Lazy<IViewComponent, ViewComponentMetaData>> ViewComponents { get; set; }

        [ImportMany]
        IEnumerable<ExportFactory<IViewComponent, ViewComponentMetaData>> ViewComponentsFactory { get; set; }

        //returns all versions of same category
        public IEnumerable<Lazy<IViewComponent, ViewComponentMetaData>> ResolveMany(ViewComponentCategory category, int major = -1, int minor = -1)
        {
            List<Lazy<IViewComponent, ViewComponentMetaData>> compatible = new List<Lazy<IViewComponent, ViewComponentMetaData>>();
            foreach (var viewComponent in ViewComponents)
            {
                if (viewComponent.Metadata.Category == category && (major == -1 || viewComponent.Metadata.Major == major)) // major compatibility check
                {
                    if (minor == -1 || viewComponent.Metadata.Minor <= minor) // minor compatibility check
                    {
                        compatible.Add(viewComponent);
                    }
                }
            }
            if (major == -1) return compatible.OrderByDescending(lazy => lazy.Metadata.Major);
            else return compatible.OrderByDescending(lazy => lazy.Metadata.Minor);
        }

        //returns latest version of specified category
        public Lazy<IViewComponent, ViewComponentMetaData> Resolve(ViewComponentCategory category, int major = -1, int minor = -1)
        {
            return ResolveMany(category, major, minor).First();
        }

        //returns all versions of same category
        public IEnumerable<ExportFactory<IViewComponent, ViewComponentMetaData>> NewResolveMany(ViewComponentCategory category, int major = -1, int minor = -1)
        {
            List<ExportFactory<IViewComponent, ViewComponentMetaData>> compatible = new List<ExportFactory<IViewComponent, ViewComponentMetaData>>();
            foreach (var viewComponent in ViewComponentsFactory)
            {
                if (viewComponent.Metadata.Category == category && (major == -1 || viewComponent.Metadata.Major == major)) // major compatibility check
                {
                    if (minor == -1 || viewComponent.Metadata.Minor <= minor) // minor compatibility check
                    {
                        compatible.Add(viewComponent);
                    }
                }
            }

            if (major == -1) return compatible.OrderByDescending(lazy => lazy.Metadata.Major);
            else return compatible.OrderByDescending(lazy => lazy.Metadata.Minor);
        }

        //returns latest version of specified category
        public ExportFactory<IViewComponent, ViewComponentMetaData> NewResolve(ViewComponentCategory category, int major = -1, int minor = -1)
        {
            return NewResolveMany(category, major, minor).First();
        }
    }
}
