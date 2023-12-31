﻿namespace Entities.Validations
{
    public abstract record SuperLotoDtoForManipulation
    {
        [ListLength(6, 6)]
        [RangeAttribute(1,60)]
        public List<int> Numbers { get; init; }
        public DateTime Date { get; init; }

    }
}
