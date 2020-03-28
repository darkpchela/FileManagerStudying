namespace FileManager.Classes.Etc
{
    enum DialogOptions { Yes, No, Cancel }

    delegate DialogOptions DialogOptionHandler();

    delegate void MessageHandler(string message);
}
