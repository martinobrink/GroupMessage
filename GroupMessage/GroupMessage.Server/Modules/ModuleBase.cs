using Nancy;

namespace GroupMessage.Server.Modules
{
    public abstract class ModuleBase : NancyModule
    {
        protected ModuleBase(string moduleBasePath) : base(moduleBasePath)
        {
        }
    }
}