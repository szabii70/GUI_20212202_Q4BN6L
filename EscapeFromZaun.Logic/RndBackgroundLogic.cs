using System;
using System.IO;

namespace EscapeFromZaun.Logic
{
    public class RndBackgroundLogic : IRndBackgroundLogic
    {
        static Random random = new Random();
        int rndNumber;
        string[] backgoundImages;

        public string GetRandomImage()
        {
            backgoundImages = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images", "*.jpg");

            rndNumber = random.Next(0, backgoundImages.Length);
            var images = backgoundImages[rndNumber].Split('\\');
            string selectedImage = images[1];
            ;
            return selectedImage;
        }
    }
}