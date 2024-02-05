using XLink.Utility;
using XLink.Actions;
using XLink.Contexts;

namespace XLink.Core
{
    public class Program
    {
        public static void Main()
        {

            ContextManager contextManager = new ContextManager();

            contextManager.Init();

            ContextFilter filter = new ContextFilter 
            { 
                ArgEntry = true,
                ResultAcceptance = true 
            };

            contextManager.ApplyFilter(filter);

            while (true)
            {

                Query query = Parser.Parse(Console.ReadLine());

                if (query == null) {continue;}

                XActionResponse result = contextManager.Execute(query);

                if(result == null) { continue; }

                OutputResult(result);

            }

            static void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop - 1;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }

            static void FormatResult(XActionResponse result)
            {
                if (result.Success)
                {
                    if (result.Result.Contains("\n"))
                    {
                        result.Result = result.Result.Substring(0, result.Result.IndexOf("\n")) + "...";
                        if (result.Result.Length > 15)
                        {
                            result.Result = result.Result.Substring(0, 15) + "...";
                        }
                    }
                }
            }

            static void OutputResult(XActionResponse result)
            {
                FormatResult(result);
                ClearCurrentConsoleLine();
                Logger.Log(result);
                Console.WriteLine();
            }

        }

    }

}

