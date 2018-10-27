using System;
using System.Linq;
using System.Reflection;
using GreenLeaf.Core;
using Windows.UI.Xaml.Controls;

namespace GreenLeaf.Uwp
{
    public class NavigationService : INavigationService
    {
        readonly Frame frame;

        public NavigationService(Frame frame)
        {
            this.frame = frame;
        }

        public void GoBack()
        {
            frame.GoBack();
        }

        public void GoForward()
        {
            frame.GoForward();
        }

        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);
            return Navigate(type, parameter);
        }

        public bool Navigate(Type source, object parameter = null)
        {
            object src = Activator.CreateInstance(source);
            return frame.Navigate(src, parameter);
        }

        /// <summary>
        /// Used to load a page which requires a unique ID
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Navigate(string page, params object[] args)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(a => a.Name.Equals(page));
            if (type == null) return false;

            object src = Activator.CreateInstance(type);
            return frame.Navigate(src);
        }

        public bool Navigate(string page)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(a => a.Name.Equals(page));
            if (type == null) return false;

            object src = Activator.CreateInstance(type);
            return frame.Navigate(src);
        }
    }
}