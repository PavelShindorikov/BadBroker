using System.Web;

namespace Application.Common.Extensions;

public static class HttpClientExtensions
{
    /// <summary>
    /// HttpClient extensions for GetAsync method with query parameters
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="uri"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> GetWithQueryAsync(this HttpClient httpClient,
        string uri,
        Dictionary<string, string> queryParams)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var param in queryParams)
            query.Add(param.Key, param.Value);
        var url = query.ToString();
        return await httpClient.GetAsync(uri + url);
    }
}
