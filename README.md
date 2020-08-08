## Solution.uFilter: Visual Studio 2019

This project was created to clean up a couple of decades of digital camera images and videos. 
The files were sometimes duplicates with different names or different files with the same name.

#### Primary Functionality:
This utility can take a SOURCE directory, compare it to a TARGET directory and copy all files to
a DESTINATION directory that are unique in SOURCE and TARGET. These unique files are first listed 
and can then be copied if the user chooses.

Copied files are renamed by prepending the Last Write
date and time to the original filename and placed at the root of DESTINATION - none of the folder 
structure from SOURCE is recreated. Files in the SOURCE and TARGET are not changed.

Uniqueness is determined via MD5 hash.

#### Secondary Functionality Includes:

Compare the hash of one file to another, returning 'true' if the first is unique 'false' if not.

List file hashes for a single file or for all the files in a directory and its subdirectories.

List unique files in a directory and its subdirectories.



### uFilter.CLI: .NET Core 3.1
  Command line utility to filter for unique files.

  [Documentation](https://github.com/devevolva/uFilter/tree/master/uFilter.CLI)