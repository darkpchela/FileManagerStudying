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

        private bool IsShifting = false;

        private void StartShifting()
        {
            if (!IsShifting)
            {
                IsShifting   = true;
                localHistory = new List<string>(globalHistory);
                localIndex   = (localHistory.Count - 1) < 0 ? 0 : (localHistory.Count - 1);
            }
        }

        public void StopShifting()
        {
            IsShifting = false;
        }

        public string GetPreviousPath()
        {
            string path;

            StartShifting();
            localIndex--;

            if (localIndex > 0)
            {
                path       = localHistory.ElementAt(localIndex);
                
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
            string path;

            StartShifting();
            localIndex++;

            if (localIndex < localHistory.Count - 1)
            {
                path       = localHistory.ElementAt(localIndex);

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

        public void UpdateHistory(string path)
        {
            globalHistory.Add(path);
        }
    }
}
