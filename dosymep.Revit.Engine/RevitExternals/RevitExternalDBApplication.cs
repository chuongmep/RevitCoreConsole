﻿using System;
using System.Collections.Generic;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

namespace dosymep.Revit.Engine.RevitExternals {
    /// <summary>
    /// External DB application.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class RevitExternalDBApplication : RevitExternalItem {
        /// <summary>
        /// Creates external DB application.
        /// </summary>
        /// <param name="revitApplication">Revit application instance.</param>
        public RevitExternalDBApplication(RevitApplication revitApplication)
            : base(revitApplication) {
        }

        /// <inheritdoc />
        protected override void ExecuteExternalItemImpl(IDictionary<string, string> journalData) {
            ControlledApplication controlledApplication = _revitApplication.Application.CreateControlledApplication();
            var application = RevitExternalItemInfo.CreateExternalApplication<IExternalDBApplication>();
            try {
                ApplyJournalData(application, journalData);
                CheckResult(application.OnStartup(controlledApplication), "Startup");
                _revitApplication.Application.SetDesignAutomationReady(MainModelPath);
            } finally {
                CheckResult(application.OnShutdown(controlledApplication), "Shutdown");
            }
        }

        private void CheckResult(ExternalDBApplicationResult startupResult, string operation) {
            if(startupResult == ExternalDBApplicationResult.Failed) {
                throw new Exception($"{operation} result failed. ");
            }
        }
    }
}