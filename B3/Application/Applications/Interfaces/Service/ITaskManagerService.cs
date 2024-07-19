namespace Applications.Interfaces.Service
{
    public interface ITaskManagerService
    {
        Task SendMessage<T>(T message);

    }
}
