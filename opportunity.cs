using System;
using Microsoft.Xrm.Sdk;

namespace Plugins
{
    public class OpportunityCalculateRevenuePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                Entity opportunity = (Entity)context.InputParameters["Target"];

                if (opportunity.LogicalName != "opportunity")
                    return;

                if (opportunity.Contains("estimatedvalue"))
                {
                    Money revenue = (Money)opportunity["estimatedvalue"];

                    decimal bonus = revenue.Value * 0.10m;

                    opportunity["new_bonusvalue"] = new Money(bonus);
                }
            }
        }
    }
}