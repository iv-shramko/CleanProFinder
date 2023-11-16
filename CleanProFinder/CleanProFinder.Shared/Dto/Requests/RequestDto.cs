﻿using CleanProFinder.Shared.Dto.CleaningServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public float Square { get; set; }
        public string Address { get; set; }
        public List<CleaningServiceDto> Services { get; set; }
    }
}