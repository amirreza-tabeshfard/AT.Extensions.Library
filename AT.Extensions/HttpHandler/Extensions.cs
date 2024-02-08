namespace AT.Extensions.HttpHandler;
public static class Extensions
{
    public async static Task<T> WebApiCallGetAsync<T>(string fullUrl)
        where T : class, new()
    {
        T result = new T();

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(fullUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage responseMessage = await client.GetAsync(fullUrl))
            {
                if (responseMessage.IsSuccessStatusCode)
                    result = responseMessage.Content.ReadAsAsync<T>().Result;
            }
        }

        return result;
    }

    public async static Task<T> WebApiCallPostAsync<T, U>(string fullUrl, U model)
        where T : class, new()
    {
        using HttpClientHandler clientHandler = new HttpClientHandler();
        using HttpClient client = new HttpClient(clientHandler);
        using StringContent stringContent = new StringContent(content: Newtonsoft.Json.JsonConvert.SerializeObject(model),
                                                              encoding: System.Text.Encoding.UTF8,
                                                              mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: "application/json").MediaType);
        using HttpResponseMessage responseMessage = await client.PostAsync(requestUri: fullUrl, content: stringContent);
        responseMessage.EnsureSuccessStatusCode();
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async static Task<T> WebApiCallPutAsync<T>(string fullUrl)
        where T : class, new()
    {
        T result = new T();

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(fullUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(client)))
            using (HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent))
                if (responseMessage.IsSuccessStatusCode)
                    result = responseMessage.Content.ReadAsAsync<T>().Result;
        }

        return result;
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(string fullUrl, U model)
        where T : class, new()
    {
        T result = new T();

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(fullUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter()))
            {
                if (responseMessage.IsSuccessStatusCode)
                    result = responseMessage.Content.ReadAsAsync<T>().Result;
            }
        }

        return result;
    }

    public async static Task<T> WebApiCallDeleteAsync<T>(string fullUrl)
        where T : class, new()
    {
        T result = new T();

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(fullUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage responseMessage = await client.DeleteAsync(fullUrl))
            {
                if (responseMessage.IsSuccessStatusCode)
                    result = responseMessage.Content.ReadAsAsync<T>().Result;
            }
        }

        return result;
    }
}