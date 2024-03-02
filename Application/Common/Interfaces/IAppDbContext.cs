namespace Application;

public interface IAppDbContext
{ 
    Task<int> SaveChangesAsync();
}