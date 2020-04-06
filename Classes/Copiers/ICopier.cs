using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.Classes.Etc;

namespace FileManager.Classes.Copiers
{
    interface ICopier
    {
        event DialogOptionEventHandler<ExistedItemAppearedEventArgs> AlreadyExistedItemAppeared; 
        void Copy(string toDirectory, bool overwrite = false);
        bool TryCopy(string toDirectory, bool overwrite = false);
        void Move(string toDirectory);
        bool TryMove(string toDirectory);
    }
}
