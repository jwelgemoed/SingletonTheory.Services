using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Utilities;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class UserEntity : IIdentifiable
	{
		#region Fields & Properties

		private List<DomainPermissionObject> _domainPermissionObjects;

		[AutoIncrement]
		public long Id { get; set; }
		public string UserName { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string PasswordHash { get; set; }
		public string Salt { get; set; }

		public List<string> Permissions { get; set; }
		public List<int> Roles { get; set; }
		public string Language { get; set; }
		public bool Active { get; set; }
		public Dictionary<string, string> Meta { get; set; }
		public string TimeZoneId { get; set; }

		//public virtual List<DomainPermissionObject> DomainPermissions { get; set; }

		public virtual List<DomainPermissionObject> DomainPermissions
		{
			get
			{
				//Return in users timeZone
				List<DomainPermissionObject> objects = new List<DomainPermissionObject>();
				foreach (var domainPermissionObject in _domainPermissionObjects)
				{
					DomainPermissionObject obj = new DomainPermissionObject();
					obj.DomainPermissionId = domainPermissionObject.DomainPermissionId;
					if (domainPermissionObject.ActiveTimeSpan != null)
					{
						obj.ActiveTimeSpan = new ActiveTimeSpan();
						obj.ActiveTimeSpan.StartDate = DateTimeUtility.ConvertTimeFromUtc(
							domainPermissionObject.ActiveTimeSpan.StartDate, TimeZoneId);
						obj.ActiveTimeSpan.EndDate = DateTimeUtility.ConvertTimeFromUtc(
							domainPermissionObject.ActiveTimeSpan.EndDate, TimeZoneId);
					}
					objects.Add(obj);
				}
				return objects;
			}
			set
			{
				//Return in users timeZone
				List<DomainPermissionObject> objects = new List<DomainPermissionObject>();
				foreach (var domainPermissionObject in value)
				{
					DomainPermissionObject obj = new DomainPermissionObject();
					obj.DomainPermissionId = domainPermissionObject.DomainPermissionId;
					if (domainPermissionObject.ActiveTimeSpan != null)
					{
						obj.ActiveTimeSpan = new ActiveTimeSpan();
						obj.ActiveTimeSpan.StartDate = DateTimeUtility.ConvertTimeToUtc(
							domainPermissionObject.ActiveTimeSpan.StartDate, TimeZoneId);
						obj.ActiveTimeSpan.EndDate = DateTimeUtility.ConvertTimeToUtc(
							domainPermissionObject.ActiveTimeSpan.EndDate, TimeZoneId);
					}
					objects.Add(obj);
				}
				_domainPermissionObjects = objects;
			}
		}

		#endregion Fields & Properties

		#region Constructors

		public UserEntity()
		{
			_domainPermissionObjects = new List<DomainPermissionObject>();

			Permissions = new List<string>();
			Roles = new List<int>();
			Meta = new Dictionary<string, string>();
			TimeZoneId = "UTC";  //set a default
		}

		#endregion Constructors

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}