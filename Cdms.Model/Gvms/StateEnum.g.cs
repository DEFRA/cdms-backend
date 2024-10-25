
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Cdms.Model.VehicleMovement;

public enum StateEnum
{

		NotFinalisable,
	
		Open,
	
		Finalised,
	
		CheckedIn,
	
		Embarked,
	
		Completed,
	
}

