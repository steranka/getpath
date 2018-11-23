using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;
// using Shell32;
using IWshRuntimeLibrary;

namespace GetPath
{
    class Program
    {
        public static string GetShortcutTargetFile(string linkPathName)
        {
            string rtn = null;
            if (System.IO.File.Exists(linkPathName))
            {
                try
                {
                    WshShell shell = new WshShell(); //Create a new WshShell Interface
                    IWshShortcut link = (IWshShortcut)shell.CreateShortcut( linkPathName ); //Link the interface to our shortcut

                    rtn = link.TargetPath;
                }
                catch (Exception ex)
                {
                    // If an exception occurs the file is a shortcut
                }
            }
            return rtn;
        }

        static void usage(){
            string usage =
                "Usage: GetPath [-l] FILE [...]\n" +
                "Options:\n" +
                "  -l\n" +
                "    The -l (long) option causes the original filename and the\n" +
                "    target of the shortcut to be printed with => separating the names\n";

            Console.WriteLine( usage);
            System.Environment.Exit( 1 );        
        }

        static void Main(string[] args)
        {
            int exitCode = 0; // zero or the number of errors
            string path = null;
            int startIdx = 0;
            bool showOriginalFileAndTarget = false;

            if (args.Length == 0) {
                usage();
            }

            if (args[ 0 ] == "-l")
            {
                // Show original file and target
                showOriginalFileAndTarget = true;
                startIdx++; // skip over -l option
            }
            else if (args[0] == "-h")
            {
                usage();
            }

            for (int ii = startIdx; ii < args.Length; ii++)
            {
                path = args[ ii ];
                if (System.IO.File.Exists( path ))
                {
                    string targetFilename = GetShortcutTargetFile( path );
                    if (targetFilename != null)
                    {
                        if (showOriginalFileAndTarget)
                        {
                            Console.WriteLine( path + " => " + targetFilename );
                        }
                        else
                        {
                            Console.WriteLine( targetFilename );
                        }
                    }
                    else
                    {
                        Console.WriteLine( "File [" + path + "] is not a shortcut." );
                        exitCode++;
                    }
                    
                }
                else
                {
                    Console.WriteLine( "File [" + path + "] does not exist." );
                    exitCode++;
                }
            }
            System.Environment.Exit(exitCode);
        }
    }
}


