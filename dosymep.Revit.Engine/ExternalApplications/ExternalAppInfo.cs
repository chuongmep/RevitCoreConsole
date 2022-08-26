using dosymep.Revit.FileInfo.RevitAddins;

namespace dosymep.Revit.Engine.ExternalApplications {
    /// <summary>
    /// External app info.
    /// </summary>
    internal class ExternalAppInfo {
        private readonly RevitAddinItem _revitAddinItem;

        /// <summary>
        /// Creates revit external app info.
        /// </summary>
        /// <param name="revitAddinItem"></param>
        public ExternalAppInfo(RevitAddinItem revitAddinItem) {
            _revitAddinItem = revitAddinItem;
        }

        /// <summary>
        /// Full path to assembly.
        /// </summary>
        public string AssemblyPath => _revitAddinItem.AssemblyPath;

        /// <summary>
        /// Full class name.
        /// </summary>
        public string FullClassName => _revitAddinItem.FullClassName;

        /// <summary>
        /// Get external application instance.
        /// </summary>
        /// <typeparam name="T">External application type.</typeparam>
        /// <returns>Returns external application instance.</returns>
        public T CreateExternalApplication<T>()
            where T : class {
            return _revitAddinItem.CreateAddinItemObject<T>();
        }
    }
}