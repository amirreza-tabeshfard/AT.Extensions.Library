namespace AT.Extensions.HttpHandler.Collections.Generic;
public static class WebApiCallDeleteAsyncExtensions : Object
{
    public async static Task<T> WebApiCallDeleteAsync<T>(String fullUrl)
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