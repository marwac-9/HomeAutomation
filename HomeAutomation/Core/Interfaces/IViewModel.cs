using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Interfaces
{
    public interface IViewModel
    {

    }
    public class ViewModelMetaData : IMetaData
    {
        public int Major { get; set; } = -1;
        public int Minor { get; set; } = -1;
        public ViewModelCategory Category { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewModelAttribute : ExportAttribute
    {
        public ViewModelAttribute(int major, int minor, ViewModelCategory cat) : base(typeof(IViewModel)) { Major = major; Minor = minor; Category = cat; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public ViewModelCategory Category { get; set; }
    }

    public enum ViewModelCategory
    {
        Main,
        DeviceTwoSettings,
        DeviceFourSettings,
        DeviceList,
        DeviceOne,
        DeviceTwo,
        DeviceThree,
        DeviceFour,
        DeviceFive
    }

    public class ViewModelResolver
    {
        private static ViewModelResolver instance;
        public static ViewModelResolver Instance()
        {
            if (instance == null)
            {
                instance = new ViewModelResolver();
            }

            return instance;
        }

        [ImportMany]
        public IEnumerable<Lazy<IViewModel, ViewModelMetaData>> ViewModels { get; set; }

        [ImportMany]
        public IEnumerable<ExportFactory<IViewModel, ViewModelMetaData>> ViewModelsFactory { get; set; }

        //returns all versions of same category
        public IEnumerable<Lazy<IViewModel, ViewModelMetaData>> ResolveMany(ViewModelCategory category, int major = -1, int minor = -1)
        {
            List<Lazy<IViewModel, ViewModelMetaData>> compatible = new List<Lazy<IViewModel, ViewModelMetaData>>();
            foreach (var viewModel in ViewModels)
            {
                if (viewModel.Metadata.Category == category && (major == -1 || viewModel.Metadata.Major == major)) // major compatibility check
                {
                    if (minor == -1 || viewModel.Metadata.Minor <= minor) // minor compatibility check
                    {
                        compatible.Add(viewModel);
                    }
                }
            }

            if (major == -1) return compatible.OrderByDescending(lazy => lazy.Metadata.Major);
            else return compatible.OrderByDescending(lazy => lazy.Metadata.Minor);
        }

        //returns latest version of specified category
        public Lazy<IViewModel, ViewModelMetaData> Resolve(ViewModelCategory category, int major = -1, int minor = -1)
        {
            return ResolveMany(category, major, minor).First();
        }

        //returns all versions of same category
        public IEnumerable<ExportFactory<IViewModel, ViewModelMetaData>> NewResolveMany(ViewModelCategory category, int major = -1, int minor = -1)
        {
            List<ExportFactory<IViewModel, ViewModelMetaData>> compatible = new List<ExportFactory<IViewModel, ViewModelMetaData>>();
            foreach (var viewModel in ViewModelsFactory)
            {
                if (viewModel.Metadata.Category == category && (major == -1 || viewModel.Metadata.Major == major)) // major compatibility check
                {
                    if (minor == -1 || viewModel.Metadata.Minor <= minor) // minor compatibility check
                    {
                        compatible.Add(viewModel);
                    }
                }
            }

            if (major == -1) return compatible.OrderByDescending(lazy => lazy.Metadata.Major);
            else return compatible.OrderByDescending(lazy => lazy.Metadata.Minor);
        }

        //returns latest version of specified category
        public ExportFactory<IViewModel, ViewModelMetaData> NewResolve(ViewModelCategory category, int major = -1, int minor = -1)
        {
            return NewResolveMany(category, major, minor).First();
        }
    }
}
