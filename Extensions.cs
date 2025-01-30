public static class Extensions
{
    public static void AddEntityWithNullIds<T>(this DbContext context, T obj)
    {
        context.ChangeTracker.TrackGraph(obj!, node =>
        {
            var entry = node.Entry;

            var pk = entry.Metadata.FindPrimaryKey()?
                .Properties.Select(n => n.Name).FirstOrDefault();

            if (pk == null)
                return;

            var entity = entry.Entity;

            var idProperty = entity.GetType().GetProperty(pk);
            if (idProperty != null && idProperty.CanWrite)
                idProperty.SetValue(entity, default);

            entry.State = EntityState.Added;
        });
    }
}
