using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class WavSongCreator : ISongCreator
    {
        public ISong CreateSong(string path)
        {
            return new WavSong(path);
        }
    }
}
