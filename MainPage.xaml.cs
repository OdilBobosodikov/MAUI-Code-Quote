namespace CodeQuote;

public partial class MainPage : ContentPage
{
    private List<string> Quates = new List<string>();

    private Random random;
    public MainPage()
	{
        random = new Random();
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMauiAsset();
    }

    async Task LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
        using var reader = new StreamReader(stream);

        while (reader.Peek() != -1)
        {
            Quates.Add(reader.ReadLine());
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var startColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        var endColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

        var colors = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 6);

        float stopOffset = .0f;

        var stops = new GradientStopCollection();

        foreach (var color in colors)
        {
            stops.Add(new GradientStop(Color.FromArgb(color.Name), stopOffset));
            stopOffset += 0.2f;
        }

        var gradient = new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 1));

        background.Background = gradient;

        int index = random.Next(Quates.Count);
        quote.Text = Quates[index];
    }
}

