﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripGuide.Data;

#nullable disable

namespace TripGuide.Migrations
{
    [DbContext(typeof(TripGuideDbContext))]
    partial class TripGuideDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ProviderKey")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("RoleId")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("LoginProvider")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

            modelBuilder.Entity("TripGuide.Models.BlogPost", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Content")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("FeaturedImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PageTitle")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("PublishedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("ShortDescription")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("TouristObjectId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("UrlHandle")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("UserId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("TouristObjectId");

                b.ToTable("BlogPosts");
            });

            modelBuilder.Entity("TripGuide.Models.Review", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("BlogPostId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Content")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DateAdded")
                    .HasColumnType("datetime2");

                b.Property<int>("Rating")
                    .HasColumnType("int");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("BlogPostId");

                b.ToTable("Reviews");
            });

            modelBuilder.Entity("TripGuide.Models.Tag", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("BlogPostId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("BlogPostId");

                b.ToTable("Tags");
            });

            modelBuilder.Entity("TripGuide.Models.TouristObject", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<TimeSpan>("ClosingTime")
                    .HasColumnType("time");

                b.Property<double>("Latitude")
                    .HasColumnType("float");

                b.Property<double>("Longitude")
                    .HasColumnType("float");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<TimeSpan>("OpeningTime")
                    .HasColumnType("time");

                b.HasKey("Id");

                b.ToTable("TouristObjects");
            });

            modelBuilder.Entity("TripGuide.Models.TripRoute", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");
                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");
                b.HasKey("Id");
                b.ToTable("TripRoutes");
            });

            modelBuilder.Entity("TripGuide.Models.TripRouteTouristObject", b =>
            {
                b.Property<Guid>("TripRouteId")
                    .HasColumnType("uniqueidentifier");
                b.Property<Guid>("TouristObjectId")
                    .HasColumnType("uniqueidentifier");
                b.HasKey("TripRouteId", "TouristObjectId");
                b.HasIndex("TouristObjectId");
                b.ToTable("TripRouteTouristObjects");
            });

            modelBuilder.Entity("TripGuide.Models.User", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("AvatarImageUrl")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("bit");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("bit");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("bit");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("TripGuide.Models.UserBlogPost", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<Guid>("BlogPostId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "BlogPostId");

                b.HasIndex("BlogPostId");

                b.ToTable("UserBlogPosts");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("TripGuide.Models.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("TripGuide.Models.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("TripGuide.Models.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("TripGuide.Models.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("TripGuide.Models.BlogPost", b =>
            {
                b.HasOne("TripGuide.Models.TouristObject", "TouristObject")
                    .WithMany()
                    .HasForeignKey("TouristObjectId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("TouristObject");
            });

            modelBuilder.Entity("TripGuide.Models.Review", b =>
            {
                b.HasOne("TripGuide.Models.BlogPost", null)
                    .WithMany("Reviews")
                    .HasForeignKey("BlogPostId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("TripGuide.Models.Tag", b =>
            {
                b.HasOne("TripGuide.Models.BlogPost", null)
                    .WithMany("Tags")
                    .HasForeignKey("BlogPostId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("TripGuide.Models.TripRouteTouristObject", b =>
            {
                b.HasOne("TripGuide.Models.TouristObject", "TouristObject")
                    .WithMany("TripRouteTouristObjects")
                    .HasForeignKey("TouristObjectId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                b.HasOne("TripGuide.Models.TripRoute", "TripRoute")
                    .WithMany("TripRouteTouristObjects")
                    .HasForeignKey("TripRouteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                b.Navigation("TouristObject");
                b.Navigation("TripRoute");
            });

            modelBuilder.Entity("TripGuide.Models.UserBlogPost", b =>
            {
                b.HasOne("TripGuide.Models.BlogPost", "BlogPost")
                    .WithMany("UserBlogPosts")
                    .HasForeignKey("BlogPostId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("TripGuide.Models.User", "User")
                    .WithMany("UserBlogPosts")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("BlogPost");

                b.Navigation("User");
            });

            modelBuilder.Entity("TripGuide.Models.BlogPost", b =>
            {
                b.Navigation("Reviews");

                b.Navigation("Tags");

                b.Navigation("UserBlogPosts");
            });

            modelBuilder.Entity("TripGuide.Models.TouristObject", b =>
            {
                b.Navigation("TripRouteTouristObjects");
            });

            modelBuilder.Entity("TripGuide.Models.TripRoute", b =>
            {
                b.Navigation("TripRouteTouristObjects");
            });

            modelBuilder.Entity("TripGuide.Models.User", b =>
            {
                b.Navigation("UserBlogPosts");
            });
#pragma warning restore 612, 618
        }
    }
}
