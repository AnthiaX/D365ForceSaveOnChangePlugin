using System;
using Microsoft.Xrm.Sdk;

public class D365ForceSaveOnChange : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        var context = (IPluginExecutionContext)
            serviceProvider.GetService(typeof(IPluginExecutionContext));

        // Prevent infinite loop
        if (context.Depth > 1)
            return;

        if (!context.InputParameters.Contains("Target"))
            return;

        var target = context.InputParameters["Target"] as Entity;
        if (target == null)
            return;

        // E.g. Only react when Total Transactions changes
        if (!target.Contains("anthia_totalpayments"))
            return;

        var serviceFactory =
            (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

        var service =
            serviceFactory.CreateOrganizationService(context.UserId);

        // No-op update to force calculated field recalculation
        var update = new Entity(target.LogicalName)
        {
            Id = target.Id
        };

        service.Update(update);
    }
}