using System;
using Microsoft.Xrm.Sdk;

namespace Plugins
{
    public class AccountSetCategoryPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];

                if (entity.LogicalName != "account")
                    return;

                if (!entity.Attributes.Contains("accountcategorycode"))
                {
                    entity["accountcategorycode"] = new OptionSetValue(1);
                }
            }
        }
    }
}