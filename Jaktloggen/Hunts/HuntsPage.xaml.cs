﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Jaktloggen
{
    public partial class HuntsPage : ContentPage
    {
        HuntsViewModel viewModel;

        public HuntsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HuntsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Hunt;
            if (item == null)
                return;

            await Navigation.PushAsync(new HuntPage(new HuntViewModel(item, Navigation)));
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HuntPage(new HuntViewModel(new Hunt(), Navigation)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.HuntItems.Count == 0)
                viewModel.LoadHuntItemsCommand.Execute(null);
        }
    }
}
