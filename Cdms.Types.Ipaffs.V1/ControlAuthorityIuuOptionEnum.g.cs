
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Types.Ipaffs;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ControlAuthorityIuuOptionEnum
{

		[EnumMember(Value = "IUUOK")]
		Iuuok,
	
		[EnumMember(Value = "IUUNA")]
		Iuuna,
	
		[EnumMember(Value = "IUUNotCompliant")]
		IUUNotCompliant,
	
}


