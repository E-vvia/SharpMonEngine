#!/usr/bin/env dotnet
if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: Symlink <source> <destination>");
    return 1;
}

string source = Path.GetFullPath(args[0]);
string destination = Path.GetFullPath(args[1]);

if (!File.Exists(source))
{
    Console.Error.WriteLine($"Source does not exist: {source}");
    return 1;
}

Directory.CreateDirectory(Path.GetDirectoryName(destination)!);

if (File.Exists(destination))
{
    File.Delete(destination);
}

File.CreateSymbolicLink(destination, source);

Console.WriteLine($"Linked: {destination} -> {source}");

return 0;