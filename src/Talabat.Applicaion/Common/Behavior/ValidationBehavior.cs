﻿using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Applicaion.Common.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
       : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
       where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator = validator;
        public async Task<TResponse> Handle( 
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {

            if (_validator is null)
                return await next();

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
                return await next();

            var errors = validationResult.Errors
                .ConvertAll(error => Error.Validation(
                    code: error.PropertyName,
                    description: error.ErrorMessage));

            return (dynamic)errors;
        }
    }
}