namespace Api.Responses;


public record ErrorResponse(int code, string message, string success = "false");