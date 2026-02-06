namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallPutAsyncExtensions
{
    #region Method<T>

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl)
where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(client));
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, TimeSpan timeout)
            where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(client));
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();

        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(client));
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, TimeSpan timeout, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(client));
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, Object payload)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, TimeSpan timeout, Object payload)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, Object payload, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));

        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, TimeSpan timeout, Object payload, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, Object payload, Dictionary<String, String> customHeaders)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, TimeSpan timeout, Object payload, Dictionary<String, String> customHeaders)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, Object payload, Dictionary<String, String> customHeaders, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(this String fullUrl, TimeSpan timeout, Object payload, Dictionary<String, String> customHeaders, CancellationToken cancellationToken)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(payload);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    #endregion

    #region Method<T, U>
    
    public async static Task<T> WebApiCallPutAsync<T, U>(this String fullUrl, U model)
        where T : class, new()
        where U : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(model);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(this String fullUrl, U model, TimeSpan timeout)
        where T : class, new()
        where U : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(model);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(this String fullUrl, U model, TimeSpan timeout, CancellationToken cancellationToken)
        where T : class, new()
        where U : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(model);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter(), cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(this String fullUrl, U model, Dictionary<String, String> customHeaders)
        where T : class, new()
        where U : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(customHeaders);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new();
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(this String fullUrl, U model, Dictionary<String, String> customHeaders, TimeSpan timeout)
        where T : class, new()
        where U : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(customHeaders);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(this String fullUrl, U model, Dictionary<String, String> customHeaders, TimeSpan timeout, CancellationToken cancellationToken)
        where T : class, new()
        where U : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(fullUrl);
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(customHeaders);
        // ----------------------------------------------------------------------------------------------------
        using HttpClient client = new()
        {
            Timeout = timeout
        };
        // ----------------------------------------------------------------------------------------------------
        client.BaseAddress = new Uri(fullUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<String, String> header in customHeaders)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        // ----------------------------------------------------------------------------------------------------
        using HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter(), cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        // ----------------------------------------------------------------------------------------------------
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    #endregion
}