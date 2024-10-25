//------------------------------------------------------------------------------
// <auto-generated>
	//     This code was generated from a template.
	//
	//     Manual changes to this file may cause unexpected behavior in your application.
	//     Manual changes to this file will be overwritten if the code is regenerated.
	//
//</auto-generated>
//------------------------------------------------------------------------------
#nullable enable


namespace Cdms.Types.Ipaffs.Mapping;

public static class VeterinaryInformationMapper
{
	public static Cdms.Model.Ipaffs.VeterinaryInformation Map(Cdms.Types.Ipaffs.VeterinaryInformation from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.VeterinaryInformation ();
to.EstablishmentsOfOriginExternalReference = ExternalReferenceMapper.Map(from?.EstablishmentsOfOriginExternalReference);
                to.EstablishmentsOfOrigins = from?.EstablishmentsOfOrigins?.Select(x => ApprovedEstablishmentMapper.Map(x)).ToArray();
                to.VeterinaryDocument = from.VeterinaryDocument;
            to.VeterinaryDocumentIssuedOn = from.VeterinaryDocumentIssueDate;
            to.AccompanyingDocumentNumbers = from.AccompanyingDocumentNumbers;
            to.AccompanyingDocuments = from?.AccompanyingDocuments?.Select(x => AccompanyingDocumentMapper.Map(x)).ToArray();
                to.CatchCertificateAttachments = from?.CatchCertificateAttachments?.Select(x => CatchCertificateAttachmentMapper.Map(x)).ToArray();
                to.IdentificationDetails = from?.IdentificationDetails?.Select(x => IdentificationDetailsMapper.Map(x)).ToArray();
                	return to;
	}
}
