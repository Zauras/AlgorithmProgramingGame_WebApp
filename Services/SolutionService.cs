using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Services
{
    public class SolutionService: ISolutionService
    {
        private readonly IUserProvider _userProvider;
        
        const string code = @"
                 public static class Solution
                 {  
                    public static int[] GetResult(int number)
                    {
                        var result = new int[number];
                        int n1=0, n2=1, n3, i; 

                        result[0] = n1;
                        result[1] = n2;

                        for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed    
                        {    
                            n3=n1+n2;    
                            n1=n2;    
                            n2=n3;
                            result[i] = n3;    
                        }

                        return result;
                    }
                 }
                ";
        
        

        public SolutionService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        
        
        public TaskSolutionSubmissionResponseModel SubmitTaskSolution(TaskSolutionSubmissionModel taskSolutionSubmission)
        {
            var computationResult = GenerateAssembly(taskSolutionSubmission.TaskSolution, "GetResult");
            var response = new TaskSolutionSubmissionResponseModel();
            
            if (computationResult.Error != null)
            {
                // Compilation error
                response.ErrorMessage = computationResult.Error;
                return response;
            }
            
            var expectedResult = new [] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 };
            
            bool isResultTruthy = expectedResult.SequenceEqual((int[]) computationResult.Result);
            if (isResultTruthy)
            {
                // Score
                _userProvider.AddScore(taskSolutionSubmission);
                response.IsSuccess = true;
                return response;
            }

            // Count but not score
            _userProvider.RegisterFailedScore(taskSolutionSubmission.UserName);
            response.IsSuccess = false;
            return response;
        }
        
        

        private ComputationModel GenerateAssembly(string code, string targetedMethodName)
        {
            var response = new ComputationModel();
            
            var tree = SyntaxFactory.ParseSyntaxTree(code);
            string fileName="TaskSolution.dll";
            string pathToAssembly = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            // Detect the file location for the library that defines the object type
            var systemRefLocation=typeof(object).GetTypeInfo().Assembly.Location;
            // Create a reference to the library
            var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
            // A single, immutable invocation to the compiler
            // to produce a library
            var compilation = CSharpCompilation.Create(fileName)
                .WithOptions(
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(systemReference)
                .AddSyntaxTrees(tree);
            
            
            //Do something with the loaded 'assembly'
            EmitResult compilationResult = compilation.Emit(pathToAssembly);

            if(compilationResult.Success)
            {
                // Load the assembly
                //Assembly asm = Assembly.Load(System.IO.File.ReadAllBytes(path));
                Assembly asm = Assembly.Load(File.ReadAllBytes(pathToAssembly));
                              //AssemblyLoadContext.Default.LoadFromAssemblyPath(pathToAssembly);
                
                // Invoke the RoslynCore.Helper.CalculateCircleArea method passing an argument
                int number = 15;
                object result = 
                    asm.GetType("Solution")
                        .GetMethod(targetedMethodName)
                        .Invoke(null, new object[] { number });
                
                try
                {
                    File.Delete(pathToAssembly);
                }
                catch (UnauthorizedAccessException exception)
                {
                    Console.WriteLine(exception);
                }
                catch (IOException exception)
                {
                    Console.WriteLine(exception);
                }
                
                response.Result = result;
                return response;
            }


            string errorMessage = "";
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    errorMessage += $@"
                            ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},
                            Location: {codeIssue.Location.GetLineSpan()},
                            Severity: {codeIssue.Severity}
                            ";
                }

            response.Error = errorMessage;
            

            return response;
            }
    }
    
}


// const string code = @"
//                 using System;
//                  public static class Solution
//                  {  
//                     public static int[] GetResult(int number)
//                     {
//                         var result = new int[number];
//                         int n1=0, n2=1, n3, i; 
//
//                         result[0] = n1;
//                         result[1] = n2;
//
//                         for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed    
//                         {    
//                             n3=n1+n2;    
//                             n1=n2;    
//                             n2=n3;
//                             result[i] = n3;    
//                         }
//
//                         return result;
//                     }
//                  }
//                 ";


// using System;
// using System.IO;
// using System.Reflection;
// using System.Runtime.Loader;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.Emit;
// namespace RoslynCore
// {
//   public static class EmitDemo
//   {
//     public static void GenerateAssembly()
//     {
//       const string code = @"using System;
// using System.IO;
// namespace RoslynCore
// {
//  public static class Helper
//  {
//   public static double CalculateCircleArea(double radius)
//   {
//     return radius * radius * Math.PI;
//   }
//   }
// }";
//       var tree = SyntaxFactory.ParseSyntaxTree(code);
//       string fileName="mylib.dll";
//       // Detect the file location for the library that defines the object type
//       var systemRefLocation=typeof(object).GetTypeInfo().Assembly.Location;
//       // Create a reference to the library
//       var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
//       // A single, immutable invocation to the compiler
//       // to produce a library
//       var compilation = CSharpCompilation.Create(fileName)
//         .WithOptions(
//           new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
//         .AddReferences(systemReference)
//         .AddSyntaxTrees(tree);
//       string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
//       EmitResult compilationResult = compilation.Emit(path);
//       if(compilationResult.Success)
//       {
//         // Load the assembly
//         Assembly asm =
//           AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
//         // Invoke the RoslynCore.Helper.CalculateCircleArea method passing an argument
//         double radius = 10;
//         object result = 
//           asm.GetType("RoslynCore.Helper").GetMethod("CalculateCircleArea").
//           Invoke(null, new object[] { radius });
//         Console.WriteLine($"Circle area with radius = {radius} is {result}");
//       }
//       else
//       {
//         foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
//         {
//           string issue = $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},
//             Location: {codeIssue.Location.GetLineSpan()},
//             Severity: {codeIssue.Severity}";
//           Console.WriteLine(issue);
//         }
//       }
//     }
//   }
// }