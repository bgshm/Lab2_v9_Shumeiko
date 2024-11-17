
namespace Lab2_v9_Shumeiko
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            _searchContext = new XMLProcessor(new LINQAnalyzer()); // Стратегія за замовчуванням
        }

        #region Глобальні змінні

        private XMLProcessor _searchContext;
        private string? SelectedFilePath { get; set; }
        private List<(string Attribute, string Keyword)> GetSearchCriteria()
        {
            var criteria = new List<(string, string)>();

            // Перебираємо чекбокси та відповідні їм записи, щоб зібрати вибрані атрибути та ключові слова
            void AddCriteria(CheckBox checkBox, Entry entry, string attributeName)
            {
                if (checkBox.IsChecked && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    criteria.Add((attributeName, entry.Text));
                }
            }

            AddCriteria(NameCheckBox, NameEntry, "name");
            AddCriteria(TypeCheckBox, TypeEntry, "type");
            AddCriteria(VersionCheckBox, VersionEntry, "version");
            AddCriteria(LicenseCheckBox, LicenseEntry, "license");
            AddCriteria(UsageCheckBox, UsageEntry, "usage");
            AddCriteria(PlatformCheckBox, PlatformEntry, "platform");

            return criteria;
        }

        #endregion

        #region Метод запиту користувача на збереження місця розташування

        private async Task<string?> PickSaveFileAsync()
        {
            try
            {
                // Визначаємо вибір типу файлу
                var htmlFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] { ".html" } }
        });

                var savePickerOptions = new PickOptions
                {
                    PickerTitle = "Select a location to save the HTML file",
                    FileTypes = htmlFileType
                };

                var result = await FilePicker.Default.PickAsync(savePickerOptions);

                // Перевіряємо, чи користувач вибрав шлях до файлу
                return result?.FullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file selection: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Лістнери кнопок

        private async void OnSelectFileClicked(object sender, EventArgs e)
        {
            string? filePath = await FileLoader.SelectFileAsync();
            if (!string.IsNullOrEmpty(filePath))
            {
                await DisplayAlert("File Selected", $"File: {filePath}", "OK");
                // Зберігаєм шлях до файлу для пошуку
                SelectedFilePath = filePath;
            }
            else
            {
                await DisplayAlert("Error", "No file selected.", "OK");
            }
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedFilePath))
            {
                DisplayAlert("Error", "Please select a file before searching.", "OK");
                return;
            }

            var criteria = GetSearchCriteria();
            if (!criteria.Any())
            {
                DisplayAlert("Error", "Please select at least one attribute and enter a keyword.", "OK");
                return;
            }

            // Визначаємо тип парсеру на основі вибраного перемикача
            if (SAXRadioButton.IsChecked)
                _searchContext.SetStrategy(new SAXAnalyzer());
            else if (DOMRadioButton.IsChecked)
                _searchContext.SetStrategy(new DOMAnalyzer());
            else if (LINQRadioButton.IsChecked)
                _searchContext.SetStrategy(new LINQAnalyzer());

            // Виконуємо пошук за списком критеріїв
            var results = _searchContext.ExecuteSearch(SelectedFilePath, criteria);

            // Виводимо результати у ListView
            ResultsList.ItemsSource = results.Any() ? results : new List<string> { "No results found." };
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            // Очищаємо ListView
            ResultsList.ItemsSource = null;

            // Знімаємо усі прапорці
            NameCheckBox.IsChecked = false;
            TypeCheckBox.IsChecked = false;
            VersionCheckBox.IsChecked = false;
            LicenseCheckBox.IsChecked = false;
            UsageCheckBox.IsChecked = false;
            PlatformCheckBox.IsChecked = false;

            // Очищаємо всі текстові поля (записи)
            NameEntry.Text = string.Empty;
            TypeEntry.Text = string.Empty;
            VersionEntry.Text = string.Empty;
            LicenseEntry.Text = string.Empty;
            UsageEntry.Text = string.Empty;
            PlatformEntry.Text = string.Empty;
        }

        private async void OnTransformButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedFilePath))
            {
                await DisplayAlert("Error", "Please select an XML file before transforming.", "OK");
                return;
            }

            // Попросимо користувача вибрати місце збереження HTML-файлу
            string? outputFilePath = await PickSaveFileAsync();
            if (outputFilePath == null)
            {
                await DisplayAlert("Error", "No output file selected.", "OK");
                return;
            }

            // Вказуємо шлях до XSL-файлу
            string xslFilePath = "D:\\Bogdan\\Шева\\ООП\\Lab2_v9_Shumeiko\\transform.xsl";

            try
            {
                // Виконуємо перетворення
                HTMLTransformer.Transform(SelectedFilePath, xslFilePath, outputFilePath);
                await DisplayAlert("Success", $"HTML file saved to: {outputFilePath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to transform XML to HTML: {ex.Message}", "OK");
            }
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Exit", "Are you sure you want to exit?", "Yes", "No"))
            {
                // Вихід з програми
                Application.Current?.Quit();
            }
        }

        #endregion
    }
}