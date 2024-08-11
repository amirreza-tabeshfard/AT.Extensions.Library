namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallGetAsyncExtensions : Object
{
    public async static Task<T> WebApiCallGetAsync<T>(String fullUrl)
        where T : class, new()
    {
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: fullUrl);
        responseMessage.EnsureSuccessStatusCode();
        return await responseMessage.Content.ReadAsAsync<T>();
    }
}