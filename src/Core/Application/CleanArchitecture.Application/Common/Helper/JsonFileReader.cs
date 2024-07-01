using Jil;

namespace CleanArchitecture.Application.Common.Helper;

public static class JsonFileReader
{
    public static async Task<T> ReadJsonFileAsync<T>(this string filePath, CancellationToken cancellationToken)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified JSON file was not found.", filePath);

        using var reader = new StreamReader(filePath);
        var json = await reader.ReadToEndAsync(cancellationToken);
        return JSON.Deserialize<T>(json);
    }
}