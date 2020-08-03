using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace uFilter.CLI
{
    public class FileSystemObject
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public char Type { get; set; }

        public FileSystemObject(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileName(path);
            Type = 'o';
        }

        public virtual void ToConsole(bool verbose) { }

        public virtual string ToStringVerbose() { return ""; }
    }

    public class DirectoryObject : FileSystemObject
    {
        public int FileCount { get; set; }
        public int DirCount { get; set; }
        public int Count { get => FileCount + DirCount; }
        public List<FileObject> FOList { get; set; }
        public List<DirectoryObject> DOList { get; set; }



        public DirectoryObject(string path) : base(path)
        {
            this.Type = 'd';
            this.FOList = new List<FileObject>();
            this.DOList = new List<DirectoryObject>();

            try
            {
                string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
                this.FileCount = files.Length;
                foreach (string file in files)
                {
                    FOList.Add(new FileObject(file));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
                this.DirCount = dirs.Length;
                foreach (string dir in dirs)
                {
                    DOList.Add(new DirectoryObject(dir));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        public override string ToString()
        {
            string fo = "";
            fo += String.Format("DIR: {0}", Path);
            return fo;
        }

        public override string ToStringVerbose()
        {
            string fo = "";
            fo += String.Format("DIR: {0} SUBDIRCOUNT {1} FILECOUNT: {2}", this.Path, this.DirCount, this.FileCount);
            return fo;
        }

        public override void ToConsole(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine(this.ToStringVerbose());
                foreach (FileObject fo in this.FOList)
                    fo.ToConsole(verbose);

                foreach (DirectoryObject subDir in this.DOList)
                {
                    subDir.ToConsole(verbose);
                }
            }
            else
            {
                Console.WriteLine(this.ToString());
                foreach (FileObject fo in this.FOList)
                    fo.ToConsole(verbose);

                foreach (DirectoryObject subDir in this.DOList)
                {
                    subDir.ToConsole(verbose);
                }
            }
        }

        public List<FileObject> ToFileList()
        {
            List<FileObject> files = new List<FileObject>();

            foreach (FileObject fo in this.FOList)
                files.Add(fo);

            foreach (DirectoryObject subDir in this.DOList)
            {
                files.AddRange(subDir.ToFileList());
            }

            return files;
        }
    }

    public class FileObject : FileSystemObject
    {
        public string HashString { get; set; }
        public byte[] Hash { get; set; }

        public DateTime LastWriteTime { get; set; }

        public FileObject(string path) : base(path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    this.Hash = md5.ComputeHash(stream);
                    this.HashString = BitConverter.ToString(Hash).Replace("-", "").ToLowerInvariant();
                    this.Type = 'f';
                    this.LastWriteTime = Directory.GetLastWriteTime(path);
                }
            }
        }

        public bool CompareHashString(FileObject fo)
        {
            return this.HashString == fo.HashString;
        }

        public bool CompareHashBytes(FileObject fo)
        {
            for (int i = 0; i < fo.Hash.Length; i++)
            {
                if (this.Hash[i] != fo.Hash[i])
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            string fo = "";
            fo += String.Format("    FILE: {0}  HASH: {1}", Path, HashString);
            return fo;
        }

        public override string ToStringVerbose()
        {
            string fo = "";
            fo += String.Format("    FILE: {0}  HASH: {1} CREATION: {2} LASTWRITE: {3} LASTACCESS: {4}",
                Path, HashString, Directory.GetCreationTime(Path), Directory.GetLastWriteTime(Path), Directory.GetLastAccessTime(Path));
            return fo;
        }

        public override void ToConsole(bool verbose)
        {
            if (verbose)
                Console.WriteLine(this.ToStringVerbose());
            else
                Console.WriteLine(this.ToString());
        }
    }
}
