/*
	BattlEye Filters Tool
	https://github.com/SpiReCZ/BattlEyeFiltersTool
	© by SpiRe 2015

	This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
	To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/.
*/
using System.Collections.Generic;

namespace BattlEyeFiltersTool
{
    // Expanded File Object which is usually BasicSet + BasicSet data + lately AddSet data
    public class FileObject : FileObjectBasic
    {
        public List<string> Lines { get; set; }
    }
    // Basic File Object which is usually AddSet
    public class FileObjectBasic
    {
        public string Name
        {
            get { return NameWithoutExt + Extension; }
        }
        public string NameWithoutExt { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
    }
}
