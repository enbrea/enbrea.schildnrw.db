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

using System;
using System.Threading.Tasks;

namespace Enbrea.SchildNRW.Db.SmokeTest
{
    public class AppService
    {
        private readonly AppConfig _appConfig;

        public AppService(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("SchildNRW Export Test");
            Console.WriteLine("---------------------\n");

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\t1 - Export My School (Eigene Schule)");
            Console.WriteLine("\t2 - Export School Classes (Klassen)");
            Console.WriteLine("\t3 - Export Students (Schüler)");
            Console.WriteLine("\t4 - Export Teachers (Lehrer)");
            Console.Write("Your selection? ");

            switch (Console.ReadLine())
            {
                case "1":
                    await ExportMySchool();
                    break;
                case "2":
                    await ExportSchoolClasses();
                    break;
                case "3":
                    await ExportStudents();
                    break;
                case "4":
                    await ExportTeachers();
                    break;
            }
        }

        private SchildNRWDbReader CreateDbReader()
        {
            return new SchildNRWDbReader(_appConfig.DbProvider, _appConfig.DbConnection);
        }

        private async Task ExportMySchool()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("My School:");
            Console.WriteLine("----------");

            await foreach (var mySchool in dbReader.MySchoolAsync())
            {
                Console.WriteLine($"Id = {mySchool.Id}");
                Console.WriteLine($"SchoolNo = {mySchool.SchoolNo}");
                Console.WriteLine($"Name1 = {mySchool.Name1}");
                Console.WriteLine($"Name2 = {mySchool.Name2}");
                Console.WriteLine($"Name3 = {mySchool.Name3}");
                Console.WriteLine($"Street = {mySchool.Street}");
                Console.WriteLine($"PostalCode = {mySchool.PostalCode}");
                Console.WriteLine($"Locality = {mySchool.Locality}");
                Console.WriteLine($"Phone = {mySchool.Phone}");
                Console.WriteLine($"Telefax = {mySchool.Telefax}");
                Console.WriteLine($"Email = {mySchool.Email}");
                Console.WriteLine($"SchoolYear = {mySchool.SchoolYear}");
                Console.WriteLine($"SchoolTerm = {mySchool.SchoolTerm}");
                Console.WriteLine($"SchoolTermCount = {mySchool.SchoolTermCount}");
                Console.WriteLine($"SchoolTermName = {mySchool.SchoolTermName}");
                Console.WriteLine($"SchoolTerm1Name = {mySchool.SchoolTerm1Name}");
                Console.WriteLine($"SchoolTerm2Name = {mySchool.SchoolTerm2Name}");
                Console.WriteLine($"SchoolTerm3Name = {mySchool.SchoolTerm3Name}");
                Console.WriteLine($"SchoolTerm4Name = {mySchool.SchoolTerm4Name}");
            }
        }

        private async Task ExportSchoolClasses()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("School Classes:");
            Console.WriteLine("---------------");

            await foreach (var schoolClass in dbReader.SchoolClassesAsync())
            {
                Console.WriteLine(@"{0}, {1}", 
                    schoolClass.Code, 
                    schoolClass.Name);
            }
        }

        private async Task ExportStudents()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("Students:");
            Console.WriteLine("---------");

            await foreach (var student in dbReader.StudentsAsync(StudentStatus.Active))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}", 
                    student.Lastname, 
                    student.Firstname, 
                    student.Birthdate?.ToString("dd.MM.yyyy"), 
                    student.Locality, 
                    student.SchoolClass);
            }
        }

        private async Task ExportTeachers()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("Teachers:");
            Console.WriteLine("---------");

            await foreach (var teacher in dbReader.TeachersAsync())
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}", 
                    teacher.Lastname, 
                    teacher.Firstname, 
                    teacher.Birthdate?.ToString("dd.MM.yyyy"), 
                    teacher.Locality);
            }
        }
    }
}
