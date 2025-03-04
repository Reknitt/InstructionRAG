namespace InstructionRAG.Domain.Exceptions;

public class UserAlreadyExistsException(string email) 
    : Exception($"User with provided email: {email} already exists");