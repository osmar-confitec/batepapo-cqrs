namespace BatePapo.DomainCore.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}