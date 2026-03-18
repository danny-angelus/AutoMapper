namespace Morphy.Configuration;

public interface IMemberConfigurationProvider
{
    void ApplyConfiguration(IMemberConfigurationExpression memberConfigurationExpression);
}