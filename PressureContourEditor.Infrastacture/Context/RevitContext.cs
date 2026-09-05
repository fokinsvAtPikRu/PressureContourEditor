using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace PressureContourEditor.Infrastacture.Context
{
    public class RevitContext :IDisposable
    {
        public UIDocument UIDocument { get; }
        public Document Document {get;}
        public UIApplication UIApplication { get; }

        public RevitContext(UIApplication uiApp)
        {
            UIApplication = uiApp;
            UIDocument=uiApp.ActiveUIDocument;
            Document=UIDocument.Document;
        }

        public void Dispose()
        {
            
        }

        public void ExecuteTransaction(string transactionName, Action action)
        {
            using (var transaction = new Transaction(Document, transactionName))
            {
                transaction.Start();
                try
                {
                    action();
                    transaction.Commit();
                }
                catch
                {
                    transaction.RollBack();
                    throw;
                }
            }
        }
        public void ExecuteTransaction(string transactionName, Func<TaskDialog> action)
        {
            using (var transaction = new Transaction(Document, transactionName))
            {
                transaction.Start();
                try
                {
                    action();
                    transaction.Commit();
                }
                catch
                {
                    transaction.RollBack();
                    throw;                
                }
            }
        }
    }
}
