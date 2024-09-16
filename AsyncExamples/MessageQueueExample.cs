using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal class MessageQueueExample
    {
        public void MessageQueueExample1()
        {
            var mqs = new InMemoryMessageQueue();

            mqs.PushMessage("hello");
            mqs.PushMessage("world");

            while (mqs.IsMessageAvailable)
            {
                var message = mqs.PullMessage();
                Console.Write(message);
                Console.Write(Environment.NewLine);
                mqs.AcknowledgeLastMessage();
            }

            // Will display "hello\nworld"
        }

        public void MessageQueueExample2()
        {
            var mqs = new InMemoryMessageQueue();

            mqs.PushMessage("hello");
            mqs.PushMessage("world");

            while (mqs.IsMessageAvailable)
            {
                var message = mqs.PullMessage();
                Console.Write(message);
                Console.Write(Environment.NewLine);
            }

            // Infinite loop that will display "hello" multiple times before crash
        }
    }
}
