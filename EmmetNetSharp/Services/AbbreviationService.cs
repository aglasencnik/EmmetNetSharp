using EmmetNetSharp.Interfaces;
using Jint;
using System;
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

        public virtual object ExpandAbbreviation()
        {
            throw new NotImplementedException();
        }

        public virtual object ExtractAbbreviation()
        {
            throw new NotImplementedException();
        }

        public virtual object ExpandMarkupAbbreviation()
        {
            throw new NotImplementedException();
        }

        public virtual object ExpandStylesheetAbbreviation()
        {
            throw new NotImplementedException();
        }

        public virtual object ParseMarkup()
        {
            throw new NotImplementedException();
        }

        public virtual object ParseStylesheet()
        {
            throw new NotImplementedException();
        }

        public virtual object ParseStylesheetSnippets()
        {
            throw new NotImplementedException();
        }

        public virtual object StringifyMarkup()
        {
            throw new NotImplementedException();
        }

        public virtual object StringifyStylesheet()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
