using System.Reflection;

namespace Erp.Data;

public class LocaleDataFetcher
{
    public const string LOCALES_FOLDER = "Locales";

    public static async Task<List<LanguageTranslation>> GetLocalesJsonContentAsStringAsync()
    {
        var languageTranslations = new List<LanguageTranslation>();
        var location = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty,
            LOCALES_FOLDER);
        if (string.IsNullOrEmpty(location)) return languageTranslations;
        var languageFolders = GetSubfolders(location);
        foreach (var folder in languageFolders)
        {
            var languageTranslation = new LanguageTranslation
            {
                LanguageCode = Path.GetFileName(folder),
            };

            var translations = new List<Translation>();
            var files = GetFilesInFolder(folder);
            foreach (var file in files)
            {
                using var reader = new StreamReader($"{file}");
                string json = await reader.ReadToEndAsync();
                translations.Add(new Translation
                {
                    Json = json,
                    FileName = Path.GetFileNameWithoutExtension(file)
                });
            }

            languageTranslation.Translations = translations;
            languageTranslations.Add(languageTranslation);
        }

        return languageTranslations;
    }

    private static IEnumerable<string> GetSubfolders(string folderPath)
    {
        return Directory.GetDirectories(folderPath);
    }

    private static List<string> GetFilesInFolder(string folderPath)
    {
        return Directory.GetFiles(folderPath).ToList();
    }
}