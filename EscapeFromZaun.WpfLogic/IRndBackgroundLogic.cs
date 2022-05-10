using System.Windows.Media;

namespace EscapeFromZaun.WpfLogic
{
    public interface IRndBackgroundLogic
    {
        string GetRandomImage();
        MediaPlayer mp { get; set; }
        MediaPlayer sp { get; set; }

        bool Muted { get; set; }
        bool MutedSound { get; set; }
        double EffectSound { get; set; }
        void ToggleMuted();
        void ToggleMutedSound();


    }
}