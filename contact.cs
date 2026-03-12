using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Plugins
{
    public class ContactCopyPhoneFromAccountPlugin : IPlugin
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
                Entity contact = (Entity)context.InputParameters["Target"];

                if (contact.LogicalName != "contact")
                    return;

                if (contact.Contains("parentcustomerid"))
                {
                    EntityReference accountRef = (EntityReference)contact["parentcustomerid"];

                    Entity account = service.Retrieve(
                        accountRef.LogicalName,
                        accountRef.Id,
                        new ColumnSet("telephone1")
                    );

                    if (account.Contains("telephone1"))
                    {
                        contact["telephone1"] = account["telephone1"];
                    }
                }
            }
        }
    }
}