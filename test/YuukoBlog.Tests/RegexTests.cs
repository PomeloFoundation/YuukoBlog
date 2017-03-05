using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xunit;

namespace YuukoBlog.Tests
{
    public class RegexTests
    {
        [Fact]
        public void get_follower_count_regex_test()
        {
            #region HTML Source
            var html = @"<nav class=""tabnav-tabs"">
    <a href=""/Kagamine/followers"" aria-selected=""true"" class=""js-selected-navigation-item selected tabnav-tab"" data-selected-links=""stargazers_main /Kagamine/followers"">
      All
      <span class=""counter"">
            41
      </span>
</a>  </nav>";
            #endregion
            var regex = new Regex(@"(?<=<span class=""counter"">)[\s\r\n0-9]{0,}");
            Assert.True(regex.Match(html).Success);
            Assert.Equal(41, Convert.ToInt32(regex.Match(html).Value.Trim()));
        }

        [Fact]
        public void get_followers_test()
        {
            #region HTML Source
            var html = @"
<!DOCTYPE html>
<html lang=""en"" class="" is-copy-enabled is-u2f-enabled"">
  <head prefix=""og: http://ogp.me/ns# fb: http://ogp.me/ns/fb# object: http://ogp.me/ns/object# article: http://ogp.me/ns/article# profile: http://ogp.me/ns/profile#"">
    <meta charset='utf-8'>

    <link crossorigin=""anonymous"" href=""https://assets-cdn.github.com/assets/frameworks-e346c09b4ba73c98f04e98ae47a4ba3caa3f583c5957702bb398f27c734f578c.css"" integrity=""sha256-40bAm0unPJjwTpiuR6S6PKo/WDxZV3Ars5jyfHNPV4w="" media=""all"" rel=""stylesheet"" />
    <link crossorigin=""anonymous"" href=""https://assets-cdn.github.com/assets/github-0a0e5e53fe034355fb794b272fb72c12896ef418224ce347150e372317e95109.css"" integrity=""sha256-Cg5eU/4DQ1X7eUsnL7csEolu9BgiTONHFQ43IxfpUQk="" media=""all"" rel=""stylesheet"" />
    
    
    
    
    

    <link as=""script"" href=""https://assets-cdn.github.com/assets/frameworks-061dc035c13b9d4be499e3ed27f75bc2e7f99d5d1d34ddd5cd7585ca8f870a6b.js"" rel=""preload"" />
    
    <link as=""script"" href=""https://assets-cdn.github.com/assets/github-e08e1eb1a0241f6202cb71c6552403da7569b28a2d0f86ef65b414f8da58c40c.js"" rel=""preload"" />

    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta http-equiv=""Content-Language"" content=""en"">
    <meta name=""viewport"" content=""width=device-width"">
    <meta content=""origin-when-cross-origin"" name=""referrer"" />
    <meta content=""noindex, follow"" name=""robots"" />
    <title>Your Followers</title>
    <link rel=""search"" type=""application/opensearchdescription+xml"" href=""/opensearch.xml"" title=""GitHub"">
    <link rel=""fluid-icon"" href=""https://github.com/fluidicon.png"" title=""GitHub"">
    <link rel=""apple-touch-icon"" href=""/apple-touch-icon.png"">
    <link rel=""apple-touch-icon"" sizes=""57x57"" href=""/apple-touch-icon-57x57.png"">
    <link rel=""apple-touch-icon"" sizes=""60x60"" href=""/apple-touch-icon-60x60.png"">
    <link rel=""apple-touch-icon"" sizes=""72x72"" href=""/apple-touch-icon-72x72.png"">
    <link rel=""apple-touch-icon"" sizes=""76x76"" href=""/apple-touch-icon-76x76.png"">
    <link rel=""apple-touch-icon"" sizes=""114x114"" href=""/apple-touch-icon-114x114.png"">
    <link rel=""apple-touch-icon"" sizes=""120x120"" href=""/apple-touch-icon-120x120.png"">
    <link rel=""apple-touch-icon"" sizes=""144x144"" href=""/apple-touch-icon-144x144.png"">
    <link rel=""apple-touch-icon"" sizes=""152x152"" href=""/apple-touch-icon-152x152.png"">
    <link rel=""apple-touch-icon"" sizes=""180x180"" href=""/apple-touch-icon-180x180.png"">
    <meta property=""fb:app_id"" content=""1401488693436528"">

      <meta property=""og:url"" content=""https://github.com"">
      <meta property=""og:site_name"" content=""GitHub"">
      <meta property=""og:title"" content=""Build software better, together"">
      <meta property=""og:description"" content=""GitHub is where people build software. More than 15 million people use GitHub to discover, fork, and contribute to over 38 million projects."">
      <meta property=""og:image"" content=""https://assets-cdn.github.com/images/modules/open_graph/github-logo.png"">
      <meta property=""og:image:type"" content=""image/png"">
      <meta property=""og:image:width"" content=""1200"">
      <meta property=""og:image:height"" content=""1200"">
      <meta property=""og:image"" content=""https://assets-cdn.github.com/images/modules/open_graph/github-mark.png"">
      <meta property=""og:image:type"" content=""image/png"">
      <meta property=""og:image:width"" content=""1200"">
      <meta property=""og:image:height"" content=""620"">
      <meta property=""og:image"" content=""https://assets-cdn.github.com/images/modules/open_graph/github-octocat.png"">
      <meta property=""og:image:type"" content=""image/png"">
      <meta property=""og:image:width"" content=""1200"">
      <meta property=""og:image:height"" content=""620"">
      <meta property=""twitter:site"" content=""github"">
      <meta property=""twitter:site:id"" content=""13334762"">
      <meta property=""twitter:creator"" content=""github"">
      <meta property=""twitter:creator:id"" content=""13334762"">
      <meta property=""twitter:card"" content=""summary_large_image"">
      <meta property=""twitter:title"" content=""GitHub"">
      <meta property=""twitter:description"" content=""GitHub is where people build software. More than 15 million people use GitHub to discover, fork, and contribute to over 38 million projects."">
      <meta property=""twitter:image:src"" content=""https://assets-cdn.github.com/images/modules/open_graph/github-logo.png"">
      <meta property=""twitter:image:width"" content=""1200"">
      <meta property=""twitter:image:height"" content=""1200"">
      <meta name=""browser-stats-url"" content=""https://api.github.com/_private/browser/stats"">
    <meta name=""browser-errors-url"" content=""https://api.github.com/_private/browser/errors"">
    <link rel=""assets"" href=""https://assets-cdn.github.com/"">
    <link rel=""web-socket"" href=""wss://live.github.com/_sockets/MjIxNjc1MDplMTdlN2E3MzU1NmJhZWRkMmVlMGFmZmViM2NmZGJmMDozNjk2NzIxZjBkMmU2ODAwNzdkYjNkNzFiZGJhMjVlY2M2MDU2ZWYwMTFlM2M3MTNhNGMxOWI0NzdjNjcyNjZi--f233bd73010c3eb1a402e80cf945443ea9309d70"">
    <meta name=""pjax-timeout"" content=""1000"">
    <link rel=""sudo-modal"" href=""/sessions/sudo_modal"">

    <meta name=""msapplication-TileImage"" content=""/windows-tile.png"">
    <meta name=""msapplication-TileColor"" content=""#ffffff"">
    <meta name=""selected-link"" value=""/Kagamine/followers"" data-pjax-transient>

    <meta name=""google-site-verification"" content=""KT5gs8h0wvaagLKAVWq8bbeNwnZZK1r1XQysX3xurLU"">
<meta name=""google-site-verification"" content=""ZzhVyEFwb7w3e0-uOTltm8Jsck2F5StVihD0exw2fsA"">
    <meta name=""google-analytics"" content=""UA-3769691-2"">

<meta content=""collector.githubapp.com"" name=""octolytics-host"" /><meta content=""github"" name=""octolytics-app-id"" /><meta content=""2A66F27D:3B1F:82F8EDC:57722D21"" name=""octolytics-dimension-request_id"" /><meta content=""2216750"" name=""octolytics-actor-id"" /><meta content=""Kagamine"" name=""octolytics-actor-login"" /><meta content=""7cf7723a458293d395f81a524520fab6d25403dfc044c84900c6a6d684c402b9"" name=""octolytics-actor-hash"" />
<meta content=""/&lt;user-name&gt;/followers"" data-pjax-transient=""true"" name=""analytics-location"" />



  <meta class=""js-ga-set"" name=""dimension1"" content=""Logged In"">



        <meta name=""hostname"" content=""github.com"">
    <meta name=""user-login"" content=""Kagamine"">

        <meta name=""expected-hostname"" content=""github.com"">
      <meta name=""js-proxy-site-detection-payload"" content=""ZmQxNzQzNTY0NzJiYmMwN2I1MWQ1MzA0Y2JlYWUwMDViZTVmZmI3YzUxNmNjNDgwOGQ1ZTc0Y2U0OWMxOTA2M3x7InJlbW90ZV9hZGRyZXNzIjoiNDIuMTAyLjI0Mi4xMjUiLCJyZXF1ZXN0X2lkIjoiMkE2NkYyN0Q6M0IxRjo4MkY4RURDOjU3NzIyRDIxIiwidGltZXN0YW1wIjoxNDY3MTAwNDU4fQ=="">


      <link rel=""mask-icon"" href=""https://assets-cdn.github.com/pinned-octocat.svg"" color=""#4078c0"">
      <link rel=""icon"" type=""image/x-icon"" href=""https://assets-cdn.github.com/favicon.ico"">

    <meta name=""html-safe-nonce"" content=""10a91f08dbdbe0bfc5a1c5e00ac164869614b386"">
    <meta content=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" name=""form-nonce"" />

    <meta http-equiv=""x-pjax-version"" content=""1d5938878076b778e5a8708e7cd0948a"">
    

        <meta content=""2216750"" name=""octolytics-dimension-user_id"" /><meta content=""Kagamine"" name=""octolytics-dimension-user_login"" />


  </head>


  <body class=""logged-in   env-production windows"">
    <div id=""js-pjax-loader-bar"" class=""pjax-loader-bar""></div>
    <a href=""#start-of-content"" tabindex=""1"" class=""accessibility-aid js-skip-to-content"">Skip to content</a>

    
    
    



        <div class=""header header-logged-in true"" role=""banner"">
  <div class=""container clearfix"">

    <a class=""header-logo-invertocat"" href=""https://github.com/"" data-hotkey=""g d"" aria-label=""Homepage"" data-ga-click=""Header, go to dashboard, icon:logo"">
  <svg aria-hidden=""true"" class=""octicon octicon-mark-github"" height=""28"" version=""1.1"" viewBox=""0 0 16 16"" width=""28""><path d=""M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0 0 16 8c0-4.42-3.58-8-8-8z""></path></svg>
</a>


        <div class=""header-search   js-site-search"" role=""search"">
  <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/search"" class=""js-site-search-form"" data-unscoped-search-url=""/search"" method=""get""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /></div>
    <label class=""form-control header-search-wrapper js-chromeless-input-container"">
      <div class=""header-search-scope""></div>
      <input type=""text""
        class=""form-control header-search-input js-site-search-focus ""
        data-hotkey=""s""
        name=""q""
        placeholder=""Search GitHub""
        aria-label=""Search GitHub""
        data-unscoped-placeholder=""Search GitHub""
        data-scoped-placeholder=""Search""
        tabindex=""1""
        autocapitalize=""off"">
    </label>
</form></div>


      <ul class=""header-nav left"" role=""navigation"">
        <li class=""header-nav-item"">
          <a href=""/pulls"" class=""js-selected-navigation-item header-nav-link"" data-ga-click=""Header, click, Nav menu - item:pulls context:user"" data-hotkey=""g p"" data-selected-links=""/pulls /pulls/assigned /pulls/mentioned /pulls"">
            Pull requests
</a>        </li>
        <li class=""header-nav-item"">
          <a href=""/issues"" class=""js-selected-navigation-item header-nav-link"" data-ga-click=""Header, click, Nav menu - item:issues context:user"" data-hotkey=""g i"" data-selected-links=""/issues /issues/assigned /issues/mentioned /issues"">
            Issues
</a>        </li>
          <li class=""header-nav-item"">
            <a class=""header-nav-link"" href=""https://gist.github.com/"" data-ga-click=""Header, go to gist, text:gist"">Gist</a>
          </li>
      </ul>

    
<ul class=""header-nav user-nav right"" id=""user-links"">
  <li class=""header-nav-item"">
    
    <a href=""/notifications"" aria-label=""You have no unread notifications"" class=""header-nav-link notification-indicator tooltipped tooltipped-s js-socket-channel js-notification-indicator"" data-channel=""tenant:1:notification-changed:2216750"" data-ga-click=""Header, go to notifications, icon:read"" data-hotkey=""g n"">
        <span class=""mail-status ""></span>
        <svg aria-hidden=""true"" class=""octicon octicon-bell"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M14 12v1H0v-1l.73-.58c.77-.77.81-2.55 1.19-4.42C2.69 3.23 6 2 6 2c0-.55.45-1 1-1s1 .45 1 1c0 0 3.39 1.23 4.16 5 .38 1.88.42 3.66 1.19 4.42l.66.58H14zm-7 4c1.11 0 2-.89 2-2H5c0 1.11.89 2 2 2z""></path></svg>
</a>
  </li>

  <li class=""header-nav-item dropdown js-menu-container"">
    <a class=""header-nav-link tooltipped tooltipped-s js-menu-target"" href=""/new""
       aria-label=""Create new…""
       data-ga-click=""Header, create new, icon:add"">
      <svg aria-hidden=""true"" class=""octicon octicon-plus left"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M12 9H7v5H5V9H0V7h5V2h2v5h5z""></path></svg>
      <span class=""dropdown-caret""></span>
    </a>

    <div class=""dropdown-menu-content js-menu-content"">
      <ul class=""dropdown-menu dropdown-menu-sw"">
        
<a class=""dropdown-item"" href=""/new"" data-ga-click=""Header, create new repository"">
  New repository
</a>

  <a class=""dropdown-item"" href=""/new/import"" data-ga-click=""Header, import a repository"">
    Import repository
  </a>


  <a class=""dropdown-item"" href=""/organizations/new"" data-ga-click=""Header, create new organization"">
    New organization
  </a>




      </ul>
    </div>
  </li>

  <li class=""header-nav-item dropdown js-menu-container"">
    <a class=""header-nav-link name tooltipped tooltipped-sw js-menu-target"" href=""/Kagamine""
       aria-label=""View profile and more""
       data-ga-click=""Header, show menu, icon:avatar"">
      <img alt=""@Kagamine"" class=""avatar"" height=""20"" src=""https://avatars3.githubusercontent.com/u/2216750?v=3&amp;s=40"" width=""20"" />
      <span class=""dropdown-caret""></span>
    </a>

    <div class=""dropdown-menu-content js-menu-content"">
      <div class=""dropdown-menu dropdown-menu-sw"">
        <div class=""dropdown-header header-nav-current-user css-truncate"">
          Signed in as <strong class=""css-truncate-target"">Kagamine</strong>
        </div>

        <div class=""dropdown-divider""></div>

        <a class=""dropdown-item"" href=""/Kagamine"" data-ga-click=""Header, go to profile, text:your profile"">
          Your profile
        </a>
        <a class=""dropdown-item"" href=""/stars"" data-ga-click=""Header, go to starred repos, text:your stars"">
          Your stars
        </a>
        <a class=""dropdown-item"" href=""/explore"" data-ga-click=""Header, go to explore, text:explore"">
          Explore
        </a>
          <a class=""dropdown-item"" href=""/integrations"" data-ga-click=""Header, go to integrations, text:integrations"">
            Integrations
          </a>
        <a class=""dropdown-item"" href=""https://help.github.com"" data-ga-click=""Header, go to help, text:help"">
          Help
        </a>


        <div class=""dropdown-divider""></div>

        <a class=""dropdown-item"" href=""/settings/profile"" data-ga-click=""Header, go to settings, icon:settings"">
          Settings
        </a>

        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/logout"" class=""logout-form"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""4WECeY2smbF68caZR8wmpbFRyflm2/Js9hi/ywpnxjApPw5/KkSpngNkv/MwippIoJsEZebtBrI/aLy8Xzw8mg=="" /></div>
          <button class=""dropdown-item dropdown-signout"" data-ga-click=""Header, sign out, icon:logout"">
            Sign out
          </button>
</form>      </div>
    </div>
  </li>
</ul>


    
  </div>
</div>


      


    <div id=""start-of-content"" class=""accessibility-aid""></div>

      <div id=""js-flash-container"">
</div>


    <div role=""main"" class=""main-content"">

      <div id=""js-pjax-container"" data-pjax-container>
        

<div class=""pagehead"">
  <div class=""container"">
    <ul class=""pagehead-actions"">
  <li>
    
  </li>
</ul>

<h1>
  <a href=""/Kagamine"">
    <img alt=""@Kagamine"" class=""avatar left"" height=""30"" src=""https://avatars1.githubusercontent.com/u/2216750?v=3&amp;s=60"" width=""30"" />
  </a>
  <a href=""/Kagamine"">Kagamine</a>
  <strong>(あまみや ゆうこ)</strong>
</h1>

  </div>
</div><!-- /.pagehead -->

<div class=""container"">
  <h2>Followers</h2>
  <div class=""tabnav"">
  <nav class=""tabnav-tabs"">
    <a href=""/Kagamine/followers"" aria-selected=""true"" class=""js-selected-navigation-item selected tabnav-tab"" data-selected-links=""stargazers_main /Kagamine/followers"">
      All
      <span class=""counter"">
        41
      </span>
</a>  </nav>
</div>

    <ol class=""follow-list clearfix"">
        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/alexinea""><img alt=""@alexinea"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/980129?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Alexinea""><a href=""/alexinea"">Alexinea</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from Shanghai, China"">Shanghai, China</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=alexinea"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""N9jvq+rOzGRE/oQ8tAx1KEoDXvoeEhnBkQb0eW5wP8zRfxT2PLQ7/ZC6Bz3qI0Bv7XLlrFkzMJdEDGvtsiNvcQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow alexinea"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=alexinea"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""0WDOUPQMMLaUNljFbY/PmTRlf4amsyHbmnawmfV5doE/sNUmzp372DaZq/POuNZ96SWMyzSEZ9/PwBiXXaJO0g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow alexinea"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/remirobert""><img alt=""@remirobert"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/3276768?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""rémi ""><a href=""/remirobert"">rémi </a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Student {EPITECH.}, iOS dev"">Student {EPITECH.}, iOS dev</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=remirobert"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""JnuWRkrAjxYC+KInNlYDQiUxWib0H8WFYHb1dus4Ol94tyAjfBQ4HYrzmRfcWNZE0Ec5YuEBJaXaABvOhhnn+Q=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow remirobert"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=remirobert"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""UOiZiRI94w07xmusuROTAAhytizNjJLPiFTR43g+tOszQAIgkiipHsI1fgw6/j7L4SwsKavVe2mDhyP5OKul6w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow remirobert"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/shisoft""><img alt=""@shisoft"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/3405885?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Jack Shi""><a href=""/shisoft"">Jack Shi</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Shisoft"">Shisoft</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=shisoft"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""OXLdwoWAEyquEEd1B7Kid/uXdTL6OUPCZZs6hfMfl/WQ/mUQaYHxAG7g1nr2wX1/7VEDCQMnyjDdH3Awug4g+w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow shisoft"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=shisoft"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""yEsXCKfk2B5Xveyb5YmqKdOyaVr8s4PTicd+J+weNpQpbb+VUXsr0kyPs2DSGMyhps0+KtX5MgDA2UmUhKqmVA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow shisoft"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/kiss96803""><img alt=""@kiss96803"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/3621793?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""RevlutionYe""><a href=""/kiss96803"">RevlutionYe</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from 成都"">成都</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=kiss96803"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""SOf75pUiyOmb4O3ZK4QjxhkYYtLA4/3Nis8oE09JQSwEh+nq46uGzYGu6S6jHN+NHqXU19PC4lj2NvIHAxE6+w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow kiss96803"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=kiss96803"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""T3hPSac1I0/HHQMWqL53gQZkMliTyY9fiHDk6BspozW+JpPBsDPO2YnwvwM2NrC+YBOaTpe4rl8b+Gc7ztiwqg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow kiss96803"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/kyriesnow""><img alt=""@kyriesnow"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/18325863?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""등소천""><a href=""/kyriesnow"">등소천</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for 大连华信"">大连华信</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=kyriesnow"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""b9jiCbiYVBJ+2+nItEBGf2JtMfbk941mClacJ8gScO4nno6vAUdxA64N/gDKwtFxl1N4OhBBYt2wc0O3YIM9/w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow kyriesnow"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=kyriesnow"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""5HMNBKsf9a7LkdRBiGe5vjKN+uLOEJnSGDOmdCvBYwZnoZKnlw+ltRzy5iRdB1wyzwQ9CB7EoP4s5Wy7+sdQ9g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow kyriesnow"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/TiriSane""><img alt=""@TiriSane"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/12566988?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""TiriSane""><a href=""/TiriSane"">TiriSane</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Shenyang Institute Of Engineering"">Shenyang Institute Of Engineering</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=TiriSane"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""ZM18ZtEOq3PzRQTgMRbW7c6q2AV204z2o41yoPJmIm76UNDostGB6t0UvpZ6ocO+eLMpxD9l7gs8Rl5A/WPUQQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow TiriSane"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=TiriSane"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""BEHbzROvBylwY3uWg7gheXnCVOEiUzTlGxPX1FuI4npMD890GMeFzmrIqA/jfniJVyZJHEvD315FxcN7gelVvQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow TiriSane"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/sintak""><img alt=""@sintak"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/15528016?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""sintak""><a href=""/sintak"">sintak</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Nov 1, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=sintak"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""FI4nmkl1CdiHK2ydD3ekUZuvJWyUw4/uOP6hZRhr5jt+MSrwWam5gkW1MzEmPteIIo1WR1dsVtoiIgi/5uLlVQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow sintak"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=sintak"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""+RbCKxlbu/+jNA3JxuQeUX2hasjLYPYviKkAmuH9RhzYJT8syjCqOF8wz5eiKSxhJnDb1wq4f6kpVWvxgiZEhQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow sintak"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/piratf""><img alt=""@piratf"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/9389616?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""潘""><a href=""/piratf"">潘</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for I&#39;m looking for…"">I&#39;m looking for…</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=piratf"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""bxUOlIveumbjqpQu63DLLQQBXN9t2Z03UIN2N/UemB8ubNHC5euffmdYbEDD3ccZAEd9PjWDC6SQhaTwqtaBPg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow piratf"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=piratf"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""3+OgPOCC59Xdugidn3QMkop2Bnh8wF6AvwLMadEaG7oB0lxwvE6PQiRZBuSx81DEpNDslX77S+PCHwFJ26+1JQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow piratf"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/autyan""><img alt=""@autyan"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/6050066?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Alex Zhang""><a href=""/autyan"">Alex Zhang</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for 上海卫东信息科技有限公司"">上海卫东信息科技有限公司</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=autyan"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""j/cFRwKYcyYiBbNxcTfcUs5JQ42FMgC++VJNxDvYfKwVNVs0ESc0Q7vRSX1h+FypNLwmdekZvgpji0TZGzJ8ow=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow autyan"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=autyan"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""AyR0qjK+GDTDPb2Eu23E+7m10LvETsEnDK6XHPlQ2gCon7MmJFNl/4/DaeQWY/FL/LP4nIpBdHUBBQ3JcHmtTg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow autyan"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/liguobao""><img alt=""@liguobao"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/10910802?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""codelover""><a href=""/liguobao"">codelover</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Feb 9, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=liguobao"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""EYDM6rlfopITPwzBhi4ngGznGeO4Ds5IGZogD9U4xSs53jMxjgDXtBqM5QwD2JUlZRuo8XSJSS+U09Rcb4+W7A=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow liguobao"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=liguobao"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""3O47AvVPGJf6thUsy3VuqOc/hL5Y0/pZEPwvEELWy70Y8jWOGBBUc7amV+IsHfE5F9aXonznksEDmjs+qP8dGA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow liguobao"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/lychees""><img alt=""@lychees"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/2507027?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""xiaodao""><a href=""/lychees"">xiaodao</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Home"">Home</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=lychees"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""INyBRBscludBtPrT652jrrCOnf/MJCXCC37FgIqbAoSlcxni6Tcsv/92DakxTyid243PkJrX0LMfWUefbgssLA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow lychees"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=lychees"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""wIfUmkcvaykfZHSOII2XnbTisMjubLbisBPTnbzTOUN6o/LhtzWjl3D38q8D3i9nSN4iEYVhr9Zo8vbbXGLzhg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow lychees"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/WPH95""><img alt=""@WPH95"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/2732352?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Penghan Wang""><a href=""/WPH95"">Penghan Wang</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Ninechapter"">Ninechapter</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=WPH95"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""RpcH0+Nd06DXbtNi7kYX2pKw9EDlsTxDQGw30abajltXXT+r5JLdwMVyCxKkWT0G4eIepKTM8+j8z2qOumRNlw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow WPH95"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=WPH95"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""4b6kObxTq1xP5uQihrdv9VQV5kXTyUNZOnVzcLVLlVjqn3t9h+RSyfnGXGBXr7/k6i1HiiZS6iQkki68ixCIoA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow WPH95"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/sakamoto-poteko""><img alt=""@sakamoto-poteko"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/4940216?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""坂本ポテコ""><a href=""/sakamoto-poteko"">坂本ポテコ</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from IN, United States"">IN, United States</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=sakamoto-poteko"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""OkHZ7OY/f1gyty2T1n8hve/SThABNzLT4prW3385unGEwCblMWCLEdH/NWD9m21ObjIAu5H0Rg30aTywOXoXcw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow sakamoto-poteko"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=sakamoto-poteko"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""rvjTDR4I8qwzPXB/3h3pxYTO9IBJbecyY7g2VbaOrYuh6/pIVL2ILMuXWw+GbrpjHz2U8q44jmOOheTrQEvsfw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow sakamoto-poteko"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/717009629""><img alt=""@717009629"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/16289643?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""师哥""><a href=""/717009629"">师哥</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from china"">china</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=717009629"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""WWbQhjYedtXy+VG1LqIiNHAr+lLKAhOzuF195+q59lrE1kwC9TsCaxM52wBu6hjrGuh3P1HTzVy72OdXKCnorA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow 717009629"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=717009629"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""hU3R3fRsh8lNHiR4sxKEy3ZaHRfxMVwHMMbGb/mYxbzIrap+jeAD3h4F8nlaTJ8hIwVjh1UdOKVcz/fBXkbi6w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow 717009629"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/dhswg20077""><img alt=""@dhswg20077"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/80653?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""dhswg20077""><a href=""/dhswg20077"">dhswg20077</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on May 4, 2009</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=dhswg20077"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""JHjWyr9K5hmNMwCbKffECpmQ4VAWGZlNR2SlrWXiG1K/Mft9APLFvSZF/UN9oipplwo8C56r7jYAH0dHQhqMYQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow dhswg20077"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=dhswg20077"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""XY1HW6agffJv6H7KIkWC9Y1AQn+/rWx+jCKL/Da8l6xSx9Pcrmb8dN3SL7UMyvGein/57pM3eN7DSx9/o3eScA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow dhswg20077"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/zhipeng-jia""><img alt=""@zhipeng-jia"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/6553274?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Zhipeng Jia""><a href=""/zhipeng-jia"">Zhipeng Jia</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Tsinghua Univerisity"">Tsinghua Univerisity</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=zhipeng-jia"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""KYYWBHgr0RlHCAvhgdxDtQNOwRMbWayRUOllBQnLZxidI/0e8oUMm3Oscp/vMc7GMyGJmYq4lkGJ1kv8HOUc3g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow zhipeng-jia"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=zhipeng-jia"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""C1yaK0Q3ihRQRbqNk+qWcLMp0l+RfsSKcvbMbMvBwGjKL122Uy1gPJPWgkW3dXxqZAIj5vlPb3avhyr5HY3ZUw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow zhipeng-jia"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/Nefarian""><img alt=""@Nefarian"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/17251593?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Nefarian""><a href=""/Nefarian"">Nefarian</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Feb 15, 2016</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=Nefarian"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""TU3G2b/zzFc5iyQglEPVa8g4E32+7jAFV6VJZtX9fNclcLRvIipWb+eKWKGjkMmdSz/nyJkSKdf5oqllhkk33w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow Nefarian"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=Nefarian"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""4Yw/SAJfWDqoCB5NgFZ0PmwCy9x6P8Nhn7X6xP7aTEEWXndZYD8v25QIwexzCrlV7UAXcMQRuLTx1XlrDqwKMw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow Nefarian"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/Earnit07""><img alt=""@Earnit07"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/16651015?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Earnit07""><a href=""/Earnit07"">Earnit07</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Jan 11, 2016</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=Earnit07"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""DO1yViAluUxFIxJR4HDpLdfF3+/GKHxadLNbG182cXgEv5aVo3Gb5+VTVH5/+1itlIayayDSxzYEvfLccdNpoQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow Earnit07"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=Earnit07"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""GeBfLEXYY6pGdzsaM93lDkAOyb0QnxhnlxzwCfAc8z44sfYiRa4W+xXCOO9X7dmdMBrum8qH8+uK2MGEUZlZCw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow Earnit07"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/evilgodsentme00""><img alt=""@evilgodsentme00"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/16533055?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""evilgodsentme00""><a href=""/evilgodsentme00"">evilgodsentme00</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Jan 4, 2016</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=evilgodsentme00"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""A5FxXuqlAW44srJX3YfmDy3SMtK8bTD5a/CqNw9fS32IYJydMATeEY80ADQHb3XUDnH8tLelOQ06EVt03SZAmg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow evilgodsentme00"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=evilgodsentme00"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""A4gf5g6zUlHqQtPnRDAmUUsGjtTX2l/96eBqBkOdQQI4gsVGG0yRDFsVLzstgd5llgXgvEnT811attPw2qS2hg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow evilgodsentme00"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/IceMao""><img alt=""@IceMao"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/15853414?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Ice1994""><a href=""/IceMao"">Ice1994</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Nov 15, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=IceMao"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""ZsZED+gtNKT2S6WWKmtZerQlLxBfU1LCNQdmQOCJmAG/sN6oVSEGK+vHyjIgb1vO1IQbgm1+gDVIde5ZmB97eg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow IceMao"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=IceMao"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""IR9MLqC6IVh8a+yhrmIZsbRqbBLOuBsCazYJMvo/Iy9hp4BezUkDXjZMhiRgpEnPsmRv5Diu+Hajt0e6qFW3VA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow IceMao"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/realityone""><img alt=""@realityone"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/4059040?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""realityone""><a href=""/realityone"">realityone</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Apr 4, 2013</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=realityone"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""/JOKKOYPhueQ8Dl98o0oUcXjdxePAIFSs9WO77IA6EmWWdxveO7BRg3WaqFT8NjCKmXMoXNLNjFJTXYZ8csALA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow realityone"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=realityone"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""/7Q2CkEpLZTQaW2xbUTk+TxGjtmYLRCT1FD5yEPie2HOqwPvhVMekbNATPsmaWwnTqMEmvOK1CUK4K6zz5rENg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow realityone"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/mrahhal""><img alt=""@mrahhal"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/4404199?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Mohammad Rahhal""><a href=""/mrahhal"">Mohammad Rahhal</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from Lebanon"">Lebanon</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=mrahhal"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""g3xkxDscSSQTaXBCNLdxt93EbpVI2+sIa3/nN+XLOAPWTM93eJfeD4BFZ8LSFTep0rWeWaMGSqNjBWq1HPgeeA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow mrahhal"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=mrahhal"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""QNEGDi7boIqJHXv2LOzBU9g9dlT9nyv0lmBYQfhobpEeXyqW/Y8y8EoE8VnRmpVLM9dGZM+8QI13cposNs6bHw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow mrahhal"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/xiaotian000""><img alt=""@xiaotian000"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/12424273?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""小TIAN.★☆""><a href=""/xiaotian000"">小TIAN.★☆</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on May 13, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=xiaotian000"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""lEhsPgYHIEQjhV9tWZK7keT0cnbbakjwzmypU7rObP6YaH125bBS42PcDKlWAo6gGlTHFVH14ZkT9HUGz6Xnkg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow xiaotian000"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=xiaotian000"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""uTjdUGmvVCsuHIspZevGUaTdgddnyYtGJ0QS6xqMjPVHFue5KkIAUCGlr9OdVvxfFqeQ72HI3O75joldgwf1JA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow xiaotian000"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/eastmelon""><img alt=""@eastmelon"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/15814101?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""eastmelon""><a href=""/eastmelon"">eastmelon</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Nov 12, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=eastmelon"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""tv3ibRY797OaTqLF7B9VZTDuou5d6GzqGkXmbE4kq1S1NKEsMIEWzaJxQwPlpqfSsMdL6QSlee/u0eiC2+VT0Q=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow eastmelon"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=eastmelon"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""j5ZsXdu4q3fFNmHEVRc5uDY2j6Y67M00XQLtMVsNX10ua/GvhxUq1nU379tqSjYf6TmWQ66HpAlqWP+C/EZaKQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow eastmelon"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/MaherJendoubi""><img alt=""@MaherJendoubi"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/1798510?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Maher Jendoubi""><a href=""/MaherJendoubi"">Maher Jendoubi</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from Paris"">Paris</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=MaherJendoubi"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""BocEWoLSu8WedV6P2U87xM9K3HD43f3qJSP0rVrkOkn+V7Qke4aqGhRyOPw/aR7dE85wv9+DsR2qyNiCnbEskg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow MaherJendoubi"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=MaherJendoubi"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""Mtrrt/HpGa8qxrp9E3M3Ipk00cuj8AvdaqgVBFd81Msgv28m1JihwM/r55ZE6KcJhW0as0LJgs6iE3QFPrTvZA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow MaherJendoubi"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/Jeffiy""><img alt=""@Jeffiy"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/5069316?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""zhangmm""><a href=""/Jeffiy"">zhangmm</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from Guangzhou, Guangdong, China"">Guangzhou, Guangdong, China</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=Jeffiy"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""XPxnMoCaDWPWfEgyNcgvGYcBQ5u0wWGlbaLfl5HtpgO6XrGSG33CdoeiILj8q+6m4eLaW6ZN2wZ21krJ741z/g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow Jeffiy"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=Jeffiy"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""UvYxDCv57VvgWs8hoUl1hhZenlSYOR6FbNUvVUURTbNuJ2nLYcHWbJpGUIP1A685oM75NHjr05Jfjhu/EV895Q=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow Jeffiy"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/chengxitian""><img alt=""@chengxitian"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/15648018?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""chengxitian""><a href=""/chengxitian"">chengxitian</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Nov 4, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=chengxitian"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""tjIEM0TBVCEs49rA5MO38vlcSeDB1TmqcPv6Kpg4S3Hl2yQ3vzljGFVjIFJZr/KajoRfkkm6iim87UipSYd5XA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow chengxitian"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=chengxitian"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""YxRQ8DiZKhdVpZtEtQ1aGQYs2lZtqcuOsY7LwEOzXJCoTZY/tf5SI1qSJJxu/ejilTSJbb1RcgwIq9j6dzDvEA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow chengxitian"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/jingruoruchu""><img alt=""@jingruoruchu"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/14814725?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""jingruoruchu""><a href=""/jingruoruchu"">jingruoruchu</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Sep 24, 2015</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=jingruoruchu"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""ut6vE0nX7S/RlVtPPWALWakn4Jck9pYy//Ho4D/QIox5r2H8G4iLLtIwGC4psnvI/fWs5Tv3hBhgtH4pcp8AxQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow jingruoruchu"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=jingruoruchu"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""Sjo+wir+6NIud698h77jlk5q/YMMrPDn9ErQfcJeUmDibIX0NLgNZ9II0fIZ7WWD66HDtqk2bW+BMY2EsPEQnw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow jingruoruchu"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/rpgmakervx""><img alt=""@rpgmakervx"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/8915334?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""code4j""><a href=""/rpgmakervx"">code4j</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for EasyArchitecture"">EasyArchitecture</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=rpgmakervx"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""qTA+euw5d6c15pG107Q+9pRxZeV0I2EtnG2uxGHMyXnnjtXURhNgFzBWtjXWZGdMFGEUqTABa8CBPiyK/epTpA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow rpgmakervx"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=rpgmakervx"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""t64fhGcLiQoknUceEOvcL+AakQ+zVBA1HjgTeBHbH+PCex8o+B/p7EWLqMZ98NdKCtwgA+rs7HFruTlbEOBk8w=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow rpgmakervx"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/twd2""><img alt=""@twd2"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/3244435?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Wende Tan""><a href=""/twd2"">Wende Tan</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Wandai"">Wandai</span></p>

        <span class=""user-following-container js-toggler-container js-social-container on"">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=twd2"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""IZWrMgYoQDdDMTbFjKAXNkMHB6Hqhtd0kj6Ih9qdlq6obydGeZtd/t0Qdq2K66o3CvYpJFH7HprIo8WRb9r//g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow twd2"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=twd2"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""P+JcxPY8SR746swyaAT3AdPiCoVmJmgGxktXukaofYa5lQDJPjPbwwRG5B40nZhvt6pbVCldP58tshiVONfuTQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow twd2"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/UFreedom""><img alt=""@UFreedom"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/5142436?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""UFreedom""><a href=""/UFreedom"">UFreedom</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Duomi Music"">Duomi Music</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=UFreedom"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""SJpZzfkEJ/lA0jBcf9yd6fQRtT47asAau2q281P9+UHJSEb6hAafGfuLAOn+bpggKScnYK8TLpUFvpxBFLotxw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow UFreedom"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=UFreedom"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""y9iA8tNOUIo1kKC1dPYedmVEJiQ5OlIuUHy0kbCImEJArqmJMy0jhEZ4jtqwC3ObxruryrEeBbF9fakZCzoV+g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow UFreedom"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/applenele""><img alt=""@applenele"" class=""avatar"" height=""75"" src=""https://avatars1.githubusercontent.com/u/5456023?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Lenny""><a href=""/applenele"">Lenny</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Sep 14, 2013</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=applenele"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""UbWFzPVhV7vRxw034dj3xtJC7d1/bf6bamV4Z6sa0GOvl1DyacPJRfMPbbqqgUu+XwPupF4/DUaMKGaN0EtMzg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow applenele"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=applenele"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""wkIFukHNw5721HYc74y63LLWKCOUUJT4Y+Kn9SFmmrwVKE2LdxD37Z/5BcbnEzNYyPK5gDqSdBR/ybbh4QmsNQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow applenele"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/Cream2015""><img alt=""@Cream2015"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/14037940?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Cream2016""><a href=""/Cream2015"">Cream2016</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for Free Job"">Free Job</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=Cream2015"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""/PIeJhQ6VpfyvzP1Thpo0D/gBIxTO/vgDhw+Cx063rUe0yrXvu/qoOhzfuJ8rD78NkFyr1+LMHWvWs+t24J/Rw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow Cream2015"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=Cream2015"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""GmCZSGY+vMxsWl6gC8nnmAiws/S1fAQ/Wt8b2kysY3ZOZPSO8v5+EKXtrstOmxAkT9V6sTwWCfFXDX3d1vngdA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow Cream2015"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/cygmris""><img alt=""@cygmris"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/10379818?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Chris""><a href=""/cygmris"">Chris</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-location"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Is from GuangZhou"">GuangZhou</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=cygmris"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""3RHoJd6FhyqEPRtRQlgIP6mq309bIpYoqrTOyHOO3Favuk8pnjUigBrWjsdamT63W065O3nN74R9b/5q0e+eaA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow cygmris"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=cygmris"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""1Q0yxb+n9ZrGdsOdhr7e4z/eroz+UU63hy9nwmt5xsmTXCHbPGgvVbtWp7T+UFQM4okqFTLUMzxKpPlNSlM4ZQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow cygmris"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/hzwer""><img alt=""@hzwer"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/10103856?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""hzwer""><a href=""/hzwer"">hzwer</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for AHSOFNU"">AHSOFNU</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=hzwer"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""q+Uui3jnA1LjWT5tIOcL4dJDfLGMoBGZef6fatd33s7e3BFIYCH73wzjn5W6sr5VI19FNFwXjhfUfPqBuA5vsQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow hzwer"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=hzwer"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""auI9vJHbPxYWpuQbRPcslUef0M2qSCaKp1pmeJAr9f9b6HgxHN613fDbeTJwaIKuf7iWU0XPDlLRuJJKNLzQ7g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow hzwer"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/Vincent-Long""><img alt=""@Vincent-Long"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/9472802?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Vincent-Long""><a href=""/Vincent-Long"">Vincent-Long</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Oct 31, 2014</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=Vincent-Long"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""zH34ynmG3KqJB1V8k3CwPiMpi2gy/sHX8REdJ5fxlexNzlX8v6ueFQMRHjvef8kDWE2sZH1ywBr8BaRCK+p+OQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow Vincent-Long"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=Vincent-Long"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""zEqMVs0Xa1HWJAHeIaNj8YgvnBzTrWSaaxEmxmMBIZ+NnaPUZm/+Ut+6fI4YCoD76NGa3x0zmMI5us2cxus10A=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow Vincent-Long"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/SkyZH""><img alt=""@SkyZH"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/4198311?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""迟遲的星星球""><a href=""/SkyZH"">迟遲的星星球</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for 张江第二职业专修学校"">张江第二职业专修学校</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=SkyZH"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""WMthXKaTutYMOGlVaqi1t/wNMautUTv2cjBfo0ITr1klUKhrU9Xb37Jhx9Iak4sINHgWD5r5zWABLqwWoGeNXQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow SkyZH"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=SkyZH"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""9Y/H+DTDg95/aEJyc8KD4GI8PIfXdkx1nA4FV+NmRaiIazPltLZ1nlGiaxWNKy9s1rogwE4A37PYxMhcXx5Sqw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow SkyZH"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/fordream""><img alt=""@fordream"" class=""avatar"" height=""75"" src=""https://avatars3.githubusercontent.com/u/3693121?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""xiaosunabc""><a href=""/fordream"">xiaosunabc</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for netease games"">netease games</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=fordream"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""tyJhRxMrI1e81XKI9xuUoWMjSOb9eCZxHv9WN3wC0NSQJtCkRi4QBkdV748N+LPT7IcZEczt0CVDy6jULd5A3Q=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow fordream"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=fordream"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""6qAtCysQv08/M2Ducp1xhV+HHSC9zqM2GNzXOMjrAQ9yQXiTF0h2g3r4LPLjWibigIklXjHjrLZgzTrgHowl0A=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow fordream"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/licongcq""><img alt=""@licongcq"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/7379271?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""licongcq""><a href=""/licongcq"">licongcq</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on Apr 23, 2014</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=licongcq"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""U/skXekbPfBxjPKyzo0hVRBC4JobOB7HZQJPoWSKvIG9hYzHYnMnPNImZ046JSur/GXh5cjomsjhjyRD7ccG3g=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow licongcq"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=licongcq"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""okKtjM4DSgrbxCUshrANjl9WdKWTiqE+pKdgOCZd58HaTPF7BjO+0z6NUwIOYAW7GbiAquNd7GRHJhJ7UVIsfQ=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow licongcq"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/bill125""><img alt=""@bill125"" class=""avatar"" height=""75"" src=""https://avatars2.githubusercontent.com/u/7195344?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""bill125""><a href=""/bill125"">bill125</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-clock"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M8 8h3v2H7c-.55 0-1-.45-1-1V4h2v4zM7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7z""></path></svg> Joined on May 7, 2014</p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=bill125"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""NpQvYfFEoviXpBYseuUL5lmY6RpgxncMAoTyxHDhwn+mwo1yks1fJ2fcLYrngkEUKxWpAN1YUWrNUlg9pONhrw=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow bill125"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=bill125"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""CLwIxe+InD/Pg4CaK2YQkkT/B8ulRCOIv+8J89F/0uP4lIwUCAD5mr99tFzSM+xTPFL6gq1BjF9tkA+VdkF9VA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow bill125"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

        
<li class=""follow-list-item left border-bottom"">
  <div class=""follower-list-align-top d-inline-block position-relative"" style=""height: 75px"">

    <a href=""/LukeXuan""><img alt=""@LukeXuan"" class=""avatar"" height=""75"" src=""https://avatars0.githubusercontent.com/u/3363954?v=3&amp;s=150"" width=""75"" /></a>
  </div>

  <div class=""follower-list-align-top d-inline-block ml-3"">
    <h3 class=""follow-list-name""><span class=""css-truncate css-truncate-target"" title=""Luke Lazurite""><a href=""/LukeXuan"">Luke Lazurite</a></span></h3>
    <p class=""follow-list-info""><svg aria-hidden=""true"" class=""octicon octicon-organization"" height=""16"" version=""1.1"" viewBox=""0 0 14 16"" width=""14""><path d=""M4.75 4.95C5.3 5.59 6.09 6 7 6c.91 0 1.7-.41 2.25-1.05A1.993 1.993 0 0 0 13 4c0-1.11-.89-2-2-2-.41 0-.77.13-1.08.33A3.01 3.01 0 0 0 7 0C5.58 0 4.39 1 4.08 2.33 3.77 2.13 3.41 2 3 2c-1.11 0-2 .89-2 2a1.993 1.993 0 0 0 3.75.95zm5.2-1.52c.2-.38.59-.64 1.05-.64.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2-.65 0-1.17-.53-1.19-1.17.06-.19.11-.39.14-.59zM7 .98c1.11 0 2.02.91 2.02 2.02 0 1.11-.91 2.02-2.02 2.02-1.11 0-2.02-.91-2.02-2.02C4.98 1.89 5.89.98 7 .98zM3 5.2c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.45 0 .84.27 1.05.64.03.2.08.41.14.59C4.17 4.67 3.66 5.2 3 5.2zM13 6H1c-.55 0-1 .45-1 1v3c0 .55.45 1 1 1v2c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-1h1v3c0 .55.45 1 1 1h2c.55 0 1-.45 1-1v-3h1v1c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-2c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1zM3 13H2v-3H1V7h2v6zm7-2H9V9H8v6H6V9H5v2H4V7h6v4zm3-1h-1v3h-1V7h2v3z""></path></svg> <span class=""css-truncate css-truncate-target"" title=""Works for UMJI SJTU"">UMJI SJTU</span></p>

        <span class=""user-following-container js-toggler-container js-social-container "">

      <span class=""follow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/follow?target=LukeXuan"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""1Cb9OBIiQ4MpnyzoxvsyVqMG9J4GsPtRByftfeWuMMNUTW8BJUArWg3BLnQAif8LPZaY0RrcQgOPJLj8EQ3tGg=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm  js-toggler-target""
            aria-label=""Follow this person"" title=""Follow LukeXuan"">
            Follow
          </button>
</form>      </span>

      <span class=""unfollow"">
        <!-- </textarea> --><!-- '""` --><form accept-charset=""UTF-8"" action=""/users/unfollow?target=LukeXuan"" data-form-nonce=""34c695586fc5973c1edc1b2bec10f8fe377e988d"" data-remote=""true"" method=""post""><div style=""margin:0;padding:0;display:inline""><input name=""utf8"" type=""hidden"" value=""&#x2713;"" /><input name=""authenticity_token"" type=""hidden"" value=""RvOOK5rdC7jt6Jw+Uej0H6ce2MZ5DQKExbg7SCnIG3f98ws/KA6lbJouRk+4TiIWlu03CjlOCPppE5CeLUTiPA=="" /></div>
          <button
            type=""submit""
            class=""btn btn-sm js-toggler-target""
            aria-label=""Unfollow this person"" title=""Unfollow LukeXuan"">
            Unfollow
          </button>
</form>      </span>
    </span>

  </div>
</li>

    </ol>

  <div class=""paginate-container"">
    
  </div>
</div>

      </div>
      <div class=""modal-backdrop js-touch-events""></div>
    </div>

        <div class=""container site-footer-container"">
  <div class=""site-footer"" role=""contentinfo"">
    <ul class=""site-footer-links right"">
        <li><a href=""https://status.github.com/"" data-ga-click=""Footer, go to status, text:status"">Status</a></li>
      <li><a href=""https://developer.github.com"" data-ga-click=""Footer, go to api, text:api"">API</a></li>
      <li><a href=""https://training.github.com"" data-ga-click=""Footer, go to training, text:training"">Training</a></li>
      <li><a href=""https://shop.github.com"" data-ga-click=""Footer, go to shop, text:shop"">Shop</a></li>
        <li><a href=""https://github.com/blog"" data-ga-click=""Footer, go to blog, text:blog"">Blog</a></li>
        <li><a href=""https://github.com/about"" data-ga-click=""Footer, go to about, text:about"">About</a></li>

    </ul>

    <a href=""https://github.com"" aria-label=""Homepage"" class=""site-footer-mark"" title=""GitHub"">
      <svg aria-hidden=""true"" class=""octicon octicon-mark-github"" height=""24"" version=""1.1"" viewBox=""0 0 16 16"" width=""24""><path d=""M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0 0 16 8c0-4.42-3.58-8-8-8z""></path></svg>
</a>
    <ul class=""site-footer-links"">
      <li>&copy; 2016 <span title=""0.43399s from github-fe132-cp1-prd.iad.github.net"">GitHub</span>, Inc.</li>
        <li><a href=""https://github.com/site/terms"" data-ga-click=""Footer, go to terms, text:terms"">Terms</a></li>
        <li><a href=""https://github.com/site/privacy"" data-ga-click=""Footer, go to privacy, text:privacy"">Privacy</a></li>
        <li><a href=""https://github.com/security"" data-ga-click=""Footer, go to security, text:security"">Security</a></li>
        <li><a href=""https://github.com/contact"" data-ga-click=""Footer, go to contact, text:contact"">Contact</a></li>
        <li><a href=""https://help.github.com"" data-ga-click=""Footer, go to help, text:help"">Help</a></li>
    </ul>
  </div>
</div>



    

    <div id=""ajax-error-message"" class=""ajax-error-message flash flash-error"">
      <svg aria-hidden=""true"" class=""octicon octicon-alert"" height=""16"" version=""1.1"" viewBox=""0 0 16 16"" width=""16""><path d=""M8.865 1.52c-.18-.31-.51-.5-.87-.5s-.69.19-.87.5L.275 13.5c-.18.31-.18.69 0 1 .19.31.52.5.87.5h13.7c.36 0 .69-.19.86-.5.17-.31.18-.69.01-1L8.865 1.52zM8.995 13h-2v-2h2v2zm0-3h-2V6h2v4z""></path></svg>
      <button type=""button"" class=""flash-close js-flash-close js-ajax-error-dismiss"" aria-label=""Dismiss error"">
        <svg aria-hidden=""true"" class=""octicon octicon-x"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48z""></path></svg>
      </button>
      Something went wrong with that request. Please try again.
    </div>


      
      <script crossorigin=""anonymous"" integrity=""sha256-Bh3ANcE7nUvkmePtJ/dbwuf5nV0dNN3VzXWFyo+HCms="" src=""https://assets-cdn.github.com/assets/frameworks-061dc035c13b9d4be499e3ed27f75bc2e7f99d5d1d34ddd5cd7585ca8f870a6b.js""></script>
      <script async=""async"" crossorigin=""anonymous"" integrity=""sha256-4I4esaAkH2ICy3HGVSQD2nVpsootD4bvZbQU+NpYxAw="" src=""https://assets-cdn.github.com/assets/github-e08e1eb1a0241f6202cb71c6552403da7569b28a2d0f86ef65b414f8da58c40c.js""></script>
      
      
      
      
      
      
    <div class=""js-stale-session-flash stale-session-flash flash flash-warn flash-banner hidden"">
      <svg aria-hidden=""true"" class=""octicon octicon-alert"" height=""16"" version=""1.1"" viewBox=""0 0 16 16"" width=""16""><path d=""M8.865 1.52c-.18-.31-.51-.5-.87-.5s-.69.19-.87.5L.275 13.5c-.18.31-.18.69 0 1 .19.31.52.5.87.5h13.7c.36 0 .69-.19.86-.5.17-.31.18-.69.01-1L8.865 1.52zM8.995 13h-2v-2h2v2zm0-3h-2V6h2v4z""></path></svg>
      <span class=""signed-in-tab-flash"">You signed in with another tab or window. <a href="""">Reload</a> to refresh your session.</span>
      <span class=""signed-out-tab-flash"">You signed out in another tab or window. <a href="""">Reload</a> to refresh your session.</span>
    </div>
    <div class=""facebox"" id=""facebox"" style=""display:none;"">
  <div class=""facebox-popup"">
    <div class=""facebox-content"" role=""dialog"" aria-labelledby=""facebox-header"" aria-describedby=""facebox-description"">
    </div>
    <button type=""button"" class=""facebox-close js-facebox-close"" aria-label=""Close modal"">
      <svg aria-hidden=""true"" class=""octicon octicon-x"" height=""16"" version=""1.1"" viewBox=""0 0 12 16"" width=""12""><path d=""M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48z""></path></svg>
    </button>
  </div>
</div>

  </body>
</html>
";
            #endregion
            var regex = new Regex(@"(?<=<span class=""css-truncate css-truncate-target"" title="").*(?=</a></span></h3>)");
            var matches = regex.Matches(html);
            Assert.Equal(41, matches.Count);
        }

