using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Order {
    public class Migrations : DataMigrationImpl {

        public int Create() {
			// Creating table ItemDetailRecord
			SchemaBuilder.CreateTable("ItemDetailRecord", table => table
				.Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
				.Column("ItemName", DbType.String)
				.Column("Quantity", DbType.Int32)
				.Column("Remark", DbType.String)
				.Column("OrderRecord_id", DbType.Int32)
			);

			// Creating table OrderRecord
			SchemaBuilder.CreateTable("OrderRecord", table => table
				.Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
				.Column("CreateTime", DbType.DateTime)
				.Column("Department", DbType.String)
				.Column("Proposer", DbType.String)
			);



            return 1;
        }
    }
}