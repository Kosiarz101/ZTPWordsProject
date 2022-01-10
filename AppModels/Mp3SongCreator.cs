using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class Mp3SongCreator : ISongCreator
    {
        public ISong CreateSong(string path)
        {
            return new Mp3Song(path);
        }
    }
}
