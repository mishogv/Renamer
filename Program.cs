namespace ReNamer
{
    using System;
    using System.IO;
    using System.Text;

    public class Program
    {
        public static void Main()
        {
            var oldName = string.Empty;

            while (string.IsNullOrEmpty(oldName))
            {
                Console.Write($"Text you want to rename:");
                oldName = Console.ReadLine();
            }

            var newName = string.Empty;

            while (string.IsNullOrEmpty(newName))
            {
                Console.Write($"Text you want to rename with:");
                newName = Console.ReadLine();
            }

            Console.WriteLine("Renaming directories....");
            RenameDirectories(Environment.CurrentDirectory, oldName, newName);
            Console.WriteLine("Directories renamed.");

            Console.WriteLine($"Renaming files....");
            RenameFiles(Environment.CurrentDirectory, oldName, newName);
            Console.WriteLine("Files renamed.");

            Console.WriteLine($"Renaming files contents....");
            RenameFilesContents(Environment.CurrentDirectory, oldName, newName);
            Console.WriteLine("Files contents renamed.");

        }

        private static void RenameFilesContents(string currentDirectory, string oldName, string newName)
        {

            var files = Directory.GetFiles(currentDirectory);
            foreach (var file in files)
            {
                if (!file.EndsWith(".dll") && !file.EndsWith(".pdb") && !file.EndsWith(".exe") && !file.EndsWith(".suo"))
                {
                    var contents = File.ReadAllText(file);
                    contents = contents.Replace(oldName, newName);
                    File.WriteAllText(file, contents, Encoding.UTF8);
                }
            }

            var subDirectories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in subDirectories)
            {
                RenameFilesContents(directory, oldName, newName);
            }
        }

        private static void RenameDirectories(string currentDirectory, string oldName, string newName)
        {
            var directories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in directories)
            {
                var newDirectoryName = directory.Replace(oldName, newName);
                if (newDirectoryName != directory)
                {
                    Directory.Move(directory, newDirectoryName);
                }
            }

            directories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in directories)
            {
                RenameDirectories(directory, oldName, newName);
            }
        }

        private static void RenameFiles(string currentDirectory, string oldName, string newName)
        {
            var files = Directory.GetFiles(currentDirectory);

            foreach (var file in files)
            {
                var newFileName = file.Replace(oldName, newName);

                if (newFileName != file)
                {
                    File.Move(file, newFileName);
                }
            }

            files = Directory.GetDirectories(currentDirectory);
            foreach (var file in files)
            {
                RenameFiles(file, oldName, newName);
            }
        }
    }
}