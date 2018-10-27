using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLeaf.Core.ViewModels
{
    public enum Pages
    {
        About,
        Back,
        CreateProgressReport,
        EditablePlant,
        FullScheduleView,
        Gardens,
        Plants,
        Plant,
        ProgressReportCollection,
        Schedules,
        Strains
    }
    public class NavigationViewModel : ViewModelBase
    {

        public IEnumerable<string> PagesList { get; } = new List<string>() { "Plants", "Gardens", "Strains", "Schedules", "About" };

        public NavigationViewModel()
        {
            
        }
    }
}
