using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TS.WebComponent
{
    public class ComponentShapes : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Jumbotron");
            builder.Describe("SelectList");
        }


    }

}