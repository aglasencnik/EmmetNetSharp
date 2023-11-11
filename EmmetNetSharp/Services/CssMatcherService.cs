using EmmetNetSharp.Interfaces;
using Jint;
using System.IO;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the CssMatcherService class.
    /// </summary>
    public class CssMatcherService : ICssMatcherService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public CssMatcherService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.CssMatcherScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods



        #endregion
    }
}
