﻿using Xamarin.Forms;

namespace Jaktloggen
{
    public partial class JaktloggenPage : TabbedPage
    {
        
        public JaktloggenPage() 
        {
            Page itemsPage, aboutPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new HuntsPage())
                                {
                                    Title = "Browse"
                                };

                    aboutPage = new NavigationPage(new AboutPage())
                                {
                                    Title = "About"
                                };
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new HuntsPage()
                                {
                                    Title = "Browse"
                                };

                    aboutPage = new AboutPage()
                                {
                                    Title = "About"
                                };
                    break;
            }

            Children.Add(itemsPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
