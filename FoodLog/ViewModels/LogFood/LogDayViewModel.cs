using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using FoodLog.FoodModels;
using FoodLog.Views.LogFood;
using System.Collections.Generic;
using FoodLog.FoodData;

namespace FoodLog.ViewModels.LogFood
{
    public class LogDayViewModel : BaseViewModel
    {
        public ObservableCollection<FoodDayGroup> Items { get; set; }
        public Command InitDayCommand { get; set; }
        public Command LoadItemsCommand { get; set; }

        public FoodDayGroup BreakFast { get; set; }
        public FoodDayGroup Lunch { get; set; }
        public FoodDayGroup Diner { get; set; }
        public FoodDayGroup Snack { get; set; }

        public string TotalEnergy { get; set; }
        public string CarbDescrip { get; set; }
        public string ProteinDescrip { get; set; }
        public string FatDescrip { get; set; }

        public DateTime Date { get; set; }
        public string DateText
        {
            get { return Date.ToString("dddd dd MMMM yy"); }
        }

        public LogDayViewModel(IFoodRepository foodRepository)
        {


            Title = "";
            Items = new ObservableCollection<FoodDayGroup>();
            if (!DesignMode.IsDesignModeEnabled)
            {
                FoodRepository = foodRepository;

                Date = DateTime.MinValue;

                BreakFast = new FoodDayGroup("Ontbijt", MealType.BreakFast);
                Items.Add(BreakFast);

                Lunch = new FoodDayGroup("Lunch", MealType.Lunch);
                Items.Add(Lunch);

                Diner = new FoodDayGroup("Diner", MealType.Diner);
                Items.Add(Diner);

                Snack = new FoodDayGroup("Snack", MealType.Snack);
                Items.Add(Snack);
            }

            InitDayCommand = new Command(async () => await ExecuteDayCommand());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<LogAddToDayPage, FoodPerDay>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as FoodPerDay;

                await FoodRepository.SaveFoodForDay(newItem);
            });
        }

        public async Task DeleteItem(FoodPerDay foodPerDay)
        {
            await FoodRepository.DeleteFoodForDay(foodPerDay);
        }

        async Task ExecuteDayCommand()
        {
            Date = DateTime.Today;
            OnPropertyChanged("DateText");

            await ExecuteLoadItemsCommand();
        }

        public void SwitchHeader(FoodDayGroup grp)
        {
            if (grp != null)
            {
                bool showPercentage = !grp.ShowPercentage;

                BreakFast.ShowPercentage = showPercentage;
                Lunch.ShowPercentage = showPercentage;
                Diner.ShowPercentage = showPercentage;
                Snack.ShowPercentage = showPercentage;

                Calculate();
            }
        }

        async internal void GotoNextDate()
        {
            Date = Date.AddDays(1);
            OnPropertyChanged("DateText");
            await ExecuteLoadItemsCommand();
        }

        async internal void GotoPreviousDate()
        {
            Date = Date.AddDays(-1);
            OnPropertyChanged("DateText");
            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                List<FoodPerDay> entries = await FoodRepository.GetFoodForDay(Date);

                BreakFast.Clear();
                Lunch.Clear();
                Diner.Clear();
                Snack.Clear();

                foreach (var entry in entries.Where(e => e.MealType == MealType.BreakFast))
                {
                    BreakFast.Add(entry);
                }

                BreakFast.Add(new FoodPerDay() { Id = -1, Date = Date, Time = Date, MealType = MealType.BreakFast });

                foreach (var entry in entries.Where(e => e.MealType == MealType.Lunch))
                {
                    Lunch.Add(entry);
                }

                Lunch.Add(new FoodPerDay() { Id = -1, Date = Date, Time = Date, MealType = MealType.Lunch });

                foreach (var entry in entries.Where(e => e.MealType == MealType.Diner))
                {
                    Diner.Add(entry);
                }

                Diner.Add(new FoodPerDay() { Id = -1, Date = Date, Time = Date, MealType = MealType.Diner });

                foreach (var entry in entries.Where(e => e.MealType == MealType.Snack))
                {
                    Snack.Add(entry);
                }
                Snack.Add(new FoodPerDay() { Id = -1, Date = Date, Time = Date, MealType = MealType.Snack });

                Calculate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Calculate()
        {
            BreakFast.Calcutate();
            Lunch.Calcutate();
            Diner.Calcutate();
            Snack.Calcutate();

            TotalEnergy = $"{Math.Round(Items.Sum(s => s.Calories), 1, MidpointRounding.ToEven):N2}";
            if (BreakFast.ShowPercentage)
            {

                CarbDescrip = $"Carbs: {Math.Round(Items.Sum(s => s.CarbInPercent), 1, MidpointRounding.ToEven):N2}%";
                ProteinDescrip = $"Prot: {Math.Round(Items.Sum(s => s.ProteinInPercent), 1, MidpointRounding.ToEven):N2}%";
                FatDescrip = $"Fat: {Math.Round(Items.Sum(s => s.FatInPercent), 1, MidpointRounding.ToEven):N2}%";
            }
            else
            {
                CarbDescrip = $"Carbs: {Math.Round(Items.Sum(s => s.Carbs), 1, MidpointRounding.ToEven):N2}";
                ProteinDescrip = $"Prot {Math.Round(Items.Sum(s => s.Protein), 1, MidpointRounding.ToEven):N2}";
                FatDescrip = $"Fat: {Math.Round(Items.Sum(s => s.Fat), 1, MidpointRounding.ToEven):N2}";
            }

            OnPropertyChanged("TotalEnergy");
            OnPropertyChanged("CarbDescrip");
            OnPropertyChanged("ProteinDescrip");
            OnPropertyChanged("FatDescrip");
        }
    }
}