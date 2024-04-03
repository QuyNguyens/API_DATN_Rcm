using System;
using System.Collections.Generic;
using BE_Movie_Rcm.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos;

public partial class MovieDataContext : DbContext
{
    public MovieDataContext()
    {
    }

    public MovieDataContext(DbContextOptions<MovieDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblActor> TblActors { get; set; }

    public virtual DbSet<TblActorMovie> TblActorMovies { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblCountryMovie> TblCountryMovies { get; set; }

    public virtual DbSet<TblFavorite> TblFavorites { get; set; }

    public virtual DbSet<TblGenre> TblGenres { get; set; }

    public virtual DbSet<TblGenreMovie> TblGenreMovies { get; set; }

    public virtual DbSet<TblMovie> TblMovies { get; set; }

    public virtual DbSet<TblRating> TblRatings { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserMovieAccess> TblUserMovieAccesses { get; set; }

    public virtual DbSet<TblUserSub> TblUserSubs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblActor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__tbl_acto__E57403ED06FC276D");

            entity.Property(e => e.ActorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblActorMovie>(entity =>
        {
            entity.HasOne(d => d.Actor).WithMany().HasConstraintName("FK_actor_actor");

            entity.HasOne(d => d.Movie).WithMany().HasConstraintName("FK_actor_movie");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__tbl_coun__8036CB4EBEA57274");

            entity.Property(e => e.CountryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblCountryMovie>(entity =>
        {
            entity.HasOne(d => d.Country).WithMany().HasConstraintName("FK_Country_Movie");

            entity.HasOne(d => d.Movie).WithMany().HasConstraintName("FK_Movie_Country");
        });

        modelBuilder.Entity<TblFavorite>(entity =>
        {
            entity.HasOne(d => d.Movie).WithMany().HasConstraintName("FK_MovieId_favorite");

            entity.HasOne(d => d.User).WithMany().HasConstraintName("FK_favorite");
        });

        modelBuilder.Entity<TblGenre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__tbl_genr__964A2006500A68A6");

            entity.Property(e => e.GenreId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblGenreMovie>(entity =>
        {
            entity.HasOne(d => d.Genre).WithMany().HasConstraintName("FK_genre_movie");

            entity.HasOne(d => d.Movie).WithMany().HasConstraintName("FK_movie_genre");
        });

        modelBuilder.Entity<TblMovie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__tbl_movi__7A88040548BD6EF8");

            entity.Property(e => e.MovieId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblRating>(entity =>
        {
            entity.HasOne(d => d.Movie).WithMany().HasConstraintName("FK_tbl_movie");

            entity.HasOne(d => d.User).WithMany().HasConstraintName("FK_tbl_user");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_user__206D91900284FAF0");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblUserMovieAccess>(entity =>
        {
            entity.HasOne(d => d.Movie).WithMany().HasConstraintName("FK_MovieId_movieAccess");

            entity.HasOne(d => d.User).WithMany().HasConstraintName("FK_UserId_movieAccess");
        });

        modelBuilder.Entity<TblUserSub>(entity =>
        {
            entity.HasOne(d => d.User).WithMany().HasConstraintName("FK_UserId_Subs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
