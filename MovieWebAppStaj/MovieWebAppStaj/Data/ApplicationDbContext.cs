using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWebAppStaj.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebAppStaj.Data
{
    public class ApplicationDbContext : IdentityDbContext<MoviesUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MoviesActor { get; set; }
        public DbSet<MoviesUser> MovieUsers { get; set; }
    }
}
