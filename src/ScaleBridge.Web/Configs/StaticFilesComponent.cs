using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Protocols.Configs
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class StaticFilesComponent
    {
        private readonly AppFunc next;
        private readonly String baseDirectory;

        public StaticFilesComponent(AppFunc next)
        {
            this.next = next;
			baseDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace(Path.Combine("bin","Debug"), "Views");
        }

        //TODO: This can be improved to load the files from memory when running on production mode.
        public Task Invoke(IDictionary<string, object> environment)
        {
            var requestPath = NormalizeRequestPath(environment);
            var startOfResource = requestPath.LastIndexOf("/");
            if (requestPath.Substring(startOfResource).Contains("."))
                return next(environment);

            var staticViewFilePath = baseDirectory + requestPath + ".html";
            if (File.Exists(staticViewFilePath))
            {
                var response = environment["owin.ResponseBody"] as Stream;
                using (var writer = new StreamWriter(response))
                {
                    return writer.WriteAsync(File.ReadAllText(staticViewFilePath));
                }
            }
            return next(environment);
        }

        private static string NormalizeRequestPath(IDictionary<string, object> environment)
        {
            var requestPath = environment["owin.RequestPath"] as string;
            return requestPath.EndsWith("/") ? requestPath.Remove(requestPath.Length - 1, 1) : requestPath;
        }
    }
}
