using System.Linq;
using System.Text.RegularExpressions;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using MyACTS.Models.Entities;

namespace MyACTS.Data;

public static class IdentityDbSetExtensions {

    public static User? GetWithCredentials(this DbSet<User> set, string username, string password) {
        var User = set.SingleOrDefault(u => u.UserName == username);
        if ( User != null ) {
            if ( Crypto.VerifyHashedPassword(password, User.PasswordHash) ) {
                return User;
            } else {
                // User.AccessFailedCount += 1;
            }
        }
        return null;
    }

    public static User? AddUserWithHashedPassword(this DbSet<User> set, User user, string password) {
        set.Add(user);
        return set.SetUserPassword(user, password); ;
    }

    public static User? SetUserPassword(this DbSet<User> set, User user, string password) {
        user.PasswordHash = Crypto.HashPassword(password);
        return user;
    }

    public static bool IsValidAsPassword(this string password) {
        var passRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        return passRegex.IsMatch(password);
    }
}

