using System;

namespace DotnetPlayground.Common.ConsoleUI;

public static class ConsoleShell
{
    private const string ProjectTitle = "PLAYGROUND";

    public static void PrintHeader(string description)
    {
        Console.Clear();

        int width = Console.WindowWidth;
        string separator = new string('=', width > 0 ? width - 1 : 80);

        Console.WriteLine(separator);
        Console.WriteLine();

        PrintCentered(ProjectTitle.ToUpperInvariant());
        Console.WriteLine();

        PrintWrappedCentered(description, width);
        Console.WriteLine();

        Console.WriteLine(separator);
        Console.WriteLine();
    }

    private static void PrintCentered(string text)
    {
        int width = Console.WindowWidth;
        int leftPadding = Math.Max((width - text.Length) / 2, 0);

        Console.WriteLine(new string(' ', leftPadding) + text);
    }

    private static void PrintWrappedCentered(string text, int width)
    {
        if (width <= 0)
            width = 80;

        int maxLineLength = width - 4;
        var words = text.Split(' ');
        string currentLine = string.Empty;

        foreach (var word in words)
        {
            if ((currentLine + word).Length > maxLineLength)
            {
                PrintCentered(currentLine.Trim());
                currentLine = string.Empty;
            }

            currentLine += word + " ";
        }

        if (!string.IsNullOrWhiteSpace(currentLine))
            PrintCentered(currentLine.Trim());
    }
}