        [Fact]
        public void parse_github_username_test()
        {
            var str = @"坂本ポテコ""><a href=""/sakamoto-poteko"">坂本ポテコ";
            var regex = new Regex(@"(?<=<a href=""/).*(?="">)");
            var result = regex.Match(str);
            Assert.Equal("sakamoto-poteko", result.Value);
        }

        [Fact]
        public void parse_github_fullname_test()
        {
            var str = @"<div class=""vcard-fullname"" itemprop=""name"">xiaodao</div>";
            var regex = new Regex(@"(?<=<div class=""vcard-fullname"" itemprop=""name"">).*(?=</div>)");
            var result = regex.Match(str);
            Assert.Equal("xiaodao", result.Value);
        }

        [Fact]
        public void parse_github_avatar_test()
        {
            #region HTML Source
            var html = @"    <meta property=""fb:app_id"" content=""1401488693436528"">

      <meta content=""https://avatars1.githubusercontent.com/u/2216750?v=3&amp;s=400"" name=""twitter:image:src"" /><meta content=""@github"" name=""twitter:site"" /><meta content=""summary"" name=""twitter:card"" /><meta content=""Kagamine (あまみや ゆうこ)"" name=""twitter:title"" /><meta content=""Kagamine has 74 repositories written in Shell, JavaScript, and C++. Follow their code on GitHub."" name=""twitter:description"" />
      <meta content=""https://avatars1.githubusercontent.com/u/2216750?v=3&amp;s=400"" property=""og:image"" /><meta content=""GitHub"" property=""og:site_name"" /><meta content=""profile"" property=""og:type"" /><meta content=""Kagamine (あまみや ゆうこ)"" property=""og:title"" /><meta content=""https://github.com/Kagamine"" property=""og:url"" /><meta content=""Kagamine has 74 repositories written in Shell, JavaScript, and C++. Follow their code on GitHub."" property=""og:description"" /><meta content=""Kagamine"" property=""profile:username"" />
      <meta name=""browser-stats-url"" content=""https://api.github.com/_private/browser/stats"">
    <meta name=""browser-errors-url"" content=""https://api.github.com/_private/browser/errors"">
    <link rel=""assets"" href=""https://assets-cdn.github.com/"">
    <link rel=""web-socket"" href=""wss://live.github.com/_sockets/MjIxNjc1MDowOmI4MGU5OGIyYTBhNWViMDVmMmM2M2FjYjhmYWIxMGY2ODY5M2E2M2E3OTM2OTgyYzE5NjhkMDE4NTYzZGNhYWM=--1aa1a0b8020c7dc5dbf6453277e81b50714aacd4"">
    <meta name=""pjax-timeout"" content=""1000"">
    <link rel=""sudo-modal"" href=""/sessions/sudo_modal"">

    <meta name=""msapplication-TileImage"" content=""/windows-tile.png"">
    <meta name=""msapplication-TileColor"" content=""#ffffff"">
    <meta name=""selected-link"" value=""/kagamine"" data-pjax-transient>

    <meta name=""google-site-verification"" content=""KT5gs8h0wvaagLKAVWq8bbeNwnZZK1r1XQysX3xurLU"">
<meta name=""google-site-verification"" content=""ZzhVyEFwb7w3e0-uOTltm8Jsck2F5StVihD0exw2fsA"">
    <meta name=""google-analytics"" content=""UA-3769691-2"">";
            #endregion
            var regex = new Regex(@"(?<=<meta content="").*(?="" property=""og:image"" /><meta content="")");
            var result = regex.Match(html).Value;
            Assert.Equal("https://avatars1.githubusercontent.com/u/2216750?v=3&amp;s=400", result);
        }
    }
}
