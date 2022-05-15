using Blazored.LocalStorage;

public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AuthorizationMessageHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        if (await _localStorage.ContainKeyAsync("access_token"))
        {
            var token = await _localStorage.GetItemAsStringAsync("access_token");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }
        Console.WriteLine("Authorization message handler called");
        return await base.SendAsync(request, cancellationToken);
    }
}
