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

public static class ImportNotificationMapper
{
	public static Cdms.Model.Ipaffs.ImportNotification Map(Cdms.Types.Ipaffs.ImportNotification from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.ImportNotification ();
to.IpaffsId = from.IpaffsId;
            to.Etag = from.Etag;
            to.ExternalReferences = from?.ExternalReferences?.Select(x => IpaffsExternalReferenceMapper.Map(x)).ToArray();
                to.ReferenceNumber = from.ReferenceNumber;
            to.Version = from.Version;
            to.LastUpdated = from.LastUpdated;
            to.LastUpdatedBy = IpaffsUserInformationMapper.Map(from?.LastUpdatedBy);
                to.IpaffsType = IpaffsImportNotificationTypeEnumMapper.Map(from?.IpaffsType);
                to.Replaces = from.Replaces;
            to.ReplacedBy = from.ReplacedBy;
            to.Status = IpaffsImportNotificationStatusEnumMapper.Map(from?.Status);
                to.SplitConsignment = IpaffsSplitConsignmentMapper.Map(from?.SplitConsignment);
                to.ChildNotification = from.ChildNotification;
            to.RiskAssessment = IpaffsRiskAssessmentResultMapper.Map(from?.RiskAssessment);
                to.JourneyRiskCategorisation = IpaffsJourneyRiskCategorisationResultMapper.Map(from?.JourneyRiskCategorisation);
                to.IsHighRiskEuImport = from.IsHighRiskEuImport;
            to.PartOne = IpaffsPartOneMapper.Map(from?.PartOne);
                to.DecisionBy = IpaffsUserInformationMapper.Map(from?.DecisionBy);
                to.DecisionDate = from.DecisionDate;
            to.PartTwo = IpaffsPartTwoMapper.Map(from?.PartTwo);
                to.PartThree = IpaffsPartThreeMapper.Map(from?.PartThree);
                to.OfficialVeterinarian = from.OfficialVeterinarian;
            to.ConsignmentValidations = from?.ConsignmentValidations?.Select(x => IpaffsValidationMessageCodeMapper.Map(x)).ToArray();
                to.AgencyOrganisationId = from.AgencyOrganisationId;
            to.RiskDecisionLockingTime = from.RiskDecisionLockingTime;
            to.IsRiskDecisionLocked = from.IsRiskDecisionLocked;
            to.IsBulkUploadInProgress = from.IsBulkUploadInProgress;
            to.RequestId = from.RequestId;
            to.IsCdsFullMatched = from.IsCdsFullMatched;
            to.ChedTypeVersion = from.ChedTypeVersion;
            to.IsGMRMatched = from.IsGMRMatched;
            	return to;
	}
}

