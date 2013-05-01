using Nancy;

namespace GroupMessage.Server.Module
{
    public abstract class ModuleBase : NancyModule
    {
        protected ModuleBase(string moduleBasePath) : base(moduleBasePath)
        {
        }
    }
}