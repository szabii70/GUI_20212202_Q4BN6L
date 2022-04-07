using System.Windows.Media;

namespace EscapeFromZaun.WpfLogic
{
    public interface IRndBackgroundLogic
    {
        string GetRandomImage();
        MediaPlayer mp { get; set; }
        bool Muted { get; set; }
        void ToggleMuted();

    }
}