namespace InstructionRAG.Domain.Exceptions;

public class UserNotFoundException(string email) 
    : Exception($"User with provided email: {email} not found");

    
