
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Types.Ipaffs;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum DecisionNotAcceptableActionEnum
{

		[EnumMember(Value = "slaughter")]
		Slaughter,
	
		[EnumMember(Value = "reexport")]
		Reexport,
	
		[EnumMember(Value = "euthanasia")]
		Euthanasia,
	
		[EnumMember(Value = "redispatching")]
		Redispatching,
	
		[EnumMember(Value = "destruction")]
		Destruction,
	
		[EnumMember(Value = "transformation")]
		Transformation,
	
		[EnumMember(Value = "other")]
		Other,
	
		[EnumMember(Value = "entry-refusal")]
		EntryRefusal,
	
		[EnumMember(Value = "quarantine-imposed")]
		QuarantineImposed,
	
		[EnumMember(Value = "special-treatment")]
		SpecialTreatment,
	
		[EnumMember(Value = "industrial-processing")]
		IndustrialProcessing,
	
		[EnumMember(Value = "re-dispatch")]
		ReDispatch,
	
		[EnumMember(Value = "use-for-other-purposes")]
		UseForOtherPurposes,
	
}

