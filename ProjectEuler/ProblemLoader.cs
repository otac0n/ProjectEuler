namespace ProjectEuler
{
    using System;
    using System.IO;

    public class ProblemLoader<T> where T : Problem, new()
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
                return File.ReadAllText(((ProblemResourceAttribute)resourceAttribs[0]).ResourceName + ".txt");
            }
        }

        public Problem LoadProblem()
        {
            return new T();
        }
    }

}
