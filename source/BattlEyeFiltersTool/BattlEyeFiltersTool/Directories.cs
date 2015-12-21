/*
	BattlEye Filters Tool
	https://github.com/SpiReCZ/BattlEyeFiltersTool
	© by SpiRe 2015

	This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
	To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattlEyeFiltersTool
{
    public static class Directories
    {
        private static MainWindow mainWindow;
        public static string mainDirPath;
        public static void DirectoriesInit(MainWindow mainWindowInstance)
        {
            mainWindow = mainWindowInstance;
        }
        public static string GetPath(int folderType)
        {

            string dirPath;
            // First run or changed Path Handler
            if (mainDirPath == null || !Directory.Exists(mainDirPath))
            {
                dirPath = Directory.GetCurrentDirectory() + "\\BattlEye";
                CheckIfFolderExists(dirPath);
                CheckIfFolderExists(dirPath + "\\BattlEye_BasicSet");
                CheckIfFolderExists(dirPath + "\\BattlEye_AddSet");
                mainDirPath = dirPath;

                try
                {
                    mainWindow.pathTextBlock.Text = dirPath;
                }
                catch (NullReferenceException) { }
            }
            else
            {
                // Else we can use directory which is already set
                dirPath = mainDirPath;
            }

            // returns needed string
            switch (folderType)
            {
                case 0: return dirPath + "\\BattlEye_BasicSet";
                case 1: return dirPath + "\\BattlEye_AddSet";
                case 2: return dirPath;
                default: return Directory.GetCurrentDirectory();
            }
        }
        public static void CheckIfFolderExists(string dirPath)
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }
            catch (ArgumentNullException e)
            {
                // Shouldn't happen ever
                System.Windows.MessageBox.Show("Something terrible happend. \n" + e + " \n " + e.InnerException + "", "CheckIfFolderExists Exception catched", MessageBoxButton.OK);
            }
            
        }
    }
}
