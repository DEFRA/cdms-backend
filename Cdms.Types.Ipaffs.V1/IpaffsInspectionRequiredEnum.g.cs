
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Types.Ipaffs;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum IpaffsInspectionRequiredEnum
{

		[EnumMember(Value = "Required")]
		Required,
	
		[EnumMember(Value = "Inconclusive")]
		Inconclusive,
	
		[EnumMember(Value = "Not required")]
		NotRequired,
	
}


