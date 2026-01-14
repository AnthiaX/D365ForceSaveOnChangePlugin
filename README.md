The D365ForceSaveOnChange Plugin is a D365 Plugin that is fully compatible with Dynamics 365 CE / Sales Enterprise.

This plugin allows you force save UI form of the selected entity when the target column/field of your choice has been updated.

The target field is defined in the D365ForceSaveOnChange.cs file. You only need to update the following on line 22 with the field that you want to trigger the UI Form save.

In my example, my custom field in the Invoice entity for Total Transactions is called anthia_totalpayments, so my code will react if the target contains this field. 

    // E.g. Only react when Total Transactions changes
        if (!target.Contains("anthia_totalpayments"))
            return;

The build-plugin.yml file must include the GitHub root path of the .csproj file. 
Note: If this is incorrect, the GitHub compiler will not be able to locate the file, and it will fail to produce the DLL file.

As a reminder: All D365 Plugin Assemblies need to be registered with a Strong Name Key File (.snk file), otherwise the XRM Toolbox Plugin Registration tool will fail to compile the assembly. 

    // In Visual Studio, you can retrieve an .snk file with the following steps:

        1. Open Visual Studio 2022
        2. Got to Tools > Command Line - Developer PowerShell
        3. sn.exe -k YourSNKFile.snk
        4. Newkey pair written

    // Convert key to token (if necessary)
        5. snk.exe -tp YourSNKFile.snk
