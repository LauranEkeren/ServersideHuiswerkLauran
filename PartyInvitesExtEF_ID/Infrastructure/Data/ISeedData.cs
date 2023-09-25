namespace Infrastructure.Data;

public interface ISeedData
{
    Task EnsurePopulated(bool dropExisting = false);
}