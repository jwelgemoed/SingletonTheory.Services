using System;

namespace SingletonTheory.OrmLite.Annotations
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public class AssociatedEntityAttribute : Attribute
	{
		#region Fields & Properties

		readonly Type _entityType;

		public Type EntityType
		{
			get { return _entityType; }
		}

		public bool IsList { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public AssociatedEntityAttribute(Type entityType)
		{
			_entityType = entityType;
			IsList = false;
		}

		#endregion Constructors
	}
}
