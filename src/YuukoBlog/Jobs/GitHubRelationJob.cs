using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Pomelo.AspNetCore.TimedJob;

namespace YuukoBlog.Jobs
{
    public class GitHubRelationJob : Job
    {
        public GitHubRelationJob(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; set; }

        /// <summary>
        /// 获取GitHub粉丝信息
        /// </summary>
        [Invoke(Interval = 1000 * 60 * 60 * 24)]
        public async void PullGitHubRelation()
        {

        }

        private async Task<long> GetFollowersCount()
        {
            var url = $"https://github.com/{ Config["BlogRoll:GitHub"] }";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var result = await client.GetAsync("/followers");
                var html = await result.Content.ReadAsStringAsync();
                var regex = new Regex(@"(?<=<span class=""counter"">)[\s\r\n0-9]{0,}");
                return Convert.ToInt64(regex.Match(html).Value.Trim());
            }
        }

        private async Task<IList<string>> GetFollowers()
        {
            var ret = new List<string>();
            var count = await GetFollowersCount();
            var pages = (count + 50) / 51;
            var url = $"https://github.com/{ Config["BlogRoll:GitHub"] }";
            for (var i = 0; i < pages; i ++)
            {
                var endpoint = "/followers?page=" + i;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(endpoint);
                    var html = await result.Content.ReadAsStringAsync();
                    var regex = new Regex(@"(?<=<span class=""css-truncate css-truncate-target"" title="").*(?=</a></span></h3>)");
                    var matches = regex.Matches(html);
                    foreach(Match x in matches)
                    {
                        var regex2 = new Regex(@"(?<=<a href=""/).*(?="">)");
                        ret.Add(regex2.Match(x.Value).Value);
                    }
                }
            }
            return ret;
        }
    }
}
