
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Types.Ipaffs;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum IpaffsPurposeForImportOrAdmissionEnum
{

		[EnumMember(Value = "Definitive import")]
		DefinitiveImport,
	
		[EnumMember(Value = "Horses Re-entry")]
		HorsesReEntry,
	
		[EnumMember(Value = "Temporary admission horses")]
		TemporaryAdmissionHorses,
	
}


