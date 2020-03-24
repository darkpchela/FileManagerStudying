using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Classes.Etc
{
    enum DialogOptions { Yes, No, Cancel }

    delegate DialogOptions OverwriteOptionsHandler();

    delegate void MessageHandler(string message);
}
