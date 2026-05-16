using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib.Events
{
    //灵感与源代码来源于 https://github.com/ZiYuKing/QQBot.NET"
    internal class EventManager(BotService service) : IDisposable
    {
        private readonly BotService _botService = service;
        public BotService Service =>_botService;
        private readonly List<StandardEvent> _events = [];
        public List<StandardEvent> Events => _events;
        public StandardEvent Add(Type type)
        {
            if (type.BaseType != typeof(StandardEvent))
                throw new Exception("类型必须继承自StandardEvent");
            if (Activator.CreateInstance(type) is not StandardEvent @event)
                throw new NullReferenceException(nameof(@event));
            @event.Manager = this;
            lock (this)
            {
                _events.Add(@event);
            }
            return @event;
        }
        public T Get<T>() where T : StandardEvent
        {
            lock (this)
            {
                foreach (var @event in _events)
                {
                    if (@event is T t)
                    {
                        return t;
                    }
                }

                return null;
            }
        }
        public void Remove<T>() where T : StandardEvent
        {
            lock (this)
            {
                foreach (var @event in _events.ToArray())
                {
                    if (@event is T)
                    {
                        _events.Remove(@event);
                        @event.Dispose();
                        return;
                    }
                }
            }
        }
        public void RemoveAll<T>() where T : StandardEvent
        {
            lock (this)
            {
                foreach (var @event in _events.ToArray())
                {
                    if (@event is T)
                    {
                        _events.Remove(@event);
                        @event.Dispose();
                    }
                }
            }
        }
        public void Remove(StandardEvent @event)
        {
            lock (this)
            {
                _events.Remove(@event);
            }
        }
        public async Task DispatchAsync(Payload payload)
        {
            foreach (var @event in _events.ToArray())
            {
                await @event.OnHandlePayloadAsync(payload);
            }
        }
        public void Dispose()
        {
            foreach (var @event in _events)
            {
                @event.Dispose();
            }
            _events.Clear();
        }
    }
}
