﻿using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record SayisalLotoDtoForCompare
    {
        [ListLength(6, 6)]
        [RangeAttribute(1, 90)]
        public List<int> Numbers { get; init; }
    }
}
