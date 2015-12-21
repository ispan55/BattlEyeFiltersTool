/*
	BattlEye Filters Tool
	https://github.com/SpiReCZ/BattlEyeFiltersTool
	© by SpiRe 2015

	This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
	To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattlEyeFiltersTool
{
    /// <summary>
    /// Interaction logic for InfoFlyout.xaml
    /// </summary>
    public partial class InfoFlyout : Page
    {
        public InfoFlyout()
        {
            InitializeComponent();
        }
        // Links
        private void exilemod_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://exilemod.com");
        }
        private void exilethread_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.exilemod.com/topic/7038-battleye-filters-tool/");
        }
        private void bithread_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://forums.bistudio.com/");
        }
    }
}
