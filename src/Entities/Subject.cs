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
    /// An entity within the SchildNRW database table "eigeneschule_faecher"
    /// </summary>
    public class Subject
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string StatisticalCode { get; set; }

        public static Subject FromDb(DbDataReader reader)
        {
            return new Subject
            {
                Id = reader.GetValue<int>("ID"),
                Code = reader.GetValue<string>("FachKrz"),
                Name = reader.GetValue<string>("Bezeichnung"),
                StatisticalCode = reader.GetValue<string>("StatistikKrz")
            };
        }
    }
}
