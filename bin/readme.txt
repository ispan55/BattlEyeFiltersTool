BattlEye Filters Tool

https://github.com/SpiReCZ/BattlEyeFiltersTool
© by SpiRe 2015

This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/.

version: 0.8.1.0

I won't provide support for the people that uses the tool in any other way that its meant to be used.

---

Support threads: http://www.exilemod.com/topic/7038-battleye-filters-tool/

If you find this application useful then please consider making a donation via Paypal as a thank you: 
https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WKE8JUXGUGGXA

---

This BattlEye Filters Tool was created to help server owners update BE filters easily.

In order to use this tool you need to add your basic filters set ( Exile or Infistar ) to the "BattlEye_BasicSet" folder and your additional filters ( for your MODs/Scripts ) to "BattlEye_AddSet" folder.

When you hit "Load Files" button the program will automatically load both sets to memory and merge them. To save your files hit "Save Files" button. 
You can also use the "Flush Files" button to delete merged files from memory.

The files are usually saved in the default "BattlEye" folder. Otherwise it saves to the same folder that the application is in.

Things you need to know about making the additional BE filters: 
1: This program compares BE index ( example: "7 execVM") from AddSet in BasicSet so you need to make the additional set (AddSet) 
in the same format as BasicSet (they don't have to be on matching line numbers). When it finds a match it will add the additional filters to the BasicSet line. 
If the BE index is not found ( example: "3 execVM" ) it will add it to the end of the file as new line. 

2: The program compares the match of filename without extension from BasicSet to AddSet.
eg: BattlEye_BasicSet\Scripts.txt & BattlEye_AddSet\ScriptsBlaExampleBlaBla.txt

3: There is an option in the program to use only "knownBeFiles.txt". The program contains the default set of BE filters files names in memory. When the checkBox is checked it creates the files using the default files names if the file does not already exist. If the file exists it loads the new files names to the whitelist in program memory.

4: The BE filters files must be .txt

For further infomation visit the support thread. I won't provide any support if you don't use the program as instructed.

5: Don't merge Exile BE filters with Infistar's BE filters. Use one or another as BasicSet. Which means don't merge filters that are already in BasicSet.

6: Tool works fine if you use it right.

---

Future implementations:
- saving selected filepath of the selected folder
- catching possible exceptions
- more features

---

changelog:

v0.8.1.0
- recompiled required DLLs
- some minor fixes
- code structural changes

v0.8.0.2
- added nulling for removed objects & modified clear of list
- rewrite of code to move it from MainWindow logic

v0.8.0.1
- fixed lines multiplication bug

v0.8
- initial public test Release
- requires .NET 4.6
- there are examples of usage inside folders