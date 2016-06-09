namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class RawHelper
    {
        public static bool IsRaw(this IHtmlHelper self)
        {
            if (self.ViewContext.HttpContext.Request.Query["raw"] == "true")
                return true;
            else
                return false;
        }
    }
}
