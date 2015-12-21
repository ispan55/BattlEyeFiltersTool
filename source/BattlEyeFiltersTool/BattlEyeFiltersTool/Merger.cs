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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace BattlEyeFiltersTool
{
    public static class Merger
    {
        public static List<object> files = new List<object>();
        private static MainWindow mainWindow;
        public static void MergerInit(MainWindow mainWindowInstance)
        {
            mainWindow = mainWindowInstance;
        }

        public static void GetFiles(string dirPath, int folderType)
        {
            try
            {
            // Get Info about DIR then files in that DIR
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            FileInfo[] filesInfo = dirInfo.GetFiles("*.txt");
            // input subfolders
            if (folderType == 0 || folderType == 1)
            {
                foreach (var fileInfo in filesInfo)
                {
                    // Create object for every File / every valid file
                    if (mainWindow.knownBeFilesCheckBox.IsChecked == true && folderType == 0)
                    {
                        if (KnownBeFiles.knownBeFiles.Any(fileInfo.Name.Contains))
                        {
                            createFileObject(fileInfo, ref folderType);
                        }
                    }
                    else
                    {
                        createFileObject(fileInfo, ref folderType);
                    }
                }
            }
            }
            catch (ArgumentNullException e)
            {
                // This shouldn't happen ever
                System.Windows.MessageBox.Show("Something terrible happend. \n" + e + " \n " + e.InnerException + "", "GetFiles Exception catched", MessageBoxButton.OK);
            }
        }
        private static void createFileObject(FileInfo file, ref int folderType)
        {
            // Create BasicSet file
            if (folderType == 0)
            {
                FileObject fileObject = new FileObject
                {
                    NameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(file.FullName),
                    Extension = file.Extension,
                    Path = file.FullName,
                    Lines = new List<string>()
                };
                files.Add(fileObject);
                mainWindow.listView_FileList.Items.Add(fileObject);
                LoadDataFromBaseFile(fileObject);
            }
            // Create AddSet file
            else
            {
                FileObject fileObjectBasic = new FileObject
                {
                    NameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(file.FullName),
                    Extension = file.Extension,
                    Path = file.FullName
                };
                mainWindow.listView_FileList.Items.Add(fileObjectBasic);
                LoadDataFromAddFile(fileObjectBasic);
            }
        }
        public static void LoadDataFromBaseFile(FileObject fileObject)
        {
            try
            {
            string[] tempLines = File.ReadAllLines(fileObject.Path);
            // Fill the object with data
            foreach (string line in tempLines)
            {
                fileObject.Lines.Add(line);
            }
            }
            catch (ArgumentNullException e)
            {
                // This shouldn't happen ever
                System.Windows.MessageBox.Show("Something terrible happend. \n" + e + " \n " + e.InnerException + "", "LoadDataFromBaseFile Exception catched", MessageBoxButton.OK);
            }
}
        public static void LoadDataFromAddFile(FileObject fileObjectBasic)
        {
            foreach (FileObject file in files)
            {
                // Check for file names match but not for exact match
                if (Regex.IsMatch(fileObjectBasic.Name, file.NameWithoutExt) && fileObjectBasic.NameWithoutExt != file.NameWithoutExt)
                {
                    string[] addLines = File.ReadAllLines(fileObjectBasic.Path);
                    foreach (string addLine in addLines)
                    {
                        try
                        {
                            // exclude one line comments
                            if (addLine.IndexOf('/', 0) != 0 && addLine.IndexOf('/', 1) != 1)
                            {
                                int endBeIndex = GetEndBeIndex(addLine);
                                string beIndex = addLine.Substring(0, endBeIndex);

                                MergeData(addLine, endBeIndex, beIndex, file);
                            }
                        } catch(ArgumentOutOfRangeException e)
                        {
                            // not needed, used for skip fault lines
                            //System.Windows.MessageBox.Show("Something terrible happend. \n" + e + " \n " + e.InnerException + "", "LoadDataFromAddFile Exception catched", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
        // Return end of BE index for further comparison of strings
        private static int GetEndBeIndex(string addLine)
        {
            int endBeIndex = 1;

            try
            {
            int beIndexPointer = addLine.IndexOf('"');
            if (addLine.IndexOf(' ') == beIndexPointer - 1)
            {
                endBeIndex = beIndexPointer - 1;
            }
            else if (addLine.IndexOf('=') == beIndexPointer - 1)
            {
                if (addLine.IndexOf('!') == beIndexPointer - 2)
                {
                    endBeIndex = beIndexPointer - 3;
                }
                else if (addLine.IndexOf(' ') == beIndexPointer - 2)
                {
                    endBeIndex = beIndexPointer - 2;
                }
            }
            else if (addLine.IndexOf('!') == beIndexPointer - 1)
            {
                endBeIndex = beIndexPointer - 2;
            }
            return endBeIndex;
            }
            catch (NullReferenceException)
            {
                // Shouldn't happen but we return 0 because we dont want to have BE index
                return 0;
            }
        }
        private static void MergeData(string addLine, int endBeIndex, string beIndex, FileObject file)
        {
            Boolean matchFound = false;
            string addBeFilters = addLine.Substring(endBeIndex);
            
            // Check for matches for every line except one line comments
            for (int i = 0; i < file.Lines.Count; i++)
            {
                if (addLine.IndexOf('/', 0) != 0 && addLine.IndexOf('/', 1) != 1)
                {
                    // Get BE index for current line in Additional file
                    int endBaseBeIndex = GetEndBeIndex(file.Lines[i]);
                    // create substring without BE index that will be added to the end of the line if match is found
                    string baseBeIndex = file.Lines[i].Substring(0, endBaseBeIndex);
                    if (Regex.IsMatch(baseBeIndex, beIndex))
                    {
                        matchFound = true;
                        file.Lines[i] = file.Lines[i] + addBeFilters;
                        break;
                    }
                }
            }
            if (matchFound == false)
            {
                // We haven't found match in all lines of file so we add whole line = BE index + BE filters
                // Can be also the case where you have difference in the starting number of BE index which is fine 
                file.Lines.Add(addLine);
            }
        }

        public static void SaveToFiles(string dirPath)
        {
            // Save all files
            foreach (FileObject file in files)
            {
                Directories.CheckIfFolderExists(dirPath);
                File.WriteAllLines(dirPath + "\\" + file.Name, file.Lines);
            }
        }
    }
}
