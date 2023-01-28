using System.Reflection;
using System.Text.RegularExpressions;

namespace PowerStationInfrastructure.helpers;

public static class ResourceHelper
{
    public static string? ReadResourceFile(string filename)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        using var stream = thisAssembly.GetManifestResourceStream(filename);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        return null;
    }

    public static Stream GetEmbeddedResourceStream(string relativeResourcePath)
    {
        if (string.IsNullOrEmpty(relativeResourcePath))
            throw new ArgumentNullException("relativeResourcePath");

        var resourcePath = String.Format("{0}.{1}",
            Regex.Replace(Assembly.GetExecutingAssembly().ManifestModule.Name, @"\.(exe|dll)$",
                string.Empty, RegexOptions.IgnoreCase), relativeResourcePath);

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        if (stream == null)
            throw new ArgumentException(String.Format("The specified embedded resource \"{0}\" is not found.", relativeResourcePath));
        return stream;
    }
}