# GetPath Utility
I needed a way to get the path that a Windows Shortcut *.lnk file
was pointing to.  I tried a few different solutions and each had
shortcomings, so I wrote this C# application.

It is a single file.  Just copy the GetPath.exe onto your path and run it.

It can be run as follows:

    getpath foo.lnk 
    getpath -l foo.lnk. bar.lnk

The exit code is zero if it worked or non-zero if it failed.
The number returned (when non zero) is the number of files
that did not exist