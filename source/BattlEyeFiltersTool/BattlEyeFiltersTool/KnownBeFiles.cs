/*
	BattlEye Filters Tool
	https://github.com/SpiReCZ/BattlEyeFiltersTool
	© by SpiRe 2015

	This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
	To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/.
*/
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BattlEyeFiltersTool
{
    public static class KnownBeFiles
    {
        // Predefined FileNames
        public static List<string> knownBeFiles = new List<string>
        {
            "attachTo.txt", "createVehicle.txt", "deleteVehicle.txt", "mpeventhandler.txt",
            "publicvariable.txt", "publicVariableVal.txt", "remotecontrol.txt", "remoteexec.txt",
            "scripts.txt", "selectplayer.txt", "setpos.txt", "setVariable.txt", "setVariableVal.txt",
            "teamswitch.txt", "waypointcondition.txt", "waypointstatement.txt"
        };
        public static void CheckKnownBeFilesExists()
        {
            string dirPath = Directories.GetPath(666) + "\\knownBeFiles.txt";
            if (!File.Exists(dirPath))
            {
                // If config file doesn't exist we create it in the folder with .exe
                File.WriteAllLines(dirPath, knownBeFiles);
            }
            else
            {
                string[] tempKnownBeFiles = File.ReadAllLines(dirPath);
                // Add Additional File Names to the List if the file exists
                foreach (string tempKnownBeFile in tempKnownBeFiles)
                {
                    if (!knownBeFiles.Any(tempKnownBeFile.Contains) && System.IO.Path.GetExtension(tempKnownBeFile) == ".txt")
                    {
                        knownBeFiles.Add(tempKnownBeFile);
                    }
                }
            }
        }
    }
}
