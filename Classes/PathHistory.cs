using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Classes
{
    class PathHistory
    {
        public List<string> localHistory { get; private set; }
        public List<string> globalHistory { get; private set; }

        private int localIndex = 0;

        private bool workInLocal = false;

        public void SetHistory(List<string> pathHistory)
        {
            localHistory  = pathHistory;
            globalHistory = pathHistory;
            localIndex    = localHistory.Count - 1;
        }

        public List<string> GetHistory()
        {
            return globalHistory;
        }
        public string GetPreviousPath()
        {
            string path;
            localIndex--;
            if (localIndex < 0)
                localIndex = 0;

            path = localHistory.ElementAt(localIndex);
            globalHistory.Add(path);
            return path;
        }

        public string GetNextPath()
        {
            string path;
            localIndex++;
            if (localIndex >localHistory.Count-1)
                localIndex = localHistory.Count;

            path = localHistory.ElementAt(localIndex);
            globalHistory.Add(path);
            return path;
        }
    }
}
