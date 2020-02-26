using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task task, bool returnToCallingcontext, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingcontext);
            }
            catch(Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
