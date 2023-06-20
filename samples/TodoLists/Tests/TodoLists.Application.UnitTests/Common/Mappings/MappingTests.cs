using System.Runtime.Serialization;
using Mapster;
using NUnit.Framework;
using TodoLists.Domain.Entities;
using TodoLists.Queries.TodoLists.GetTodos;

namespace TodoLists.Application.UnitTests.Common.Mappings;

public class MappingTests
{

    [Test]
    [TestCase(typeof(TodoList), typeof(TodoListDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        instance.Adapt(source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
