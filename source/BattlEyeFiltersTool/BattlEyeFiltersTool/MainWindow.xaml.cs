/*
	BattlEye Filters Tool
	https://github.com/SpiReCZ/BattlEyeFiltersTool
	© by SpiRe 2015

	This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
	To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/.
*/
using System.Windows;
using System.Windows.Forms;
using System.IO;
using MahApps.Metro.Controls;

namespace BattlEyeFiltersTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindow mainWindow;

        public MainWindow()
        {
            mainWindow = this;
            // Reference for the MainWindow in other classes
            Directories.DirectoriesInit(mainWindow);
            Merger.MergerInit(mainWindow);
            InitializeComponent();
        }
        // CheckBoxes
        private void knownBeFilesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            reloadKnownBeFiles.IsEnabled = true;
            KnownBeFiles.CheckKnownBeFilesExists();
        }
        private void knownBeFilesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            reloadKnownBeFiles.IsEnabled = false;
        }
        private void reloadKnownBeFiles_Click(object sender, RoutedEventArgs e)
        {
            KnownBeFiles.CheckKnownBeFilesExists();
        }
        // Buttons
        private void info_click(object sender, RoutedEventArgs e)
        {
            // Flyout switch open/close
            if (this.infoFlyout.IsOpen == false)
            {
                this.infoFlyout.IsOpen = true;
            }
            else
            {
                this.infoFlyout.IsOpen = false;
            }
        }
        private void selectFolder_click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (Directory.Exists(dialog.SelectedPath))
            {
                Directories.mainDirPath = dialog.SelectedPath;
                pathTextBlock.Text = dialog.SelectedPath;
            }
            else // Fuse for folder which doesn't exist
            {
                //while (!Directory.Exists(dialog.SelectedPath))
                //{
                if (System.Windows.MessageBox.Show("Selected Directory \"" + System.IO.Path.GetFileName(dialog.SelectedPath) + "\" doesn't exists! Do you want to create the directory ?", "Create Directories Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Directories.CheckIfFolderExists(dialog.SelectedPath);
                    Directories.mainDirPath = dialog.SelectedPath;
                    pathTextBlock.Text = dialog.SelectedPath;
                }
                else
                {
                    pathTextBlock.Text = Directories.mainDirPath;
                }
                //}

            }
        }
        private void loadFiles_click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                Directories.CheckIfFolderExists(Directories.GetPath(i));
                Merger.GetFiles(Directories.GetPath(i), i);
            }
            if (listView_FileList.Items.Count > 0)
            {
                selectFolder.IsEnabled = false;
                loadFiles.IsEnabled = false;
                flushFiles.IsEnabled = true;
                saveFiles.IsEnabled = true;
            }
        }
        private void flushFiles_click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listView_FileList.Items.Count; i++)
            {
                listView_FileList.Items[i] = null;
            }
            listView_FileList.Items.Clear();
            if (listView_FileList.Items.Count == 0)
            {
                selectFolder.IsEnabled = true;
                loadFiles.IsEnabled = true;
                flushFiles.IsEnabled = false;
                saveFiles.IsEnabled = false;
            }
        }
        private void saveFiles_click(object sender, RoutedEventArgs e)
        {
            Merger.SaveToFiles(Directories.GetPath(2));
        }
        // Links
        private void donate_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WKE8JUXGUGGXA");
        }
        private void github_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/SpiReCZ/BattlEyeFiltersTool");
        }
    }
}
