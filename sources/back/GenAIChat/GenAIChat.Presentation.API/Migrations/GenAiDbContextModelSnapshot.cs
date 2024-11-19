﻿// <auto-generated />
using System;
using GenAIChat.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GenAIChat.Presentation.API.Migrations
{
    [DbContext(typeof(GenAiDbContext))]
    partial class GenAiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("GenAIChat.Domain.Document.DocumentDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("MetadataId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MetadataId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("GenAIChat.Domain.Document.DocumentMetadataDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DocumentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sha256Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("SizeBytes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Metadatas", (string)null);
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.ProjectDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prompt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PromptResponseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PromptResponseId");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.UserStoryDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("UserStories", (string)null);
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.UserStoryTaskDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Cost")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserStoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserStoryId");

                    b.ToTable("Tasks", (string)null);
                });

            modelBuilder.Entity("GenAIChat.Domain.Prompt.PromptDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Payload")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Prompts", (string)null);
                });

            modelBuilder.Entity("GenAIChat.Domain.Document.DocumentDomain", b =>
                {
                    b.HasOne("GenAIChat.Domain.Document.DocumentMetadataDomain", "Metadata")
                        .WithMany()
                        .HasForeignKey("MetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenAIChat.Domain.Project.ProjectDomain", null)
                        .WithMany("Documents")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.ProjectDomain", b =>
                {
                    b.HasOne("GenAIChat.Domain.Prompt.PromptDomain", "PromptResponse")
                        .WithMany()
                        .HasForeignKey("PromptResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PromptResponse");
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.UserStoryDomain", b =>
                {
                    b.HasOne("GenAIChat.Domain.Project.ProjectDomain", null)
                        .WithMany("UserStories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.UserStoryTaskDomain", b =>
                {
                    b.HasOne("GenAIChat.Domain.Project.UserStoryDomain", null)
                        .WithMany("Tasks")
                        .HasForeignKey("UserStoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.ProjectDomain", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("UserStories");
                });

            modelBuilder.Entity("GenAIChat.Domain.Project.UserStoryDomain", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
