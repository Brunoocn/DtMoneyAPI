using DtMoneyAPI.Data;
using DtMoneyAPI.Interfaces;
using DtMoneyAPI.Models;
using DtMoneyAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(conectionString));
builder.Services.AddScoped<ITransactionService, TransactionService>(); //Transient, Singlenton, Scoped

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("api/transaction", (ITransactionService transactionService, SqlContext context ) => {
   var items = context.Transactions.ToList();
    return Results.Ok(items);
});

app.MapPost("api/transaction", (ITransactionService transactionService, SqlContext context,Transaction transaction) =>
{
    context.Transactions.Add(transaction);
    context.SaveChanges();
    return Results.Ok();
});

app.MapDelete("api/transactions/{id}", (ITransactionService transactionService, SqlContext context, int id) =>
{
    var item = context.Transactions.FirstOrDefault(item => item.Id == id);
    context.Transactions.Remove(item);
    context.SaveChanges();
    return Results.Ok();
}
);

app.MapPut("api/transactions/{id}", (ITransactionService transactionService, SqlContext context, int id, Transaction transaction) => 
{
    var item = context.Transactions.AsNoTracking().FirstOrDefault(item => item.Id == id);
    if (item == null)
    {
        return Results.NotFound();
    }
    else
    {
        context.Transactions.Update(transaction);
        context.SaveChanges();
        return Results.Ok();
    }
});



app.Run();
