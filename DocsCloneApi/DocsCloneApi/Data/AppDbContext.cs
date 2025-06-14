﻿using Microsoft.EntityFrameworkCore;
using DocsCloneApi.Models;

namespace DocsCloneApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }
    }
}
