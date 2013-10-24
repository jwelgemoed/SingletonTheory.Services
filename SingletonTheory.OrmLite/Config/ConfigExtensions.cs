using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using SingletonTheory.OrmLite.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace SingletonTheory.OrmLite.Config
{
	public static class ConfigExtensions
	{
		#region Fields & Properties

		private static Dictionary<Type, ModelDefinition> _typeModelDefinitionMap = new Dictionary<Type, ModelDefinition>();

		#endregion Fields & Properties

		#region Public Methods

		public static ModelDefinition GetModelDefinition(this Type modelType)
		{
			ModelDefinition modelDefinition;

			if (_typeModelDefinitionMap.TryGetValue(modelType, out modelDefinition))
				return modelDefinition;

			if (modelType.IsValueType() || modelType == typeof(string))
				return null;

			AliasAttribute aliasAttribute = modelType.FirstAttribute<AliasAttribute>();
			SchemaAttribute schemaAttribute = modelType.FirstAttribute<SchemaAttribute>();
			modelDefinition = new ModelDefinition
			{
				ModelType = modelType,
				Name = modelType.Name,
				Alias = aliasAttribute != null ? aliasAttribute.Name : null,
				Schema = schemaAttribute != null ? schemaAttribute.Name : null
			};

			modelDefinition.CompositeIndexes.AddRange(modelType.GetCustomAttributes(typeof(CompositeIndexAttribute), true).ToList().ConvertAll(x => (CompositeIndexAttribute)x));

			List<PropertyInfo> objProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

			bool hasIdField = CheckForIdField(objProperties);

			for (int i = 0; i < objProperties.Count; i++)
			{
				PropertyInfo propertyInfo = objProperties[i];

				SequenceAttribute sequenceAttribute = propertyInfo.FirstAttribute<SequenceAttribute>();
				ComputeAttribute computeAttribute = propertyInfo.FirstAttribute<ComputeAttribute>();
				DecimalLengthAttribute decimalAttribute = propertyInfo.FirstAttribute<DecimalLengthAttribute>();
				BelongToAttribute belongToAttribute = propertyInfo.FirstAttribute<BelongToAttribute>();
				bool isPrimaryKey = propertyInfo.Name == OrmLiteConfig.IdField || (!hasIdField && i == 0) || propertyInfo.HasAttributeNamed(typeof(PrimaryKeyAttribute));
				bool isNullableType = IsNullableType(propertyInfo.PropertyType);
				bool isNullable = (!propertyInfo.PropertyType.IsValueType && !propertyInfo.HasAttributeNamed(typeof(RequiredAttribute))) || isNullableType;
				Type propertyType = isNullableType ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;
				aliasAttribute = propertyInfo.FirstAttribute<AliasAttribute>();
				IndexAttribute indexAttribute = propertyInfo.FirstAttribute<IndexAttribute>();
				bool isIndex = indexAttribute != null;
				bool isUnique = isIndex && indexAttribute.Unique;
				StringLengthAttribute stringLengthAttribute = propertyInfo.CalculateStringLength(decimalAttribute);
				DefaultAttribute defaultValueAttribute = propertyInfo.FirstAttribute<DefaultAttribute>();
				ReferencesAttribute referencesAttribute = propertyInfo.FirstAttribute<ReferencesAttribute>();
				//var referenceAttr = propertyInfo.FirstAttribute<ReferenceAttribute>();
				ForeignKeyAttribute foreignKeyAttribute = propertyInfo.FirstAttribute<ForeignKeyAttribute>();

				FieldDefinition fieldDefinition = new FieldDefinition();

				fieldDefinition.Name = propertyInfo.Name;
				fieldDefinition.Alias = aliasAttribute != null ? aliasAttribute.Name : null;
				fieldDefinition.FieldType = propertyType;
				fieldDefinition.PropertyInfo = propertyInfo;
				fieldDefinition.IsNullable = isNullable;
				fieldDefinition.IsPrimaryKey = isPrimaryKey;
				fieldDefinition.AutoIncrement = isPrimaryKey && propertyInfo.HasAttributeNamed(typeof(AutoIncrementAttribute));
				fieldDefinition.IsIndexed = isIndex;
				fieldDefinition.IsUnique = isUnique;
				fieldDefinition.FieldLength = stringLengthAttribute != null ? stringLengthAttribute.MaximumLength : (int?)null;
				fieldDefinition.DefaultValue = defaultValueAttribute != null ? defaultValueAttribute.DefaultValue : null;
				fieldDefinition.ForeignKey = foreignKeyAttribute == null ? referencesAttribute != null ? new ForeignKeyConstraint(referencesAttribute.Type) : null : new ForeignKeyConstraint(foreignKeyAttribute.Type, foreignKeyAttribute.OnDelete, foreignKeyAttribute.OnUpdate, foreignKeyAttribute.ForeignKeyName);
				//fieldDefinition.IsReference = referenceAttr != null && propertyType.IsClass;
				fieldDefinition.GetValueFn = propertyInfo.GetPropertyGetterFn();
				fieldDefinition.SetValueFn = propertyInfo.GetPropertySetterFn();
				fieldDefinition.Sequence = sequenceAttribute != null ? sequenceAttribute.Name : string.Empty;
				fieldDefinition.IsComputed = computeAttribute != null;
				fieldDefinition.ComputeExpression = computeAttribute != null ? computeAttribute.Expression : string.Empty;
				fieldDefinition.Scale = decimalAttribute != null ? decimalAttribute.Scale : (int?)null;
				fieldDefinition.BelongToModelName = belongToAttribute != null ? belongToAttribute.BelongToTableType.GetModelDefinition().ModelName : null;

				var isIgnored = propertyInfo.HasAttributeNamed(typeof(IgnoreAttribute)); // || fieldDefinition.IsReference;
				if (isIgnored)
				{
					modelDefinition.IgnoredFieldDefinitions.Add(fieldDefinition);
				}
				else
				{
					modelDefinition.FieldDefinitions.Add(fieldDefinition);
				}
			}

			modelDefinition.SqlSelectAllFromTable = "SELECT {0} FROM {1} ".Fmt(OrmLiteConfig.DialectProvider.GetColumnNames(modelDefinition), OrmLiteConfig.DialectProvider.GetQuotedTableName(modelDefinition));

			Dictionary<Type, ModelDefinition> snapshot, newCache;
			do
			{
				snapshot = _typeModelDefinitionMap;
				newCache = new Dictionary<Type, ModelDefinition>(_typeModelDefinitionMap);
				newCache[modelType] = modelDefinition;
			}
			while (!ReferenceEquals(Interlocked.CompareExchange(ref _typeModelDefinitionMap, newCache, snapshot), snapshot));

			return modelDefinition;
		}

		#endregion Public Methods

		#region Private Methods

		private static bool IsNullableType(Type theType)
		{
			return (theType.IsGenericType && theType.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		private static bool CheckForIdField(IEnumerable<PropertyInfo> objProperties)
		{
			// Not using Linq.Where() and manually iterating through objProperties just to avoid dependencies on System.Xml??
			foreach (var objProperty in objProperties)
			{
				if (objProperty.Name != OrmLiteConfig.IdField) continue;
				return true;
			}

			return false;
		}

		#endregion Private Methods

		public static object indexAttribute { get; set; }
	}
}
