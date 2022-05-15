using Microsoft.Win32;
using SamuraiDbWork.GlobalDB;
using SamuraiWay.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SamuraiWay.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public event EventHandler AuthSuccessEvent;
        Image imgeg = null;
        string name, login, pass;
        BitmapImage imageSource;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Login { get => login; set { login = value; OnPropertyChanged(); }}
        public string Pass { get => pass; set { pass = value; OnPropertyChanged(); }}
        public BitmapImage ImageSrc { get => imageSource; set { imageSource = value; OnPropertyChanged(); } }
        public Command ChangePFPCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command LoginCommand { get; set; }
        public RegisterViewModel()
        {
            
            LoginCommand = new Command(()=>
            {
                if (new UserManager().CheckAuth(Login,Pass))
                {
                    MessageBox.Show("Успешно залогинился, харош");
                    Properties.Settings.Default.currentUserId = new UserManager().GetIdByLogin(Login);
                    Properties.Settings.Default.Save();
                    AuthSuccessEvent?.Invoke(this, new EventArgs());
                }
                else
                    MessageBox.Show("rinni_type_beat_mp.mp3");
            });
            RegisterCommand = new Command(() =>
            {
                UserManager dbManager = new UserManager();
                bool isSucceed;
                if (imgeg is null)
                {
                    isSucceed = dbManager.RegisterUser(Login, Pass, Name);
                }
                else
                {
                    byte[] img = null;
                    MemoryStream mem = new MemoryStream();
                    imgeg.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                    BinaryReader reader = new BinaryReader(mem);
                    mem.Position = 0;
                    img = reader.ReadBytes((int)mem.Length);
                    isSucceed = dbManager.RegisterUser(Login,Pass,Name,img);
                }
                if (isSucceed)
                {
                    Properties.Settings.Default.currentUserId = dbManager.GetIdByLogin(Login);
                    Properties.Settings.Default.Save();
                }
            });
            ChangePFPCommand = new Command(() => 
            {
                OpenFileDialog ofd = new OpenFileDialog() { Filter = "Images |*.jpg;*.jpeg;*.png" };
                if (ofd.ShowDialog() ?? false)
                {
                    imgeg = Image.FromFile(ofd.FileName);
                    var temp = new BitmapImage();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        imgeg.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        stream.Position = 0;
                        temp.BeginInit();
                        temp.StreamSource = stream;
                        temp.CacheOption = BitmapCacheOption.OnLoad;
                        temp.EndInit();
                    }
                    ImageSrc = temp;
                }
            });
        }

    }
}
