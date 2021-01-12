using m183_shovel_knight_security.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace m183_shovel_knight_security.Data
{
    public class Seeder
    {
        private readonly Guid _postholderGuid = new Guid("8CC9DD4B-87A6-4690-9872-EC8A1C4F4446");
        public void SeedRoles(ModelBuilder modelBuilder)
        {
            List<Role> rList = new List<Role>()
            {
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" },
            };

            modelBuilder.Entity<Role>().HasData(rList);
        }

        public void SeedUsers(ModelBuilder modelBuilder)
        {
            string password = "123";
            byte[] passwordSalt;
            byte[] passwordHash;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            List <User> uList = new List<User>()
            {
                new User {Id = Guid.NewGuid(), Nickname = "vinny", PasswordHash = passwordHash, PasswordSalt = passwordSalt, RoleId = 2 },
                new User {Id = Guid.NewGuid(), Nickname = "timo", PasswordHash = passwordHash, PasswordSalt = passwordSalt, RoleId = 2 },
                new User {Id = Guid.NewGuid(), Nickname = "test", PasswordHash = passwordHash, PasswordSalt = passwordSalt, RoleId = 1 },
                new User {Id = _postholderGuid, Nickname = "postholder", PasswordHash = passwordHash, PasswordSalt = passwordSalt, RoleId = 1 },
            };

            modelBuilder.Entity<User>().HasData(uList);
        }

        public void SeedPosts(ModelBuilder modelBuilder)
        {

            List<Post> pList = new List<Post>()
            {
                new Post {Id = Guid.NewGuid(), Text = "My new Plague Knight Fanart!", ImageUrl = "https://static.wikia.nocookie.net/shovelknight/images/0/09/Plague_Knight_Treasure_Trove.png/revision/latest/scale-to-width-down/700?cb=20180901211615", UserId = _postholderGuid },
                new Post {Id = Guid.NewGuid(), Text = "A practitioner of the ancient code of Shovelry, Shovel Knight!", ImageUrl = "https://vignette.wikia.nocookie.net/shovelknight/images/b/b8/Shovel_Knight_Treasure_Trove.png/revision/latest/scale-to-width-down/300?cb=20180901211211", UserId = _postholderGuid },
                new Post {Id = Guid.NewGuid(), Text = "The Enchantress is really badass!", ImageUrl = "https://vignette.wikia.nocookie.net/shovelknight/images/7/71/The_Enchantress_Treasure_Trove.png/revision/latest/scale-to-width-down/300?cb=20180901211400", UserId = _postholderGuid },
                new Post {Id = Guid.NewGuid(), Text = "Direct from the dead :)", ImageUrl = "https://static.wikia.nocookie.net/shovelknight/images/2/25/Specter_Knight_Treasure_Trove.png/revision/latest/scale-to-width-down/700?cb=20180901211532", UserId = _postholderGuid },
                new Post {Id = Guid.NewGuid(), Text = "My fanmade showdown tittle screen", ImageUrl = "https://static.wikia.nocookie.net/shovelknight/images/6/6f/Slideshow-4.jpg/revision/latest/scale-to-width-down/670?cb=20190404005418", UserId = _postholderGuid },
                new Post {Id = Guid.NewGuid(), Text = "Even if Propeller Knight is strong, Timo always loses. xD", ImageUrl = "https://vignette.wikia.nocookie.net/shovelknight/images/2/29/Propeller_Knight_Treasure_Trove.png/revision/latest/scale-to-width-down/300?cb=20180901211840", UserId = _postholderGuid },
                new Post {Id = Guid.NewGuid(), Text = "Animated fish ^^", ImageUrl = "https://static.wikia.nocookie.net/shovelknight/images/d/d3/Lunkeroth-630x147.gif/revision/latest?cb=20180215090938", UserId = _postholderGuid },
            };

            modelBuilder.Entity<Post>().HasData(pList);
        }

    }
}
