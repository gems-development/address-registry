using System.Reflection;

namespace Gems.AddressRegistry.DataImportTool.Helpers;

internal static class InputFileNameHelper
{
	private static string _inputFilesDirectory;

	static InputFileNameHelper()
	{
		var assemblyPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!;

		_inputFilesDirectory = Path.Combine(assemblyPath, "Data");
	}

	public static string GetFilePath(string fileName)
	{
		var fileNames = Directory.GetFiles(_inputFilesDirectory);
		var matchingFileName = fileNames.FirstOrDefault(o => Path.GetFileName(o).Equals(fileName, StringComparison.OrdinalIgnoreCase))
			?? throw new FileNotFoundException($"В директории \"{_inputFilesDirectory}\" не найден файл \"{fileName}\"");

		return matchingFileName;
	}

	public static string GetFilePathByExtension(string extension)
	{
		var fileNames = Directory.GetFiles(_inputFilesDirectory);
		var matchingFileNames = fileNames.Where(o => Path.GetFileName(o).EndsWith(extension, StringComparison.OrdinalIgnoreCase)).ToArray();

		if (matchingFileNames.Length == 0)
			throw new FileNotFoundException($"В директории \"{_inputFilesDirectory}\" не найден файл с расширением \"{extension}\"");
		if (matchingFileNames.Length > 1)
			throw new InvalidOperationException($"В директории \"{_inputFilesDirectory}\" обнаружено более одного файла с расширением \"{extension}\"");

		return matchingFileNames.First();
	}

	public static string GetFilePathByNamePrefix(string prefix)
	{
		var fileNames = Directory.GetFiles(_inputFilesDirectory);
		var matchingFileNames = fileNames.Where(o => Path.GetFileName(o).StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToArray();

		if (matchingFileNames.Length == 0)
			throw new FileNotFoundException($"В директории \"{_inputFilesDirectory}\" не найден файл с приставкой \"{prefix}\"");
		if (matchingFileNames.Length > 1)
			throw new InvalidOperationException($"В директории \"{_inputFilesDirectory}\" обнаружено более одного файла с приставкой \"{prefix}\"");

		return matchingFileNames.First();
	}
}
