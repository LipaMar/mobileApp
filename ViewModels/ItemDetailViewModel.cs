using System;

using mobileApp.Models;

namespace mobileApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Student Item { get; set; }
        public ItemDetailViewModel(Student item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
