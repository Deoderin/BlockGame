public interface IUpdateableServices : IService
{
    void RegisterUpdatableSystem(IFixedUpdateable updateableSystems);
    void UnRegisterUpdatableSystem(IFixedUpdateable updateableSystems);
    void CleanUp();
}