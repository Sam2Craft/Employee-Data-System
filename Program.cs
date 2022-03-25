using System;
using System.IO;

namespace PracEXM2
{
    class Program
    {
        static void Main(string[] args)
        {
            int CNST = 50;
            int count = 0;
            string [] empID = new string[CNST];
            string [] dptID = new string [CNST];
            double [] bill = new double [CNST];
            double [] admin = new double [CNST];
            GetInfo(empID,dptID,bill,admin, ref count, CNST);


        }
        
        static void GetInfo (string [] empID, string [] dptID, double [] bill, double [] admin, ref int count, int CNST)
        {
            StreamReader inFile = new StreamReader("Input.txt");
            string fileInput = inFile.ReadLine(); //dk
           while (fileInput != null)
            {
                string [] temp = fileInput.Split("#"); //dk
                empID[count] = temp [0];
                dptID [count] = temp[1];
                bill[count] = double.Parse(temp[2]);
                admin [count] = double.Parse(temp[3]);
                count++;
                System.Console.WriteLine($"{count}) \n Employee: {temp[0]} \n Department: {temp[1]} \n Billable hours: {temp[2]} \n Admin hours: {temp[3]}");
                fileInput = inFile.ReadLine();
                
            }
            inFile.Close();
            BillRange(bill, count, empID, CNST, admin, dptID);
        }
        static void BillRange (double [] bill, int count, string [] empID, int CNST, double [] admin, string [] deptID)
        {
            double avgPerc = 0;
            double totalPercent = 0;
            double bP = 0;
            double average = 0;
            double sum = 0;
            double max = bill[0];
            double min = bill[0];
            string lowest = empID[0];
            string highest =empID[0];
            double [] billPerc = new double [CNST];
            for (int u = 1; u<count; u++)
            {
                if(bill[u] < min)
                {
                    min = bill[u];
                    lowest = empID[u];
                }
                if (bill[u] > max)
                {
                    max = bill[u];
                    highest = empID[u];
                }
                BillAverage(bill, count, u, ref sum, ref average);
                BillPercentage(bill, admin, u, count, ref bP, billPerc, empID, ref totalPercent, deptID );
                avgPerc =totalPercent / u;
            }
            System.Console.WriteLine($"The Average of the pecentages for billable hours is {avgPerc * 100}");
            System.Console.WriteLine($" \nThe range of billable time of the listed employees is: {max - min} \n \nThe Employee with the lowest billable hours is Employee: {lowest} with {min} billable hours \nThe Employee with the highest billable hours is Employee: {highest} with {max} billable hours");
            System.Console.WriteLine($" \nThe total amount of billable hours worked among all employees is: {sum} \nThe average billable hours between employees is: {average}");
            
        }

        static void BillAverage(double [] bill, int count, int u, ref double sum, ref double average)
        {
             sum = sum + bill[u];
             average = sum/u;
        }
       
       static void BillPercentage( double [] bill, double[] admin, int u, int count, ref double bP, double []billPerc, string [] empID, ref double totalPercent, string [] deptID)
       {
           
               bP = bill[u] / (bill[u] + admin[u]);
               billPerc [u] = bP;
                totalPercent = totalPercent + bP;
               System.Console.WriteLine($"Percentage of billable hours of total hours for {deptID[u]}: {billPerc [u] * 100}");
           
       }

   

       
    }
}

        //The organization is interested in ensuring that employees spend at least 80% of their time on client billable work.  
        //The employees are salaried employees and the organization expects each employee to work on average 45 hours per week.  
        //To assist in the analysis the organization has provided Jeff with a data file from their time reporting system.  
        //The file contains the employee ID, department an employee works in, billable time, and administrative support time.
        //Each of the time fields are in hours with a quarter of an hour being the smallest increment. 
        // For example 2 hours and 15 minutes would appear as 2.25 on the file.
