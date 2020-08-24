using Demo.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.CM
{
    public class TodoContext : DbContext
    {

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
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
