﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Jaktloggen.InputViews;
using Jaktloggen.Models;
using Jaktloggen.Services;
using Jaktloggen.Utils;
using Xamarin.Forms;


namespace Jaktloggen
{
    public class LogViewModel : BaseViewModel
    {
        HuntViewModel hunt;
        public HuntViewModel Hunt
        {
            get { return hunt; }
            set { hunt = value; Item.JaktId = value.ID; }
        }

        public Logg Item { get; set; }

        public override string ID
        {
            get { return Item.ID; }
            set { Item.ID = value; OnPropertyChanged(nameof(ID)); }
        }

        public int Hits
        {
            get { return Item.Treff; }
            set { 
                Item.Treff = value; 
                OnPropertyChanged(nameof(Hits)); 
                if (Shots < Hits) { Shots = Hits; }
                if (Observed < Hits) { Observed = Hits; }
            }
        }
        public int Observed
        {
            get { return Item.Sett; }
            set { Item.Sett = value; OnPropertyChanged(nameof(Observed)); }
        }
        public int Shots
        {
            get { return Item.Skudd; }
            set { Item.Skudd = value; OnPropertyChanged(nameof(Shots)); }
        }

        public DateTime Date
        {
            get { return Item.Dato; }
            set { 
                var dateWithTime = new DateTime(value.Year, value.Month, value.Day,
                                                Item.Dato.Hour, Item.Dato.Minute, Item.Dato.Second);

                Item.Dato = dateWithTime;  
                OnPropertyChanged(nameof(Date)); }
        }

        public string DateFormatted => Date.ToString("dd.MM kl. hh:mm", new CultureInfo("nb-NO"));

        public TimeSpan Time
        {
            get { return Item.Dato.TimeOfDay; }
            set {
                var dateWithTime = new DateTime(Item.Dato.Year, Item.Dato.Month, Item.Dato.Day,
                                                value.Hours, value.Minutes, value.Seconds);
                
                Item.Dato = dateWithTime; 
                OnPropertyChanged(nameof(Time)); }
        }

        public double Latitude
        {
            get
            {
                double lat = 0;
                double.TryParse(Item.Latitude, out lat);
                return lat;
            }
            set
            {
                Item.Latitude = value.ToString();
                OnPropertyChanged(nameof(Latitude));
                OnPropertyChanged(nameof(PositionInfo));
            }
        }

        public double Longitude
        {
            get
            {
                double lon = 0;
                double.TryParse(Item.Longitude, out lon);
                return lon;
            }
            set
            {
                Item.Longitude = value.ToString();
                OnPropertyChanged(nameof(Longitude));
                OnPropertyChanged(nameof(PositionInfo));
            }
        }


        public string ImageFilename
        {
            get => Utility.GetImageFileName(Item.ImagePath);
            set
            {
                Item.ImagePath = value;
                OnPropertyChanged(nameof(ImageFilename));
                OnPropertyChanged(nameof(Image));
            }
        }

        public string Notes
        {
            get { return Item.Notes; }
            set { Item.Notes = value; OnPropertyChanged(nameof(Notes)); }
        }

        public string Gender
        {
            get { return Item.Gender; }
            set { Item.Gender = value; OnPropertyChanged(nameof(Gender)); }
        }
        public string Weather
        {
            get { return Item.Weather; }
            set { Item.Weather = value; OnPropertyChanged(nameof(Weather)); }
        }
        public string Age
        {
            get { return Item.Age; }
            set { Item.Age = value; OnPropertyChanged(nameof(Age)); }
        }
        public string WeaponType
        {
            get { return Item.WeaponType; }
            set { Item.WeaponType = value; OnPropertyChanged(nameof(WeaponType)); }
        }
        public int Weight
        {
            get { return Item.Weight; }
            set { Item.Weight = value; OnPropertyChanged(nameof(Weight)); }
        }
        public int ButchWeight
        {
            get { return Item.ButchWeight; }
            set { Item.ButchWeight = value; OnPropertyChanged(nameof(ButchWeight)); }
        }
        public int Tags
        {
            get { return Item.Tags; }
            set { Item.Tags = value; OnPropertyChanged(nameof(Tags)); }
        }


