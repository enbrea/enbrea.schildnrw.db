#region ENBREA - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
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
    /// Aggregated entity from SchildNRW database tables "SchuelerLernabschnittsdaten" and "SchuelerLeistungsdaten"
    /// </summary>
    public class StudentCourseAttendance
    {
        public string CourseCategory { get; set; }
        public int CourseId { get; set; }
        public string CourseType { get; set; }
        public int StudentId { get; set; }

        public static StudentCourseAttendance FromDb(DbDataReader reader)
        {
            return new StudentCourseAttendance
            {
                CourseId = reader.GetValue<int>("Kurs_ID"),
                StudentId = reader.GetValue<int>("Schueler_ID"),
                CourseType = reader.GetValue<string>("Kursart"),
                CourseCategory = reader.GetValue<string>("KursartAllg")
            };
        }
    }
}
