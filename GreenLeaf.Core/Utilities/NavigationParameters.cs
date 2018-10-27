namespace GreenLeaf.Core.Utilities
{
    public class NavigationParameters
    {
        /// <summary>
        /// The ID of the object being requested
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the object being requested
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The view model associated with the object
        /// </summary>
        public object ViewModel { get; set; }


        public NavigationParameters(int value)
        {
            Id = value;
        }

        public NavigationParameters(string value)
        {
            Name = value;
        }

        public NavigationParameters(object obj)
        {
            ViewModel = obj;
        }
    }
}
