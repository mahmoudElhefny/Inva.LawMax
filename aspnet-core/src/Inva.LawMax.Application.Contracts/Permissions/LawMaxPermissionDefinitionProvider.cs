using Inva.LawMax.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Inva.LawMax.Permissions;

public class LawMaxPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LawMaxPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LawMaxPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LawMaxResource>(name);
    }
}
