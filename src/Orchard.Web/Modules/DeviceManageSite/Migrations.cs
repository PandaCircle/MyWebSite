using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace DeviceManageSite {
    public class Migrations : DataMigrationImpl {

        public int Create() {

            SchemaBuilder.CreateTable("ResourceRecord", table => table
            .Column<int>("Id", column => column.PrimaryKey().Identity())
            .Column<string>("Content")
            .Column<int>("AttachUnit")
            .Column<int>("ResourceType_Id")
            );

            SchemaBuilder.CreateTable("ResourceTypeRecord", table => table
            .Column<int>("Id", column => column.PrimaryKey().Identity())
            .Column<string>("DisplayName")
            );

            SchemaBuilder.CreateTable("ClsResRecord", table => table
             .Column<int>("Id", column => column.PrimaryKey().Identity())
             .Column<int>("Resource_Id")
             .Column<int>("Classify_Id")
            );

            SchemaBuilder.CreateTable("ClassifyRecord", table => table
             .Column<int>("Id", column => column.PrimaryKey().Identity())
             .Column<string>("ClsName")
             .Column<int>("ResourceType_Id")
            );
            

            return 1;
        }
    }
}