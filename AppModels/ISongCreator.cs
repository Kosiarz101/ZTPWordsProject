using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public interface ISongCreator
    {
        public ISong CreateSong(string path);
    }
}
