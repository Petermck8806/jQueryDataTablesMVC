namespace jQueryDataTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSexColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Sex", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Sex");
        }
    }
}
