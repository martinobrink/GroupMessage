using Nancy;

namespace GroupMessage.Server.Module
{
    /// <summary>
    /// Takes care of redirecting from the root URL to the admin gui
    /// </summary>
    public class StaticModule : ModuleBase
    {
        public StaticModule() : base("")
        {
            Get ["/"] = parameters =>
            {
                return Response.AsRedirect("index.html");
            };
        }
    }
}
