namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallDeleteAsyncExtensions
{
    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: fullUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: fullUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: fullUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, CancellationToken cancellationToken, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: fullUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, Dictionary<String, String> queryParams)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();

        UriBuilder uriBuilder = new UriBuilder(fullUrl);
        System.Collections.Specialized.NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;

        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();

        client.BaseAddress = new Uri(finalUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: finalUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, CancellationToken cancellationToken, Dictionary<String, String> queryParams)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();

        UriBuilder uriBuilder = new UriBuilder(fullUrl);
        System.Collections.Specialized.NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (KeyValuePair<String, String> param in queryParams)
            query[param.Key] = param.Value;

        uriBuilder.Query = query.ToString();
        String finalUrl = uriBuilder.ToString();

        client.BaseAddress = new Uri(finalUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: finalUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, Dictionary<String, String> queryParams, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
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
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: finalUrl);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(this String fullUrl, CancellationToken cancellationToken, Dictionary<String, String> queryParams, TimeSpan timeout)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
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
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage responseMessage = await client.DeleteAsync(requestUri: finalUrl, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }
}