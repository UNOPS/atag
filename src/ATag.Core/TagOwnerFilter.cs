namespace ATag.Core
{
	public struct TagOwnerFilter
	{
		public TagOwnerFilter(int ownerType, string ownerId)
		{
			this.OwnerType = ownerType;
			this.OwnerId = ownerId;
		}

		public int OwnerType { get; }
		public string OwnerId { get; }
	}
}