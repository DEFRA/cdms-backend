
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Types.Ipaffs;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum IpaffsJourneyRiskCategorisationResultRiskLevelMethodEnum
{

		[EnumMember(Value = "System")]
		System,
	
		[EnumMember(Value = "User")]
		User,
	
}


