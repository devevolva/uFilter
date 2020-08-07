## uFilter: 
  Unique Filter for Files and Directories.

#### Usage:
 All directory(DIR) searches are recursive. Listed files always display their 
 hashes.

 The verbose keyword will cause additional information to be displayed when
 listing files and directories:
 
  For files LastWrite, LastAccess and Creation dates are present.
  
  For Directories file and subdirectory counts are included.


#### List All
  List info for a single file or directory.
  
    uf [verbose] FILE|DIR
    uf [v] FILE|DIR

  List all files for the given directory.
  
    uf list all [verbose] DIR
    uf la [v] DIR
    
#### Compare Unique
   Compare FILE_SOURCE to FILE_TARGET and return string 'true' if their hashes 
  match and 'false' if not.
  
    uf compare unique FILE_SOURCE FILE_TARGET
    uf cu FILE_SOURCE FILE_TARGET


#### List Unique
  Print unique list of files from DIR_SOURCE.

    uf list unique [verbose] DIR_SOURCE
    uf lu [v] DIR_SOURCE
    
  Create unique list of files from DIR_SOURCE and compare that to DIR_TARGET. 
  Finally, print the unique files from DIR_SOURCE not in DIR_TARGET.
  
    uf list unique [verbose] DIR_SOURCE DIR_TARGET
    uf lu [v] DIR_SOURCE DIR_TARGET

#### Materialize Unique
   Create unique list of files from DIR_SOURCE. Then compare unique list to 
  DIR_TARGET and print the unique files from DIR_SOURCE not in DIR_TARGET.
  Finally type 'yes' or 'y' to copy the final list to DIR_DESTINATION. Files
  will have their LastWrite times prepended to the original filename. This 
  process is nondestructive, original files and folders are left in place.
  Files copied to DIR_DESTINATION will all be at the root of the directory,
  no directory structure from DIR_SOURCE is recreated.
  
    uf materialize unique [verbose] DIR_SOURCE DIR_TARGET DIR_DESTINATION
    uf mu [v] DIR_SOURCE DIR_TARGET DIR_DESTINATION

#### Help
  Print this help information.
  
    uf help