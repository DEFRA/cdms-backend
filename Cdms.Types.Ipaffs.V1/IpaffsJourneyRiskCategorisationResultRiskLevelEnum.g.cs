
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Types.Ipaffs;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum IpaffsJourneyRiskCategorisationResultRiskLevelEnum
{

		[EnumMember(Value = "High")]
		High,
	
		[EnumMember(Value = "Medium")]
		Medium,
	
		[EnumMember(Value = "Low")]
		Low,
	
}


