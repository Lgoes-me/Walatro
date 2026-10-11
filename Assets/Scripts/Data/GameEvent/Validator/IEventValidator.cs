public interface IValidator : IComponentData
{
    bool Validate();
}

public interface IGameplayStartValidator : IValidator
{

}

public interface IMinigameStartValidator : IValidator
{

}

public interface IMinigameEndValidator : IValidator
{

}

public interface IGameplayEndValidator : IValidator
{

}

public interface ILifeLostValidator : IValidator
{

}
    
public interface ITimeOverValidator : IValidator
{

}

public interface ICreatedValidator : IValidator
{

}