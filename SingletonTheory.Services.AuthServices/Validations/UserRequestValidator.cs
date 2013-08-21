using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Validations
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleSet(ApplyTo.Post, () =>
                                  {
                                      RuleFor(x => x.UserName).NotEmpty();
                                      RuleFor(x => x.Password).NotEmpty();
                                      RuleFor(x => x.Role).NotEmpty();
                                  });
            RuleSet(ApplyTo.Put, () =>
                                 {
                                     RuleFor(x => x.Id).GreaterThan(0);
                                     RuleFor(x => x.Role).NotEmpty();
                                 });
        }
    }
}