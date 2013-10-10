using System;

namespace SingletonTheory.OrmLite.Annotations
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class DatabaseAttribute : System.Attribute
	{
		#region Fields & Properties

		readonly string _databaseConfigSetting;

		public string DatabaseConfigSetting
		{
			get { return _databaseConfigSetting; }
		}

		#endregion Fields & Properties

		#region Constructors

		public DatabaseAttribute(string databaseConfigSetting)
		{
			_databaseConfigSetting = databaseConfigSetting;
		}

		#endregion Constructors
	}
}