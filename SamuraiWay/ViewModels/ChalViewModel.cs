using SamuraiDbWork;
using SamuraiDbWork.GlobalDB;
using SamuraiDbWork.LocalDB;
using SamuraiDbWork.Models;
using SamuraiWay.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SamuraiWay.ViewModels
{
    public class ChalViewModel : BaseViewModel
    {
        bool canStart, canEnd;
        string timeleft;
        UserModel currentUser;
        ChallengeModel currentChal;
        Challenges chSource = new Challenges();
        ObservableCollection<ChallengeModel> AvaliableChallenges = new ObservableCollection<ChallengeModel>();
        BitmapImage pfp = new BitmapImage();
        public ChallengeModel CurrentChal
        {
            get => currentChal;
            set { currentChal = value; OnPropertyChanged(); }
        }
        public string UserName { get => currentUser.Name ?? ""; set { currentUser.Name = value; OnPropertyChanged(); } }
        public string ChalName { get => currentChal?.Name ?? ""; set { currentChal.Name = value; OnPropertyChanged(); } }
        public BitmapImage Pfp { get => pfp; set { pfp = value; OnPropertyChanged(); } }
        public bool CanStart { get => canStart; set { canStart = value; OnPropertyChanged();} }
        public bool CanEnd { get => canEnd; set { canEnd = value; OnPropertyChanged(); } }
        public Command SelectRandomChallengeCommand { get; set; }
        public Command SetCurrentChallengeCommand { get; set; }
        public Command FailChallengeCommand { get; set; }
        public string TimeLeft { get => timeleft; set { timeleft = value; OnPropertyChanged(); } }

        public ChalViewModel()
        {
            CanStart = Properties.Settings.Default.challengeEnd <= DateTime.Now.Date;
            CanEnd = !CanStart;
            if (new UserManager().IsServerConnected())
                currentUser = new UserManager().GetUserById(Properties.Settings.Default.currentUserId);
            else
                currentUser = new UserModel() { Login = "offlineUser", Name = "Offline User", ProfilePicture = null };
            // Setting up profile picture
            if(currentUser.ProfilePicture != null)
            {
                using(MemoryStream mem = new MemoryStream(currentUser.ProfilePicture))
                {
                    Pfp.BeginInit();
                    Pfp.StreamSource = mem;
                    Pfp.CacheOption = BitmapCacheOption.OnLoad;
                    Pfp.EndInit();
                }
            }
                foreach (ChallengeModel chal in chSource.GetAllChallenges())
                {
                    AvaliableChallenges.Add(chal);
                }
            CurrentChal = AvaliableChallenges[Properties.Settings.Default.currentChallengeId];
            if (CanEnd)
                TimeLeft = DateTime.Compare(Properties.Settings.Default.challengeEnd, DateTime.Now.Date) + " day(s)";
            SetupCommands();
        }
        void SetupCommands()
        {
            SelectRandomChallengeCommand = new Command(() =>
            {
                CurrentChal = AvaliableChallenges[new Random().Next(0, AvaliableChallenges.Count)];
            });
            SetCurrentChallengeCommand = new Command(() => 
            {
                if (!new UserManager().SetCurrentChallenge(currentUser.Login, CurrentChal.Name, CurrentChal.Description))
                    MessageBox.Show("Failed to set current challenge for user");
                else
                {
                    Properties.Settings.Default.currentChallengeId = CurrentChal.id;
                    Properties.Settings.Default.challengeEnd = DateTime.Now.Date.AddDays(1);
                    Properties.Settings.Default.Save();
                    CanStart = false;
                    CanEnd = true;
                    TimeLeft = DateTime.Compare(Properties.Settings.Default.challengeEnd, DateTime.Now.Date) + " day(s)";
                    MessageBox.Show("Challenge set! Good luck mate");
                }

            });
            FailChallengeCommand = new Command(() => 
            {
                new UserManager().SetCurrentChallenge(currentUser.Login);
                Properties.Settings.Default.challengeEnd = DateTime.Now.Date;
                Properties.Settings.Default.Save();
                CanStart = true;
                CanEnd = false;
                TimeLeft = "";
                MessageBox.Show("T w T");
            });
        }
    }
}
