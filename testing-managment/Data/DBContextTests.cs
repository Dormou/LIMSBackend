using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using testing_managment.Models.Entities;

namespace testing_managment.Data
{
    public class DBContextTests : DbContext
    {
        //Конструктор конекста БД Тестов
        public DBContextTests(DbContextOptions<DBContextTests> opts) : base(opts)
        {
            Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", false);
        }

        //Метод конфигурирования таблиц
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dut>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Company>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<TestProgram>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<TestBlock>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<CaseInBlock>().HasKey(x => x.Case);
        }

        //Таблицы
        public DbSet<Dut> Dut => Set<Dut>();
        public DbSet<Company> Company => Set<Company>();
        public DbSet<TestProgram> TestProgram => Set<TestProgram>();
        public DbSet<TestBlock> TestBlock => Set<TestBlock>();
        public DbSet<CaseInBlock> CaseInBlock => Set<CaseInBlock>();
    }
}