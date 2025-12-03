using ChatApplication.Application.DTOs.Auth.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<AuthRequest>
    {
        public RegisterRequestValidator() {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required to create an account!")
                .Length(3, 20);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required to create an account!")
                .MinimumLength(3);
        }
    }
}
