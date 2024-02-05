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

                string command = Console.ReadLine();
                ClearCurrentConsoleLine();
                Query query = Parser.Parse(command);
                if (query == null)
                {
                    continue;
                }
                XActionResponse result = contextManager.Execute(query.ContextName, query.ActionName, query.Args);

                if(result == null)
                {
                    continue;
                }
                else
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

                Logger.Log(result);

                Console.WriteLine();
            }

            static void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop - 1;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }

        }

    }

}

