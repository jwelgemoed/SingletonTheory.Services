using SingletonTheory.Library.IO;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using System.Collections.Generic;
using System.IO;

namespace SingletonTheory.Services.AuthServices.Data
{
	public static class PermissionData
	{
		public static void CreatePermissions(string filePath)
		{
			FileUtilities.AssertDirectoryExists(filePath);

			GenericRepository.ClearCollection(ConfigSettings.MongoAuthAdminDatabaseName);
			string[] fileNames = Directory.GetFiles(filePath);

			for (int i = 0; i < fileNames.Length; i++)
			{
				FileUtilities.AssertFileExists(fileNames[i]);
				string fileName = fileNames[i];
				if (fileName.Contains("DomainPermissionEntity"))
				{
					AddCollection<DomainPermissionEntity>(fileName, GenericRepository.DomainPermissionsCollection);
				}
				else if (fileName.Contains("FunctionalPermissionEntity"))
				{
					AddCollection<FunctionalPermissionEntity>(fileName, GenericRepository.FunctionalPermissionsCollection);
				}
				else if (fileName.Contains("PermissionEntity"))
				{
					AddCollection<PermissionEntity>(fileName, GenericRepository.PermissionsCollection);
				}
				else if (fileName.Contains("RoleEntity"))
				{
					AddCollection<RoleEntity>(fileName, GenericRepository.RolesCollection);
				}
			}
		}

		private static void AddCollection<T>(string fileName, string collectionName)
		{
			List<T> entities = SerializationUtilities.ReadFile<List<T>>(fileName);
			for (int i = 0; i < entities.Count; i++)
			{
				GenericRepository.Add<T>(ConfigSettings.MongoAuthAdminDatabaseName, collectionName, entities[i], false);
			}
		}
	}
}