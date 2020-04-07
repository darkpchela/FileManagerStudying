using System.Collections.Generic;
using System.Linq;

namespace FileManager.Classes.Etc
{
     class History
    {
        public List<string> localHistory  { get; private set; }  = new List<string>();
        public List<string> globalHistory { get; private set; }  = new List<string>();
        public int          localIndex    { get; private set; }  = 0;

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

        public string GetPreviousElement()
        {
            string element;

            StartShifting();
            localIndex--;

            if (localIndex > 0)
            {
                element    = localHistory.ElementAt(localIndex);
                
                return element;
            }
            else
            {
                localIndex = 0;
                element    = localHistory.ElementAt(localIndex);
                
                return element;
            }
        }

        public string GetNextElement()
        {
            string element;

            StartShifting();
            localIndex++;

            if (localIndex < localHistory.Count - 1)
            {
                element    = localHistory.ElementAt(localIndex);

                return element;
            }
            else
            {
                localIndex = localHistory.Count-1;
                element    = localHistory.ElementAt(localIndex);

                return element;
            }
        }
        public void ClearHistory()
        {
            globalHistory.Clear();
        }

        public void UpdateHistory(string element)
        {
            globalHistory.Add(element);
        }
    }
}
