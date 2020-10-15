namespace ASM2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryIdToCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CategoryID", c => c.Int(nullable: true));
            CreateIndex("dbo.Courses", "CategoryID");
            AddForeignKey("dbo.Courses", "CategoryID", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            DropColumn("dbo.Courses", "CategoryID");
        }
    }
}