
namespace Lab2_v9_Shumeiko
{
    public static class FileLoader
    {
        public static async Task<string?> SelectFileAsync()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                return result?.FullPath;
            }
            catch (Exception ex)
            {
                // Обробка винятків
                Console.WriteLine($"Error selecting file: {ex.Message}");
                return null;
            }
        }
    }
}
