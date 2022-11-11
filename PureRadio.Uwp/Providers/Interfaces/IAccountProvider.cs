using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.User;
using PureRadio.Uwp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Providers.Interfaces
{
    public interface IAccountProvider
    {
        /// <summary>
        /// 当授权状态改变时发生.
        /// </summary>
        event EventHandler<AuthorizeStateChangedEventArgs> StateChanged;

        /// <summary>
        /// 当前的授权状态.
        /// </summary>
        AuthorizeState State { get; }

        /// <summary>
        /// 当前的账号信息.
        /// </summary>
        AccountInfo AccountInfo { get; }

        /// <summary>
        /// 获取包含授权码的查询字符串.
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求所需的查询参数.</param>
        /// <param name="requestType">请求类型.</param>
        /// <param name="needToken">是否需要令牌.</param>
        /// <returns>包含授权验证的查询字符串.</returns>
        Task<string> GenerateAuthorizedQueryStringAsync(string url, Dictionary<string, string> parameters, RequestType requestType = RequestType.Default, bool needToken = false);

        /// <summary>
        /// 获取包含授权码的查询字典.
        /// </summary>
        /// /// <param name="url">请求地址</param>
        /// <param name="parameters">请求所需的查询参数.</param>
        /// <param name="requestType">请求类型.</param>
        /// <param name="needToken">是否需要访问令牌.</param>
        /// <returns>包含授权验证码的查询字典.</returns>
        Task<Dictionary<string, string>> GenerateAuthorizedQueryDictionaryAsync(string url, Dictionary<string, string> parameters, RequestType requestType = RequestType.Default, bool needToken = false);

        /// <summary>
        /// 获取当前登录用户的访问令牌.
        /// </summary>
        /// <returns>账户授权的令牌.</returns>
        Task<string> GetTokenAsync();

        /// <summary>
        /// 用户登录.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task<bool> TrySignInAsync();

        /// <summary>
        /// 用户登录.
        /// </summary>
        /// <param name="phone">登陆手机号.</param>
        /// <param name="password">登陆密码.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task<bool> TrySignInAsync(string phone, string password);

        /// <summary>
        /// 用户退出.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task SignOutAsync();

        /// <summary>
        /// 当前的访问令牌是否有效.
        /// </summary>
        /// <returns>有效为<c>true</c>，无效为<c>false</c>.</returns>
        bool IsTokenValidAsync();
    }
}
