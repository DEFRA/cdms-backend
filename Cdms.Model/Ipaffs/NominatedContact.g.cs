//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable

using JsonApiDotNetCore.Resources.Annotations;
using System.Text.Json.Serialization;
using System.Dynamic;


namespace Cdms.Model.Ipaffs;

/// <summary>
/// Person to be nominated for text and email contact for the consignment
/// </summary>
public partial class NominatedContact  //
{


        /// <summary>
        /// Name of nominated contact
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Name of nominated contact")]
    public string? Name { get; set; }

	
        /// <summary>
        /// Email address of nominated contact
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Email address of nominated contact")]
    public string? Email { get; set; }

	
        /// <summary>
        /// Telephone number of nominated contact
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Telephone number of nominated contact")]
    public string? Telephone { get; set; }

	}


