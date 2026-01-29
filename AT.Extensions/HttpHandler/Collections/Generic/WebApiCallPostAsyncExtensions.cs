using System.Web;

namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallPostAsyncExtensions
{
    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: fullUrl, content: stringContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: fullUrl, content: stringContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, Dictionary<String, String> queryParams)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);
        // ----------------------------------------------------------------------------------------------------
        UriBuilder uriBuilder = new(fullUrl);
        System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;
        // ----------------------------------------------------------------------------------------------------
        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();
        // ----------------------------------------------------------------------------------------------------
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: finalUrl, content: stringContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, CancellationToken cancellationToken, Dictionary<String, String> queryParams)
            where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);
        // ----------------------------------------------------------------------------------------------------
        UriBuilder uriBuilder = new(fullUrl);
        System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;
        // ----------------------------------------------------------------------------------------------------
        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();
        // ----------------------------------------------------------------------------------------------------
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: finalUrl, content: stringContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, Dictionary<String, String> queryParams, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        UriBuilder uriBuilder = new(fullUrl);
        System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;
        // ----------------------------------------------------------------------------------------------------
        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();
        // ----------------------------------------------------------------------------------------------------
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: finalUrl, content: stringContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, CancellationToken cancellationToken, Dictionary<String, String> queryParams, TimeSpan timeout)
            where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        UriBuilder uriBuilder = new(fullUrl);
        System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;
        // ----------------------------------------------------------------------------------------------------
        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();
        // ----------------------------------------------------------------------------------------------------
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: finalUrl, content: stringContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: fullUrl, content: stringContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(this String fullUrl, U model, CancellationToken cancellationToken, TimeSpan timeout)
            where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };
        using StringContent stringContent = new(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                encoding: System.Text.Encoding.UTF8,
                                                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json).MediaType);
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: fullUrl, content: stringContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }
}