namespace App_QLPK.Application.Interfaces.Services;


// public interface IPasswordHasher
// {
//     string Hash(string password);
//     bool Verify(string password, string hash);
// }


public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}