using GreenLeaf.Core.ViewModels;

namespace GreenLeaf.Core.Utilities
{
    public static class NavigationEvents
    {
        public delegate void RequestNewPageHandler(Pages pageName, params object[] parameters);
        public static event RequestNewPageHandler RequestNewPage;

        /// <summary>
        /// Request a new page
        /// </summary>
        /// <param name="pageName">The name of the page being requested.</param>
        /// <param name="parameters">Any parameters for the constructor.</param>
        /// <returns></returns>
        public static bool RequestPage(Pages pageName, params object[] parameters)
        {
            if (RequestNewPage != null)
            {
                RequestNewPage(pageName, parameters);
                return true;
            }

            return false;
        }
    }
}
