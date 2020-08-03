## uFilter: 
  Unique Filter for Files and Directories.

#### Usage:
  All directory(DIR) searches are recursive. Listed files always display their 
 hashes.

  The verbose keyword will cause additional information to be displayed when
  listing files and directories.
  For files LastWrite, LastAccess and Creation dates are present.
  For Directories files and subdirectory counts are included.

  List info for a single file or directory.
  
    uf [verbose] FILE|DIR
    uf [v] FILE|DIR

  Compare FILE_SOURCE to FILE_TARGET and return string 'true' if their hashes 
  match and 'false' if not.
  
    uf compare unique FILE_SOURCE FILE_TARGET
    uf cu FILE_SOURCE FILE_TARGET

  List all files for the given directory.
  
    uf list all [verbose] DIR
    uf la [v] DIR


  Create unique list of files from DIR_SOURCE. Then compare DIR_SOURCE to 
  DIR_TARGET and list the unique files from DIR_SOURCE not in DIR_TARGET.
  
    uf list unique [verbose] DIR_SOURCE DIR_TARGET
    uf lu [v] DIR_SOURCE DIR_TARGET

  Create unique list of files from DIR_SOURCE. Then compare DIR_SOURCE to 
  DIR_TARGET and list the unique files from DIR_SOURCE not in DIR_TARGET.
  Finally type 'yes' or 'y' to copy the final list to DIR_DESTINATION.
  
    uf materialize unique [verbose] DIR_SOURCE DIR_TARGET DIR_DESTINATION
    uf mu [v] DIR_SOURCE DIR_TARGET DIR_DESTINATION

  Print this help information.
  
    uf help