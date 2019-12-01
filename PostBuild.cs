using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        //Variables
        string TargetDir = args[0].Trim('"').TrimEnd('\\');
        string TargetFileName = args[1].Trim('"');
        //Copy Data dir
        if (File.Exists(TargetDir + ".zip"))
            File.Delete(TargetDir + ".zip");
        if (File.Exists(TargetDir + @"\package.zip"))
            File.Delete(TargetDir + @"\package.zip");
        if (Directory.Exists(TargetDir + @"\package"))
            Directory.Delete(TargetDir + @"\package", true);
        ZipFile.CreateFromDirectory(TargetDir, TargetDir + ".zip");
        Directory.CreateDirectory(TargetDir + @"\package\Data");
        ZipFile.ExtractToDirectory(TargetDir + ".zip", TargetDir + @"\package\Data");
        File.Delete(TargetDir + ".zip");
        //Remove useless Files
        Directory.GetFiles(TargetDir + @"\package\Data")
        .Where(s => new string[] { ".xml", ".pdb" }.Contains(Path.GetExtension(s)))
        .ToList().ForEach(s => File.Delete(s));
        //Add package scripts
        string programName = Path.GetFileNameWithoutExtension(TargetFileName);
        File.WriteAllText(TargetDir + @"\package\Install.bat",
            "@echo off\r\necho INSTALL\r\npowershell \"$s=(New-Object -COM WScript.Shell).CreateShortcut('%appdata%\\Microsoft\\Windows\\Start Menu\\Programs\\" + programName + ".lnk');$s.TargetPath='%cd%\\" + programName + ".exe';$s.Save()\"\r\ntimeout /t 1");
        File.WriteAllText(TargetDir + @"\package\Remove.bat",
            "@echo off\r\necho REMOVE\r\ndel \"%appdata%\\Microsoft\\Windows\\Start Menu\\Programs\\" + programName + ".lnk\"\r\ntaskkill /f /im \"" + programName + ".exe\"\r\ntimeout /t 1");
        //Package up result
        ZipFile.CreateFromDirectory(TargetDir + @"\package", TargetDir + @"\package.zip");
        Directory.Delete(TargetDir + @"\package", true);
    }
}
