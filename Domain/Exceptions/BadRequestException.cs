﻿using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Domain.Exceptions;

public class BadRequestException : Exception, ICustomException
{
    private List<ErrorMessageResponse> Errors { get; }

    public int StatusCode { get => 400; }

    public BadRequestException(List<ErrorMessageResponse> errors) : base()
    {
        Errors = errors;
    }

    public BadRequestException(string field, string message) : base()
    {
        Errors =
            new List<ErrorMessageResponse>
            {
                new ErrorMessageResponse
                {
                    Field = field,
                    Message = message
                }
            };
    }

    public string GetResponse() => JsonSerializer.Serialize(Errors);
}
