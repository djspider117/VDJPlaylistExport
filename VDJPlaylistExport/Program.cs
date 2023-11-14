namespace VDJPlaylistExport;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Minimum 2 arguments. First argument should be the path to the .m3u file (should be exported from Virtual DJ) and the second is a FOLDER path to where you want to export the playlist.");
            return;
        }

        var m3uSrc = args[0];
        var destFolder = args[1];

        if (!File.Exists(m3uSrc))
        {
            Console.WriteLine($"Playlist file not found: {m3uSrc}");
            return;
        }

        if (!Directory.Exists(destFolder))
        {
            Console.WriteLine($"Destination folder not found {destFolder}");
            return;
        }

        var lines = await File.ReadAllLinesAsync(m3uSrc);

        var existingFiles = Directory.GetFiles(destFolder);
        if (existingFiles.Any())
        {
            Console.WriteLine("Destination folder already contains files. Do you want to clear the folder? (Y/N): ");
            var resp = Console.ReadLine();
            if (resp.ToLower() == "y")
            {
                Directory.Delete(destFolder, recursive: true);
                Directory.CreateDirectory(destFolder);
            }
        }

        Console.WriteLine("Starting copying files...");

        for (int i = 0; i < lines.Length; i+=2)
        {
            var ext = lines[i];
            var songPath = lines[i + 1];

            var dir = Path.GetDirectoryName(songPath);
            var extvdj = new ExtVDJ(ext.Replace("#EXTVDJ:", string.Empty));

            var newFileName = $"{(i / 2):D3}. {extvdj.GetSafeFilename()}{Path.GetExtension(songPath)}";
            var targetPath = Path.Combine(destFolder, newFileName);

            Console.WriteLine(extvdj);
            Console.WriteLine($"\t{songPath}");
            Console.WriteLine($"\t{targetPath}");
            Console.WriteLine();
            File.Copy(songPath, targetPath, true);
        }

    }
}