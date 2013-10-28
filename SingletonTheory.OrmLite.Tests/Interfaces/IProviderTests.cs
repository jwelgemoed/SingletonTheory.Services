namespace SingletonTheory.OrmLite.Tests.Interfaces
{
	interface IProviderTests
	{
		void ShouldClearLookup();
		void ShouldClearAllCollections();
		void ShouldCreateCollections();
		void ShouldDeleteAll();
		void ShouldDeleteShipper();
		void ShouldDropAndCreate();
		void ShouldHaveSetDialectProvider();
		void ShouldInsertShipper();
		void ShouldNotClearLookup();
		void ShouldSelectShipperAndTree();
		void ShouldSelectShipperAndTreeWithExpression();
		void ShouldThrowArgumentNullExceptionForNullConnectionString();
		void ShouldUpdateShipper();
	}
}
