// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     UserUpdatedData class.
//  Author:          Ben Moore
//  Created date:    26/07/2015
//  Copyright:       Donky Networks Ltd 2016
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Donky.Core
{
	internal class UserUpdatedData
	{
		public Guid NetworkProfileId { get; set; }
		public string ExternalUserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DisplayName { get; set; }
		public string CountryIsoCode { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
		public List<string> OperatingSystems { get; set; }
		public string AvatarUrl { get; set; }
		public string AvatarAssetId { get; set; }
		public string NewExternalUserId { get; set; }
		public int UtcOffsetMins { get; set; }
		public DateTime RegisteredOn { get; set; }
		public Dictionary<string, string> AdditionalProperties { get; set; }
		public List<string> SelectedTags { get; set; }
		public string BillingStatus { get; set; }
		public bool IsAnonymous { get; set; }
	}
}

