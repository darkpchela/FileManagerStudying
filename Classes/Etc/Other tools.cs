using System;

namespace FileManager.Classes.Etc
{
    delegate void MessageHandler(string message);
    delegate void MessageEventHandler(object sender, string message);

    //----------------------------------------------
    enum DialogOptions { Yes, No, YesToAll, NoToAll ,Cancel, Default }

    delegate DialogOptions DialogOptionEventHandler<T>(object sender, T e) where T:EventArgs;

    class ExistedItemAppearedEventArgs : EventArgs
    {
        public (string,string)[] collisions;
        public (string, string) currentCollision;
        public bool     multipleProcces;
    }
    //----------------------------------------------------
    class SelectedFileChangedEventArgs : EventArgs
    {
        public string fullName;
        public bool isDirectory;
    }
}
