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
    /// An entity within the SchildNRW database table "kurse"
    /// </summary>
    public class Course
    {
        public short CalendarYear { get; set; }
        public string CourseCategory { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public short SchoolTerm { get; set; }
        public int? SchoolYear { get; set; }
        public int SubjectId { get; set; }
        public string Teacher { get; set; }

        public static Course FromDb(DbDataReader reader)
        {
            return new Course
            {
                Id = reader.GetValue<int>("ID"),
                CalendarYear = reader.GetValue<short>("Jahr"),
                SchoolTerm = reader.GetValue<short>("Abschnitt"),
                Name = reader.GetValue<string>("KurzBez"),
                SchoolYear = reader.GetValue<int?>("Jahrgang_ID"),
                SubjectId = reader.GetValue<int>("Fach_ID"),
                CourseCategory = reader.GetValue<string>("KursartAllg"),
                Teacher = reader.GetValue<string>("LehrerKrz")
            };
        }
    }
}