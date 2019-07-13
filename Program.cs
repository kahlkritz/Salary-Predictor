using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Attempt
{
    class Program
    {
        static void Main(string[] args)
        {
//GLOBAL VARIABLE DECLARATIONS
            double learningRate = 0.000001;         //Learning Rate instantiation
            double calculatedSal = 0;               //Calculated Salary
            double empSal = 0;                      //Known Salary

            double trainingErr = 0;                 //Training error as expected - actual
            double trainingSSE = 1000000000000000;

            double validationErr = 0;               //Validation error as expected - actual
            double validationSSE = 100000000000000;

            double testingSSE = 0;                  //Testing error as expected - actual
            double testError = 0;

            ArrayList listOfEmps = new ArrayList();
            ArrayList weightsList = new ArrayList();

            double inE, inS, inP, inR, inC, inA, inX, inBias = -1;   //Input Declarations



//INITIATE WEIGHTS TO 0.1
//ADD WEIGHTS TO WEIGHTLIST ARRAY
            for (int addWeightsCounter = 0; addWeightsCounter < 8; addWeightsCounter++)
            {
                weightsList.Add(0.1);
            }



//TRAINING SET
            Console.WriteLine("Training...");
            int trainingCounter = 0;
            while (trainingSSE > 600000000)
            {
                trainingErr = 0;
                for (int x = 0; x < 8; x++)
                {
                    int employeeCounter = 0;
                    StreamReader SR1 = new StreamReader("SalData.csv");
                    while (employeeCounter < 1500)
                    {
                        string[] data = (SR1.ReadLine()).Split(',');
                        empSal = double.Parse(data[0]);
                        inE = double.Parse(data[1]);
                        inS = double.Parse(data[2]);
                        inP = double.Parse(data[3]);
                        inR = double.Parse(data[4]);
                        inC = double.Parse(data[5]);
                        inA = double.Parse(data[6]);
                        inX = double.Parse(data[7]);

                        calculatedSal = (inE * (double)weightsList[0]) + (inS * (double)weightsList[1]) + (inP * (double)weightsList[2]) + (inR * (double)weightsList[3]) + (inC * (double)weightsList[4]) + (inA * (double)weightsList[5]) + (inX * (double)weightsList[6]) + (inBias * (double)weightsList[7]);

                        if (x == 7)
                            weightsList[x] = (double)weightsList[x] - learningRate * derivative(calculatedSal,empSal,inBias);
                        else
                            weightsList[x] = (double)weightsList[x] - learningRate * derivative(calculatedSal, empSal, double.Parse(data[x+1]));
                        employeeCounter++;
                        trainingErr += empSal - calculatedSal;
                    }
                    SR1.Close();
                }
                trainingSSE = Math.Pow(trainingErr, 2);
                trainingCounter++;
            }



//DISPLAY WEIGHTS OBTAINED AFTER TRAINING
            Console.WriteLine("Test Set Weights:");
            for (int dispTestCount = 0; dispTestCount < 8; dispTestCount++)
            {
                Console.Write("w{0}: {1}    ", dispTestCount, weightsList[dispTestCount]);
            }
            Console.WriteLine();
            Console.WriteLine("Training error: {0}", trainingErr);
            Console.WriteLine("Training SSE:   {0}", trainingSSE);
            Console.WriteLine("Iterations: {0}", trainingCounter);
            Console.WriteLine();
            Console.WriteLine();



//VALIDATION SET
            Console.WriteLine("Validating...");
            int validationCounter = 0;
            while (validationCounter < 1000 )//validationSSE > 9000000000)
            {
                validationErr = 0;
                for (int i = 0; i < 8; i++)
                {
                    int empCount = 0;
                    StreamReader SR2 = new StreamReader("SalData.csv");
                    while (empCount < 2000)
                    {
                        if (empCount > 1500)
                        {
                            string[] data = (SR2.ReadLine()).Split(',');
                            empSal = double.Parse(data[0]);
                            inE = double.Parse(data[1]);
                            inS = double.Parse(data[2]);
                            inP = double.Parse(data[3]);
                            inR = double.Parse(data[4]);
                            inC = double.Parse(data[5]);
                            inA = double.Parse(data[6]);
                            inX = double.Parse(data[7]);

                            calculatedSal = (inE * (double)weightsList[0]) + (inS * (double)weightsList[1]) + (inP * (double)weightsList[2]) + (inR * (double)weightsList[3]) + (inC * (double)weightsList[4]) + (inA * (double)weightsList[5]) + (inX * (double)weightsList[6]) + (inBias * (double)weightsList[7]);

                            if (i == 7)
                                weightsList[i] = (double)weightsList[i] - learningRate * derivative(calculatedSal, empSal, inBias);
                            else
                                weightsList[i] = (double)weightsList[i] - learningRate * derivative(calculatedSal,empSal, double.Parse(data[i+1]));
                            validationErr += empSal - calculatedSal;
                        }
                        validationErr += empSal - calculatedSal;
                        empCount++;
                    }
                    SR2.Close();
                }
                validationCounter++;
                validationSSE = Math.Pow(validationErr, 2);
            }



//DISPLAY WEIGHTS OBTAINED AFTER VALIDATION
            Console.WriteLine("Validation Set Weights:");
            for (int dispValidationCount = 0; dispValidationCount < 8; dispValidationCount++)
            {
                Console.Write("w{0}: {1}    ", dispValidationCount, weightsList[dispValidationCount]);
            }
            Console.WriteLine();
            Console.WriteLine("Validation error: {0}", validationErr);
            Console.WriteLine("Validation SSE: {0}", validationSSE);
            Console.WriteLine("Iterations: {0}", validationCounter);
            Console.ReadLine();
            Console.Clear();



//FINAL TEST RESULTS
            Console.Clear();
            Console.WriteLine("Final Test Results:");
            Console.WriteLine();
            StreamReader SR4 = new StreamReader("Evaluation.csv");
            while(!SR4.EndOfStream)
            {
                string[] data = (SR4.ReadLine()).Split(',');
                inE = double.Parse(data[0]);
                inS = double.Parse(data[1]);
                inP = double.Parse(data[2]);
                inR = double.Parse(data[3]);
                inC = double.Parse(data[4]);
                inA = double.Parse(data[5]);
                inX = double.Parse(data[6]);

                calculatedSal = (inE * (double)weightsList[0]) + (inS * (double)weightsList[1]) + (inP * (double)weightsList[2]) + (inR * (double)weightsList[3]) + (inC * (double)weightsList[4]) + (inA * (double)weightsList[5]) + (inX * (double)weightsList[6]) + (inBias * (double)weightsList[7]);;
                Console.WriteLine("Calculated Salary: {0}", calculatedSal);
                Console.WriteLine();
            }
            Console.ReadLine();
        }



//RETURN DERIVATIVE
        private static double derivative(double calculatedSal, double empSal, double input)
        {
            return ((-2) * (empSal - calculatedSal) * input);
        }
    }
}
