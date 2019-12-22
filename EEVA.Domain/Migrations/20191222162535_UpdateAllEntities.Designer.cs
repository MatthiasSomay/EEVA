﻿// <auto-generated />
using System;
using EEVA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EEVA.Domain.Migrations
{
    [DbContext(typeof(EEVAContext))]
    [Migration("20191222162535_UpdateAllEntities")]
    partial class UpdateAllEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EEVA.Domain.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Contact");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseYear")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionPhrase")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("EEVA.Domain.Models.StudentExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ExamId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentExams");
                });

            modelBuilder.Entity("EEVA.Domain.Models.StudentExamAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("StudentExamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentExamId");

                    b.ToTable("StudentExamAnswers");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Student", b =>
                {
                    b.HasBaseType("EEVA.Domain.Models.Contact");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Teacher", b =>
                {
                    b.HasBaseType("EEVA.Domain.Models.Contact");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Answer", b =>
                {
                    b.HasOne("EEVA.Domain.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Exam", b =>
                {
                    b.HasOne("EEVA.Domain.Models.Course", "Course")
                        .WithMany("Exams")
                        .HasForeignKey("CourseId");

                    b.HasOne("EEVA.Domain.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("EEVA.Domain.Models.Question", b =>
                {
                    b.HasOne("EEVA.Domain.Models.Course", null)
                        .WithMany("Questions")
                        .HasForeignKey("CourseId");

                    b.HasOne("EEVA.Domain.Models.Exam", null)
                        .WithMany("ExamQuestions")
                        .HasForeignKey("ExamId");
                });

            modelBuilder.Entity("EEVA.Domain.Models.StudentExam", b =>
                {
                    b.HasOne("EEVA.Domain.Models.Exam", "Exam")
                        .WithMany("StudentExams")
                        .HasForeignKey("ExamId");

                    b.HasOne("EEVA.Domain.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("EEVA.Domain.Models.StudentExamAnswer", b =>
                {
                    b.HasOne("EEVA.Domain.Models.StudentExam", null)
                        .WithMany("StudentExamAnswers")
                        .HasForeignKey("StudentExamId");
                });
#pragma warning restore 612, 618
        }
    }
}
