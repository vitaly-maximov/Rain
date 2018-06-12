using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RainSimulationWpf.Rain
{
    class MapReader
    {
	    public static double[] Read(string filename)
	    {
		    if (!File.Exists(filename))
		    {
			    throw new FileNotFoundException();
		    }

		    string[] lines = File.ReadAllLines(filename);
		    if (lines.Length < 1)
		    {
			    throw new FormatException("Test should contain at least one line.");
		    }

		    return ReadLine(lines[0]);
	    }

	    private static double[] ReadLine(string line)
	    {
		    return line
			    .Split(' ')
			    .Where(number => !string.IsNullOrEmpty(number))
			    .Select(number => double.Parse(number, CultureInfo.InvariantCulture))
			    .ToArray();
	    }
	}
}
