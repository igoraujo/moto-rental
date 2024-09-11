using System.Data;
using System.Reflection;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using MotoRental.Borders.Helpers;
using MotoRental.Repositories.Base.Queries;
using Npgsql;

namespace MotoRental.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly IConfiguration _config;
    protected readonly string _connectionString;

    public BaseRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config["ConnectionStrings:PostgreSQL"];
    }
    
    public async Task<IEnumerable<T>?> FindAll()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return await connection.GetAllAsync<T>();
        }
    }

    public async Task<T> FindById(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return await connection.GetAsync<T>(id);
        }
    }

    public async Task<int> Save(T obj)
    {
        string tableName = obj.GetType().Name.ToLower();
        var parameters = obj.ConvertToLowercaseParameters();
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
        var columns = string.Join(", ", properties.Select(p => p.Name.ToLower()));
        var values = string.Join(", ", properties.Select(p => $"@{p.Name.ToLower()}"));
        string query = string.Format(BaseQuery.Insert, tableName, columns, values);
        
        using (var connection = new NpgsqlConnection(_connectionString))
        { 
            return await connection.ExecuteAsync(query, parameters);
        }
    }
    
    public async Task<bool> Update(T obj)
    {
        const string keyColumn = "Id";
        string tableName = obj.GetType().Name.ToLower();
        
        var parameters = obj.ConvertToLowercaseParameters();
        var properties = typeof(T).GetProperties().Where(p => p.Name.ToLower() != keyColumn.ToLower());
        
        var setClause = string.Join(", ", properties.Select(p => $"{p.Name.ToLower()} = @{p.Name.ToLower()}"));
        var whereClause = $"{keyColumn.ToLower()} = @{keyColumn.ToLower()}";
        string query = string.Format(BaseQuery.Update, tableName, setClause, whereClause);

        await using (var connection = new NpgsqlConnection(_connectionString))
        {
            var afectedLines = await connection.ExecuteAsync(query, parameters);
            return afectedLines > 0;
        }
    }

    public async Task<bool> Delete(T obj)
    {
        int id = obj.GetIdValue();
        string tableName = obj.GetType().Name.ToLower();
        string query = string.Format(BaseQuery.Delete, tableName, id);
        
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var afectedLines = await connection.ExecuteAsync(query, id);
            return afectedLines > 0;
        }
    }

    // public async Task<IEnumerable<T>> FindAll(string endpoint)
    // {
    //     // _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
    //
    //     _uri = new Uri($"{_url}/{endpoint}");
    //
    //     _handler = new HttpClientHandler();
    //     _handler.CookieContainer = GetCookieContainer(_cookieValue);
    //
    //     IEnumerable<T> items = new List<T>();
    //
    //     using (var httpClient = new HttpClient(_handler))
    //     {
    //         var request = new HttpRequestMessage
    //         {
    //             RequestUri = _uri
    //         };
    //         request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
    //         request.Headers.Host = _uri.Host;
    //
    //         var response = await httpClient.SendAsync(request);
    //
    //         if (response.IsSuccessStatusCode)
    //         {
    //             items = await response.Content.ReadAsAsync<IEnumerable<T>>();
    //         }
    //
    //         return items;
    //     }
    //
    // }
    //
    // public async Task<T> FindById(int id, string endpoint)
    // {
    //     // _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
    //
    //     _uri = new Uri($"{_url}/{endpoint}/{id}");
    //
    //     _handler = new HttpClientHandler();
    //     _handler.CookieContainer = GetCookieContainer(_cookieValue);
    //
    //     using (var httpClient = new HttpClient(_handler))
    //     {
    //         var request = new HttpRequestMessage
    //         {
    //             RequestUri = _uri
    //         };
    //         request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
    //         request.Headers.Host = _uri.Host;
    //
    //         var response = await httpClient.SendAsync(request);
    //
    //         return await GetResponse(response, id);
    //     }
    //
    // }
    //
    // public async Task<T> Save(T input, string endpoint)
    // {
    //     var content = ReplaceContent(input);
    //
    //     _uri = new Uri($"{_url}/{endpoint}");
    //     _handler = new HttpClientHandler();
    //     // _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
    //     _handler.CookieContainer = GetCookieContainer(_cookieValue);
    //
    //     using (var httpClient = new HttpClient(_handler))
    //     {
    //         var json = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ContentType.JSON);
    //
    //         var request = new HttpRequestMessage
    //         {
    //             Method = HttpMethod.Post,
    //             RequestUri = _uri,
    //             Content = json
    //         };
    //
    //         request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
    //         request.Headers.Host = _uri.Host;
    //
    //         var response = await httpClient.SendAsync(request);
    //
    //         return await GetResponse(response, content);
    //     }
    // }
    //
    // public async Task<T> Update(T content, string endpoint)
    // {
    //     // _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
    //     _uri = new Uri($"{_url}/{endpoint}/{GetIdValue(content)}");
    //     _handler = new HttpClientHandler();
    //     _handler.UseCookies = false;
    //
    //     using (var httpClient = new HttpClient(_handler))
    //     {
    //         using (var request = new HttpRequestMessage(HttpMethod.Put, _uri))
    //         {
    //             // request.Headers.TryAddWithoutValidation("Authorization", "Basic c3Vwb3J0ZUBhaXNvZnR3YXJlLmNvbS5icjpAMjAyMQ==");
    //             // request.Headers.TryAddWithoutValidation(HeaderKey.COOKIE, $"{CookieName.JSESSIONID}={_cookieValue}");
    //
    //             var json = JsonConvert.SerializeObject(content);
    //
    //             request.Content = new StringContent(json, Encoding.UTF8, ContentType.JSON);
    //             request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType.JSON);
    //
    //             var response = await httpClient.SendAsync(request);
    //
    //             return await GetResponse(response, content);
    //         }
    //     }
    // }
    //
    // public async Task<HttpResponseMessage> Delete(int id, string endpoint)
    // {
    //     // _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
    //     _uri = new Uri($"{_url}/{endpoint}/{id}");
    //     _handler = new HttpClientHandler();
    //     _handler.UseCookies = false;
    //
    //     HttpResponseMessage response = new HttpResponseMessage();
    //
    //     using (var httpClient = new HttpClient(_handler))
    //     {
    //         using (var request = new HttpRequestMessage(HttpMethod.Delete, _uri))
    //         {
    //             // request.Headers.TryAddWithoutValidation(HeaderKey.COOKIE, $"{CookieName.JSESSIONID}={_cookieValue}");
    //
    //             response = await httpClient.SendAsync(request);
    //
    //             if (response.StatusCode != HttpStatusCode.NoContent)
    //             {
    //                 throw new Exception($"{response.ReasonPhrase} -> Error deleting item");
    //             }
    //         }
    //     }
    //
    //     return response;
    // }

    // private CookieContainer GetCookieContainer(string cookie)
    // {
    //     _cookie = new Cookie(CookieName.JSESSIONID, cookie, "/", _config.BaseDomain);
    //     var cookies = new CookieContainer();
    //     cookies.Add(_cookie);
    //     return cookies;
    // }

    ///TODO Remover metodo duplicado
    private T ReplaceContent(T content)
    {
        var type = content.GetType();

        foreach (var property in type.GetProperties())
        {
            if (property.PropertyType == typeof(string))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, "");
                }
            }
            if (property.PropertyType == typeof(object))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, new object());
                }
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, "");
                }
            }
            else if (property.PropertyType == typeof(bool))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, false);
                }
            }
            else if (property.PropertyType == typeof(int))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, 0);
                }
            }
            else if (property.PropertyType == typeof(decimal))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, 0);
                }
            }
            else if (property.PropertyType == typeof(long))
            {
                if (property.GetValue(content) is null)
                {
                    property.SetValue(content, 0);
                }
            }
        }

        return content;
    }
    
}
