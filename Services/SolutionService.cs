using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;
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
        private readonly ICodeTaskProvider _codeTaskProvider;

        public SolutionService(IUserProvider userProvider, ICodeTaskProvider codeTaskProvider)
        {
            _userProvider = userProvider;
            _codeTaskProvider = codeTaskProvider;
        }
        
        
        public TaskSolutionSubmissionResponseModel SubmitTaskSolution(TaskSolutionSubmissionModel taskSolutionSubmission)
        {
            var taskIds = _codeTaskProvider.GetAll().Select(task => task.CodeTaskId);
            if (!taskIds.Contains(taskSolutionSubmission.CodeTaskId))
            {
                return new TaskSolutionSubmissionResponseModel
                    { ValidationErrorMessage = "Invalid Tasks selection" };
            }
            
            var computationResult = GenerateAssembly(taskSolutionSubmission.TaskSolution, "GetResult");
            var response = new TaskSolutionSubmissionResponseModel();
            
            if (computationResult.Error != null)
            {
                response.ComputationErrorMessage = computationResult.Error;
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
            

            EmitResult compilationResult = compilation.Emit(pathToAssembly);

            if(compilationResult.Success)
            {
                Assembly asm = Assembly.Load(File.ReadAllBytes(pathToAssembly));

                // Invoke the RoslynCore.Helper.CalculateCircleArea method passing an argument
                int number = 15;
                
                var computationMethod = asm.GetType("Solution")?.GetMethod(targetedMethodName);
                if (computationMethod == null)
                {
                    response.Error = $"Invalid code structure. Please have static Solution class with static {targetedMethodName} method";
                }
                else
                {
                    object result = computationMethod .Invoke(null, new object[] { number });
                    response.Result = result;
                }
            }
            else
            {
                string errorMessage = "";
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    errorMessage += $@"Line {codeIssue.Location.GetLineSpan()}: {codeIssue.GetMessage()}.

                                    ";
                }

                response.Error = errorMessage;
            }

            try
            {
                File.Delete(pathToAssembly);
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine(exception);
                response.Error = "Please delete TaskSolution.dll by hand...";
                return response;
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception);
                response.Error = "Please delete TaskSolution.dll by hand...";
                return response;
            }
            
            return response;
            }
    }
    
}