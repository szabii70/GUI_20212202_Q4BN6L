using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.WpfLogic
{
    public class RndBackgroundLogic : IRndBackgroundLogic
    {
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
    }
}
