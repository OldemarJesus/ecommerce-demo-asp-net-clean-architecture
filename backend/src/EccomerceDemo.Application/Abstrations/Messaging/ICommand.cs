namespace EccomerceDemo.Application.Abstrations.Messaging;

public interface ICommand : IBaseCommand;

public interface ICommand<TResponse>;

public interface IBaseCommand;