```
public async static Task CopyEntity(Guid id)
  {
      using var context = new SampleDbContext();

      var invoice = await context.Invoices
          .Include(n => n.InvoiceItems)
          .AsNoTracking()
          .FirstOrDefaultAsync(n => n.Id == id);

      if (invoice != null)
      {
          context.AddEntityWithNullIds(invoice);
          await context.SaveChangesAsync();
      }
  }
```
