using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileManager.Classes.Etc;

namespace FileManager.Classes.Copiers
{
    class FileCopier:ICopier
    {
        public  FileInfo      currentFileInfo { get; private set; }
        private DialogOptions currentSelectedOption;

        public event DialogOptionEventHandler<ExistedItemAppearedEventArgs> AlreadyExistedItemAppeared;
        private void OnAlreadyExistedFileAppeared(ExistedItemAppearedEventArgs e)
        {
            DialogOptionEventHandler<ExistedItemAppearedEventArgs> handler = AlreadyExistedItemAppeared;
            this.currentSelectedOption = handler?.Invoke(this, e) ?? DialogOptions.Cancel;
        }

        public void SetFile(FileInfo file)
        {
            currentFileInfo = file;
        }//OK
        public void SetFile(string path)
        {
            currentFileInfo = new FileInfo(path);
        }//OK
        public void Copy(string toDirectory, bool overwrite = false)
        {
            currentSelectedOption = DialogOptions.Default;

            string newPath = Path.Combine(toDirectory, currentFileInfo.Name);

            if (currentFileInfo.FullName.Contains(" -copy"))
            {
                PathValidator.FileCopyPathUpdate(ref newPath);
            }
            else
            if (currentFileInfo.FullName.Equals(newPath))
            {
                newPath = newPath + " -copy";
                PathValidator.FileCopyPathUpdate(ref newPath);
            }


            if (File.Exists(newPath) && overwrite == false)
            {

                ExistedItemAppearedEventArgs e = new ExistedItemAppearedEventArgs();
                e.multipleProcces  = false;
                e.currentCollision = (currentFileInfo.FullName, newPath);

                OnAlreadyExistedFileAppeared(e);

                if (currentSelectedOption==DialogOptions.Yes)
                    currentFileInfo.CopyTo(newPath, true);

            }
            else
            currentFileInfo.CopyTo(newPath, overwrite);
        }//OK
        public bool TryCopy(string toDirectory, bool overwrite=false)
        {
            try
            {
                Copy(toDirectory, overwrite);
                return true;
            }
            catch(Exception)
            {
                return false;
                throw;
            }
        }//OK

        public void Move(string toDirectory)
        {
            currentSelectedOption = DialogOptions.Default;

            string newPath = Path.Combine(toDirectory,  currentFileInfo.Name);

            if (File.Exists(newPath))
            {
                ExistedItemAppearedEventArgs e = new ExistedItemAppearedEventArgs();

                e.currentCollision = (currentFileInfo.FullName, newPath);
                e.multipleProcces = false;

                OnAlreadyExistedFileAppeared(e);

                if (currentSelectedOption == DialogOptions.Yes)
                {
                    currentFileInfo.CopyTo(newPath, true);
                    currentFileInfo.Delete();
                }
            }
            else
                currentFileInfo.MoveTo(newPath);
            
                
        }//OK  

        public bool TryMove(string toDirectory)//OK
        {
            try
            {
                Move(toDirectory);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
