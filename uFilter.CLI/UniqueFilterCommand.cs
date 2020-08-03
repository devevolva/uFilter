using System;
using System.Collections.Generic;
using System.IO;

namespace uFilter.CLI
{
    public class UniqueFilterCommand
    {
        public List<FileObject> FilterForUniqueFiles(string pathSource, string pathTarget, bool isVerbose)
        {
            // Build hierarchical directory objects ///////////////////////////
            DirectoryObject source = new DirectoryObject(pathSource);
            Console.WriteLine("Created source directory object: {0}", source.Path);
            DirectoryObject target = new DirectoryObject(pathTarget);
            Console.WriteLine("Created target directory object: {0}", target.Path);


            // Build Source and Target file object lists //////////////////////
            List<FileObject> sourceFileList = source.ToFileList();
            Console.WriteLine("Source file object list created: {0} files", sourceFileList.Count);
            List<FileObject> targetFileList = target.ToFileList();
            Console.WriteLine("Target file object list created: {0} files", targetFileList.Count);


            // Build Unique source list, filter that by target list into a unique list.
            List<FileObject> uniqueSourceFileList = new List<FileObject>();
            List<FileObject> uniqueDestinationList = new List<FileObject>();

            // Pass empty list uniqueDestinationList to compare sourceFileList to itself
            uniqueSourceFileList.AddRange(ToUniqueFileObjectList(sourceFileList, uniqueDestinationList));
            Console.WriteLine("Unique source file list created: {0} files", uniqueSourceFileList.Count);
            uniqueDestinationList.AddRange(ToUniqueFileObjectList(uniqueSourceFileList, targetFileList));
            Console.WriteLine("Unique destination file list created: {0} files", uniqueDestinationList.Count);

            return uniqueDestinationList;
        }

        public List<FileObject> ToUniqueFileObjectList(List<FileObject> sourceFileList, List<FileObject> targetFileList)
        {
            List<FileObject> uniqueFileObjectList = new List<FileObject>();
            bool isUniqueToTargetList = true;
            bool isUniqueToDestinationList = true;

            foreach (FileObject sourceFile in sourceFileList)
            {
                // Check file is already in destination list, if so don't check target.
                isUniqueToDestinationList = true;
                foreach (FileObject destinationFile in uniqueFileObjectList)
                {
                    if (destinationFile.CompareHashString(sourceFile))
                    {
                        isUniqueToDestinationList = false;
                        break;
                    }
                }

                if (isUniqueToDestinationList)
                {
                    // Check file is in taget list, if so dont copy to unique list.
                    isUniqueToTargetList = true;
                    foreach (FileObject targetFile in targetFileList)
                    {
                        if (sourceFile.CompareHashString(targetFile))
                        {
                            isUniqueToTargetList = false;
                            Console.WriteLine("NOT UNIQUE: {0} matches {1}", sourceFile, targetFile);
                            break;
                        }
                    }
                    if (isUniqueToTargetList)
                    {
                        uniqueFileObjectList.Add(sourceFile);
                    }

                }
            }

            return uniqueFileObjectList;
        }

        public void MaterializeList(List<FileObject> fileObjectList, string pathDestination)
        {
            // Create destination dir if not exists ///////////////////////////
            if (!Directory.Exists(pathDestination))
            {
                try
                {
                    Directory.CreateDirectory(pathDestination);
                    Console.WriteLine("Created destination path: {0}", pathDestination);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Create a copy of the unique files in destination directory /////
            Console.WriteLine("Type 'yes' or 'y' to materialize files at {0}?", pathDestination);
            string input = Console.ReadLine();

            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                // Copy files to destination directory renamed with last write time.
                foreach (FileObject fo in fileObjectList)
                {
                    string newFilename = Path.Combine(pathDestination, fo.LastWriteTime.ToString("yyyyMMdd HH.mm.ss") + " " + Path.GetFileName(fo.Path));

                    File.Copy(fo.Path, newFilename, true);
                    Console.WriteLine("COPIED: {0}    TO: {1}", fo.Path, newFilename);
                }
            }
        }

        public void FileObjectListToConsole(List<FileObject> foList, bool isVerbose)
        {
            foreach (FileObject fo in foList)
            {
                fo.ToConsole(isVerbose);
            }
        }
    }
}
