namespace DatabaseAnalysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bases",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                        Server = c.String(),
                        BaseName = c.String(),
                        UserDB = c.String(),
                        PasswordDB = c.String(),
                        URI = c.String(),
                        User = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.BaseStructures",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Base_Code = c.Int(),
                        StructureDB_Code = c.Int(),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.Bases", t => t.Base_Code)
                .ForeignKey("dbo.BaseStructureDBs", t => t.StructureDB_Code)
                .Index(t => t.Base_Code)
                .Index(t => t.StructureDB_Code);
            
            CreateTable(
                "dbo.BaseStructureDBs",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        TableName = c.String(),
                        StorageTableName = c.String(),
                        Metadata = c.String(),
                        Purpose = c.String(),
                        SizeTable = c.Long(nullable: false),
                        CountRecords = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        StorageFieldName = c.String(),
                        FieldName = c.String(),
                        Metadata = c.String(),
                        BaseStructureDB_Code = c.Int(),
                        Indexes_Code = c.Int(),
                        StructureDB_Code = c.Int(),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.BaseStructureDBs", t => t.BaseStructureDB_Code)
                .ForeignKey("dbo.Indexes", t => t.Indexes_Code)
                .ForeignKey("dbo.StructureDBs", t => t.StructureDB_Code)
                .Index(t => t.BaseStructureDB_Code)
                .Index(t => t.Indexes_Code)
                .Index(t => t.StructureDB_Code);
            
            CreateTable(
                "dbo.Indexes",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        StorageIndexName = c.String(),
                        BaseStructureDB_Code = c.Int(),
                        StructureDB_Code = c.Int(),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.BaseStructureDBs", t => t.BaseStructureDB_Code)
                .ForeignKey("dbo.StructureDBs", t => t.StructureDB_Code)
                .Index(t => t.BaseStructureDB_Code)
                .Index(t => t.StructureDB_Code);
            
            CreateTable(
                "dbo.StructureDBs",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        StorageTableName = c.String(),
                        TableName = c.String(),
                        Metadata = c.String(),
                        Purpose = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indexes", "StructureDB_Code", "dbo.StructureDBs");
            DropForeignKey("dbo.Fields", "StructureDB_Code", "dbo.StructureDBs");
            DropForeignKey("dbo.Indexes", "BaseStructureDB_Code", "dbo.BaseStructureDBs");
            DropForeignKey("dbo.Fields", "Indexes_Code", "dbo.Indexes");
            DropForeignKey("dbo.Fields", "BaseStructureDB_Code", "dbo.BaseStructureDBs");
            DropForeignKey("dbo.BaseStructures", "StructureDB_Code", "dbo.BaseStructureDBs");
            DropForeignKey("dbo.BaseStructures", "Base_Code", "dbo.Bases");
            DropIndex("dbo.Indexes", new[] { "StructureDB_Code" });
            DropIndex("dbo.Indexes", new[] { "BaseStructureDB_Code" });
            DropIndex("dbo.Fields", new[] { "StructureDB_Code" });
            DropIndex("dbo.Fields", new[] { "Indexes_Code" });
            DropIndex("dbo.Fields", new[] { "BaseStructureDB_Code" });
            DropIndex("dbo.BaseStructures", new[] { "StructureDB_Code" });
            DropIndex("dbo.BaseStructures", new[] { "Base_Code" });
            DropTable("dbo.StructureDBs");
            DropTable("dbo.Indexes");
            DropTable("dbo.Fields");
            DropTable("dbo.BaseStructureDBs");
            DropTable("dbo.BaseStructures");
            DropTable("dbo.Bases");
        }
    }
}
