using Cdms.Model;

namespace Cdms.Business.Services;

public record MovementLinkContext(Movement PersistedMovement, Movement? ExistingMovement) : LinkContext
{
    public override string GetIdentifiers()
    {
        return string.Join(',', PersistedMovement._MatchReferences);
    }
}