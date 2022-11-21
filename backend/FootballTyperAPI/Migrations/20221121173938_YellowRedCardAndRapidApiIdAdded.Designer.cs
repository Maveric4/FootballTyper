﻿// <auto-generated />
using System;
using FootballTyperAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FootballTyperAPI.Migrations
{
    [DbContext(typeof(FootballTyperAPIContext))]
    [Migration("20221121173938_YellowRedCardAndRapidApiIdAdded")]
    partial class YellowRedCardAndRapidApiIdAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FootballTyperAPI.Models.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AwayTeamScoreBet")
                        .HasColumnType("int");

                    b.Property<bool>("AwayTeamWin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("BetDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BetProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BetResult")
                        .HasColumnType("int");

                    b.Property<string>("BettorUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HomeAwayDrawn")
                        .HasColumnType("bit");

                    b.Property<int>("HomeTeamScoreBet")
                        .HasColumnType("int");

                    b.Property<bool>("HomeTeamWin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBetProcessed")
                        .HasColumnType("bit");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<float>("PointsFactor")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Bets", (string)null);
                });

            modelBuilder.Entity("FootballTyperAPI.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<bool>("IsMatchProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatchNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("MatchProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Referee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Stage")
                        .HasColumnType("int");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.ToTable("Match", (string)null);
                });

            modelBuilder.Entity("FootballTyperAPI.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Coach")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Drawn")
                        .HasColumnType("int");

                    b.Property<int>("GoalsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("GoalsFor")
                        .HasColumnType("int");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayedMatchesNbr")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Won")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("FootballTyperAPI.Models.TopScorerDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("Goals")
                        .HasColumnType("int");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RapidApiId")
                        .HasColumnType("int");

                    b.Property<int>("RedCards")
                        .HasColumnType("int");

                    b.Property<string>("Team")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YellowCards")
                        .HasColumnType("int");

                    b.Property<int>("YellowRedCards")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TopScorers", (string)null);
                });

            modelBuilder.Entity("FootballTyperAPI.Models.TyperUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastFiveBets")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeaguesStr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PositionStr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RankStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalCorrectWinnerBets")
                        .HasColumnType("int");

                    b.Property<int>("TotalExactScoreBets")
                        .HasColumnType("int");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("int");

                    b.Property<int>("TotalWrongBets")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TyperUser");
                });

            modelBuilder.Entity("FootballTyperAPI.Models.Bet", b =>
                {
                    b.HasOne("FootballTyperAPI.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("FootballTyperAPI.Models.Match", b =>
                {
                    b.HasOne("FootballTyperAPI.Models.Team", "AwayTeam")
                        .WithMany()
                        .HasForeignKey("AwayTeamId");

                    b.HasOne("FootballTyperAPI.Models.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamId");

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");
                });
#pragma warning restore 612, 618
        }
    }
}
