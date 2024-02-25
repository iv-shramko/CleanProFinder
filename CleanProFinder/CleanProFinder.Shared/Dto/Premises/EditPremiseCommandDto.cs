using System;

namespace CleanProFinder.Shared.Dto.Premises
{
    public class EditPremiseCommandDto : EditablePremiseDto
    {
        public Guid Id { get; set; }
    }
}
