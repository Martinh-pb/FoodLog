using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.FoodData;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using Xamarin.Forms;

namespace FoodLog.Views.LogFood
{
    public partial class LogOverviewPage : ContentPage
    {
        LogDayViewModel viewModel;

        public LogOverviewPage()
        {
            InitializeComponent();

            if (!DesignMode.IsDesignModeEnabled)
            {
                var r = Injector.Resolve<IFoodRepository>();
                viewModel = new LogDayViewModel(r);
                BindingContext = viewModel;
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FoodPerDay;
            if (item == null)
                return;

            if (item.Id < 0)
            {
                await AddItem(item.MealType);
            }
            else
            {
                await Navigation.PushAsync(new LogDetailPage(new LogDetailViewModel(item)));
            }
            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await AddItem(MealType.BreakFast);
        }

        async Task AddItem(MealType mealType)
        {
            FoodPerDay entry = new FoodPerDay()
            {
                MealType = mealType,
                Date = viewModel.Date,
                Time = viewModel.Date
            };

            var repo = Injector.Resolve<IFoodRepository>();
            await Navigation.PushModalAsync(new NavigationPage(new LogAddPage(new LogAddViewModel(repo, entry))));
        }

        async public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            FoodPerDay toDelete = mi.CommandParameter as FoodPerDay;

            await viewModel.DeleteItem(toDelete);
        }

        public void ListHeaderTapped(object sender, EventArgs e)
        {
            Grid grid = sender as Grid;
            FoodDayGroup g = grid.BindingContext as FoodDayGroup;
            viewModel.SwitchHeader(g);
        }

        public void Previous_Date_Clicked(object sender, EventArgs e)
        {
            viewModel.GotoPreviousDate();
        }

        public void Next_Date_Clicked(object sender, EventArgs e)
        {
            viewModel.GotoNextDate();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Date == DateTime.MinValue)
            {
                viewModel.InitDayCommand.Execute(null);
            }
        }
    }
    /*
     * 
     * 
     <ListView.GroupHeaderTemplate>
                <DataTemplate>
                    <ViewCell>                    
                        <StackLayout BackgroundColor="#000" Orientation="Vertical" 
                                     Margin="0" Padding="5">
                            <StackLayout.GestureRecognizers>
                                <TapGestureRecognizer Tapped="ListHeaderTapped" 
                                                      CommandParameter="{Binding .}"/>
                            </StackLayout.GestureRecognizers>
                            
                            <StackLayout Orientation="Horizontal">
                                <Label TextColor="White" Text="{Binding Title}" FontSize="Small"/>
                                <Label TextColor="White" Text="{Binding Calculate}" FontSize="Small"/>
                                <Label TextColor="White" Text="{Binding Calories}" FontSize="Small"/>
                            </StackLayout>
                        </StackLayout>
                    </ViewCell>
                </DataTemplate>
            </ListView.GroupHeaderTemplate>
            <ListView.ItemTemplate>
                <DataTemplate>
                    <ViewCell>
                    </ViewCell>                    
                </DataTemplate>
            </ListView.ItemTemplate>
                        <ListView.ItemTemplate>
                <DataTemplate>
                    <ViewCell>
                        <Grid Margin="5,0,5,0" Padding="0,5,0,5" RowSpacing="0">
                            <Grid.RowDefinitions>
                                <RowDefinition Height="20"/>
                                <RowDefinition Height="15"/>
                            </Grid.RowDefinitions>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="8*"/>
                                <ColumnDefinition Width="2*"/>
                            </Grid.ColumnDefinitions>
                            
                            <Label Grid.Row="0" Grid.Column="0" Text="{Binding Food.Name}" FontSize="Small"/>
                            <Label Grid.Row="0" Grid.Column="1" HorizontalOptions="End" Text="{Binding Calories}" FontSize="Small"/>
                            <Label Grid.Row="1" Grid.Column="0" Text="{Binding AmountDescription}" FontSize="Micro"/>
                        </Grid>
                    </ViewCell>
                </DataTemplate>
            </ListView.ItemTemplate>
     */
}
