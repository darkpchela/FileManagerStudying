using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Classes
{
    class PathHistory
    {
        public List<string> localHistory  { get; private set; }  = new List<string>();
        public List<string> globalHistory { get; private set; }  = new List<string>();

        private int localIndex = 0;

        private bool IsMoving = false;

        private void StartMoving()
        {
            if (!IsMoving)
            {
                IsMoving = true;
                localHistory = globalHistory;
                localIndex = (localHistory.Count - 1) < 0 ? 0 : (localHistory.Count - 1);
            }
        }

        public void StopMoving()
        {
            IsMoving     = false;
        }

        public string GetPreviousPath()
        {
            StartMoving();
            string path;
            localIndex--;
            if (localIndex >= 0)
            {
                path = localHistory.ElementAt(localIndex);
                globalHistory.Add(path);
                
                return path;
            }
            else
            {
                localIndex = 0;
                path       = localHistory.ElementAt(localIndex);
                
                return path;
            }
        }

        public string GetNextPath()
        {
            StartMoving();
            string path;
            localIndex++;
            if (localIndex <= localHistory.Count - 1)
            {

                path = localHistory.ElementAt(localIndex);
                globalHistory.Add(path);
                return path;
            }
            else
            {
                localIndex = localHistory.Count-1;
                path       = localHistory.ElementAt(localIndex);
                return path;
            }
        }
        public void ClearHistory()
        {
            globalHistory.Clear();
        }
    }
}
