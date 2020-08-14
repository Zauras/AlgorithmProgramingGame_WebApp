//mongod --dbpath <data_directory_path>
// mongo
use CodeSolutionsDb
db.createCollection('CodeTasks')
db.CodeTasks.insert({ 'Name':'Fibonacci Sequence', 'Description':'The Fibonacci sequence is a set of numbers that starts with a one or a zero, followed by a one, and proceeds based on the rule that each number (called a Fibonacci number) is equal to the sum of the preceding two numbers. If the Fibonacci sequence is denoted F (n), where n is the first term in the sequence, the following equation obtains for n = 0, where the first two terms are defined as 0 and 1 by convention: F (0) = 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 ...'})
db.CodeTasks.find({}).pretty()

db.createCollection('Users');
db.Users.insertMany([
{
        "Name":"Eve Random",
        "SubmissionsCount":5,
        "Scores":[
            {
                "CodeTaskId":1,
                "SolutionCode":"public static class Solution{public static int[] GetResult(int number){var result = new int[number];int n1=0, n2=1, n3, i;result[0] = n1;result[1] = n2;for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed {n3=n1+n2;n1=n2;n2=n3;result[i] = n3;}return result;}}"
            },
            {
                "CodeTaskId":1,
                "SolutionCode":"public static class Solution{public static int[] GetResult(int number){var result = new int[number];int n1=0, n2=1, n3, i;result[0] = n1;result[1] = n2;for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed {n3=n1+n2;n1=n2;n2=n3;result[i] = n3;}return result;}}"
            }
        ]
    },
{
"Name":"John Mesh",
"SubmissionsCount":3,
"Scores":[
{
    "CodeTaskId":1,
    "SolutionCode":"public static class Solution{public static int[] GetResult(int number){var result = new int[number];int n1=0, n2=1, n3, i;result[0] = n1;result[1] = n2;for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed {n3=n1+n2;n1=n2;n2=n3;result[i] = n3;}return result;}}"
},
{
    "CodeTaskId":1,
    "SolutionCode":"public static class Solution{public static int[] GetResult(int number){var result = new int[number];int n1=0, n2=1, n3, i;result[0] = n1;result[1] = n2;for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed {n3=n1+n2;n1=n2;n2=n3;result[i] = n3;}return result;}}"
},
{
    "CodeTaskId":1,
    "SolutionCode":"public static class Solution{public static int[] GetResult(int number){var result = new int[number];int n1=0, n2=1, n3, i;result[0] = n1;result[1] = n2;for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed {n3=n1+n2;n1=n2;n2=n3;result[i] = n3;}return result;}}"
}

]
},
    {
        "Name":"Arnold Handsome",
        "SubmissionsCount":8,
        "Scores":[
            {
                "CodeTaskId":1,
                "SolutionCode":"public static class Solution{public static int[] GetResult(int number){var result = new int[number];int n1=0, n2=1, n3, i;result[0] = n1;result[1] = n2;for(i=2; i<number; ++i) //loop starts from 2 because 0 and 1 are already printed {n3=n1+n2;n1=n2;n2=n3;result[i] = n3;}return result;}}"
            }
        ]
    },
]);