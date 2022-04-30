using System;
using SamuraiDbWork;
using System.Collections.ObjectModel;
using SamuraiWay.Models;

namespace SamuraiWay.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        Challenge currentChal;
        Challenges chSource = new Challenges(App.dbPath);
        ObservableCollection<Challenge> AvaliableChallenges = new ObservableCollection<Challenge>();
        public Command SelectRandomChallengeCommand { get; set; }
        public Challenge CurrentChal { get => currentChal;
            set { currentChal = value; OnPropertyChanged(); } }

        public MainWindowViewModel()
        {
            foreach (Challenge chal in chSource.GetAllChallenges())
            {
                AvaliableChallenges.Add(chal);
            }
            SelectRandomChallengeCommand = new Command(() =>
            {
                CurrentChal = AvaliableChallenges[new Random().Next(0, AvaliableChallenges.Count)];
            });
        }
    }
}
