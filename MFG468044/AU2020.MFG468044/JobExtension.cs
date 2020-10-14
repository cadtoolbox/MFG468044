using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Connectivity.Extensibility.Framework;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Entities;
using Autodesk.Connectivity.JobProcessor.Extensibility;
using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;

[assembly: ApiVersion("14.0")]
[assembly: ExtensionId("DB624F13-753C-4C82-BB21-E17E3513D662")]


namespace MFG468044
{
    public class JobExtension : IJobHandler 
    {
        private static string JOB_TYPE = "AU2020.MFG468044";

        #region IJobHandler Implementation
        public bool CanProcess(string jobType)
        {
            return jobType == JOB_TYPE;
        }

        public JobOutcome Execute(IJobProcessorServices context, IJob job)
        {


            try
            {
                Connection connection = context.Connection;
                Autodesk.Connectivity.WebServicesTools.WebServiceManager mWsMgr = connection.WebServiceManager;
               

                long mFileId = Convert.ToInt64(job.Params["FileId"]);
                File mFile = mWsMgr.DocumentService.GetFileById(mFileId);


                if (mFile.Cat.CatName == "Released")
                {

                    // Do Something if The File Is Released

                }


                return JobOutcome.Success;
            }
            catch (Exception ex)
            {
                context.Log(ex, "Job failed: " + ex.ToString() + " ");
                return JobOutcome.Failure;
            }

        }

        public void OnJobProcessorShutdown(IJobProcessorServices context)
        {
            //throw new NotImplementedException();
        }

        public void OnJobProcessorSleep(IJobProcessorServices context)
        {
            //throw new NotImplementedException();
        }

        public void OnJobProcessorStartup(IJobProcessorServices context)
        {
            //throw new NotImplementedException();
        }

        public void OnJobProcessorWake(IJobProcessorServices context)
        {
            //throw new NotImplementedException();
        }
        #endregion IJobHandler Implementation
    }
}
