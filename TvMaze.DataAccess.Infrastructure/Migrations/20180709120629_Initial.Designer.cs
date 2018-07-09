﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TvMaze.DataAccess;

namespace TvMaze.DataAccess.Infrastructure.Migrations
{
    [DbContext(typeof(TvMazeDbContext))]
    [Migration("20180709120629_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TvMaze.DataAccess.Domain.Entities.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Name");

                    b.Property<int>("TvMazeId");

                    b.HasKey("ActorId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TvMaze.DataAccess.Domain.Entities.ActorTvShow", b =>
                {
                    b.Property<int>("ActorId");

                    b.Property<int>("TvShowId");

                    b.HasKey("ActorId", "TvShowId");

                    b.HasIndex("TvShowId");

                    b.ToTable("ActorTvShows");
                });

            modelBuilder.Entity("TvMaze.DataAccess.Domain.Entities.TvShow", b =>
                {
                    b.Property<int>("TvShowId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("TvMazeId");

                    b.HasKey("TvShowId");

                    b.ToTable("TvShows");
                });

            modelBuilder.Entity("TvMaze.DataAccess.Domain.Entities.ActorTvShow", b =>
                {
                    b.HasOne("TvMaze.DataAccess.Domain.Entities.Actor", "Actor")
                        .WithMany("ActorTvShows")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TvMaze.DataAccess.Domain.Entities.TvShow", "TvShow")
                        .WithMany("ActorTvShows")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
