using System;
using System.Collections.Generic;
using FoodLog.FoodModels;

namespace FoodLog.Models.Recipe
{
    public class Recipe : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }
        public List<RecipeItem> Items { get; set; }

        public Recipe()
        {
            Items = new List<RecipeItem>();
        }

        public object Clone()
        {
            Recipe item = new Recipe()
            {
                Id = this.Id,
                Name = this.Name,
                IsReadOnly = this.IsReadOnly,
                Items = new List<RecipeItem>()
            };

            foreach (var i in this.Items)
            {
                item.Items.Add(i.Clone() as RecipeItem);
            }

            return item;
        }
    }

    public class RecipeItem : ICloneable
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Food Food { get; set; }
        public double Amount { get; set; }

        public object Clone()
        {
            RecipeItem item = new RecipeItem()
            {
                Id = this.Id,
                RecipeId = this.RecipeId,
                Food = this.Food.Clone() as Food,
                Amount = this.Amount
            };

            return item;
        }
    }
}
