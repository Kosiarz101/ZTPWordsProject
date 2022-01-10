using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using WMPLib;

namespace ZTPWordsProject.AppModels
{
    public class Mp3Song : ISong
    {
        WindowsMediaPlayer player;
        MediaPlayer mediaPlayer;
        public bool IsPlaying { get; set; } = false;
        public Mp3Song(string path)
        {
            mediaPlayer = new MediaPlayer();           
            string completePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Songs", path);
            player = new WindowsMediaPlayer();
            player.URL = completePath;
            //mediaPlayer.Open(new Uri(@"F:\Users\Kosiarz\Documents\Projekty\C#\WPF\ZTPWordsProject\bin\Debug\netcoreapp3.1\Songs\Battlefield1942Theme.mp3", UriKind.Relative));
        }
        public void PlaySong()
        {
            player.controls.play();
            IsPlaying = true;
        }

        public void StopPlaying()
        {
            player.controls.stop();
            IsPlaying = false;
        }
    }
}
