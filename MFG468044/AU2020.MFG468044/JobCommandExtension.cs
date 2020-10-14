/*=====================================================================
  
  OFC - Thomas J. Rambach

  2020-07-18

=====================================================================*/


using System;
using System.Collections.Generic;
using System.Reflection;

using Autodesk.Connectivity.Extensibility.Framework;
using Autodesk.Connectivity.Explorer.Extensibility;
using Autodesk.Connectivity.WebServices;
using Autodesk.Connectivity.WebServicesTools;
using System.Windows.Forms;
using System.Linq;

namespace MFG468044
{
    public class JobCommandExtension : IExplorerExtension
    {

        public void JobCommandHandler(object s, CommandItemEventArgs e)
        {
            // Queue an update job
            //
            JobParam param1 = new JobParam();
            JobParam param2 = new JobParam();
            JobParam param3 = new JobParam();
            JobParam param4 = new JobParam();

            DocumentService DocSrvc = e.Context.Application.Connection.WebServiceManager.DocumentService;


            int i = 0;

            foreach (ISelection vaultObj in e.Context.CurrentSelectionSet)
            {


                File oLatestFileVer = DocSrvc.GetLatestFileByMasterId(vaultObj.Id);
    
                
                param1.Name = "EntityId";
                param1.Val = oLatestFileVer.Id.ToString();

                param2.Name = "FileId";
                param2.Val = oLatestFileVer.Id.ToString();

                param3.Name = "EntityClassId";
                param3.Val = "FILE";

                param4.Name = "JobType";
                param4.Val = "";


                    // Add the job to the queue
                    try
                    {

                        Job oJob = e.Context.Application.Connection.WebServiceManager.JobService.AddJob("AU2020.MFG468044", "AU2020.MFG468044: " + vaultObj.Label, new JobParam[] { param1, param2, param3, param4 }, 100);
                        i = i + 1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

    

;
            }

            MessageBox.Show(i + " Job(s) Queued");

        }


        public string ResourceCollectionName()
        {
            return String.Empty;
        }

        public IEnumerable<CommandSite> CommandSites()
        {
            List<CommandSite> sites = new List<CommandSite>();

            // ### Define New Commands

            // Define Queue Command
            CommandItem queueJobCmdItem = new CommandItem("Command.Job", "Queue Job");
            queueJobCmdItem.NavigationTypes = new SelectionTypeId[] { SelectionTypeId.File };
            queueJobCmdItem.MultiSelectEnabled = true;
            queueJobCmdItem.Image = Properties.Resources.Update;
            queueJobCmdItem.Description = "Administrative File Purge";
            queueJobCmdItem.Hint = "Sends file to the Job Processor";
            queueJobCmdItem.Execute += JobCommandHandler;


            // ### Deploy New Command

            // Define Menu Item
            CommandSite queueContextMenu = new CommandSite("Menu.QueueJob", "Queue Job");
            queueContextMenu.Location = CommandSiteLocation.FileContextMenu;
            queueContextMenu.DeployAsPulldownMenu = false;
            queueContextMenu.AddCommand(queueJobCmdItem);
            sites.Add(queueContextMenu);

            return sites;
        }


        public IEnumerable<DetailPaneTab> DetailTabs()
        {
            return null;
        }

        public void OnLogOn(IApplication application)
        {
            // NoOp;
        }

        public void OnLogOff(IApplication application)
        {
            // NoOp;
        }

        public void OnStartup(IApplication application)
        {
            // NoOp;
        }

        public void OnShutdown(IApplication application)
        {
            // NoOp;
        }


        public IEnumerable<string> HiddenCommands()
        {
            return null;
        }


        public IEnumerable<CustomEntityHandler> CustomEntityHandlers()
        {
            return null;
        }
    }
}
