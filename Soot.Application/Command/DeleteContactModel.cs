using linqPlusPlus;
using Soot.Application.Base;

namespace Soot.Application.Command;

public class DeleteContactModel : ModelBase, IExternalIdModel
{
    public override bool IsValid =>
        !(!ExternalId.HasContent() && !SourceName.HasContent());
    public string? ExternalId { get; set; }
    public string? SourceName { get; set; }
}