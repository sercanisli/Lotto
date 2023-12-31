﻿using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record SuperLotoDtoForInsertion : SuperLotoDtoForManipulation
    {
        public string Date { get; init; }
        public SuperLotoDtoForInsertion()
        {
        }
    }
}
