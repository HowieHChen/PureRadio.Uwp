using PureRadio.Uwp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Providers.Interfaces
{
    /// <summary>
    /// 用于进行网络请求.
    /// </summary>
    public interface IHttpProvider
    {
        /// <summary>
        /// 内部的超时时长设置，默认为100秒.
        /// </summary>
        TimeSpan OverallTimeout { get; set; }

        /// <summary>
        /// 网络客户端.
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// 获取 <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="url">请求地址.</param>
        /// <param name="method">请求方法.</param>
        /// <param name="parameters">查询参数.</param>
        /// <param name="requestType">请求类型.</param>
        /// <param name="needToken">是否需要令牌.</param>
        /// <param name="needSign">是否需要签名验证.</param>
        /// <returns><see cref="HttpRequestMessage"/>.</returns>
        Task<HttpRequestMessage> GetRequestMessageAsync(string url, HttpMethod method, Dictionary<string, string> parameters = null, RequestType requestType = RequestType.Default, bool needToken = false, bool needSign = false);

        /// <summary>
        /// 发送请求.
        /// </summary>
        /// <param name="request">需要发送的 <see cref="HttpRequestMessage"/>.</param>
        /// <returns>返回的 <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        /// <summary>
        /// 发送请求.
        /// </summary>
        /// <param name="request">需要发送的 <see cref="HttpRequestMessage"/>.</param>
        /// <param name="cancellationToken">请求的 <see cref="CancellationToken"/>.</param>
        /// <returns>返回的 <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

        /// <summary>
        /// 解析响应.
        /// </summary>
        /// <param name="response">得到的 <see cref="HttpResponseMessage"/>.</param>
        /// <typeparam name="T">需要转换的目标类型.</typeparam>
        /// <returns>转换结果.</returns>
        Task<T> ParseAsync<T>(HttpResponseMessage response);

        /// <summary>
        /// 解析响应.
        /// </summary>
        /// <param name="response">得到的 <see cref="string"/>.</param>
        /// <typeparam name="T">需要转换的目标类型.</typeparam>
        /// <returns>转换结果.</returns>
        T ParseAsync<T>(string response);
    }
}
