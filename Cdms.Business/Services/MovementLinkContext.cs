using Cdms.Model;

namespace Cdms.Business.Services;

public class MovementLinkContext(Movement movement) : LinkContext
{
    public Movement Movement { get; } = movement;
}