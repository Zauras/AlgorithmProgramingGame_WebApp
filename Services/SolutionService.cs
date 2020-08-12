using System.IO;
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
            _userProvider.AddScore(taskSolutionSubmission);

            //var result = GenerateAssembly(taskSolutionSubmission.TaskSolution, "GetResult");
            var response = new TaskSolutionSubmissionResponseModel();
            
            // if (result is string errorMessage)
            // {
            //     // Compilation error
            //     response.ErrorMessage = errorMessage;
            //     return response;
            // }
            //
            // var expectedResult = new [] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 };
            //
            // //_userProvider.RegisterSubmission();
            // if (expectedResult == result)
            // {
            //     // Score
            //     _userProvider.AddScore(taskSolutionSubmission);
            //     response.IsSuccess = true;
            // }
            // else
            // {
            //     // Count but not score
            //     _userProvider.IncrementSubmissionsCount();
            //     response.IsSuccess = false;
            // }

            return response;
        }
        
        

        private object GenerateAssembly(string code, string targetedMethodName)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(code);
            string fileName="TaskSolution.dll";
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
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            EmitResult compilationResult = compilation.Emit(path);

            if(compilationResult.Success)
            {
                // Load the assembly
                Assembly asm =
                    AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                // Invoke the RoslynCore.Helper.CalculateCircleArea method passing an argument
                int number = 15;
                object result = 
                    asm.GetType("Solution")
                        .GetMethod(targetedMethodName)
                        .Invoke(null, new object[] { number });

                return result;
            }
            else
            {
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    string issue = $@"
                                    ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},
                                    Location: {codeIssue.Location.GetLineSpan()},
                                    Severity: {codeIssue.Severity}
                                    ";
                    
                    return issue;
                }
            }
            return null;
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