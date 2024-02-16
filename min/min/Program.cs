namespace min;

using NUglify;

internal static class Program
{
    public static int Main(string[] args)
    {
        var dir = args.FirstOrDefault();
        if (dir is null || !Directory.Exists(dir))
        {
            Console.WriteLine("A valid directory must be specified.");
            return 1;
        }

        var htmlFiles = Directory.GetFiles(dir, "*.html", SearchOption.AllDirectories);
        Console.WriteLine($"Found {htmlFiles.Length} HTML files.");

        var cssFiles = Directory.GetFiles(dir, "*.css", SearchOption.AllDirectories);
        Console.WriteLine($"Found {cssFiles.Length} CSS files.");
        if (cssFiles.Length + htmlFiles.Length == 0)
        {
            Console.WriteLine("No files found, aborting.");
            return 1;
        }

        foreach (var cssFile in cssFiles)
        {
            var content = File.ReadAllText(cssFile);
            try
            {
                var uglifiedContent = Uglify.Css(content);
                File.WriteAllText(cssFile, uglifiedContent.Code);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to minify {cssFile}:");
                Console.WriteLine(ex);
                continue;
            }

            Console.WriteLine($"Minified {cssFile}.");
            Console.WriteLine();
        }

        foreach (var htmlFile in htmlFiles)
        {
            var content = File.ReadAllText(htmlFile);
            try
            {
                var uglifiedContent = Uglify.Html(content);
                File.WriteAllText(htmlFile, uglifiedContent.Code);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to minify {htmlFile}:");
                Console.WriteLine(ex);
                continue;
            }

            Console.WriteLine($"Minified {htmlFile}.");
            Console.WriteLine();
        }

        return 0;
    }
}
