namespace Cdms.Backend.Data;

public interface IDataEntity
{
    public string Id { get; set; }

    public string Etag { get; set; }
}