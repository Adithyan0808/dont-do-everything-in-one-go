namespace CertificationHub.Application.Exceptions;

public sealed class NotFoundException(string entityName, object key)
    : Exception($"{entityName} with key '{key}' was not found.");
