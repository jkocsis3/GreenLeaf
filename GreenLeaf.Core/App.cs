using GreenLeaf.Core.Utilities;
using GreenLeaf.Core.ViewModels;
using MvvmCross.ViewModels;

namespace GreenLeaf.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            //base.Initialize();

            //Setup.AddServices(this);
            //Set the startup object
            BasicData.CopyDateBase();
            //BasicData.CopyDateBaseBack();
#if DEBUG
            TestData.CreateDatabase();
#endif
            RegisterAppStart<NavigationViewModel>();            
        }       
    }
}
