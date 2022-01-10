using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class WavSong : ISong
    {
        SoundPlayer player;
        public bool IsSongPlaying { get; set; } = false;
        public WavSong(string path)
        {
            string completePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Songs", path);
            player = new SoundPlayer(completePath);
        }
        public void PlaySong()
        {
            player.PlayLooping();
            IsSongPlaying = true;
        }

        public void StopPlaying()
        {
            player.Stop();
            IsSongPlaying = false;
        }
    }
}
