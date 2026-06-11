namespace CertificationHub.Application.Exceptions;

public sealed class UnauthorizedException(string message = "Unauthorized request.") : Exception(message);
