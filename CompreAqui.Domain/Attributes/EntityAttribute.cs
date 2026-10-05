using System;

namespace CompreAqui.Domain.Attributes
{
    public sealed class EntityAttribute : Attribute
    {
        public EntityAttribute()
        {
        }

        public EntityAttribute(string collectionName, string displayName = null, bool clearCollection = true)
        {
            string str = collectionName;
            this.CollectionName = str ?? throw new ArgumentNullException(nameof(collectionName));
            this.DisplayName = displayName;
            this.ClearCollection = clearCollection;
        }

        public bool ClearCollection { get; }

        public string CollectionName { get; }

        public string DisplayName { get; }
    }
}
