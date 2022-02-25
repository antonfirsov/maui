#nullable enable
using System.IO;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Essentials.Implementations;

namespace Microsoft.Maui
{
	public partial class FontRegistrar : IFontRegistrar
	{
		string? LoadNativeAppFont(string font, string filename, string? alias)
		{
			if (FileSystemImplementation.AppPackageFileExists(filename))
				return $"ms-appx:///{filename}";

			var packagePath = Path.Combine("Assets", filename);
			if (FileSystemImplementation.AppPackageFileExists(packagePath))
				return $"ms-appx:///Assets/{filename}";

			packagePath = Path.Combine("Fonts", filename);
			if (FileSystemImplementation.AppPackageFileExists(packagePath))
				return $"ms-appx:///Fonts/{filename}";

			packagePath = Path.Combine("Assets", "Fonts", filename);
			if (FileSystemImplementation.AppPackageFileExists(packagePath))
				return $"ms-appx:///Assets/Fonts/{filename}";

			// TODO: check other folders as well

			throw new FileNotFoundException($"Native font with the name {filename} was not found.");
		}
	}
}