namespace ContactsClient.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// View-model for Select group window.
    /// </summary>
    public class SelectGroupWindowVM : GroupWindowVM
    {
        /// <summary>
        /// List of group names.
        /// </summary>
        public List<string> GroupNames { get; set; }
    }
}