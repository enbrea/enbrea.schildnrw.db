#region ENBREA - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
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
    /// An entity within the SchildNRW database table "kurslehrer"
    /// </summary>
    public class TeacherCourseAttendance
    {
        public float? Proportion { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

        public static TeacherCourseAttendance FromDb(DbDataReader reader)
        {
            return new TeacherCourseAttendance
            {
                CourseId = reader.GetValue<int>("Kurs_ID"),
                TeacherId = reader.GetValue<int>("Lehrer_ID"),
                Proportion = reader.GetValue<float?>("Anteil")
            };
        }
    }
}
