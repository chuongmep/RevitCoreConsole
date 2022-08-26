﻿using System;
using System.Collections.Generic;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace dosymep.Revit.Engine.RevitExternals {
    /// <summary>
    /// External application.
    /// </summary>
    internal class RevitExternalApplication : RevitExternalItem {
        public RevitExternalApplication(Application application)
            : base(application) {
        }

        protected override void ExecuteExternalItemImpl(IDictionary<string, string> journalData) {
            UIControlledApplication controlledApplication = _application.CreateUIControlledApplication();
            var application = RevitExternalItemInfo.CreateExternalApplication<IExternalApplication>();
            CheckResult(application.OnStartup(controlledApplication));
        }

        private void CheckResult(Result startupResult) {
            if(startupResult == Result.Cancelled) {
                throw new OperationCanceledException();
            } else if(startupResult == Result.Failed) {
                throw new Exception("Startup result failed.");
            }
        }
    }
}