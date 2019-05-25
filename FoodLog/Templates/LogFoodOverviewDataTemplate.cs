using System;
using FoodLog.FoodModels;
using Xamarin.Forms;

namespace FoodLog.Templates
{
    public class LogFoodOverviewDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LogFoodTemplate { get; set; }
        public DataTemplate AddFoodTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            FoodPerDay f = item as FoodPerDay;
            return f.Id >= 0 ? LogFoodTemplate : AddFoodTemplate;
        }
        
    }
}
