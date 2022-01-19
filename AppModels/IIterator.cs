using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public interface IIterator
    {
        public bool HasNext();

        public IIterator GetNext();
    }
}
