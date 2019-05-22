using System;

using FoodLog.FoodModels;

namespace FoodLog.ViewModels.LogFood
{
    public class LogDetailViewModel : BaseViewModel
    {
        public FoodPerDay Item { get; set; }
        public LogDetailViewModel(FoodPerDay item = null)
        {
            Title = item.Food?.Name;
            Item = item;
        }
    }
}
