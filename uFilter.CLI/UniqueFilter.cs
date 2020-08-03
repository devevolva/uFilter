using System;
using System.IO;
using System.Collections.Generic;

namespace uFilter.CLI
{
    public class UniqueFilter
    {
        public bool isVerbose = false;
        public void Execute(string[] args)
        {
            // DEFAULTS ///////////////////////////////////////////////////////
            string pathSource = "";
            string pathTarget = "";
            string pathDestination = "";
            //string pathSource = "d:/tmp/pics";
            //string pathTarget = "d:/tmp/video";
            //string pathDestination = "d:/tmp/uFilterOutput";


            // CREATE LISTS FOR FILES, DIRS AND COMMAND KEYWORDS //////////////
            List<string> files = new List<string>();
            List<string> dirs = new List<string>();
            List<string> keywords = new List<string>{ "list", "all", "unique", "compare", "materialize", "verbose", "help",
                                                      "la", "lu", "cu", "mu", "v"};
            string commands = "";


            // PARSE ARGS /////////////////////////////////////////////////////
            foreach (string arg in args)
            {
                string possiblePath = arg.Replace('\\', '/');
                if (Directory.Exists(arg))
                {
                    possiblePath = RemoveTrailingSlashes(possiblePath);
                    dirs.Add(possiblePath);
                }
                else if (File.Exists(arg))
                {
                    files.Add(arg);
                }
                else if (keywords.Contains(arg))
                {
                    if (arg.ToLower() == "verbose" || arg.ToLower() == "v")
                        isVerbose = true;
                    else
                        commands += arg + " ";
                }
                else
                {
                    string error = arg + " is not a valid keyword, file or directory!";
                    //Console.WriteLine("{0} is not a valid keyword, file or directory!", arg);
                    throw new Exception(error);
                }
            }

            // COMMANDS ///////////////////////////////////////////////////////
            commands = commands.ToLower().Trim();
            
            // NO COMMANDS SINGLE FILE | DIR //////////////////////////////////
            if (commands == "" && files.Count == 1)
            {
                FileObject fo = new FileObject(files[0]);
                fo.ToConsole(isVerbose);
            }
            else if (commands == "" && dirs.Count == 1)
            {
                DirectoryObject dir = new DirectoryObject(dirs[0]);
                dir.ToConsole(isVerbose);
            }

            // HELP ///////////////////////////////////////////////////////////
            else if (commands == "help")
            {
                UniqueFilterHelp ufHelp = new UniqueFilterHelp();
                Console.WriteLine(ufHelp.ToString());
            }

            // LIST ALL FILES /////////////////////////////////////////////////
            else if (commands == "list all" || commands == "la")
            {
                if (dirs.Count == 1)
                {
                    Console.WriteLine("List all files...");
                    DirectoryObject dir = new DirectoryObject(dirs[0]);
                    dir.ToConsole(isVerbose);
                }
            }

            // LIST UNIQUE FILES //////////////////////////////////////////////
            else if (commands == "list unique" || commands == "lu")
            {
                if (dirs.Count == 2)
                {
                    Console.WriteLine("Unique File List... ");

                    pathSource = dirs[0];
                    pathTarget = dirs[1];
                    List<FileObject> uniqueDestinationList = new List<FileObject>();
                    UniqueFilterCommand ufCommand = new UniqueFilterCommand();

                    uniqueDestinationList = ufCommand.FilterForUniqueFiles(pathSource, pathTarget, isVerbose);
                    ufCommand.FileObjectListToConsole(uniqueDestinationList, isVerbose);
                }
            }

            // COMPARE FILE HASHES TO DETERMINE UNIQUENESS ////////////////////
            else if (commands == "compare unique" || commands == "cu")
            {
                if (files.Count == 2)
                {
                    FileObject source = new FileObject(files[0]);
                    FileObject target = new FileObject(files[1]);
                    Console.WriteLine(source.CompareHashBytes(target).ToString());
                }
            }

            // MATERIALIZE UNIQUE LIST ////////////////////////////////////////
            else if (commands == "materialize unique" || commands == "mu")
            {
                if (dirs.Count == 3)
                {
                    Console.WriteLine("Materialize unique files...");

                    pathSource = dirs[0];
                    pathTarget = dirs[1];
                    pathDestination = dirs[2];
                    UniqueFilterCommand ufCommand = new UniqueFilterCommand();
                    List<FileObject> uniqueDestinationList = new List<FileObject>();

                    uniqueDestinationList = ufCommand.FilterForUniqueFiles(pathSource, pathTarget, isVerbose);
                    ufCommand.FileObjectListToConsole(uniqueDestinationList, isVerbose);
                    ufCommand.MaterializeList(uniqueDestinationList, pathDestination);
                }
            }

            // ERRORS /////////////////////////////////////////////////////////
            else
            {
                Console.WriteLine("invalid arguments given!");
            }
        }
        
        public string RemoveTrailingSlashes(string path)
        {   
            // Depends on all \'s being replaced with /'s in PARSE ARGS section above.
            while (path.EndsWith('/'))
            {
                path = path.Remove(path.Length - 1);
            }
            return path;
        }
    }
}
