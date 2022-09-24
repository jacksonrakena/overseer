﻿// <auto-generated />
using System;
using Abyss.Persistence.Document;
using Abyss.Persistence.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Abyss.Migrations
{
    [DbContext(typeof(AbyssDatabaseContext))]
    partial class AbyssDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Abyss.Persistence.Relational.BlackjackGameRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Adjustment")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("DateGameFinish")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DealerCards")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DidPlayerDoubleDown")
                        .HasColumnType("boolean");

                    b.Property<decimal>("PlayerBalanceAfterGame")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PlayerBalanceBeforeGame")
                        .HasColumnType("numeric");

                    b.Property<string>("PlayerCards")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PlayerFinalBet")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PlayerId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("PlayerInitialBet")
                        .HasColumnType("numeric");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("bj_games");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.DocumentRecord<ulong, Abyss.Persistence.Document.GuildConfig>", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<GuildConfig>("Data")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("GuildConfigurations");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.Reminder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("CreatorId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("DueAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("MessageId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("reminders");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCurrencyCreated")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCurrencyDestroyed")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PayeeBalanceAfterTransaction")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PayeeBalanceBeforeTransaction")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PayeeId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("PayerBalanceAfterTransaction")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PayerBalanceBeforeTransaction")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PayerId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.TriviaCategoryVoteRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TimesPicked")
                        .HasColumnType("integer");

                    b.Property<decimal>("TriviaRecordId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("TriviaRecordId");

                    b.ToTable("trivia_category_votes");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.TriviaRecord", b =>
                {
                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("CorrectAnswers")
                        .HasColumnType("integer");

                    b.Property<int>("IncorrectAnswers")
                        .HasColumnType("integer");

                    b.Property<int>("TotalMatches")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("trivia_records");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.UserAccount", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("BadgesString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Coins")
                        .HasColumnType("numeric");

                    b.Property<int>("ColorB")
                        .HasColumnType("integer");

                    b.Property<int>("ColorG")
                        .HasColumnType("integer");

                    b.Property<int>("ColorR")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTimeOffset?>("FirstInteraction")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("LatestInteraction")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.TriviaCategoryVoteRecord", b =>
                {
                    b.HasOne("Abyss.Persistence.Relational.TriviaRecord", "TriviaRecord")
                        .WithMany("CategoryVoteRecords")
                        .HasForeignKey("TriviaRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TriviaRecord");
                });

            modelBuilder.Entity("Abyss.Persistence.Relational.TriviaRecord", b =>
                {
                    b.Navigation("CategoryVoteRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
