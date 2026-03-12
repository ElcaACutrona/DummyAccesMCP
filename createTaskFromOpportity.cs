using System;
using Microsoft.Xrm.Sdk;

namespace Plugins
{
    public class OpportunityCreateTaskOnClosePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory factory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                Entity opportunity = (Entity)context.InputParameters["Target"];

                if (opportunity.LogicalName != "opportunity")
                    return;

                if (opportunity.Contains("statecode"))
                {
                    OptionSetValue state = (OptionSetValue)opportunity["statecode"];

                    if (state.Value == 1)
                    {
                        Entity task = new Entity("task");
                        task["subject"] = "Follow-up cliente dopo chiusura opportunità";
                        task["scheduledend"] = DateTime.Now.AddDays(7);

                        service.Create(task);
                    }
                }
            }
        }
    }
}