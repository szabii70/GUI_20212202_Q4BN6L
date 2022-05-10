using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace EscapeFromZaun.WpfLogic
{
    public class RndBackgroundLogic : IRndBackgroundLogic
    {
        public MediaPlayer sp { get; set; }
        public MediaPlayer mp { get; set; }
        public bool  Muted { get; set; }
        public bool MutedSound { get; set; }

        public double EffectSound { get; set; }

        private double volume;
        IMessenger messenger;

        static Random random = new Random();
        int rndNumber;
        string[] backgoundImages;

        public string GetRandomImage()
        {
            backgoundImages = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/MainMenuBackground", "*.jpg");

            rndNumber = random.Next(0, backgoundImages.Length);
            var images = backgoundImages[rndNumber].Split('\\');
            string selectedImage = images[1];
            ;
            return selectedImage;
        }

        public RndBackgroundLogic(IMessenger messenger)
        {
            this.messenger = messenger;
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string newpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(path, @"..\..\..\")) + "Views\\Audio\\Songs\\Final song.mp3";
            string path2 = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string newpath2 = System.IO.Path.GetFullPath(System.IO.Path.Combine(path, @"..\..\..\")) + "Views\\Audio\\Songs\\shoot2.wav";
            mp = new MediaPlayer();
            mp.Open(new Uri(newpath));
            mp.Play();
            volume = mp.Volume;
            sp = new MediaPlayer();
            sp.Open(new Uri(newpath2));
            EffectSound = 1;
            sp.Volume = EffectSound;
        }

        public void ToggleMuted()
        {
            if (!Muted)
            {
                mp.Volume = 0;
                Muted = true;
                messenger.Send("Muted", "SoundInfo");
            }
            else
            {
                mp.Volume = volume;
                Muted = false;
                messenger.Send("Muted", "SoundInfo");
            }

        }
        public void ToggleMutedSound()
        {
            if (!MutedSound)
            {
                sp.Volume = 0;
                MutedSound = true;
                messenger.Send("MutedSound", "SoundInfo");
            }
            else
            {
                sp.Volume = volume;
                MutedSound = false;
                messenger.Send("MutedSound", "SoundInfo");
            }

        }
    }
}
