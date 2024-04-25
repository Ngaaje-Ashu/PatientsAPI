namespace PatientAPIClient;

public abstract class ApiClient
{
    
    private static readonly HttpClient _httpClient = new();

    public ApiClient()
    {
        _httpClient.BaseAddress= new Uri("https://localhost:7167/api/");

        DefaultAddress = _httpClient.BaseAddress;

    }

    public HttpClient Client
    {
        get
        {
            return _httpClient;
        }
    }

    public Uri DefaultAddress { get; private set; }
}