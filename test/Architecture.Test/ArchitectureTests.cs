using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Test;

public class ArchitectureTests
{
    private const string DomainNameSpace = "CleanArchitecture.Domain";
    private const string ApplicationNameSpace = "CleanArchitecture.Application";
    private const string InfrastructureNameSpace = "CleanArchitecture.Infrastructure";
    private const string PresentationWebApiNameSpace = "CleanArchitecture.WebApi";

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
    {
        // Arrange

        var assembly = typeof(CleanArchitecture.Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNameSpace,
            InfrastructureNameSpace,
            PresentationWebApiNameSpace
        };

        // Act

        var testResult = Types.InAssembly(assembly)
                              .ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_Have_Dependency_On_Other_Projects()
    {
        // Arrange

        var assembly = typeof(CleanArchitecture.Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNameSpace,
            PresentationWebApiNameSpace
        };

        // Act

        var testResult = Types.InAssembly(assembly)
                              .ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PresentationWebApi_Should_Not_Have_Dependency_On_Other_Projects()
    {
        // Arrange

        var assembly = typeof(CleanArchitecture.WebApi.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            DomainNameSpace
        };

        // Act

        var testResult = Types.InAssembly(assembly)
                              .ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Other_Projects()
    {
        // Arrange

        var assembly = typeof(CleanArchitecture.Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNameSpace,
            PresentationWebApiNameSpace
        };

        // Act

        var testResult = Types.InAssembly(assembly)
                              .ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_Have_Dependency_On_Domain()
    {
        // Arrange

        var assembly = typeof(CleanArchitecture.Application.AssemblyReference).Assembly;


        // Act

        var testResult = Types.InAssembly(assembly)
                              .That()
                              .HaveNameEndingWith("Handler")
                              .Should()
                              .HaveDependencyOn(DomainNameSpace)
                              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}