        public ImageSource Image
        {
            get
            {
                if (string.IsNullOrEmpty(ImageFilename)) 
                { 
                    return null; 
                }

                return  Utility.GetImageSource(ImageFilename);
            }
        }

        string infoMessage = "Posisjon";
        public string InfoMessage
        {
            get { return infoMessage; }
            set { SetProperty(ref infoMessage, value); OnPropertyChanged(nameof(PositionInfo)); }
        }

        List<Jeger> _hunters = new List<Jeger>();
        public List<Jeger> Hunters
        {
            get { return _hunters; }
            set {
                _hunters = value;
                OnPropertyChanged(nameof(Hunter));
                OnPropertyChanged(nameof(HunterText));
            }
        }
        public Jeger Hunter
        {
            get => Hunters?.SingleOrDefault(h => h.ID == Item.JegerId);
            set
            {
                Item.JegerId = value?.ID;
                OnPropertyChanged(nameof(Hunter));
                OnPropertyChanged(nameof(HunterText));
                OnPropertyChanged(nameof(LogTitle));
                OnPropertyChanged(nameof(SelectedPickerHunter));
            }
        }

        private List<PickerItem> _pickerHunters;
        public List<PickerItem> PickerHunters
        {
            get { return _pickerHunters; }
            set { _pickerHunters = value; OnPropertyChanged(nameof(PickerHunters)); }
        }

        public PickerItem SelectedPickerHunter => PickerHunters?.SingleOrDefault(p => p.ID == Hunter?.ID);


        List<Dog> _dogs = new List<Dog>();
        public List<Dog> Dogs
        {
            get { return _dogs; }
            set
            {
                _dogs = value;
                OnPropertyChanged(nameof(Dogs));
                OnPropertyChanged(nameof(DogText));
            }
        }
        public Dog Dog
        {
            get => Dogs?.SingleOrDefault(d => d.ID == Item.DogId);
            set
            {
                Item.DogId = value?.ID;
                OnPropertyChanged(nameof(Dog));
                OnPropertyChanged(nameof(DogText));
                OnPropertyChanged(nameof(LogTitle));
            }
        }

        List<Art> _species = new List<Art>();
        public List<Art> Species
        {
            get { return _species; }
            set
            {
                _species = value;
                OnPropertyChanged(nameof(Species));
                OnPropertyChanged(nameof(SpecieText));
            }
        }
        public Art Specie
        {
            get => Species?.SingleOrDefault(d => d.ID == Item.ArtId);
            set
            {
                Item.ArtId = value?.ID;
                OnPropertyChanged(nameof(Specie));
                OnPropertyChanged(nameof(SpecieText));
                OnPropertyChanged(nameof(LogTitle));
                OnPropertyChanged(nameof(SelectedPickerSpecie));
            }
        }

        private List<PickerItem> _pickerSpecies;
        public List<PickerItem> PickerSpecies
        {
            get { return _pickerSpecies; }
            set { _pickerSpecies = value; OnPropertyChanged(nameof(PickerSpecies)); }
        }

        public PickerItem SelectedPickerSpecie => PickerSpecies?.SingleOrDefault(p => p.ID == Specie?.ID);

        public string PositionInfo
        {
            get
            {
                if (!string.IsNullOrEmpty(InfoMessage))
                {
                    return InfoMessage;
                }
                return Latitude <= 0 ? "" : $"{Latitude}, {Longitude}";
            }
        }

        public string HunterText => Hunter?.Fornavn;
        public string DogText => Dog?.Navn;
        public string SpecieText => Specie?.Navn;
        public string LogTitle 
        { 
            get
            {
                var lbl = "";
                if (Specie != null)
                    lbl = Specie.Navn + ". ";
                
                lbl += $"{Shots} skudd, {Hits} treff";

                if (Observed > 0)
                {
                    lbl += $", {Observed} sett";
                }
                if (Hunter != null)
                {
                    lbl += $" ({Hunter.Fornavn})";
                }
                return lbl;
            } 
        }

