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

    public virtual DbSet<TblAccessTime> TblAccessTimes { get; set; }

    public virtual DbSet<TblActor> TblActors { get; set; }

    public virtual DbSet<TblActorMovie> TblActorMovies { get; set; }

    public virtual DbSet<TblBuyVip> TblBuyVips { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblCountryMovie> TblCountryMovies { get; set; }

    public virtual DbSet<TblFavorite> TblFavorites { get; set; }

    public virtual DbSet<TblGenre> TblGenres { get; set; }

    public virtual DbSet<TblGenreMovie> TblGenreMovies { get; set; }

    public virtual DbSet<TblMovie> TblMovies { get; set; }

    public virtual DbSet<TblRating> TblRatings { get; set; }

    public virtual DbSet<TblStatistic> TblStatistics { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserMovieAccess> TblUserMovieAccesses { get; set; }

    public virtual DbSet<TblUserSub> TblUserSubs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-48QSU778\\SQLEXPRESS;Database=Movie_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccessTime>(entity =>
        {
            entity.HasKey(e => e.AccessTimeId).HasName("PK__tbl_acce__438552FA7F510D3C");

            entity.HasOne(d => d.Country).WithMany(p => p.TblAccessTimes).HasConstraintName("FK_Country_access_time");

            entity.HasOne(d => d.User).WithMany(p => p.TblAccessTimes).HasConstraintName("FK_access_time");
        });

        modelBuilder.Entity<TblActor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__tbl_acto__E57403ED06FC276D");

            entity.Property(e => e.ActorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblActorMovie>(entity =>
        {
            entity.HasKey(e => e.ActorMovieId).HasName("PK__tbl_acto__54CB92F79937CF85");

            entity.HasOne(d => d.Actor).WithMany(p => p.TblActorMovies).HasConstraintName("FK_actor_actor");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblActorMovies).HasConstraintName("FK_actor_movie");
        });

        modelBuilder.Entity<TblBuyVip>(entity =>
        {
            entity.HasKey(e => e.BuyVipId).HasName("PK__tbl_buy___D8F3BE6A9638CB01");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__tbl_coun__8036CB4E4C907968");
        });

        modelBuilder.Entity<TblCountryMovie>(entity =>
        {
            entity.HasKey(e => e.CountryMovieId).HasName("PK__tbl_coun__0F96CA818F4EFB17");

            entity.HasOne(d => d.Country).WithMany(p => p.TblCountryMovies).HasConstraintName("FK_Country_Movie");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblCountryMovies).HasConstraintName("FK_Movie_Country");
        });

        modelBuilder.Entity<TblFavorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__tbl_favo__CE74FAF5400EC3D4");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblFavorites).HasConstraintName("FK_MovieId_favorite");

            entity.HasOne(d => d.User).WithMany(p => p.TblFavorites).HasConstraintName("FK_favorite");
        });

        modelBuilder.Entity<TblGenre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__tbl_genr__964A2006500A68A6");

            entity.Property(e => e.GenreId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblGenreMovie>(entity =>
        {
            entity.HasKey(e => e.GenreMovieId).HasName("PK__tbl_genr__724599E76AD98FF2");

            entity.HasOne(d => d.Genre).WithMany(p => p.TblGenreMovies).HasConstraintName("FK_genre_movie");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblGenreMovies).HasConstraintName("FK_movie_genre");
        });

        modelBuilder.Entity<TblMovie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__tbl_movi__7A88040548BD6EF8");

            entity.Property(e => e.MovieId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__tbl_rati__FCCDF85CB56850F8");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblRatings).HasConstraintName("FK_MovieId_Rating");

            entity.HasOne(d => d.User).WithMany(p => p.TblRatings).HasConstraintName("FK_UserId_Rating");
        });

        modelBuilder.Entity<TblStatistic>(entity =>
        {
            entity.HasKey(e => e.StatisticId).HasName("PK__tbl_stat__FE4CF68F93ACD85F");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblStatistics).HasConstraintName("FK_MovieId_statistic");

            entity.HasOne(d => d.User).WithMany(p => p.TblStatistics).HasConstraintName("FK_statistic");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_user__206D91900284FAF0");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblUserMovieAccess>(entity =>
        {
            entity.HasKey(e => e.MovieAccessId).HasName("PK__tbl_user__7C14DDE018642E24");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblUserMovieAccesses).HasConstraintName("FK_MovieId_movieAccess");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserMovieAccesses).HasConstraintName("FK_UserId_movieAccess");
        });

        modelBuilder.Entity<TblUserSub>(entity =>
        {
            entity.HasKey(e => e.UserSubId).HasName("PK__tbl_user__A4B6EBBF93D75BE9");

            entity.HasOne(d => d.IsTypeNavigation).WithMany(p => p.TblUserSubs).HasConstraintName("FK_SubUser_BuyPrenium");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserSubs).HasConstraintName("FK_UserId_Subs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
