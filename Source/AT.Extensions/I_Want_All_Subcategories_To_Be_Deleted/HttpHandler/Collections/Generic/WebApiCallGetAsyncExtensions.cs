namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.HttpHandler.Collections.Generic;
public static class WebApiCallGetAsyncExtensions
{
    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: fullUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: fullUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, Dictionary<String, String> queryParams)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);

        UriBuilder uriBuilder = new UriBuilder(fullUrl);
        System.Collections.Specialized.NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;

        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();

        client.BaseAddress = new Uri(finalUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: finalUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, CancellationToken cancellationToken, Dictionary<String, String> queryParams)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler);

        UriBuilder uriBuilder = new UriBuilder(fullUrl);
        System.Collections.Specialized.NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;

        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();

        client.BaseAddress = new Uri(finalUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: finalUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, Dictionary<String, String> queryParams, TimeSpan timeout)
            where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };

        UriBuilder uriBuilder = new UriBuilder(fullUrl);
        System.Collections.Specialized.NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;

        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();

        client.BaseAddress = new Uri(finalUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: finalUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, CancellationToken cancellationToken, Dictionary<String, String> queryParams, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };

        UriBuilder uriBuilder = new UriBuilder(fullUrl);
        System.Collections.Specialized.NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;

        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();

        client.BaseAddress = new Uri(finalUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: finalUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: fullUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallGetAsync<T>(this String fullUrl, CancellationToken cancellationToken, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClientHandler clientHandler = new();
        using HttpClient client = new(clientHandler)
        {
            Timeout = timeout
        };

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpResponseMessage responseMessage = await client.GetAsync(requestUri: fullUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }
}