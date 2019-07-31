namespace movieLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieModelUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "MovieId", "dbo.Movies");
            DropIndex("dbo.Files", new[] { "MovieId" });
            DropTable("dbo.Files");
        }
    }
}
