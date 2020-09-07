using Demo.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Model.CM
{
    public class TodoContext : DbContext
    {

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            //this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();
            foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
            {
                this.AddRange(entity.Entity);
            }
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public virtual DbSet<Xianlu> Xianlu { get; set; }
        public virtual DbSet<Biaoduan> Biaoduan { get; set; }
        public virtual DbSet<Gongdian> Gongdian { get; set; }
        public virtual DbSet<Jianli> Jianli { get; set; }
        public virtual DbSet<JianliEnclosure> JianliEnclosure { get; set; }
        public virtual DbSet<Jianshe> Jianshe { get; set; }
        public virtual DbSet<JiansheEnclosure> JiansheEnclosure { get; set; }
        public virtual DbSet<Shigong> Shigong { get; set; }
        public virtual DbSet<ShigongEnclosure> ShigongEnclosure { get; set; }
        public virtual DbSet<ShigongImages> ShigongImages { get; set; }
    }
}
