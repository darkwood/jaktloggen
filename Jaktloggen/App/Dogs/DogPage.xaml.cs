﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Jaktloggen
{
    public partial class DogPage : ContentPage
    {
        DogViewModel viewModel;

        public DogPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DogViewModel(new Dog(), null);
        }

        public DogPage(DogViewModel dogViewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = dogViewModel;
        }


        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Save", viewModel);
            await Navigation.PopAsync();
        }
    }
}
