using System;
using Microsoft.Xrm.Sdk;

namespace Plugins
{
    public class LeadValidateEmailPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                Entity lead = (Entity)context.InputParameters["Target"];

                if (lead.LogicalName != "lead")
                    return;

                if (!lead.Attributes.Contains("emailaddress1"))
                {
                    throw new InvalidPluginExecutionException(
                        "Email obbligatoria per creare un Lead."
                    );
                }
            }
        }
    }
}