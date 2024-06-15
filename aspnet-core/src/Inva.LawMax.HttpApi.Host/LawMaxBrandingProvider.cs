using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Inva.LawMax;

[Dependency(ReplaceServices = true)]
public class LawMaxBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "LawMax";
}
