

// using App_QLPK.Infrastructure.Persistence;

// namespace App_QLPK.Infrastructure.Repositories;

// public class UnitOfWork
// {
//     public readonly QlpkDbContext _context;

//     public UnitOfWork(QlpkDbContext context)
//     {
//         _context = context;
//     }


//     public async Task BeginTransactionAsync()
//     {
//         await _context.Database.BeginTransactionAsync();
//     }

//     public async Task CommitTransaction()
//     {
//         await _context.Database.CommitTransactionAsync();
//     }

//     public async Task RollBackTransaction()
//     {
//         await _context.Database.RollbackTransactionAsync();
//     }

//     public async Task SaveChangesAsync()
//     {
//         await _context.SaveChangesAsync();
//     }
// }