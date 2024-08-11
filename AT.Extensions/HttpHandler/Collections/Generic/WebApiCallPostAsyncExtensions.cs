namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallPostAsyncExtensions : Object
{
    public async static Task<T> WebApiCallPostAsync<T, U>(String fullUrl, U model)
    where T : class, new()
    {
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                              encoding: System.Text.Encoding.UTF8,
                                                              mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: fullUrl, content: stringContent);
        responseMessage.EnsureSuccessStatusCode();
        return await responseMessage.Content.ReadAsAsync<T>();
    }
}