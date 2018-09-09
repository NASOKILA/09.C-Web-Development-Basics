namespace SliceFile
{
    using System;
    using System.IO;
    using System.Drawing;
    using static System.Net.Mime.MediaTypeNames;

    public class Program
    {
        static void Main(string[] args)
        {
        
        }

        static void Slice(string sourceFile, string destinationPath) {

            //we slice the file
            


            //check if directory exists if not create one
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            //We use FileStream to read the file and create a FileInfo object to store the data in it.
            using (FileStream source = new FileStream(sourceFile, FileMode.Open)) //we open the file
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                //we prosess each new piece and we give it a name
                long partLength = (source.Length / parts) + 1;
            }

        }


    }
}
