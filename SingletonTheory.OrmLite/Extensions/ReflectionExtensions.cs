using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using SingletonTheory.OrmLite.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SingletonTheory.OrmLite.Extensions
{
	public static class ReflectionExtensions
	{
		public static bool HasAttributeNamed(this Type typeToCheck, Type attributeType)
		{
			object[] attributes = typeToCheck.GetCustomAttributes(true);
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].GetType().Equals(attributeType))
					return true;
			}

			return false;
		}

		public static bool HasAttributeNamed(this PropertyInfo typeToCheck, Type attributeType)
		{
			object[] attributes = typeToCheck.GetCustomAttributes(true);
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].GetType().Equals(attributeType))
					return true;
			}

			return false;
		}

		public static bool IsListType(this Type typeToCheck)
		{
			if (!typeToCheck.IsGenericType)
				return false;

			return typeToCheck.HasInterface(typeof(IList<>));

			//Type[] interfaces = typeToCheck.GetInterfaces();
			//for (int i = 0; i < interfaces.Length; i++)
			//{
			//	if (interfaces[i].GetGenericTypeDefinition() == typeof(IList<>))
			//		return true;
			//}

			//return false;
		}

		public static bool IsChildType(this Type typeToCheck)
		{
			if (typeToCheck.IsGenericType)
			{
				Type genericType = typeToCheck.GetGenericType();

				return genericType.HasInterface(typeof(IChildEntity<>));
			}

			return typeToCheck.HasInterface(typeof(IChildEntity<>));
		}

		public static bool HasInterface(this Type typeToCheck, Type interfaceType)
		{
			Type[] interfaces = typeToCheck.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (interfaces[i].IsGenericType && interfaces[i].GetGenericTypeDefinition() == interfaceType)
					return true;
			}

			return false;
		}

		public static bool HasAttribute(this FieldDefinition fieldDefinition, Type attributeType)
		{
			return fieldDefinition.PropertyInfo.HasAttributeNamed(attributeType);
		}

		public static Type GetGenericType(this Type typeToCheck)
		{
			if (typeToCheck.IsGenericType)
				return typeToCheck.GetGenericArguments()[0];

			throw new InvalidOperationException("The type to check is not a Generic type.");
		}

		public static void CallGenericMethod(this object instance, string methodName, Type genericType, object[] parameters)
		{
			MethodInfo method = instance.GetType().GetMethod(methodName);
			MethodInfo genericMethod = method.MakeGenericMethod(genericType);
			genericMethod.Invoke(instance, parameters);
		}

		public static IEnumerable CallGenericSelectMethod(this object instance, string methodName, Type genericType, params object[] parameters)
		{
			MethodInfo method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			MethodInfo genericMethod = method.MakeGenericMethod(genericType);

			return genericMethod.Invoke(instance, parameters) as IEnumerable;
		}

		public static StringLengthAttribute CalculateStringLength(this PropertyInfo propertyInfo, DecimalLengthAttribute decimalAttribute)
		{
			var attr = propertyInfo.FirstAttribute<StringLengthAttribute>();
			if (attr != null) return attr;

			var componentAttr = propertyInfo.FirstAttribute<StringLengthAttribute>();
			if (componentAttr != null)
				return new StringLengthAttribute(componentAttr.MaximumLength);

			return decimalAttribute != null ? new StringLengthAttribute(decimalAttribute.Precision) : null;
		}
	}
}
