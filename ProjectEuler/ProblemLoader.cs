namespace ProjectEuler
{
    using System;
    using System.IO;

    public interface IProblemLoader
    {
        string LoadResource();
        ResultAttribute LoadResultInfo();
        Problem LoadProblem();
    }

    public class ProblemLoader<T> : IProblemLoader where T : Problem, new()
    {
        private Type problemType;

        public ProblemLoader()
        {
            this.problemType = typeof(T);
        }

        public string LoadResource()
        {
            var resourceAttribs = problemType.GetCustomAttributes(typeof(ProblemResourceAttribute), false);

            if (resourceAttribs.Length == 0)
            {
                return null;
            }
            else
            {
                var resourceName = ((ProblemResourceAttribute)resourceAttribs[0]).ResourceName;
                if (string.IsNullOrEmpty(resourceName))
                {
                    return null;
                }
                else
                {
                    return File.ReadAllText(@"Resources\" + resourceName + ".txt");
                }
            }
        }

        public ResultAttribute LoadResultInfo()
        {
            var resultAttribs = problemType.GetCustomAttributes(typeof(ResultAttribute), true);

            return (ResultAttribute)resultAttribs[0];
        }

        public Problem LoadProblem()
        {
            return new T();
        }
    }

}
