namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallPutAsyncExtensions : Object
{
    public async static Task<T> WebApiCallPutAsync<T>(String fullUrl)
        where T : class, new()
    {
        T result = new T();

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(fullUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));

            using (HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(client)))
            using (HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, httpContent))
                if (responseMessage.IsSuccessStatusCode)
                    result = responseMessage.Content.ReadAsAsync<T>().Result;
        }

        return result;
    }

    public async static Task<T> WebApiCallPutAsync<T, U>(String fullUrl, U model)
        where T : class, new()
    {
        T result = new T();

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(fullUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));

            using (HttpResponseMessage responseMessage = await client.PutAsync(fullUrl, model, new System.Net.Http.Formatting.JsonMediaTypeFormatter()))
            {
                if (responseMessage.IsSuccessStatusCode)
                    result = responseMessage.Content.ReadAsAsync<T>().Result;
            }
        }

        return result;
    }
}