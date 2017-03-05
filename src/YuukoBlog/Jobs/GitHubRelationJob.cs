using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Pomelo.AspNetCore.Extensions.BlobStorage.Models;
using Pomelo.AspNetCore.TimedJob;
using YuukoBlog.Models;

namespace YuukoBlog.Jobs
{
    public class GitHubRelationJob : Job
    {
        public GitHubRelationJob(IConfiguration config, BlogContext db)
        {
            Config = config;
            DB = db;
        }

        public IConfiguration Config { get; set; }

        public BlogContext DB { get; set; }

        /// <summary>
        /// 获取GitHub粉丝信息
        /// </summary>
        [Invoke(Interval = 1000 * 60 * 60 * 2, SkipWhileExecuting = true)]
        public async void PullGitHubRelation()
        {
            Console.WriteLine("Getting blog roll from github.com ...");

            try
            {
                var rolls = new List<string>();

                if (Convert.ToBoolean(Config["BlogRoll:Follower"]))
                {
                    var followers = await GetFollowers();
                    rolls.AddRange(followers);
                    foreach (var x in followers)
                        await UpdateUserInformation(x, BlogRollType.Follower);
                }

                if (Convert.ToBoolean(Config["BlogRoll:Following"]))
                {
                    var following = await GetFollowing();
                    rolls.AddRange(following);
                    foreach (var x in following)
                        await UpdateUserInformation(x, BlogRollType.Following);
                }

                rolls = rolls.Distinct().ToList();
                var delete = DB.BlogRolls.Where(x => !rolls.Contains(x.GitHubId)).ToList();
                foreach (var x in delete)
                    DB.BlogRolls.Remove(x);
                DB.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #region Pull Blog Rolls From GitHub

        private async Task<long> GetFollowersCount()
        {
            var url = "https://github.com";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var result = await client.GetAsync($"/{ Config["BlogRoll:GitHub"] }/followers");
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
            var url = "https://github.com";
            for (var i = 1; i <= pages; i++)
            {
                var endpoint = $"/{ Config["BlogRoll:GitHub"] }/followers?page=" + i;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(endpoint);
                    var html = await result.Content.ReadAsStringAsync();
                    var regex = new Regex(@"(?<=<span class=""css-truncate css-truncate-target"" title="").*(?=</a></span></h3>)");
                    var matches = regex.Matches(html);
                    foreach (Match x in matches)
                    {
                        var regex2 = new Regex(@"(?<=<a href=""/).*(?="">)");
                        ret.Add(regex2.Match(x.Value).Value);
                    }
                }
            }
            return ret;
        }

        private async Task<long> GetFollowingCount()
        {
            var url = "https://github.com";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var result = await client.GetAsync($"/{ Config["BlogRoll:GitHub"] }/following");
                var html = await result.Content.ReadAsStringAsync();
                var regex = new Regex(@"(?<=<span class=""counter"">)[\s\r\n0-9]{0,}");
                return Convert.ToInt64(regex.Match(html).Value.Trim());
            }
        }

        private async Task<IList<string>> GetFollowing()
        {
            var ret = new List<string>();
            var count = await GetFollowersCount();
            var pages = (count + 50) / 51;
            var url = "https://github.com";
            for (var i = 1; i <= pages; i++)
            {
                var endpoint = $"/{ Config["BlogRoll:GitHub"] }/following?page=" + i;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(endpoint);
                    var html = await result.Content.ReadAsStringAsync();
                    var regex = new Regex(@"(?<=<span class=""css-truncate css-truncate-target"" title="").*(?=</a></span></h3>)");
                    var matches = regex.Matches(html);
                    foreach (Match x in matches)
                    {
                        var regex2 = new Regex(@"(?<=<a href=""/).*(?="">)");
                        ret.Add(regex2.Match(x.Value).Value);
                    }
                }
            }
            return ret;
        }


        public async Task<Guid> UpdateAvatar(string URL, Guid? Id = null)
        {
            try
            {
                Blob avatar;
                if (Id.HasValue)
                    avatar = DB.Blobs.Single(x => x.Id == Id.Value);
                else
                {
                    avatar = new Blob();
                    avatar.Id = Guid.NewGuid();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    var result = await client.GetAsync("");
                    avatar.ContentType = result.Content.Headers.ContentType.ToString();
                    avatar.FileName = "avatar";
                    avatar.Time = DateTime.Now;
                    avatar.Bytes = await result.Content.ReadAsByteArrayAsync();
                    avatar.ContentLength = avatar.Bytes.LongCount();
                }
                if (!Id.HasValue)
                    DB.Blobs.Add(avatar);
                DB.SaveChanges();
                return avatar.Id;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return default(Guid);
            }
        }

        private async Task UpdateUserInformation(string Username, BlogRollType Type)
        {
            try
            {
                var needInsert = true;
                var br = DB.BlogRolls.SingleOrDefault(x => x.GitHubId == Username);
                if (br == null)
                    br = new BlogRoll();
                else
                {
                    needInsert = false;
                    if (br.Type == BlogRollType.Follower && Type == BlogRollType.Following)
                        return;
                }
                br.Type = Type;
                br.GitHubId = Username;
                var url = $"https://github.com/{ Config["BlogRoll:GitHub"] }";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync("/" + Username);
                    var fullnameRegex = new Regex(@"(?<=<div class=""vcard-fullname"" itemprop=""name"">).*(?=</div>)");
                    var html = await result.Content.ReadAsStringAsync();
                    br.NickName = fullnameRegex.Match(html).Value;
                    if (string.IsNullOrEmpty(br.NickName))
                        br.NickName = Username;
                    var websiteRegex = new Regex(@"(?<=<a href=""http).*(?="" class=""url"" rel="")");
                    var website = websiteRegex.Match(html).Value;
                    br.URL = string.IsNullOrEmpty(website) ? null : "http" + website;
                    var avatarRegex = new Regex(@"(?<=<meta content="").*(?="" property=""og:image"" /><meta content="")");
                    var avatarURL = avatarRegex.Match(html).Value.Replace("&amp;", "&").Replace("s=400", "s=150");
                    if (br.AvatarId == null)
                        br.AvatarId = await UpdateAvatar(avatarURL);
                    else
                        await UpdateAvatar(avatarURL, br.AvatarId.Value);
                }

                if (needInsert)
                    DB.BlogRolls.Add(br);
                DB.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
    }
}
