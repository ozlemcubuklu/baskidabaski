using Microsoft.Extensions.Localization;

using System.Reflection;
using System.Runtime.CompilerServices;

namespace TraversalCoreProje.Services.LanguageService
{
    public class SharedResource { }
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type=typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(SharedResource),assemblyName.Name);
        }

   

    public LocalizedString GetKey(string Key)
    {
        return _localizer[Key];
    } }
}
