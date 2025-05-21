#region Enbrea - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    Enbrea
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 */
#endregion

using System.Data.Common;

namespace Enbrea.SchildNRW.Db
{
    /// <summary>
    /// An entity within the SchildNRW database table "versetzung"
    /// </summary>
    public class SchoolClass
    {
        public string AlternativeTeacher { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string NextSchoolClass { get; set; }
        public string PreviousSchoolClass { get; set; }
        public string SchoolClassOrganisation { get; set; }
        public string SchoolClassType { get; set; }
        public int? SchoolYearId { get; set; }
        public string Teacher { get; set; }

        public static SchoolClass FromDb(DbDataReader reader)
        {
            return new SchoolClass
            {
                Id = reader.GetValue<int>("ID"),
                Name = reader.GetValue<string>("Bezeichnung"),
                Code = reader.GetValue<string>("Klasse"),
                SchoolYearId = reader.GetValue<int>("Jahrgang_ID"),
                NextSchoolClass = reader.GetValue<string>("FKlasse"),
                PreviousSchoolClass = reader.GetValue<string>("VKlasse"),
                SchoolClassOrganisation = reader.GetValue<string>("OrgFormKrz"),
                Teacher = reader.GetValue<string>("KlassenlehrerKrz"),
                AlternativeTeacher = reader.GetValue<string>("StvKlassenlehrerKrz"),
                SchoolClassType = reader.GetValue<string>("Klassenart")
            };
        }
    }
}
