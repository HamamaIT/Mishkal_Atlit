using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;

namespace OpenFile
{
    class Program
    {
        static void Main(string[] args)
        {

            
            //string target_file = @"c:\users\ronen.HAMAMA\shkilot.csv";
            string target_file = ConfigurationManager.AppSettings.Get("target_file");

            int line_count = 0;

            string shkila_time = "";
            //string orig_path = @"C:\Users\ronen.HAMAMA\Documents\Mishkal\";
            string orig_path = ConfigurationManager.AppSettings.Get("orig_path");
            string numericWeightIn = "";
            string numericWeightOut = "";
            //string archive_folder = @"C:\Users\ronen.hamama\Documents\Mishkal_Archive\";
            string archive_folder = ConfigurationManager.AppSettings.Get("archive_folder");

            char[] delimiterChars = { ' ', ',', '.', '\\', '/', '\t' };
            int date_day = 0;
            int date_month = 0;
            int date_year = 2000;
            int shkila_nr = 0;
            string date_time = "";
            string date_hour = "";
            var car_nr = "";
            var customer = "";
            var destination = "";
            var material = "";
            var car_company = "";
            string numericWeight = "";
            var notes = "";
            string employee = "";
            string driver = "";
            string weight_type = "";


            Encoding encode = System.Text.Encoding.GetEncoding("windows-1255");

            bool PathisEmpty = !Directory.EnumerateFiles(orig_path).Any();

            if (PathisEmpty == false)  // if the original folder is not empty process the files
            {

                // get the list of the txt files in the folder
                string[] filePaths = Directory.GetFiles(orig_path, "*.txt");
                foreach (string file in filePaths)
                {
                    line_count = 0;                   // reset line counter for each new file
                    Console.WriteLine(file);
                    foreach (string line in File.ReadLines(file, encode))
                    {
                        //שקילה או קבלה
                        if (line_count == 5)
                        {
                            string[] words = line.Split(delimiterChars);
                            weight_type = words[1];
                            //System.Console.WriteLine(weight_type,encode);

                        }

                        // shkila_nr nr from line 7 as number
                        if (line_count == 6)        // מספר שקילה weight nr
                        {
                            shkila_nr = Int32.Parse(line);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(Environment.NewLine + shkila_nr + ",",wrencode);
                            //}


                            //Console.WriteLine("Shkila " + shkila_nr);
                        }
                        if (line_count == 7)        // תאריך ושעת השקילה date and time
                        {
                            date_time = line;
                            //System.Console.WriteLine(line);
                            string[] words = date_time.Split(delimiterChars);
                            date_day = Int32.Parse(words[0]);
                            date_month = Int32.Parse(words[1]);
                            date_year = Int32.Parse(words[2]);
                            date_hour = words[3];
                            //System.Console.WriteLine("The date is:" + date_day +  " " + date_month + " "+  date_time);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(date_time + ",", wrencode);
                            //}
                            //Console.WriteLine("Shkila Date and time  " + date_time);
                        }
                        if (line_count == 8)        // car number at line 8
                        {

                            car_nr = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(car_nr + ",", wrencode);
                            //}
                            //Console.WriteLine("Car Nr: " + result);
                        }
                        if (line_count == 10)       // Customer at line 10
                        {
                            customer = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(customer + ",", wrencode);
                            //}
                            //Console.WriteLine("Customer name: " + result);
                        }
                        if (line_count == 11)       //Destination at line 11
                        {
                            destination = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(destination + ",", wrencode);
                            //}
                            //Console.WriteLine("Destination: " + result);
                        }
                        if (line_count == 12)       //Material at line 12
                        {
                            material = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(material + ",", wrencode);
                            //}
                            //Console.WriteLine("Material: " + result);
                        }
                        if (line_count == 13)       //Car Company at line 13
                        {
                            car_company = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(car_company + ",", wrencode);
                            //}
                            //Console.WriteLine("Car Company: " + result);
                        }
                        if (line_count == 16)       //Out weight at line 16, extracting numerals only
                        {
                            numericWeightOut = new String(line.ToCharArray().Where(c => Char.IsDigit(c)).ToArray());
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(numericWeightOut + ",", wrencode);
                            //}
                            //Console.WriteLine("Out Weight: " + numericWeightOut + " KG");
                        }
                        if (line_count == 17)       //In weight at line 17, extracting numerals only
                        {
                            numericWeightIn = new String(line.ToCharArray().Where(c => Char.IsDigit(c)).ToArray());
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(numericWeightIn + ",", wrencode);
                            //}
                            //Console.WriteLine("In Weight: " + numericWeightIn + " KG");
                        }
                        if (line_count == 18)       //Neto weight at line 17, extracting numerals only
                        {
                            numericWeight = new String(line.ToCharArray().Where(c => Char.IsDigit(c)).ToArray());
                            //int calcNeto = Int32.Parse(numericWeightOut) - Int32.Parse(numericWeightIn);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(calcNeto + ",", wrencode);
                            //}
                            //Console.WriteLine("Neto Weight: " + numericWeight + " KG" + "   Calculated: " + calcNeto);
                        }
                        if (line_count == 19)       //Notes at line 19
                        {
                            string[] words = line.Split(delimiterChars);            //split the line in words without spaces
                            notes = String.Join("", words);                         // join the words toghether as a single string
                            notes = notes.Substring(notes.LastIndexOf(':') + 1);    //take off the title before the :
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(notes + ",", wrencode);
                            //}
                            //Console.WriteLine("Notes: " + result);
                        }
                        if (line_count == 20)       //Employee at line 20
                        {
                            employee = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(employee + ",", wrencode);
                            //}
                            //Console.WriteLine("Employee: " + result);
                        }
                        if (line_count == 21)       //Driver at line 21
                        {
                            driver = line.Substring(line.LastIndexOf(':') + 1);
                            //using (StreamWriter swrite = File.AppendText(target_file))
                            //{
                            //    swrite.Write(driver + ",", wrencode);
                            //}
                            //Console.WriteLine("Driver: " + result, encode);
                        }



                        line_count++;

                    }
                    // ***  Write all to DB
                    try
                    {
                        //SqlConnection conn = new SqlConnection(
                        //                       "server =Appserver\\SQLEXPRESS;" +
                        //                       "Trusted_connection=yes;" +
                        //                       "database = Atlit_Mishkal;" +
                        //                       "connection timeout=30");
                        //SqlCommand sqlcmd = new SqlCommand();
                        //sqlcmd.Connection = conn;
                        //sqlcmd.CommandText = "INSERT INTO dbo.Shkila(Shkila_nr, date, car_nr, customer, destination, material, company, weight_in, weight_out, weight_neto, notes, employee, driver, date_day, date_month, date_year, date_hour, weight_type)" +
                        //                                           "values (@shkila_nr, @date, @car_nr, @customer, @destination, @material, @company, @weight_in, @weight_out, @weight_neto, @notes, @employee, @driver, @date_day, @date_month, @date_year, @date_hour, @weight_type)";
                        ////sqlcmd.CommandText = "insert into dbo.Shkila(Shkila_nr) values (@shkila_nr)";
                        //conn.Open();
                        //sqlcmd.Parameters.Add(new SqlParameter("@shkila_nr", shkila_nr));
                        //sqlcmd.Parameters.Add(new SqlParameter("@date", date_time));
                        //sqlcmd.Parameters.Add(new SqlParameter("@car_nr", car_nr));
                        //sqlcmd.Parameters.Add(new SqlParameter("@customer", customer));
                        //sqlcmd.Parameters.Add(new SqlParameter("@destination", destination));
                        //sqlcmd.Parameters.Add(new SqlParameter("@material", material));
                        //sqlcmd.Parameters.Add(new SqlParameter("@company", car_company));
                        //sqlcmd.Parameters.Add(new SqlParameter("@weight_out", numericWeightOut));
                        //sqlcmd.Parameters.Add(new SqlParameter("@weight_in", numericWeightIn));
                        //sqlcmd.Parameters.Add(new SqlParameter("@weight_neto", numericWeight));
                        //sqlcmd.Parameters.Add(new SqlParameter("@notes", notes));
                        //sqlcmd.Parameters.Add(new SqlParameter("@employee", employee));
                        //sqlcmd.Parameters.Add(new SqlParameter("@driver", driver));
                        //sqlcmd.Parameters.Add(new SqlParameter("@date_day", date_day));
                        //sqlcmd.Parameters.Add(new SqlParameter("@date_month", date_month));
                        //sqlcmd.Parameters.Add(new SqlParameter("@date_year", date_year));
                        //sqlcmd.Parameters.Add(new SqlParameter("@date_hour", date_hour));
                        //sqlcmd.Parameters.Add(new SqlParameter("@weight_type", weight_type));
                        //sqlcmd.ExecuteNonQuery();
                        //conn.Close();

                        // MOve the file to archive
                        File.Copy(file, archive_folder + Path.GetFileName(file));
                        //File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }


                }
            }



        }
    }

}
