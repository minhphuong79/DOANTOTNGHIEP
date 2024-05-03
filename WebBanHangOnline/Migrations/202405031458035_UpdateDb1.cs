namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Category", "SeoTitle");
            DropColumn("dbo.Category", "SeoDescription");
            DropColumn("dbo.Category", "SeoKeywords");
            DropColumn("dbo.News", "SeoTitle");
            DropColumn("dbo.News", "SeoDescription");
            DropColumn("dbo.News", "SeoKeywords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "SeoKeywords", c => c.String());
            AddColumn("dbo.News", "SeoDescription", c => c.String());
            AddColumn("dbo.News", "SeoTitle", c => c.String());
            AddColumn("dbo.Category", "SeoKeywords", c => c.String(maxLength: 150));
            AddColumn("dbo.Category", "SeoDescription", c => c.String(maxLength: 250));
            AddColumn("dbo.Category", "SeoTitle", c => c.String(maxLength: 150));
        }
    }
}
