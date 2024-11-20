using Cdms.Model;
using Cdms.Model.Ipaffs;

namespace Cdms.Business.Services;

public class LinkResult
{
    public LinkState State { get; set; }
    public List<ImportNotification> Notifications { get; set; } = new();
    public List<Movement> Movements { get; set; } = new();
}