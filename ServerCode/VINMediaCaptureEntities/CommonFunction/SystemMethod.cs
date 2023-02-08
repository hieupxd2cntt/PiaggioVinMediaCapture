using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VINMediaCaptureEntities.CommonFunction
{
    public static class SystemMethod
    {
        public static CultureInfo CultureApp = new CultureInfo("en-US");
        public static CultureInfo CultureAppVn = new CultureInfo("vi-VN");
        /// <summary>
        /// Tạo 1 DataTable có cấu trúc từ 1 class Linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable LinqToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public static DataTable ConvertTo111<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }
        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
       
        /// <summary>
        /// Tạo 1 DataTable có cấu trúc từ 1 class Linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable LinqToDataTable<T>(this IList<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                                                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        /// <summary>
        /// Tạo 1 DataTable có cấu trúc từ 1 class Linq bao gồm cả header khi query trả ra ko có dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable LinqToDataTableIncludeHeader<T>(this IEnumerable<T> varlist)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                Type colType = prop.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                dataTable.Columns.Add(prop.Name, colType);
            }
            foreach (T item in varlist)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// convert string to double (vietnam lang)
        /// </summary>
        /// <param name="strDouble"></param>
        /// <returns></returns>
        public static double StringToDouble(this string strDouble)
        {
            double dec;
            if (double.TryParse(strDouble, NumberStyles.Float, CultureApp, out dec))
            {
                return dec;
            }
            return 0;
        }

        /// <summary>
        /// convert string to float (vietnam lang)
        /// </summary>
        /// <param name="strDouble"></param>
        /// <returns></returns>
        public static float StringToFloat(this string strDouble)
        {
            float dec;
            if (float.TryParse(strDouble, NumberStyles.Float, CultureApp, out dec))
            {
                return dec;
            }
            return 0;
        }
        /// <summary>
        /// Convert string to decimal
        /// </summary>
        /// <param name="strDouble"></param>
        /// <returns></returns>
        public static decimal StringToDecimal(this string strDouble)
        {
            if (CultureApp.NumberFormat.NumberDecimalSeparator == ",")
            {
                strDouble = strDouble.Replace(".", "");
            }
            else
            {
                strDouble = strDouble.Replace(",", "");
            }
            decimal dec;
            if (decimal.TryParse(strDouble, NumberStyles.Float, CultureApp, out dec))
            {
                return dec;
            }
            return 0;
        }
     
        public static string ToCurrency(this float value)
        {
            return value.ToString("0,0", CultureApp);
        }
        public static string ToStringFomat(this Int64 value)
        {
            return value.ToString("N0");
        }
        public static string ToDecimalFomat(this Decimal value)
        {
            return value.ToString("N2");
        }
        public static string ToStringFomat(this Int32 value)
        {
            return value.ToString("N0", CultureApp);
        }
        public static string ToNumber(this float value)
        {
            return value.ToString("0.###", CultureApp);
        }
        public static string ToMoney(this float value)
        {
            return value.ToString("0,#.#", CultureApp);
        }
        public static string ToNumber00(this float value)
        {
            return value.ToString("0.00", CultureApp);
        }
        public static string ToNumber000(this float value)
        {
            return value.ToString("0.000", CultureApp);
        }
        

        public static float Round(this float value)
        {
            return (float)Math.Round(value, 0);
        }

        public static string ToNumberWithFormat(this string value)
        {
            if (CultureApp.NumberFormat.NumberDecimalSeparator == ",")
                return value.Replace(".", ",");
            else
                return value.Replace(",", ".");
        }
        /// <summary>
        /// Convert string số(Không chứa Separator) sang string với fomat(có Separator)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNumberStringWithFormat(this string value)
        {
            try
            {
                if (CultureApp.NumberFormat.NumberDecimalSeparator == ",")
                {
                    value = value.Replace(".", ",");
                    return Convert.ToInt64(value.Replace(",", "")).ToStringFomat();
                }
                else
                {
                    value = value.Replace(",", ".");
                    return Convert.ToInt64(value.Replace(".", "")).ToStringFomat();
                }
                    
                
            }
            catch (Exception ex)
            {
                return "0";
            }   
        }
        public static string ToDecimalString(this string value)
        {
            try
            {
              
                return Convert.ToDecimal(value).ToDecimalFomat();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// Convert string to array
        /// </summary>
        /// <param name="value">Các giá trị cách nhau bởi dấu phẩy(,)</param>
        /// <returns></returns>
        public static string[] ToStringArray(this string value)
        {
            char[] charSeparator={','};
            return value.Split(charSeparator);
        }
        /// <summary>
        /// Convert string sang mảng decimal, các phần tử cách nhau bởi dấu phẩy(,)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal[] ToDecimalArray(this string value)
        {
            char[] charSeparator = { ',' };
            return value.Split(charSeparator).Select(x=>Convert.ToDecimal(x)).ToArray();
        }

        /// <summary>
        /// convert string to date with formats { "yyyyMMdd", "dd/MM/yyyy", "yyyyMM", "yyyyMMddHHmmss", "dd/MM/yyyy HH:mm:ss"}
        /// </summary>
        /// <param name="strDate">string date</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(this string strDate)
        {
            string[] formats = { "yyyyMMdd", "dd/MM/yyyy", "dd-MM-yyyy", "yyyyMM", "yyyyMMddHHmmss", "dd/MM/yyyy HH:mm:ss" };
            if (!string.IsNullOrEmpty(strDate))
            {
                try
                {
                    var date = DateTime.ParseExact(strDate, formats, CultureAppVn, DateTimeStyles.None);
                    return date;
                }
                catch (Exception e)
                {
                    //
                }
            }
            return DateTime.Now;
        }
        public static DateTime? StringToDateTimeNull(this string strDate)
        {
            string[] formats = { "yyyyMMdd", "dd/MM/yyyy", "yyyyMM", "yyyyMMddHHmmss", "dd/MM/yyyy HH:mm:ss" };
            if (!string.IsNullOrEmpty(strDate))
            {
                try
                {
                    var date = DateTime.ParseExact(strDate, formats, CultureAppVn, DateTimeStyles.None);
                    return date;
                }
                catch (Exception e)
                {

                }
            }
            return null;
        }
        public static DateTime? StringToDateTimeWithCultureInfo(this string strDate)
        {
            if (!string.IsNullOrEmpty(strDate))
            {
                try
                {
                    DateTime dt = DateTime.Parse(strDate, SystemMethod.CultureAppVn);
                    return dt;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// Convert string to list string. Cách nhau bởi dấu phẩy
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> StringToListString(this string value)
        {
            char[] charSeparator = { ',' };
            return value.Split(charSeparator).ToList();
        }
        public static string StringToSpaceIfEmpty(this string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                return strInput;
            }
            return " ";
        }

        public static string StringDateToString(this string strDate)
        {
            string[] formats = { "yyyyMMdd", "dd/MM/yyyy", "yyyyMM", "yyyyMMddHHmmss", "dd/MM/yyyy HH:mm:ss" };
            if (!string.IsNullOrEmpty(strDate))
            {
                try
                {
                    var date = DateTime.ParseExact(strDate, formats, CultureApp, DateTimeStyles.None);
                    return date.ToString("dd/MM/yyyy");
                }
                catch (Exception e)
                {
                    //
                }
            }
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        public static string StringDateToString2(this string strDate)
        {
            string[] formats = { "yyyyMMdd", "dd/MM/yyyy", "yyyyMM", "yyyyMMddHHmmss", "dd/MM/yyyy HH:mm:ss" };
            if (!string.IsNullOrEmpty(strDate))
            {
                try
                {
                    var date = DateTime.ParseExact(strDate, formats, CultureApp, DateTimeStyles.None);
                    return date.ToString("dd/MM/yyyy");
                }
                catch (Exception e)
                {
                    //
                }
            }
            return "";
        }

       
        public static string ToYear(this DateTime date)
        {
            return date.ToString("yyyy");
        }
        public static DateTime ToYear(this string date)
        {
            return string.Format("{0}{1}", date, "0101").StringToDateTime();
        }
        public static DateTime ToYear(this int date)
        {
            return string.Format("{0}{1}", date.ToString("0000"), "0101").StringToDateTime();
        }

        public static int ToInt(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;
            if (CultureApp.NumberFormat.NumberDecimalSeparator == ",")
            {
                number = number.Replace(".", ",");
                return Convert.ToInt32(number.Replace(",", ""));
            }
            else
            {
                number = number.Replace(",", ".");
                return Convert.ToInt32(number.Replace(".", ""));
            }
            //return Convert.ToInt32(number.Replace(CultureApp.NumberFormat.NumberDecimalSeparator, ""));
        }
        public static Int64 ToInt64(this string number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;
            if (CultureApp.NumberFormat.NumberDecimalSeparator == ",")
            {
                number = number.Replace(".", ",");
                return Convert.ToInt64(number.Replace(",", ""));
            }
            else
            {
                number = number.Replace(",", ".");
                return Convert.ToInt64(number.Replace(",", ""));
            }
            //return Convert.ToInt64(number.Replace(CultureApp.NumberFormat.NumberDecimalSeparator, ""));        
        }
        /// <summary>
        /// Convert DateTime sang String với định dạng hiển thị trên hệ thống "dd/MM/yyyy"
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateToStringWithDisplayFormat(this DateTime date)
        {
            return String.Format("{0:dd/MM/yyyy}", date);
        }

        /// <summary>
        /// convert date to string with format "yyyyMMdd"
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateToStringWithDefaultFormat(this DateTime date)
        {
            return String.Format("{0:yyyyMMdd}", date);
        }

        public static string DateTimeToStringWithDefaultFormat(this DateTime date)
        {
            return String.Format("{0:yyyyMMddHHmmss}", date);
        }
        /// <summary>
        /// Kiem tra text co phai la tieng viet khong dau khong? Tra ra true neu no la tieng viet khong dau, fail neu no la tieng viet co unicode
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEnglishText(this string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return true;
                //
                //if (!System.Text.RegularExpressions.Regex.IsMatch(text, @"^[\sa-zA-Z0-9._]|\\{1,}$"))
                if (!System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9A-Za-z\\_. /*%^!,#,&,(),$,(,)+={};',:@-]+$"))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
             return false;   
            }
        }
        /// <summary>
        /// Convert object sang object khác
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myobj"></param>
        /// <returns></returns>
        public static T CastObject<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                try
                {
                    propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                    if (myobj.GetType().GetProperty(memberInfo.Name) != null)
                    {
                        value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);
                        propertyInfo.SetValue(x, value, null);
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            return (T)x;
        }  
       
        public static void ByteArrayToFile(this byte[] byteArray,string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                
            }
        }
    }
}
