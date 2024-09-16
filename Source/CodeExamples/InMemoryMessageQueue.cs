using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    // This is supposed to be action performed on a remote message queue service
    internal class InMemoryMessageQueue
    {
        private ConcurrentQueue<string> _messageQueue = new ConcurrentQueue<string>();

        public bool PushMessage(string message)
        {
            _messageQueue.Append(message);
            return true;
        }

        public string? PullMessage()
        {
            var peek = _messageQueue.TryPeek(out var result);

            return peek ? result : null;
        }

        public bool AcknowledgeLastMessage() => _messageQueue.TryDequeue(out var _);

        public bool IsMessageAvailable => _messageQueue.TryPeek(out var _);
    }
}
