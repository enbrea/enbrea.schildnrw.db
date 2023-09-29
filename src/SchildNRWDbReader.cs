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

using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Enbrea.SchildNRW.Db
{
    public class SchildNRWDbReader
    {
        private readonly string _dbConnectionString;
        private readonly SchildNRWDbProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchildNRWDbReader<T>"/> class.
        /// </summary>
        public SchildNRWDbReader(SchildNRWDbProvider provider, string dbConnectionString)
        {
            _provider = provider;
            _dbConnectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns back all countries (Staaten)
        /// </summary>
        /// <returns>An async enumerator of countries</returns>
        public async IAsyncEnumerable<Country> CountriesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Country.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID, Bezeichnung, StatistikKrz, Sichtbar " +
                    $"from k_staat";
            }
        }

        /// <summary>
        /// Returns back all course categories (allgemeine Kursarten)
        /// </summary>
        /// <returns>An async enumerator of course categories</returns>
        public async IAsyncEnumerable<CourseCategory> CourseCategoriesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => CourseCategory.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID, KursartAllg, InternBez, Bezeichnung, Sichtbar " +
                    $"from eigeneschule_kursartallg";
            }
        }

        /// <summary>
        /// Returns back all courses (Kurse) for a given school term (Abschnitt)
        /// </summary>
        /// <param name="year">School year</param>
        /// <param name="schoolTerm">School term</param>
        /// <returns>An async enumerator of courses</returns>
        public async IAsyncEnumerable<Course> CoursesAsync(short year, short schoolTerm)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Course.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID,  Jahr, Abschnitt, KurzBez, Jahrgang_ID, Fach_ID, KursartAllg, LehrerKrz " +
                    $"from kurse " +
                    $"where Jahr = @year and Abschnitt = @schoolTerm";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "year";
                dbParameter1.Value = year;
                var dbParameter2 = dbCommand.CreateParameter();
                dbParameter2.ParameterName = "schoolTerm";
                dbParameter2.Value = schoolTerm;

                dbCommand.Parameters.Add(dbParameter1);
                dbCommand.Parameters.Add(dbParameter2);
            }
        }

        /// <summary>
        /// Returns back all course types (Kursarten)
        /// </summary>
        /// <returns>An async enumerator of course types</returns>
        public async IAsyncEnumerable<CourseType> CourseTypesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => CourseType.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID, Kursart, KursartAllg, InternBez, Bezeichnung, Sichtbar " +
                    $"from eigeneschule_kursart";
            }
        }

        /// <summary>
        /// Returns back info about the school (Eigene Schuldaten)
        /// </summary>
        /// <returns>An async enumerator of <see cref="MySchool"></returns>
        public async IAsyncEnumerable<MySchool> MySchoolAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => MySchool.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID, SchulNr, Bezeichnung1, Bezeichnung2, Bezeichnung3, Strasse, PLZ, Ort, Telefon, Fax, EMail, Schuljahr, SchuljahrAbschnitt, " +
                    $"AnzahlAbschnitte, AbschnittBez, BezAbschnitt1, BezAbschnitt2, BezAbschnitt3, BezAbschnitt4 " +
                    $"from eigeneschule";
            }
        }

        /// <summary>
        /// Returns back all school classes (Klassen)
        /// </summary>
        /// <returns>An async enumerator of school classes</returns>
        public async IAsyncEnumerable<SchoolClass> SchoolClassesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => SchoolClass.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID, Bezeichnung, Klasse, Jahrgang_ID, FKlasse, VKlasse, OrgFormKrz, KlassenlehrerKrz, StvKlassenlehrerKrz, Klassenart " +
                    $"from versetzung";
            }
        }

        /// <summary>
        /// Returns back all school class organizations (Organisationsformen)
        /// </summary>
        /// <returns>An async enumerator of school class organizations</returns>
        public async IAsyncEnumerable<SchoolClassOrganization> SchoolClassOrganizationsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => SchoolClassOrganization.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select ID, Bezeichnung, StatistikKrz, Sichtbar " +
                    $"from k_klassenorgform";
            }
        }
        /// <summary>
        /// Returns back all student-course-relationships for a given school year (Schuljahr) and school term (Abschnitt)
        /// </summary>
        /// <param name="year">School year</param>
        /// <param name="schoolTerm">School term</param>
        /// <returns>An async enumerator of student-course-relationships</returns>
        public async IAsyncEnumerable<StudentCourseAttendance> StudentCourseAttendancesAsync(short year, short schoolTerm)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => StudentCourseAttendance.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select distinct sld.Kurs_ID, sld.Kursart, sld.KursartAllg, slas.Schueler_ID " +
                    $"from SchuelerLernabschnittsdaten slas " +
                    $"join SchuelerLeistungsdaten sld " +
                    $"on sld.Abschnitt_ID = slas.ID " +
                    $"where not (sld.Kurs_ID is null) and (slas.Jahr = @year) and (slas.Abschnitt = @schoolTerm)";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "year";
                dbParameter1.Value = year;
                var dbParameter2 = dbCommand.CreateParameter();
                dbParameter2.ParameterName = "schoolTerm";
                dbParameter2.Value = schoolTerm;

                dbCommand.Parameters.Add(dbParameter1);
                dbCommand.Parameters.Add(dbParameter2);
            }
        }

        /// <summary>
        /// Returns back all student-course-relationships for a given course (Kurse) and school year (Schuljahr) and school term (Abschnitt)
        /// </summary>
        /// <param name="courseId">Cuurse Id</param>
        /// <param name="year">School year</param>
        /// <param name="schoolTerm">School term</param>
        /// <returns>An async enumerator of student-course-relationships</returns>
        public async IAsyncEnumerable<StudentCourseAttendance> StudentCourseAttendancesAsync(int courseId, short year, short schoolTerm)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => StudentCourseAttendance.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select distinct sld.Kurs_ID, sld.Kursart, sld.KursartAllg, slas.Schueler_ID " +
                    $"from SchuelerLernabschnittsdaten slas " +
                    $"join SchuelerLeistungsdaten sld " +
                    $"on sld.Abschnitt_ID = slas.ID " +
                    $"where (sld.Kurs_ID = @courseId) and (slas.Jahr = @year) and (slas.Abschnitt = @schoolTerm)";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "courseId";
                dbParameter1.Value = courseId;
                var dbParameter2 = dbCommand.CreateParameter();
                dbParameter2.ParameterName = "year";
                dbParameter2.Value = year;
                var dbParameter3 = dbCommand.CreateParameter();
                dbParameter3.ParameterName = "schoolTerm";
                dbParameter3.Value = schoolTerm;

                dbCommand.Parameters.Add(dbParameter1);
                dbCommand.Parameters.Add(dbParameter2);
                dbCommand.Parameters.Add(dbParameter3);
            }
        }

        /// <summary>
        /// Returns back all students (Schüler)
        /// </summary>
        /// <returns>An async enumerator of students</returns>
        public async IAsyncEnumerable<Student> StudentsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Student.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select S.ID, S.Status, S.Name, S.Vorname, S.SchulEmail, S.Geburtsdatum, S.Geschlecht, S.Klasse, S.Aufnahmedatum, " +
                    $"S.Entlassdatum, S.Strasse, S.PLZ, o.Bezeichnung AS Ort, o.Kreis, o.Land " +
                    $"from schueler s " +
                    $"join k_ort o " +
                    $"on s.PLZ = o.PLZ ";
            }
        }

        /// <summary>
        /// Returns back all students (Schüler) for a given status
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>An async enumerator of students</returns>
        public async IAsyncEnumerable<Student> StudentsAsync(StudentStatus status)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Student.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select S.ID, S.Status, S.Name, S.Vorname, S.SchulEmail, S.Geburtsdatum, S.Geburtsname, S.Geburtsort, S.Geschlecht, S.Klasse, " +
                    $"S.Jahrgang, S.StaatKrz, S.StaatKrz2, S.Aufnahmedatum, S.Entlassdatum, S.Strasse, S.PLZ, o.Bezeichnung AS Ort, o.Kreis, o.Land " +
                    $"from schueler s " +
                    $"join k_ort o " +
                    $"on s.PLZ = o.PLZ " +
                    $"where (Status = @status)";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "status";
                dbParameter1.Value = (short)status;

                dbCommand.Parameters.Add(dbParameter1);
            }
        }

        /// <summary>
        /// Returns back all students (Schüler) for a given status and which are enrolled within a specific date range
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="enrollmentDate">Enrollment date</param>
        /// <param name="leaveDate">Leave date</param>
        /// <returns>An async enumerator of students</returns>
        public async IAsyncEnumerable<Student> StudentsAsync(StudentStatus status, DateTime enrollmentDate, DateTime leaveDate)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Student.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select S.ID, S.Status, S.Name, S.Vorname, S.SchulEmail, S.Geburtsdatum, S.Geschlecht, S.Klasse, S.Aufnahmedatum, " +
                    $"S.Entlassdatum, S.Strasse, S.PLZ, o.Bezeichnung AS Ort, o.Kreis, o.Land " +
                    $"from schueler s " +
                    $"join k_ort o " +
                    $"on s.PLZ = o.PLZ " +
                    $"where (Status = @status) and ((Aufnahmedatum is null) or (Aufnahmedatum >= @enrollmentDate)) and ((Entlassdatum is null) or (Entlassdatum >= @leaveDate))";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "status";
                dbParameter1.Value = (short)status;

                var dbParameter2 = dbCommand.CreateParameter();
                dbParameter2.ParameterName = "enrollmentDate";
                dbParameter2.Value = enrollmentDate;

                var dbParameter3 = dbCommand.CreateParameter();
                dbParameter3.ParameterName = "leaveDate";
                dbParameter3.Value = leaveDate;

                dbCommand.Parameters.Add(dbParameter1);
                dbCommand.Parameters.Add(dbParameter2);
                dbCommand.Parameters.Add(dbParameter3);
            }
        }

        /// <summary>
        /// Returns back all students (Schüler) which are enrolled within a specific date range
        /// </summary>
        /// <param name="enrollmentDate">Enrollment date</param>
        /// <param name="leaveDate">Leave date</param>
        /// <returns>An async enumerator of students</returns>
        public async IAsyncEnumerable<Student> StudentsAsync(DateTime enrollmentDate, DateTime leaveDate)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Student.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select S.ID, S.Status, S.Name, S.Vorname, S.SchulEmail, S.Geburtsdatum, S.Geschlecht, S.Klasse, S.Aufnahmedatum, " +
                    $"S.Entlassdatum, S.Strasse, S.PLZ, o.Bezeichnung AS Ort, o.Kreis, o.Land " +
                    $"from schueler s " +
                    $"join k_ort o " +
                    $"on s.PLZ = o.PLZ " +
                    $"where ((Aufnahmedatum is null) or (Aufnahmedatum >= @enrollmentDate)) and ((Entlassdatum is null) or (Entlassdatum >= @leaveDate))";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "enrollmentDate";
                dbParameter1.Value = enrollmentDate;

                var dbParameter2 = dbCommand.CreateParameter();
                dbParameter2.ParameterName = "leaveDate";
                dbParameter2.Value = leaveDate;

                dbCommand.Parameters.Add(dbParameter1);
                dbCommand.Parameters.Add(dbParameter2);
            }
        }

        /// <summary>
        /// Returns back all student-school class-relationships for a given school year (Schuljahr) and school term (Abschnitt)
        /// </summary>
        /// <param name="year">School year</param>
        /// <param name="schoolTerm">School term</param>
        /// <returns></returns>
        public async IAsyncEnumerable<StudentSchoolClassAttendance> StudentSchoolClassAttendancesAsync(int year, int schoolTerm)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => StudentSchoolClassAttendance.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select Klasse, Schueler_ID " +
                    $"from SchuelerLernabschnittsdaten " +
                    $"where (Jahr = @year) and (Abschnitt = @schoolTerm)";

                var dbParameter1 = dbCommand.CreateParameter();
                dbParameter1.ParameterName = "year";
                dbParameter1.Value = year;
                
                var dbParameter2 = dbCommand.CreateParameter();
                dbParameter2.ParameterName = "schoolTerm";
                dbParameter2.Value = schoolTerm;

                dbCommand.Parameters.Add(dbParameter1);
                dbCommand.Parameters.Add(dbParameter2);
            }
        }

        /// <summary>
        /// Returns back all subjects (Fächer)
        /// </summary>
        /// <returns>An async enumerator of subjects</returns>
        public async IAsyncEnumerable<Subject> SubjectsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Subject.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText = $"select * from eigeneSchule_faecher";
            }
        }

        /// <summary>
        /// Returns back all teacher-course-relationships
        /// </summary>
        /// <returns>An async enumerator of teacher-course-relationships</returns>
        public async IAsyncEnumerable<TeacherCourseAttendance> TeacherCourseAttendancesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => TeacherCourseAttendance.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText = $"select Kurs_ID, Lehrer_ID, Anteil from kurslehrer";
            }
        }

        /// <summary>
        /// Returns back all teachers (Lehrer)
        /// </summary>
        /// <returns>An async enumerator of teachers</returns>
        public async IAsyncEnumerable<Teacher> TeachersAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Teacher.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText = 
                    $"select l.ID, l.Kuerzel, l.Nachname, l.Vorname, l.Anrede, l.Titel, l.Geburtsdatum, l.Geschlecht, l.EMail, " +
                    $"l.Strasse, l.PLZ, o.Bezeichnung AS Ort, o.Kreis, o.Land, l.Tel, l.Handy, l.DatumZugang, l.DatumAbgang, " +
                    $"l.EMailDienstlich " +
                    $"from k_lehrer l " +
                    $"join k_ort o " +
                    $"on l.PLZ = o.PLZ ";
            }
        }

        /// <summary>
        /// Creates either a MS SQL Server or a MySQL database connection
        /// </summary>
        /// <returns>The newly created database connection</returns>
        private DbConnection CreateConnection()
        {
            return _provider switch
            {
                SchildNRWDbProvider.MsSql => new SqlConnection(_dbConnectionString),
                SchildNRWDbProvider.MySql => new MySqlConnection(_dbConnectionString),
                _ => throw new ArgumentException("Illegal argument")
            };
        }

        /// <summary>
        /// Opens the internal database connection, executes an SQL query and iterates over the result set.
        /// </summary>
        /// <typeparam name="TEntity">Enttiy type to be created</typeparam>
        /// <param name="setCommand">Action for initializing the sql command</param>
        /// <param name="createEntity">Action for creating a new TEntity instance</param>
        /// <returns>An async enumerator of TEntity instances</returns>
        private async IAsyncEnumerable<TEntity> EntitiesAsync<TEntity>(Action<DbCommand> setCommand, Func<DbDataReader, TEntity> createEntity)
        {
            using DbConnection dbConnection = CreateConnection();

            await dbConnection.OpenAsync();
            try
            {
                using var dbTransaction = dbConnection.BeginTransaction();
                using var dbCommand = dbConnection.CreateCommand();

                dbCommand.Transaction = dbTransaction;
                setCommand(dbCommand);

                using var reader = await dbCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    yield return createEntity(reader);
                }
            }
            finally
            {
                await dbConnection.CloseAsync();
            }
        }
    }
}
