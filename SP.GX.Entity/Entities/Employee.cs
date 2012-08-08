using System;
using System.Collections.Generic;
using System.Text;
// SPGenesis
using SPGenesis.Core;
using SPGenesis.Entities;
// SharePoint 14
using Microsoft.SharePoint;
// GenerationX
using SP.GX.Library.Generation;

namespace SP.GX.Entities
{
    [Entity(ContentTypeId = "{571AE8B9-ABD5-4984-954D-DBBF3B239FB3}")]
    public class Employee
    {
        [EntityIdentity]
        public int EmployeeID { get; set; }

        [EntityFieldText(ID = "{9cc801eb-25e9-4253-9af5-c9119bc56576}")]
        public string FirstName { get; set; }

        [EntityFieldText(ID = "{a315d21a-6305-4db5-9e14-8893698dd3d2}")]
        public string LastName { get; set; }

        [EntityFieldDateTime(ID = "{23091343-57ae-4c67-abe3-73a26bbcdb04}")]
        public DateTime BrithDate { get; set; }

        public int Age { get { return DateTime.Today.Year - this.BrithDate.Date.Year; } }
    }
}