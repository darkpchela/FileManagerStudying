using System;
using System.Linq;
using System.IO;

namespace FileManager.Classes
{
    class WindowsDrivesInfo
    {
        public static DriveInfo[] drivesInfo  { get; private set; }  = DriveInfo.GetDrives();

        public static string[]    drivesNames { get; private set; } = (from dr in drivesInfo select dr.Name).ToArray();

        public double GetAvailableSpaceAtDrive(string driveName)
        {
            double freeSpace;
            try
            {
                DriveInfo di = new DriveInfo(@"{driveName}");
                freeSpace = (di.AvailableFreeSpace / 1024) / 1024;
                return freeSpace;
            }
            catch
            {
                //excActionDrive?.Invoke();
                freeSpace = (drivesInfo[0].AvailableFreeSpace/1024)/1024;
                return freeSpace;
            }
        }

        public static void RefreshInfo()
        {
            drivesInfo  = DriveInfo.GetDrives();
            drivesNames = (from dr in drivesInfo select dr.Name).ToArray();
        }
    }
}
