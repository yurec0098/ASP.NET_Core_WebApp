﻿namespace Timesheets.Domain
{
	public class BaseEntity<TUniqueId> where TUniqueId : struct
	{
		public TUniqueId Id { get; set; }
		public bool IsDeleted { get; set; }
	}
}
