﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.CleaningServices
{
    public class EditCleaningServiceCommandDto : EditableCleaningServiceDto
    {
        public Guid Id { get; set; }
    }
}