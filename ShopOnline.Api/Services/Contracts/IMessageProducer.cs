namespace ShopOnline.Api.Services.Contracts
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
