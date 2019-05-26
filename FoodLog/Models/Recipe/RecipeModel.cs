using System;
using System.Collections.Generic;
using System.Linq;
using FoodLog.FoodModels;

namespace FoodLog.Models.Recipe
{
    public class Recipe : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }
        public List<RecipeItem> Items { get; set; }
        public Macros Macros { get; set; }

        public Recipe()
        {
            Macros = new Macros();
            Items = new List<RecipeItem>();
        }

        public void Add(RecipeItem item)
        {
            Items.Add(item);
            CalculateSummary();
        }

        public void CalculateSummary()
        {
            Macros.Calories = Math.Round( Items.Sum(c => c.Macros.Calories), 1, MidpointRounding.ToEven);
            Macros.Carbs = Math.Round(Items.Sum(c => c.Macros.Carbs), 1, MidpointRounding.ToEven);
            Macros.Fat = Math.Round(Items.Sum(c => c.Macros.Fat), 1, MidpointRounding.ToEven);
            Macros.Protein = Math.Round(Items.Sum(c => c.Macros.Protein), 1, MidpointRounding.ToEven);
        }

        public string MacroSummary
        { 
            get
            {
                return $"Carbs: {Macros.Carbs:N2} Protein: {Macros.Protein:N2} Fat: {Macros.Fat:N2}";
            }
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

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value; 
                if (Food != null)
                {
                    Macros = Food.GetMacros(_amount);
                }
                else
                {
                    Macros = new Macros();
                }
            }
        }

        public Macros Macros { get; set; }

        public object Clone()
        {
            RecipeItem item = new RecipeItem()
            {
                Id = this.Id,
                RecipeId = this.RecipeId,
                Food = this.Food.Clone() as Food,
                Amount = this.Amount,
                Macros = this.Macros.Clone() as Macros
            };

            return item;
        }
    }
}
