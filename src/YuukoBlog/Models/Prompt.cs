﻿namespace YuukoBlog.Models
{
    public class Prompt
    {
        public string Title { get; set; }
        public int StatusCode { get; set; } = 200;
        public string Details { get; set; }
        public string RedirectUrl { get; set; }
        public string RedirectText { get; set; }
        public string Requires { get; set; }
        public dynamic Hint { get; set; }
        public bool HideBack { get; set; }
    }
}