        public ICommand LoadItemsCommand { protected get; set; }
        public ICommand PositionCommand { protected set; get; }
        public ICommand ImageCommand { protected set; get; }
        public ICommand DateCommand { protected set; get; }
        public ICommand NotesCommand { protected set; get; }
        public ICommand DeleteCommand { protected set; get; }
        public ICommand HunterCommand { protected set; get; }
        public ICommand DogCommand { protected set; get; }
        public ICommand SpeciesCommand { protected set; get; }
        public ICommand ObservedCommand { protected set; get; }
        public ICommand HitsCommand { protected set; get; }
        public ICommand ShotsCommand { protected set; get; }

        public LogViewModel(HuntViewModel huntVm, Logg item, INavigation navigation)
        {
            Item = item;
            Navigation = navigation;
            Hunt = huntVm;

            Title = string.IsNullOrEmpty(item.ID) ? "Ny loggføring" : Date.ToString();
            CreateCommands(item);

                    }
        
        public async Task OnAppearing()
        {
            Hunters = await App.HunterDataStore.GetItemsAsync();
            Dogs = await App.DogDataStore.GetItemsAsync();
            Species = await App.SpecieDataStore.GetItemsAsync(); //TODO: Need this call?

            PickerSpecies = await LogFactory.CreatePickerSpecies(Item.ArtId);
            PickerHunters = await LogFactory.CreatePickerHunters(Item.JegerId);

            if (string.IsNullOrEmpty(Item.ID))
            {
                Date = DateTime.Now;
                if (Hunt.HunterIds.Count == 1) { Hunter = Hunters.SingleOrDefault(x => x.ID == Hunt.HunterIds[0]); }
                if (Hunt.DogIds.Count == 1) { Dog = Dogs.SingleOrDefault(x => x.ID == Hunt.DogIds[0]); }
                await SetPositionAsync();
                await SaveAsync();
            }
        }

        private async Task SetPositionAsync()
        {
            InfoMessage = "Henter din posisjon...";
            try
            {
                var position = await LocationService.GetPositionAsync();

                InfoMessage = "Posisjon";

                Latitude = position.Latitude;
                Longitude = position.Longitude;

                OnPropertyChanged(nameof(PositionInfo));

            }
            catch (Exception)
            {
                InfoMessage = "Henting av posisjon feilet.";

                //todo: log this
                //throw new Exception(ex.Message, ex);
            }
        }


