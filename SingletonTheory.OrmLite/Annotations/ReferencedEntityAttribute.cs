using System;

namespace SingletonTheory.OrmLite.Annotations
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class ReferencedEntityAttribute : Attribute
	{
		#region Fields & Properties

		readonly Type _entityType;
		private string _idProperty;

		public Type EntityType
		{
			get { return _entityType; }
		}

		public string IdProperty
		{
			get { return _idProperty; }
		}

		#endregion Fields & Properties

		#region Constructors

		public ReferencedEntityAttribute(Type entityType, string idProperty)
		{
			_entityType = entityType;
			_idProperty = idProperty;
		}

		#endregion Constructors
	}
}