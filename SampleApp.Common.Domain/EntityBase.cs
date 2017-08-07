namespace SampleApp.Common.Domain
{
    public abstract class EntityBase<TIdType> : IAggregateRoot
    {
        public TIdType Id { get; set; }


        public override bool Equals(object entity)
        {
            var tmp = entity as EntityBase<TIdType>;
            return (tmp != null) && (this == tmp);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public static bool operator ==(EntityBase<TIdType> entity1, EntityBase<TIdType> entity2)
        {
            if (((object) entity1 == null) && ((object) entity2 == null))
            {
                return true;
            }

            if (((object) entity1 == null) || ((object) entity2 == null))
            {
                return false;
            }

            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityBase<TIdType> entity1, EntityBase<TIdType> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}