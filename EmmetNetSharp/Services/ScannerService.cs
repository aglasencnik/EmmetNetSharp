using EmmetNetSharp.Interfaces;
using Jint;
using System.IO;

namespace EmmetNetSharp.Services
{
    /// <summary>
    /// Represents the ScannerService class.
    /// </summary>
    public class ScannerService : IScannerService
    {
        #region Fields

        private readonly Engine _engine;

        #endregion

        #region Ctor

        public ScannerService()
        {
            _engine = new Engine();

            var code = File.ReadAllText(Path.Combine(PackageDefaults.ScriptsFolderPath, PackageDefaults.ScannerScriptPath));
            _engine.Execute(code ?? string.Empty);
        }

        #endregion

        #region Methods



        #endregion
    }
}
