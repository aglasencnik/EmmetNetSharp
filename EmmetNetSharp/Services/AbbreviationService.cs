using EmmetNetSharp.Interfaces;
using Jint;
using System.IO;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the AbbreviationService class.
    /// </summary>
    public class AbbreviationService : IAbbreviationService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public AbbreviationService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.AbbreviationScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods



        #endregion
    }
}