        private void CreateCommands(Logg item)
        {
            ImageCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new InputImage("Bilde", ImageFilename, async obj =>
                {
                    ImageFilename = obj.ImageFilename;
                    await SaveAsync();
                }));
            });

            HitsCommand = new Command(async (object isMoreButton) =>
            {
                if (isMoreButton != null && !((bool)isMoreButton))
                {
                    await SaveAsync();
                }
                else
                {
                    var inputView = new InputEntry("Antall treff", Hits.ToString(), async obj =>
                    {
                        Hits = int.Parse(obj.Value);
                        await SaveAsync();
                    });
                    inputView.Numeric = true;
                    await Navigation.PushAsync(inputView);
                }
            });

            ShotsCommand = new Command(async (object isMoreButton) =>
            {
                if (isMoreButton != null && !((bool)isMoreButton))
                {
                    await SaveAsync();
                }
                else
                {
                    var inputView = new InputEntry("Antall skudd", Shots.ToString(), async obj =>
                    {
                        Shots = int.Parse(obj.Value);
                        await SaveAsync();
                    });
                    inputView.Numeric = true;
                    await Navigation.PushAsync(inputView);
                }
            });

            ObservedCommand = new Command(async (object isMoreButton) =>
            {
                if (isMoreButton != null && !((bool)isMoreButton))
                {
                    await SaveAsync();
                }
                else
                {
                    var inputView = new InputEntry("Antall sett", Observed.ToString(), async obj =>
                    {
                        Observed = int.Parse(obj.Value);
                        await SaveAsync();
                    });
                    inputView.Numeric = true;
                    await Navigation.PushAsync(inputView);
                }

            });

            PositionCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new InputPosition(Latitude, Longitude, async obj => {
                    Latitude = obj.Latitude;
                    Longitude = obj.Longitude;
                    await SaveAsync();
                }));
            });
            HunterCommand = new Command( async (object selectedPickerItem) =>
            {
                if (selectedPickerItem != null && selectedPickerItem as PickerItem != null)
                {
                    var id = (selectedPickerItem as PickerItem).ID;
                    Hunter = _hunters.SingleOrDefault(h => h.ID == id);
                    await SaveAsync();
                }
                else
                {
                    var inputView = new InputPicker(
                        "Velg jeger",
                        async obj =>
                        {
                            var id = obj.PickerItems.SingleOrDefault(p => p.Selected)?.ID;
                            Hunter = _hunters.SingleOrDefault(h => h.ID == id);
                            PickerHunters = await LogFactory.CreatePickerHunters(id);
                            await SaveAsync();
                        }, async () =>
                        {
                            Hunters = await App.HunterDataStore.GetItemsAsync();
                            var huntersVM = Hunters.Select(h => new HunterViewModel(h, Navigation));
                            var items = huntersVM.Select(h => new PickerItem
                            {
                                ID = h.ID,
                                Title = h.Name,
                                ImageSource = h.Image,
                                Selected = Hunter != null && h.ID == Hunter.ID
                            }).ToList();

                            return items;
                        });

                    await Navigation.PushAsync(inputView);
                }
            });
            SpeciesCommand = new Command(async (object selectedPickerItem) =>
            {
                if (selectedPickerItem != null && selectedPickerItem as PickerItem != null)
                {
                    var id = (selectedPickerItem as PickerItem).ID;
                    Specie = _species.SingleOrDefault(h => h.ID == id);
                    await SaveAsync();
                }
                else
                {
                    var inputView = new InputPicker(
                        "Velg art",
                        finished: async obj =>
                        {
                            var id = obj.PickerItems.SingleOrDefault(p => p.Selected)?.ID;
                            Specie = _species.SingleOrDefault(h => h.ID == id);
                            PickerSpecies = await LogFactory.CreatePickerSpecies(id);
                            await SaveAsync();
                        },
                        populate: () =>
                        {
                            var speciesVM = _species.Select(h => new SpecieViewModel(h, Navigation));
                            var items = speciesVM.Select(h => new PickerItem
                            {
                                ID = h.ID,
                                Title = h.Name,
                                Selected = Specie != null && Specie.ID == h.ID
                            }).ToList();
                        return Task.FromResult(items);
                        });

                    await Navigation.PushAsync(inputView);
                }
            });

            DogCommand = new Command(async () =>
            {
                var inputView = new InputPicker(
                    "Velg hund",
                    async obj =>
                    {
                        var id = obj.PickerItems.SingleOrDefault(p => p.Selected)?.ID;
                        Dog = _dogs.SingleOrDefault(h => h.ID == id);
                        await SaveAsync();
                    }, async () =>
                    {
                        Dogs = await App.DogDataStore.GetItemsAsync();
                        var dogsVM = Dogs.Select(h => new DogViewModel(h, Navigation));
                        var items = dogsVM.Select(h => new PickerItem
                        {
                            ID = h.ID,
                            Title = h.Name,
                            ImageSource = h.Image,
                            Selected = Dog != null && Dog.ID == h.ID
                        }).ToList();

                        return items;
                    });

                await Navigation.PushAsync(inputView);
            });

            DateCommand = new Command(async () =>
            {
                var inputDate = new InputDate("Tidspunkt", Date, async obj =>
                {
                    Date = obj.Value;
                    await SaveAsync();
                }, 
                true);
                await Navigation.PushAsync(inputDate);
            });

            NotesCommand = new Command(async () =>
            {
                var inputView = new InputEntry("Notater", Notes, async obj =>
                {
                    Notes = obj.Value;
                    await SaveAsync();
                });
                inputView.Multiline = true;
                await Navigation.PushAsync(inputView);
            });
        }

        private void Save()
        {
            Task.Run(SaveAsync);
        }
        public async Task SaveAsync()
        {
            if (!string.IsNullOrEmpty(ID))
            {
                await App.LogDataStore.UpdateItemAsync(Item);
            }
            else
            {
                await App.LogDataStore.AddItemAsync(Item);
            }
        }
    }
}
