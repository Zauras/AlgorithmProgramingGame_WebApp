namespace AlgorithmProgramingGame_WebApp.TestAssets
{
    public static class FibonacciSolution
    {
        public const string code = @"
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
    }
}