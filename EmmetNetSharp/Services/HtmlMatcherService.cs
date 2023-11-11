using EmmetNetSharp.Interfaces;
using Jint;
using System.IO;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the HtmlMatcherService class.
    /// </summary>
    public class HtmlMatcherService : IHtmlMatcherService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public HtmlMatcherService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.HtmlMatcherScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods



        #endregion
    }
}
