using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidwestGeeks.ServiceModel.Types;
using ServiceStack.FluentValidation;

namespace MidwestGeeks.ServiceInterface.Validators
{
    public class MeetingValidator : AbstractValidator<Meeting>
    {
        public MeetingValidator()
        {
            RuleFor(m => m.Name).NotNull();
            RuleFor(m => m.Where).NotNull();
            RuleFor(m => m.Day).NotNull();
            RuleFor(m => m.Day).GreaterThan(DateTime.Now);
        }
    }
}
